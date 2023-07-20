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
        List<UserItems> UserList = new List<UserItems>();
        Logger logger;
        public MainPage()
        {
            InitializeComponent();

            #region Changing old accounts.json file location
            try
            {
                if (File.Exists("./accountlist.json"))
                {
                    var res = MessageBox.Show("The file \"accounts.json\" was found in the root folder of the application. Do you want to transfer the accounts registered here?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        if (File.Exists(Path.Combine(References.AppFilesPath, "accountlist.json")))
                            File.Delete(Path.Combine(References.AppFilesPath, "accountlist.json"));
                        File.Move("./accountlist.json", Path.Combine(References.AppFilesPath, "accountlist.json"));
                    }
                }
            }
            catch { }
            #endregion

            refreshList();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            if (SettingsHelper.Settings.runOnAppStartup)
                ClaimWorker.RunWorkerAsync();
        }

        private void ClaimWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (var user in UserList)
            {
                runClaimProgress(user).Wait();
                Thread.Sleep(49);
            }
        }

        private Task runClaimProgress(UserItems user)
        {
            string logPath = Path.Combine(References.AppFilesPath, "Logs", $"Log{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt");
            logger = new Logger(logPath);
            logger.Log("Progress Started!");

            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("log-level=4");
                IWebDriver driver = new ChromeDriver(options);

                /* Login */

                driver.Navigate().GoToUrl("https://pixai.art/login");

                #region Login
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
                        driver.FindElement(By.Id("email-input")).SendKeys(user.email);
                        driver.FindElement(By.Id("password-input")).SendKeys(user.pass);

                        // Submit the login form
                        driver.FindElement(By.CssSelector("button[type='submit']")).Submit();
                        break;
                    }
                    catch { }

                    Thread.Sleep(49);
                }
                #endregion

                logger.Log($"{user.name} - Successfully logged in.");

                /* After Login */

                try
                {
                    #region Select profile button
                    while (true)
                    {
                        try
                        {
                            // Check if the profile has an image and click on it
                            driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > img")).Click();
                            break;
                        }
                        catch
                        {
                            try
                            {
                                // If the profile doesn't have an image, click on a different element
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > div")).Click();
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
                                { }
                            }
                        }
                        Thread.Sleep(49);
                    }
                    #endregion

                    logger.Log("Profile button clicked.");

                #region Open profile
                drowdownmenu:
                    try
                    {
                        // Click on the profile button in the dropdown menu
                        driver.FindElement(By.CssSelector("li[role='menuitem'][tabindex='0']")).Click();
                    }
                    catch
                    {
                        Thread.Sleep(49);
                        goto drowdownmenu;
                    }
                    #endregion

                    logger.Log("Profile opened.");

                #region Open credit page
                opencreditpage:
                    try
                    {
                        // Open the credit page
                        driver.FindElement(By.CssSelector("div[id='root'] > div.flex > div > div > div > div.flex.flex-col.gap-8 > div > div.flex.flex-col.gap-4 > div > a")).Click();
                    }
                    catch
                    {
                        Thread.Sleep(49);
                        goto opencreditpage;
                    }
                    #endregion

                    logger.Log("Credit page opened.");

                #region Claim credit
                claimcredit:
                    try
                    {
                        // Claim the credit
                        driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button")).Click();
                        goto endprogress;
                    }
                    catch
                    {
                        try
                        {
                            // Check if the credit is already claimed
                            var text = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button > div > div > div")).Text;
                            notifyIcon.ShowBalloonTip(100, "Info!", text, ToolTipIcon.Info);
                            logger.Log($"{user.name} - {text}");
                            if (text != null) goto endprogress;
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

        void refreshList()
        {
            lvaccounts.Items.Clear();
            UserList = ListHelper.UserList;
            foreach (var item in UserList)
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
                var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
                AddOrEditPage editPage = new AddOrEditPage(selected);
                editPage.ShowDialog();
                refreshList();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvaccounts.SelectedItems.Count == 1)
            {
                var res = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
                    UserList.Remove(selected);
                    ListHelper.UserList = UserList;
                    refreshList();
                }
            }
        }

        private void runSingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvaccounts.SelectedItems.Count == 1 && !ClaimWorker.IsBusy)
            {
                var selected = UserList.Where(x => x.id == int.Parse(lvaccounts.SelectedItems[0].Text)).FirstOrDefault();
                runClaimProgress(selected);
            }
            else
            {
                if (lvaccounts.SelectedItems.Count == 0)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Please select item on list.", ToolTipIcon.Warning);
                else if (lvaccounts.SelectedItems.Count > 1)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Please select 1 item on list.", ToolTipIcon.Warning);
                else if (ClaimWorker.IsBusy)
                    notifyIcon.ShowBalloonTip(100, "Info!", "Wait claim progress is running!", ToolTipIcon.Warning);
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
            Application.Exit();
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnStartClaim_Click(object sender, EventArgs e)
        {
            ClaimWorker.RunWorkerAsync();
        }

        private void ClaimWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            notifyIcon.ShowBalloonTip(100, "Info!", "Credit claim progress completed!", ToolTipIcon.None);
        }
    }

}
