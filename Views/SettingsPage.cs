using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoPixAiCreditClaimer.Helpers;
using Microsoft.Win32;

namespace AutoPixAiCreditClaimer.Views
{
    public partial class SettingsPage : Form
    {
        public SettingsPage()
        {
            InitializeComponent();

            cbRunOnAppStartup.Checked = SettingsHelper.Settings.runOnAppStartup;
            cbRunOnWindowsStartup.Checked = SettingsHelper.Settings.runOnWindowsStartup;
            chShowBrowserOnClaimProgress.Checked = SettingsHelper
                .Settings
                .showBrowserOnClaimProgress;
            cbScrollAutomation.Checked = SettingsHelper.Settings.scrollPageAutomation;
            cbAutoExit.Checked = SettingsHelper.Settings.AutoExitApp;
        }

        private void btnhideform_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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


        private void btnSave_Click(object sender, EventArgs e)
        {
            SettingsHelper.Settings = new SettingsItems
            {
                runOnWindowsStartup = cbRunOnWindowsStartup.Checked,
                runOnAppStartup = cbRunOnAppStartup.Checked,
                showBrowserOnClaimProgress = chShowBrowserOnClaimProgress.Checked,
                scrollPageAutomation = cbScrollAutomation.Checked,
                AutoExitApp = cbAutoExit.Checked,
            };

            if (cbRunOnWindowsStartup.Checked)
                AddStartup();
            else
                RemoveStartup();

            DialogResult = DialogResult.OK;
        }

        public void AddStartup()
        {
            using (
                RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                    true
                )
            )
            {
                key.SetValue(
                    AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""),
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        AppDomain.CurrentDomain.FriendlyName
                    )
                );
            }
        }

        public void RemoveStartup()
        {
            using (
                RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                    true
                )
            )
            {
                key.DeleteValue(AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), false);
            }
        }
    }
}
