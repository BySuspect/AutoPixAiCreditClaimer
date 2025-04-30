using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoPixAiCreditClaimer.Helpers;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoPixAiCreditClaimer.Views
{
    public partial class MainView : Form
    {
        // List to store user items
        List<UserModel> userList = new List<UserModel>();
        LoggingHelper logger;

        public MainView()
        {
            InitializeComponent();
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

        #region App Update Check

        static async Task CheckVersion()
        {
            string[] serverVersion = (await GetLatestReleaseVersion())
                .ToString()
                .ToLower()
                .Replace("v", "")
                .Split('.');
            string[] currentVersion = Application.ProductVersion.Split('.');

            for (int i = 0; i < serverVersion.Length; i++)
            {
                if (int.Parse(serverVersion[i]) > int.Parse(currentVersion[i]))
                {
                    MessageBox.Show("New version available! Please Update.");
                    Process.Start(
                        new ProcessStartInfo
                        {
                            FileName =
                                "https://github.com/BySuspect/AutoPixAiCreditClaimer/releases/latest",
                            UseShellExecute = true,
                        }
                    );
                    return;
                }
                else if (int.Parse(serverVersion[i]) < int.Parse(currentVersion[i]))
                {
                    return;
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
                    JToken tag = jsonObj["tag_name"];
                    if (tag != null && !tag.ToString().ToLower().Contains("pre"))
                    {
                        return tag.ToString();
                    }
                }

                return null;
            }
        }
        #endregion

        private async Task<Task> runClaimProgress(UserModel user)
        {
            string logPath = Path.Combine(
                References.AppFilesPath,
                "Logs",
                $"Log{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log"
            );
            logger = new LoggingHelper(logPath);
            logger.Log($"App version: {System.Windows.Forms.Application.ProductVersion}");
            logger.Log("Progress Started!");

            if (lblInfo.InvokeRequired)
                lblInfo.Invoke(new Action(() => lblInfo.Text = user.name));
            else
                lblInfo.Text = user.name;

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
                options.AddArguments("--window-size=1,1");
                options.AddArgument("--force-device-scale-factor=0.70");
                options.AddArgument("--enable-automation");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--log-level=OFF");
                options.AddArgument(
                    "--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.0.0 Safari/537.36"
                );
                using (IWebDriver driver = new ChromeDriver(service, options))
                {
                    // Some driver improvements
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    driver.Manage().Window.Size = new System.Drawing.Size(1500, 3000);

                    /* Login */

                    driver.Navigate().GoToUrl("https://pixai.art/login");

                    #region Login
                    int loginPageNavigateCounter = 0;
                    const int maxLoginAttempts = 10;

                    while (loginPageNavigateCounter <= maxLoginAttempts)
                    {
                        try
                        {
                            // Click the button to dismiss the initial pop-up
                            driver
                                .FindElement(
                                    By.CssSelector("form > div > div > button:nth-of-type(4)")
                                )
                                .Click();
                            break;
                        }
                        catch
                        {
                            if (loginPageNavigateCounter == maxLoginAttempts)
                            {
                                logger.Log("*****Can't navigate to login page*****");
                                MessageBox.Show(
                                    "There is an error now please try again later. If the error continues, please open an issue on GitHub for a fix.\nError code: 3"
                                );
                                goto endprogress;
                            }

                            Thread.Sleep(300);
                            loginPageNavigateCounter++;
                        }
                    }

                    // Enter the user's email and password
                    var emailInput = driver.FindElement(By.Id("email-input"));
                    emailInput.Clear();
                    emailInput.SendKeys(user.email);

                    var passwordInput = driver.FindElement(By.Id("password-input"));
                    passwordInput.Clear();
                    passwordInput.SendKeys(user.pass);

                    // Submit the login form
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    //TODO: there is some times has login issue with clicking button

                    while (true)
                    {
                        Thread.Sleep(100);
                        try
                        {
                            var errorText = driver
                                .FindElement(
                                    By.CssSelector(
                                        "section > div > div:nth-of-type(2) > div:nth-of-type(2) > button > span"
                                    )
                                )
                                .GetAttribute("innerHTML");

                            if (errorText == "Network Error")
                            {
                                logger.Log("*****Network Error*****");
                                MessageBox.Show(
                                    "There is a network error!\nCheck your connection."
                                );
                                goto endprogress;
                            }
                            else if (!string.IsNullOrEmpty(errorText))
                            {
                                logger.Log("*****Login Error*****");
                                MessageBox.Show("There is an error!\n" + errorText);
                                goto endprogress;
                            }
                        }
                        catch { }
                        var url = driver.Url.TrimEnd('/');
                        if (url is "https://pixai.art")
                            break;
                    }
                    #endregion

                    logger.Log(
                        $"{user.name} - Successfully logged in. - isPopupClosed:{isPopupClosed}"
                    );

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
                                    driver
                                        .FindElement(
                                            By.XPath(
                                                "//*[@id=\"root\"]/div/div[1]/header/div/span/img"
                                            )
                                        )
                                        .Click();
                                    Thread.Sleep(300);
                                    break;
                                }
                                catch
                                {
                                    try
                                    {
                                        // If the profile doesn't have an image, click on a different element
                                        driver
                                            .FindElement(
                                                By.XPath(
                                                    "//*[@id=\"root\"]/div/div[1]/header/div/span/div"
                                                )
                                            )
                                            .Click();
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
                                            Thread.Sleep(10000);
                                            if (!isPopupClosed)
                                            {
                                                isPopupClosed = checkPopup(driver);
                                                if (!isPopupClosed)
                                                {
                                                    logger.Log("*****Cant skip popup*****");
                                                    MessageBox.Show(
                                                        "There is an error now please try again later. If the error continues, please open an issue on GitHub for a fix.\nError code: 0"
                                                    );
                                                    goto endprogress;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            Thread.Sleep(49);
                        }
                        #endregion

                        logger.Log($"Profile button clicked. - isPopupClosed:{isPopupClosed}");

                        #region Open profile
                        drowdownmenu:
                        // Find and click on the profile button in the dropdown menu
                        try
                        {
                            driver
                                .FindElement(By.XPath("//*[@id=\"app\"]/body/div[4]/div[3]/ul/a"))
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

                        logger.Log($"Profile opened. - isPopupClosed:{isPopupClosed}");

                        #region Claim credit
                        bool isClaimed = false;

                        goto checkIsClaimed;

                        claimcredit:
                        try
                        {
                            string claimBtnText = driver
                                .FindElement(
                                    By.XPath(
                                        "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button/span"
                                    )
                                )
                                .GetAttribute("innerHTML");
                            if (claimBtnText.ToLower() != "claimed")
                            {
                                miniClaimLoop:
                                // Click on the claim button
                                driver
                                    .FindElement(
                                        By.XPath(
                                            "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button"
                                        )
                                    )
                                    .Click();

                                int claimAtemptCounter = 0;

                                while (true)
                                {
                                    Thread.Sleep(TimeSpan.FromSeconds(1));
                                    var claimBtn = driver
                                        .FindElement(
                                            By.XPath(
                                                "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button"
                                            )
                                        )
                                        .GetAttribute("innerHTML");

                                    if (claimBtn.Contains("Claimed"))
                                        break;

                                    if (claimAtemptCounter >= 30)
                                    {
                                        logger.Log("*****Claim button error*****");
                                        MessageBox.Show(
                                            "There is an error now please try again later. If the error continues, please open an issue on GitHub for a fix.\nError code: 55"
                                        );
                                        goto endprogress;
                                    }
                                    claimAtemptCounter++;
                                }
                                driver.Navigate().Refresh();
                                Thread.Sleep(300);
                                claimBtnText = driver
                                    .FindElement(
                                        By.XPath(
                                            "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button/span"
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
                                            By.XPath(
                                                "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button"
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
                                MessageBox.Show(
                                    "There is an error now please try again later. If the error continues, please open an issue on GitHub for a fix.\nError code: 1"
                                );
                            }
                        }
                        #endregion

                        checkIsClaimed:

                        logger.Log("Claim checking from history");
                        int testCounter = 0;

                        #region Open credit history tab
                        try
                        {
                            clickCredits:

                            Thread.Sleep(1000);
                            testCounter = 0;

                            // Click on the credits tabBy.XPath(
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "arguments[0].click();",
                                driver.FindElement(
                                    By.XPath(
                                        "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[2]/div[1]/div[2]/div/a[5]"
                                    )
                                )
                            );

                            Thread.Sleep(1000);
                            testCounter = 1;

                            // Check if the credits tab is selected
                            bool isSelected = bool.Parse(
                                driver
                                    .FindElement(
                                        By.XPath(
                                            "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[2]/div[1]/div[2]/div/a[5]"
                                        )
                                    )
                                    .GetAttribute("aria-selected")
                            );

                            testCounter = 2;

                            if (!isSelected)
                                goto clickCredits;

                            Thread.Sleep(1500);

                            // Getting the credits history list
                            var creditsList = driver.FindElements(
                                By.CssSelector("table > tbody > tr")
                            );
                            foreach (var item in creditsList)
                            {
                                Thread.Sleep(100);
                                IWebElement th = item.FindElement(By.CssSelector("th"));
                                IList<IWebElement> tds = item.FindElements(By.CssSelector("td"));

                                string change = th.FindElement(By.CssSelector("pre")).Text;
                                string type = tds[0].Text; // Type: "Daily Claim"
                                DateTime.TryParse(
                                    tds[1].Text.Split('(')[0],
                                    out DateTime changeDate
                                );

                                string claimBtnText = "";
                                try
                                {
                                    claimBtnText = driver
                                        .FindElement(
                                            By.XPath(
                                                "//*[@id=\"root\"]/div/div[2]/div/div/div/div/div[1]/section/div/div[3]/div[2]/button/span"
                                            )
                                        )
                                        .GetAttribute("innerHTML")
                                        .ToLower();
                                }
                                catch
                                {
                                    new Exception("cant get claim button text.");
                                }

                                // Check if the credit has already been claimed for today
                                if (
                                    changeDate.Date == DateTime.Now.Date
                                    && type == "Daily Claim"
                                    && !isClaimed
                                    && claimBtnText == "claimed"
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
                        catch (Exception ex)
                        {
                            logger.Log($"TestNum: {testCounter} - Error: " + ex.Message);
                            MessageBox.Show(
                                $"There is an error now please try again later. If the error continues, please open an issue on GitHub for a fix.\nError code: 2 Error num: {testCounter}"
                            );
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        logger.Log("Error: " + ex.Message);
                    }

                    endprogress:
                    driver.Quit();
                    logger.Log($"{user.name} - Progress done.");
                    if (lblInfo.InvokeRequired)
                        lblInfo.Invoke(new Action(() => lblInfo.Text = "Finished!"));
                    else
                        lblInfo.Text = "Finished!";
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error: {ex.Message}");
                MessageBox.Show("Error! \n" + ex.Message);
                if (lblInfo.InvokeRequired)
                    lblInfo.Invoke(new Action(() => lblInfo.Text = "Error!"));
                else
                    lblInfo.Text = "Error!";
                throw;
            }
            return Task.CompletedTask;
        }

        private bool checkPopup(IWebDriver driver)
        {
            Console.WriteLine("Checking for popup");
            try
            {
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
                    driver
                        .FindElement(By.XPath("//*[@id=\"app\"]/body/div[2]/div[3]/div/div/button"))
                        .Click();
                    return true;
                }
                catch
                {
                    try
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            "arguments[0].click();",
                            driver.FindElement(By.CssSelector("button[aria-label='Dismiss']"))
                        );
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        #region Controls

        #region List Control

        void refreshList()
        {
            lvaccounts.Items.Clear();
            userList = ListHelper.UserList;
            foreach (var item in userList)
            {
                ListViewItem lw = new ListViewItem();
                lw.Text = item.id.ToString();
                lw.SubItems.Add(item.name);
                lw.SubItems.Add(item.email);
                lw.SubItems.Add("****************");
                lvaccounts.Items.Add(lw);
            }
        }

        private void btnaddnew_Click(object sender, EventArgs e)
        {
            AddOrEditPage page = new AddOrEditPage();
            page.ShowDialog();
            refreshList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvaccounts.SelectedItems.Count == 1)
            {
                var selected = userList
                    .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                    .FirstOrDefault();
                AddOrEditPage editPage = new AddOrEditPage(selected);
                editPage.ShowDialog();
                refreshList();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvaccounts.SelectedItems.Count == 1)
            {
                var res = MessageBox.Show(
                    "Are you sure you want to delete?",
                    "Warning!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
                if (res == DialogResult.OK)
                {
                    var selected = userList
                        .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                        .FirstOrDefault();
                    userList.Remove(selected);
                    ListHelper.UserList = userList;
                    refreshList();
                }
            }
        }

        private void runSingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvaccounts.SelectedItems.Count == 1 && !ClaimWorker.IsBusy)
            {
                var selected = userList
                    .Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text))
                    .FirstOrDefault();

                if (selected == null)
                    return;

                btnStartClaim.Enabled = false;
                btnStartClaim.Text = "Working in progress...";

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

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        #endregion

        #region Form Control

        private void btnminiform_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnhideform_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        #region Mouse move codes
        private Point mouseLoc;

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouseLoc.X;
                int dy = e.Location.Y - mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        #endregion

        #endregion

        #region Notify Icon

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region Settings

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsPage page = new SettingsPage();
            page.ShowDialog();
        }

        #endregion

        #endregion

        private async void ClaimWorker_DoWork(
            object sender,
            System.ComponentModel.DoWorkEventArgs e
        )
        {
            foreach (var user in userList)
            {
                await runClaimProgress(user);
                Thread.Sleep(49);
            }
        }

        private void btnStartClaim_Click(object sender, EventArgs e)
        {
            btnStartClaim.Enabled = false;
            btnStartClaim.Text = "Working in progress...";
            ClaimWorker.RunWorkerAsync();
        }

        private void ClaimWorker_RunWorkerCompleted(
            object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e
        )
        {
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
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
