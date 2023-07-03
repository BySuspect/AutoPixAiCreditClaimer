using AutoPixAiCreditClaimer.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer.Pages
{
    public partial class MainPage : Form
    {
        List<UserItems> UserList = new List<UserItems>();

        public MainPage()
        {
            InitializeComponent();
            refreshList();
        }

        // Event handler for the form load event
        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        // Asynchronous method to start the automation process
        private Task startAutomation()
        {
            foreach (var user in UserList)
            {
                IWebDriver driver = new ChromeDriver();

                /* Login */

                // Navigate to the login page
                driver.Navigate().GoToUrl("https://pixai.art/login");

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

                /* After Login */

                try
                {
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
                            notifyIcon.ShowBalloonTip(1000, "Info!", text, ToolTipIcon.Info);
                            if (text != null) goto endprogress;
                        }
                        catch
                        {
                            goto claimcredit;
                        }
                    }
                }
                catch { }

            endprogress:
                driver.Quit();
                Thread.Sleep(49);
            }

            return Task.CompletedTask;
        }

        #region Controls

        #region List Control

        // Refresh the user list displayed in the Accounts List
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

        // Event handler for the "Add New" button click event
        private void btnaddnew_Click(object sender, EventArgs e)
        {
            AddOrEditPage addOrEditPage = new AddOrEditPage();
            addOrEditPage.ShowDialog();
            refreshList();
        }

        // Event handler for the Accounts List's "Edit" context menu item click event
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

        // Event handler for the Accounts List's "Delete" context menu item click event
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

        // Event handler for the Accounts List's "Refresh" context menu item click event
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        #endregion

        #region Form Control

        // Event handler for the "Minimize" button click event
        private void btnminiform_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Event handler for the "Exit" button click event
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

        // Event handler for the notify icon's "Exit" context menu item click event
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Event handler for the notify icon's double-click event
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        #endregion

        #endregion

        // Event handler for the "Start Claim" button click event
        private void btnStartClaim_Click(object sender, EventArgs e)
        {
            startAutomation();
        }
    }

}
