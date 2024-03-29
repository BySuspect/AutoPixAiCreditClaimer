using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer.Pages
{
    public partial class NewVersionAlertPage : Form
    {
        public NewVersionAlertPage()
        {
            InitializeComponent();
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

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                "https://github.com/BySuspect/AutoPixAiCreditClaimer/releases/latest"
            );
            this.DialogResult = DialogResult.OK;
        }
    }
}
