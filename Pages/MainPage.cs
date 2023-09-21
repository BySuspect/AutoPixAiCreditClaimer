using AutoPixAiCreditClaimer.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    var res = MessageBox.Show("The file \"accounts.json\" was found in the root folder of the application. Do you want to transfer the accounts registered here?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        // Check if the new location file exists and delete it if so
                        if (File.Exists(Path.Combine(References.AppFilesPath, "accountlist.json")))
                            File.Delete(Path.Combine(References.AppFilesPath, "accountlist.json"));
                        // Move the old accounts.json file to the new location
                        File.Move("./accountlist.json", Path.Combine(References.AppFilesPath, "accountlist.json"));
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
            // If the application setting is to run claim on app startup, start the claim worker
            if (SettingsHelper.Settings.runOnAppStartup)
                ClaimWorker.RunWorkerAsync();
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
            string logPath = Path.Combine(References.AppFilesPath, "Logs", $"Log{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt");
            logger = new Logger(logPath);
            logger.Log($"App version: {System.Windows.Forms.Application.ProductVersion}");
            logger.Log("Progress Started!");

            try
            {
                // Set Chrome options for headless browsing if needed
                ChromeOptions options = new ChromeOptions();
                if (!SettingsHelper.Settings.showBrowserOnClaimProgress)
                {
                    options.AddArgument("--headless=new");
                }
                options.AddArgument("--enable-automation");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--log-level=2");
                options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.0.0 Safari/537.36");
                IWebDriver driver = new ChromeDriver(options);

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
                            driver.FindElement(By.CssSelector("div[id='root'] > div > div > button")).Click();
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
                            if (driver.FindElement(By.CssSelector("header > div:nth-of-type(2)")).Text.Contains("Sign Up")
                                || driver.FindElement(By.CssSelector("header > div:nth-of-type(2)")).Text.Contains("Log in"))
                            {
                                continue;
                            }
                        }
                        catch
                        {
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
                                    driver.FindElement(By.CssSelector("header > div:nth-of-type(2)")).Click();
                                    Thread.Sleep(300);
                                    break;
                                }
                                catch
                                {
                                    try
                                    {
                                        // Check if the password is incorrect
                                        driver.FindElement(By.CssSelector("svg[data-testid='ReportProblemOutlinedIcon']"));
                                        notifyIcon.ShowBalloonTip(1000, "Error!", $"{user.name} is an Invalid Account", ToolTipIcon.Error);
                                        goto endprogress;
                                    }
                                    catch
                                    {
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
                        driver.FindElement(By.CssSelector("li[role='menuitem'][tabindex='0']")).Click();
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
                            try
                            {
                                driver.FindElement(By.CssSelector("header > div:nth-of-type(2)")).Click();
                                Thread.Sleep(300);
                                goto drowdownmenu;
                            }
                            catch
                            {
                                Thread.Sleep(49);
                                goto drowdownmenu;
                            }
                        }
                    }
                    #endregion

                    logger.Log("Profile opened.");

                #region Open credit page
                opencreditpage:
                    // Find and click on the credit page link
                    try
                    {
                        driver.FindElement(By.CssSelector("div[role='tablist'] > a:nth-of-type(5)")).Click();
                        //driver.FindElement(By.CssSelector("div[id='root'] > div.flex > div > div > div > div.flex.flex-col.gap-8 > div > div.flex.flex-col.gap-4 > div > a")).Click();
                        Thread.Sleep(300);
                    }
                    catch
                    {
                        Thread.Sleep(49);
                        goto opencreditpage;
                    }
                    #endregion

                    logger.Log("Credit page opened.");

                    if (SettingsHelper.Settings.scrollPageAutomation)
                    {

                        #region Scroll to button
                        try
                        {
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                            try
                            {
                                IWebElement element = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > div > section"));
                                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                            }
                            catch (NoSuchElementException ex)
                            {
                                logger.Log("Element not found: " + ex.Message);
                                throw;
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Log($"Error: {ex.Message}");
                            MessageBox.Show("Now have an error please disable \"PageScroll\" on settings and try again.");
                            goto endprogress;
                        }
                        #endregion

                        logger.Log("Credit page scrolled.");
                    }

                    #region Claim credit
                    bool isClaimed = false;
                claimcredit:
                    // Find and click the claim credit button
                    try
                    {
                        driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > div > section > button")).Click();
                        isClaimed = true;
                        Thread.Sleep(500);

                        try
                        {
                            // Check credit is claimed
                            driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > div > section > button > div > div > div"));
                        }
                        catch
                        {
                            goto claimcredit;
                        }
                        goto endprogress;
                    }
                    catch
                    {
                        try
                        {
                            if (!isClaimed)
                            {
                                // Check if the credit is already claimed
                                var text = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > div > section > button > div > div > div")).Text;
                                notifyIcon.ShowBalloonTip(100, "Info!", text, ToolTipIcon.Info);
                                logger.Log($"{user.name} - {text}");
                                if (text != null) goto endprogress;
                            }
                            else goto endprogress;
                        }
                        catch
                        {
                            goto claimcredit;
                        }
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
            }
            catch (Exception ex)
            {
                logger.Log($"Error: {ex.Message}");
            }
            return Task.CompletedTask;
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
                var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
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
                var res = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    // Get the selected user item from the UserList using the ListView selection
                    var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
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
                var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
                // Start the claim progress for the selected user
                runClaimProgress(selected).Wait();
                notifyIcon.ShowBalloonTip(100, "Info!", "Single Account's credit claimed!", ToolTipIcon.Info);
            }
            else
            {
                // Display a warning balloon tip if no item is selected, or more than one item is selected, or the claim worker is busy
                if (lvaccounts.SelectedItems.Count == 0)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Please select an item on the list.", ToolTipIcon.Warning);
                else if (lvaccounts.SelectedItems.Count > 1)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Please select only 1 item on the list.", ToolTipIcon.Warning);
                else if (ClaimWorker.IsBusy)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Wait claim progress is running!", ToolTipIcon.Warning);
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
            ClaimWorker.RunWorkerAsync();
        }

        // Event handler for the completion of the claim worker
        private void ClaimWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Show a balloon tip indicating the completion of the credit claim progress
            notifyIcon.ShowBalloonTip(100, "Info!", "Credit claim progress completed!", ToolTipIcon.None);

            if (SettingsHelper.Settings.AutoExitApp)
            {
                //Exit application if AutoExit is enabled
                System.Windows.Forms.Application.Exit();
            }
        }

    }

}
