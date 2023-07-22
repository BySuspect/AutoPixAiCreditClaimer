using AutoPixAiCreditClaimer.Helpers;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer.Pages
{
    // A partial class representing the settings page form
    public partial class SettingsPage : Form
    {
        // Constructor for the SettingsPage form
        public SettingsPage()
        {
            InitializeComponent();

            // Load the current application settings into the form's checkboxes
            cbRunOnAppStartup.Checked = SettingsHelper.Settings.runOnAppStartup;
            cbRunOnWindowsStartup.Checked = SettingsHelper.Settings.runOnWindowsStartup;
            chShowBrowserOnClaimProgress.Checked = SettingsHelper.Settings.showBrowserOnClaimProgress;
        }

        // Event handler for the "Cancel" button click
        private void btnhideform_Click(object sender, EventArgs e)
        {
            // Set the form's DialogResult to Cancel, indicating the cancel action
            DialogResult = DialogResult.Cancel;
        }

        #region Mouse move codes

        // Track the mouse location when the form is clicked
        private Point _mouseLoc;

        // Event handler for the form's MouseDown event
        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        // Event handler for the form's MouseMove event
        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            // Move the form with the mouse movement when the left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        #endregion

        // Event handler for the "Save" button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Update the application settings with the selected checkbox values
            SettingsHelper.Settings = new SettingsItems
            {
                runOnWindowsStartup = cbRunOnWindowsStartup.Checked,
                runOnAppStartup = cbRunOnAppStartup.Checked,
                showBrowserOnClaimProgress = chShowBrowserOnClaimProgress.Checked,
            };

            // Add or remove the application from Windows startup based on the selected checkbox value
            if (cbRunOnWindowsStartup.Checked)
                AddStartup();
            else
                RemoveStartup();

            // Set the form's DialogResult to OK, indicating the successful completion of the operation
            DialogResult = DialogResult.OK;
        }

        // Method to add the application to Windows startup
        public void AddStartup()
        {
            // Add the application to the Windows startup registry key with the application's executable path
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName));
            }
        }

        // Method to remove the application from Windows startup
        public void RemoveStartup()
        {
            // Remove the application from the Windows startup registry key
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), false);
            }
        }
    }

}
