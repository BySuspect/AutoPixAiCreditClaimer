using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoPixAiCreditClaimer.Helpers;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoPixAiCreditClaimer.Pages
{
    public partial class MainPage : Form
    {
        // List to store user items
        List<UserItems> UserList = new List<UserItems>();
        Logger logger;

        public MainPage()
        {
            InitializeComponent();

            #region Changing old accounts.json file location
            try
            {
                // Check if the old accounts.json file exists in the root folder
                if (File.Exists("./accountlist.json"))
                {
                    // Ask the user if they want to transfer the accounts to a new location
                    var res = MessageBox.Show(
                        "The file \"accounts.json\" was found in the root folder of the application. Do you want to transfer the accounts registered here?",
                        "Warning!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    if (res == DialogResult.Yes)
                    {
                        // Check if the new location file exists and delete it if so
                        if (File.Exists(Path.Combine(References.AppFilesPath, "accountlist.json")))
                            File.Delete(Path.Combine(References.AppFilesPath, "accountlist.json"));
                        // Move the old accounts.json file to the new location
                        File.Move(
                            "./accountlist.json",
                            Path.Combine(References.AppFilesPath, "accountlist.json")
                        );
                    }
                }
            }
            catch { }
            #endregion

            // Refresh the user list in the UI
            refreshList();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
#if !DEBUG
            // Checking for new version
            Task.Run(CheckVersion);
#endif
            // If the application setting is to run claim on app startup, start the claim worker
            if (SettingsHelper.Settings.runOnAppStartup)
                ClaimWorker.RunWorkerAsync();
        }

        static async Task CheckVersion()
        {
            string[] serverVersion = (await GetLatestReleaseVersion())
                .ToString()
                .Replace("V", "")
                .Split('.');
            string[] currentVersion = Application.ProductVersion.Split('.');

            NewVersionAlertPage page = new NewVersionAlertPage();
            if (int.Parse(serverVersion[0]) > int.Parse(currentVersion[0]))
            {
                page.ShowDialog();
            }
            else if (int.Parse(serverVersion[0]) == int.Parse(currentVersion[0]))
            {
                if (int.Parse(serverVersion[1]) > int.Parse(currentVersion[1]))
                {
                    page.ShowDialog();
                }
                else if (int.Parse(serverVersion[1]) == int.Parse(currentVersion[1]))
                {
                    if (int.Parse(serverVersion[2]) > int.Parse(currentVersion[2]))
                    {
                        page.ShowDialog();
                    }
                }
            }
        }

        static async Task<string> GetLatestReleaseVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.0.0 Safari/537.36"
                );

                string apiUrl =
                    $"https://api.github.com/repos/BySuspect/AutoPixAiCreditClaimer/releases/latest";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(responseBody);
                    JToken tagToken = jsonObj["tag_name"];
                    if (tagToken != null)
                    {
                        return tagToken.ToString();
                    }
                }

                return null;
            }
        }

        private void ClaimWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Claim credit for each user in the UserList
            foreach (var user in UserList)
            {
                // Call the runClaimProgress method for each user and wait for completion
                runClaimProgress(user).Wait();
                Thread.Sleep(49);
            }
        }

        // Method to claim credit progress for a specific user
        private Task runClaimProgress(UserItems user)
        {
            // Define the log path with the current date and time
            string logPath = Path.Combine(
                References.AppFilesPath,
                "Logs",
                $"Log{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt"
            );
            logger = new Logger(logPath);
            logger.Log($"App version: {System.Windows.Forms.Application.ProductVersion}");
            logger.Log("Progress Started!");

            bool isPopupClosed = false;

            try
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true; //disabling cmd window

                ChromeOptions options = new ChromeOptions();
                if (!SettingsHelper.Settings.showBrowserOnClaimProgress)
                {
                    options.AddArgument("--headless=new");
                }
                options.AddArgument("--enable-automation");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--log-level=OFF");
                options.AddArgument(
                    "--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.0.0 Safari/537.36"
                );
                IWebDriver driver = new ChromeDriver(service, options);

                // Delay for loading the page
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                /* Login */

                driver.Navigate().GoToUrl("https://pixai.art/login");

                #region Login
                // Attempt to log in
                while (true)
                {
                    try
                    {
                        try
                        {
                            // Click the button to dismiss the initial pop-up
                            driver
                                .FindElement(By.CssSelector("form > div > div > button:nth-of-type(4)"))
                                .Click();
                        }
                        catch { }

                        // Enter the user's email and password
                        var emailInput = driver.FindElement(By.Id("email-input"));
                        emailInput.Clear();
                        emailInput.SendKeys(user.email);

                        var passwordInput = driver.FindElement(By.Id("password-input"));
                        passwordInput.Clear();
                        passwordInput.SendKeys(user.pass);

                        // Submit the login form
                        driver.FindElement(By.CssSelector("button[type='submit']")).Submit();
                        break;
                    }
                    catch { }

                    Thread.Sleep(49);
                }
                #endregion

                logger.Log($"{user.name} - Successfully logged in.");

                Thread.Sleep(300);

                /* After Login */

                try
                {
                    #region Select profile button
                    // Find and click on the profile button
                    while (true)
                    {
                        try
                        {
                            // Checking if the account is logged in
                            if (
                                driver
                                    .FindElement(By.CssSelector("header > div:nth-of-type(2)"))
                                    .Text.Contains("Sign Up")
                                || driver
                                    .FindElement(By.CssSelector("header > div:nth-of-type(2)"))
                                    .Text.Contains("Log in")
                            )
                            {
                                continue;
                            }
                            else
                                throw new Exception();
                        }
                        catch
                        {
                            Thread.Sleep(300);

                            isPopupClosed = checkPopup(driver);

                            try
                            {
                                // Check if the profile has an image and click on it
                                driver.FindElement(By.CssSelector("header > img")).Click();
                                Thread.Sleep(300);
                                break;
                            }
                            catch
                            {
                                try
                                {
                                    // If the profile doesn't have an image, click on a different element
                                    driver.FindElement(By.CssSelector("header > div")).Click();
                                    Thread.Sleep(300);
                                    break;
                                }
                                catch
                                {
                                    try
                                    {
                                        // Check if the password is incorrect
                                        driver.FindElement(
                                            By.CssSelector(
                                                "svg[data-testid='ReportProblemOutlinedIcon']"
                                            )
                                        );
                                        notifyIcon.ShowBalloonTip(
                                            1000,
                                            "Error!",
                                            $"{user.name} is an Invalid Account",
                                            ToolTipIcon.Error
                                        );
                                        goto endprogress;
                                    }
                                    catch
                                    {
                                        if (!isPopupClosed)
                                        {
                                            isPopupClosed = checkPopup(driver);
                                        }
                                    }
                                }
                            }
                        }

                        Thread.Sleep(49);
                    }
                    #endregion

                    logger.Log("Profile button clicked.");

                    #region Open profile
                    drowdownmenu:
                    // Find and click on the profile button in the dropdown menu
                    try
                    {
                        driver
                            .FindElement(
                                By.CssSelector(
                                    "ul[role='menu'] > li[role='menuitem']:nth-of-type(1)"
                                )
                            )
                            .Click();
                        Thread.Sleep(300);
                    }
                    catch
                    {
                        // Checking to if automation is not clicked to profile button
                        try
                        {
                            driver.FindElement(By.CssSelector("header > img")).Click();
                            Thread.Sleep(300);
                            goto drowdownmenu;
                        }
                        catch
                        {
                            if (!isPopupClosed)
                            {
                                isPopupClosed = checkPopup(driver);
                            }
                            try
                            {
                                driver.FindElement(By.CssSelector("header > div")).Click();
                                Thread.Sleep(300);
                                goto drowdownmenu;
                            }
                            catch
                            {
                                if (!isPopupClosed)
                                {
                                    isPopupClosed = checkPopup(driver);
                                }
                                Thread.Sleep(300);
                                goto drowdownmenu;
                            }
                        }
                    }
                    #endregion

                    logger.Log("Profile opened.");

                    #region Claim credit
                    bool isClaimed = false;

                    goto checkIsClaimed;

                    claimcredit:
                    try
                    {
                        string claimBtnText = driver
                            .FindElement(
                                By.CssSelector(
                                    "section > div > div:nth-of-type(2) > div:nth-of-type(2) > button > span"
                                )
                            )
                            .GetAttribute("innerHTML");
                        if (claimBtnText.ToLower() != "claimed")
                        {
                            miniClaimLoop:
                            // Click on the claim button
                            driver
                                .FindElement(
                                    By.CssSelector(
                                        "section > div > div:nth-of-type(2) > div:nth-of-type(2) > button"
                                    )
                                )
                                .Click();
                            Thread.Sleep(300);
                            driver.Navigate().Refresh();
                            Thread.Sleep(300);
                            claimBtnText = driver
                                .FindElement(
                                    By.CssSelector(
                                        "section > div > div:nth-of-type(2) > div:nth-of-type(2) > button > span"
                                    )
                                )
                                .GetAttribute("innerHTML");

                            // Check if the claim button text has changed to "Claimed"
                            if (claimBtnText.ToLower() == "claimed")
                            {
                                isClaimed = true;
                                goto checkIsClaimed;
                            }
                            else
                                goto miniClaimLoop;
                        }
                        else if (claimBtnText.ToLower() == "claimed")
                        {
                            goto checkIsClaimed;
                        }
                        else
                            goto claimcredit;
                    }
                    catch
                    {
                        if (!isPopupClosed)
                        {
                            isPopupClosed = checkPopup(driver);
                        }
                        try
                        {
                            Thread.Sleep(500);
                            if (!isClaimed)
                            {
                                // Check is page style changed
                                driver
                                    .FindElement(
                                        By.CssSelector(
                                            "section > div > div:nth-of-type(2) > div:nth-of-type(2) > button"
                                        )
                                    )
                                    .Click();
                                goto claimcredit;
                            }
                            else
                                goto endprogress;
                        }
                        catch
                        {
                            notifyIcon.ShowBalloonTip(
                                1000,
                                "Error!",
                                "Something is broken right now. Please open an issue on GitHub!",
                                ToolTipIcon.Warning
                            );
                        }
                    }
                    #endregion

                    checkIsClaimed:

                    logger.Log("Claim checking from history");

                    #region Open credit history tab
                    try
                    {
                        clickCredits:
                        Thread.Sleep(500);
                        // Click on the credits tab
                        driver
                            .FindElement(
                                By.XPath(
                                    "//*[@id=\"root\"]/div[2]/div[2]/div/div/div/div[2]/div[1]/div[2]/div/a[4]"
                                )
                            )
                            .Click();

                        // Check if the credits tab is selected
                        bool isSelected = bool.Parse(
                            driver
                                .FindElement(
                                    By.XPath(
                                        "//*[@id=\"root\"]/div[3]/div/div/div/div[2]/div[1]/div[2]/div/a[4]"
                                    )
                                )
                                .GetAttribute("aria-selected")
                        );
                        if (!isSelected)
                            goto clickCredits;
                        Thread.Sleep(1000);

                        // Getting the credits history list
                        var creditsList = driver.FindElements(By.CssSelector("table > tbody > tr"));
                        foreach (var item in creditsList)
                        {
                            IWebElement th = item.FindElement(By.CssSelector("th"));
                            IList<IWebElement> tds = item.FindElements(By.CssSelector("td"));

                            string change = th.FindElement(By.CssSelector("pre")).Text;
                            string type = tds[0].Text; // Type: "Daily Claim"
                            DateTime.TryParse(tds[1].Text.Split('(')[0], out DateTime changeDate);

                            // Check if the credit has already been claimed for today
                            if (
                                changeDate.Date == DateTime.Now.Date
                                && type == "Daily Claim"
                                && !isClaimed
                            )
                            {
                                notifyIcon.ShowBalloonTip(
                                    100,
                                    "Info!",
                                    $"The credit has already been claimed for {user.name}",
                                    ToolTipIcon.Info
                                );
                                logger.Log($"{user.name} - Already Claimed!");
                                isClaimed = true;
                                goto endprogress;
                            }
                        }
                        Thread.Sleep(500);
                        if (!isClaimed)
                            goto claimcredit;
                    }
                    catch { }
                    #endregion
                }
                catch (Exception ex)
                {
                    logger.Log("Error: " + ex.Message);
                }

                endprogress:
                driver.Quit();
                logger.Log($"{user.name} - Progress done.");
            }
            catch (Exception ex)
            {
                logger.Log($"Error: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        private bool checkPopup(IWebDriver driver)
        {
            Console.WriteLine("Checking for popup");
            try
            {
                //check for popup
                driver
                    .FindElement(
                        By.XPath("//*[@id=\"app\"]/body/div[4]/div[3]/div/div[2]/div/button")
                    )
                    .Click();
                return true;
            }
            catch
            {
                try
                {
                    //check for popup for browser is fullscreen
                    driver
                        .FindElement(By.XPath("//*[@id=\"app\"]/body/div[2]/div[3]/div/div/button"))
                        .Click();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #region Controls

        #region List Control

        // Method to refresh the ListView control with user items
        void refreshList()
        {
            lvaccounts.Items.Clear();
            // Get the updated UserList from the ListHelper
            UserList = ListHelper.UserList;
            // Populate the ListView with user data
            foreach (var item in UserList)
            {
                ListViewItem lw = new ListViewItem();
                lw.Text = item.id.ToString();
                lw.SubItems.Add(item.name);
                lw.SubItems.Add(item.email);
                lw.SubItems.Add("****************"); // Display asterisks for password
                lvaccounts.Items.Add(lw);
            }
        }

        // Event handler for the "Add New" button click
        private void btnaddnew_Click(object sender, EventArgs e)
        {
            // Open the "AddOrEditPage" dialog to add a new user item
            AddOrEditPage page = new AddOrEditPage();
            page.ShowDialog();
            // Refresh the ListView after adding a new item
            refreshList();
        }

        // Event handler for the "Edit" context menu item click
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a single item is selected in the ListView
            if (lvaccounts.SelectedItems.Count == 1)
            {
                // Get the selected user item from the UserList using the ListView selection
                var selected = UserList
                    .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                    .FirstOrDefault();
                // Open the "AddOrEditPage" dialog to edit the selected user item
                AddOrEditPage editPage = new AddOrEditPage(selected);
                editPage.ShowDialog();
                // Refresh the ListView after editing the item
                refreshList();
            }
        }

        // Event handler for the "Delete" context menu item click
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a single item is selected in the ListView
            if (lvaccounts.SelectedItems.Count == 1)
            {
                // Display a warning message box to confirm deletion
                var res = MessageBox.Show(
                    "Are you sure you want to delete?",
                    "Warning!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
                if (res == DialogResult.OK)
                {
                    // Get the selected user item from the UserList using the ListView selection
                    var selected = UserList
                        .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                        .FirstOrDefault();
                    // Remove the selected item from the UserList
                    UserList.Remove(selected);
                    // Update the ListHelper with the modified UserList
                    ListHelper.UserList = UserList;
                    // Refresh the ListView after deleting the item
                    refreshList();
                }
            }
        }

        // Event handler for the "Run Single" context menu item click
        private void runSingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a single item is selected in the ListView and the claim worker is not busy
            if (lvaccounts.SelectedItems.Count == 1 && !ClaimWorker.IsBusy)
            {
                // Get the selected user item from the UserList using the ListView selection
                var selected = UserList
                    .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                    .FirstOrDefault();

                if (selected == null)
                    return;

                btnStartClaim.Enabled = false;
                btnStartClaim.Text = "Working in progress...";

                // Start the claim progress for the selected user
                runClaimProgress(selected).Wait();
                notifyIcon.ShowBalloonTip(
                    100,
                    "Info!",
                    "Single Account's claim progress completed!",
                    ToolTipIcon.Info
                );

                btnStartClaim.Enabled = true;
                btnStartClaim.Text = "Start Claim";
            }
            else
            {
                // Display a warning balloon tip if no item is selected, or more than one item is selected, or the claim worker is busy
                if (lvaccounts.SelectedItems.Count == 0)
                    notifyIcon.ShowBalloonTip(
                        100,
                        "Info!",
                        "Please select an item on the list.",
                        ToolTipIcon.Warning
                    );
                else if (lvaccounts.SelectedItems.Count > 1)
                    notifyIcon.ShowBalloonTip(
                        100,
                        "Info!",
                        "Please select only 1 item on the list.",
                        ToolTipIcon.Warning
                    );
                else if (ClaimWorker.IsBusy)
                    notifyIcon.ShowBalloonTip(
                        100,
                        "Info!",
                        "Wait claim progress is running!",
                        ToolTipIcon.Warning
                    );
            }
        }

        // Event handler for the "Refresh" context menu item click
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Refresh the ListView to display the latest user items
            refreshList();
        }

        #endregion

        #region Form Control

        // Event handler for the "Minimize Form" button click
        private void btnminiform_Click(object sender, EventArgs e)
        {
            // Minimize the form when the button is clicked
            this.WindowState = FormWindowState.Minimized;
        }

        // Event handler for the "Hide Form" button click
        private void btnhideform_Click(object sender, EventArgs e)
        {
            // Exit the application when the button is clicked
            System.Windows.Forms.Application.Exit();
        }

        #region Mouse move codes

        // Track the mouse location when the form is clicked
        private Point _mouseLoc;

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        // Move the form with the mouse movement
        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            // Move the form with the mouse if the left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        #endregion

        #endregion

        #region Notify Icon

        // Event handler for the "Exit" context menu item click
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the application when the "Exit" context menu item is clicked
            System.Windows.Forms.Application.Exit();
        }

        // Event handler for the "Notify Icon" double-click
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Show the main form when the notify icon is double-clicked
            this.Show();
        }

        #endregion

        #region Settings

        // Event handler for the "Settings" button click
        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Open the "SettingsPage" dialog to manage application settings
            SettingsPage page = new SettingsPage();
            page.ShowDialog();
        }

        #endregion

        #endregion

        // Event handler for the "Start Claim" button click
        private void btnStartClaim_Click(object sender, EventArgs e)
        {
            // Start the claim worker asynchronously
            btnStartClaim.Enabled = false;
            btnStartClaim.Text = "Working in progress...";
            ClaimWorker.RunWorkerAsync();
        }

        // Event handler for the completion of the claim worker
        private void ClaimWorker_RunWorkerCompleted(
            object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e
        )
        {
            // Show a balloon tip indicating the completion of the credit claim progress
            notifyIcon.ShowBalloonTip(
                100,
                "Info!",
                "Credit claim progress completed!",
                ToolTipIcon.None
            );

            btnStartClaim.Enabled = true;
            btnStartClaim.Text = "Start Claim";

            if (SettingsHelper.Settings.AutoExitApp)
            {
                //Exit application if AutoExit is enabled
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
