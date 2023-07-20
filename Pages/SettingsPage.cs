using AutoPixAiCreditClaimer.Helpers;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer.Pages
{
    public partial class SettingsPage : Form
    {
        public SettingsPage()
        {
            InitializeComponent();
            cbRunOnAppStartup.Checked = SettingsHelper.Settings.runOnAppStartup;
            cbRunOnWindowsStartup.Checked = SettingsHelper.Settings.runOnWindowsStartup;
        }
        private void btnhideform_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SettingsHelper.Settings = new SettingsItems
            {
                runOnWindowsStartup = cbRunOnWindowsStartup.Checked,
                runOnAppStartup = cbRunOnAppStartup.Checked,
            };

            if (cbRunOnWindowsStartup.Checked)
                AddStartup();
            else
                RemoveStartup();

            DialogResult = DialogResult.OK;
        }
        public void AddStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName));
            }
        }

        public void RemoveStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), false);
            }
        }
    }
}
