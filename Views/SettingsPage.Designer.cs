namespace AutoPixAiCreditClaimer.Views
{
    partial class SettingsPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be
        ///     disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify the contents of
        /// this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnhideform = new Button();
            btnSave = new Button();
            cbRunOnWindowsStartup = new CheckBox();
            cbRunOnAppStartup = new CheckBox();
            chShowBrowserOnClaimProgress = new CheckBox();
            cbAutoExit = new CheckBox();
            SuspendLayout();
            // 
            // btnhideform
            // 
            btnhideform.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnhideform.BackColor = Color.White;
            btnhideform.BackgroundImageLayout = ImageLayout.None;
            btnhideform.Cursor = Cursors.Hand;
            btnhideform.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnhideform.FlatStyle = FlatStyle.Flat;
            btnhideform.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnhideform.ForeColor = Color.Blue;
            btnhideform.Location = new Point(104, -2);
            btnhideform.Margin = new Padding(4);
            btnhideform.Name = "btnhideform";
            btnhideform.Size = new Size(64, 28);
            btnhideform.TabIndex = 4;
            btnhideform.Text = "X";
            btnhideform.UseVisualStyleBackColor = false;
            btnhideform.Click += btnhideform_Click;
            btnhideform.MouseDown += FormMouseDown;
            btnhideform.MouseMove += FormMouseMove;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.Location = new Point(45, 158);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // cbRunOnWindowsStartup
            // 
            cbRunOnWindowsStartup.AutoSize = true;
            cbRunOnWindowsStartup.Location = new Point(12, 46);
            cbRunOnWindowsStartup.Name = "cbRunOnWindowsStartup";
            cbRunOnWindowsStartup.Size = new Size(149, 21);
            cbRunOnWindowsStartup.TabIndex = 5;
            cbRunOnWindowsStartup.Text = "Start With Windows";
            cbRunOnWindowsStartup.UseVisualStyleBackColor = true;
            // 
            // cbRunOnAppStartup
            // 
            cbRunOnAppStartup.AutoSize = true;
            cbRunOnAppStartup.Location = new Point(12, 73);
            cbRunOnAppStartup.Name = "cbRunOnAppStartup";
            cbRunOnAppStartup.Size = new Size(148, 21);
            cbRunOnAppStartup.TabIndex = 5;
            cbRunOnAppStartup.Text = "Run With App Start";
            cbRunOnAppStartup.UseVisualStyleBackColor = true;
            // 
            // chShowBrowserOnClaimProgress
            // 
            chShowBrowserOnClaimProgress.AutoSize = true;
            chShowBrowserOnClaimProgress.Location = new Point(12, 100);
            chShowBrowserOnClaimProgress.Name = "chShowBrowserOnClaimProgress";
            chShowBrowserOnClaimProgress.Size = new Size(116, 21);
            chShowBrowserOnClaimProgress.TabIndex = 5;
            chShowBrowserOnClaimProgress.Text = "Show Browser";
            chShowBrowserOnClaimProgress.UseVisualStyleBackColor = true;
            // 
            // cbAutoExit
            // 
            cbAutoExit.AutoSize = true;
            cbAutoExit.Location = new Point(12, 127);
            cbAutoExit.Name = "cbAutoExit";
            cbAutoExit.Size = new Size(78, 21);
            cbAutoExit.TabIndex = 5;
            cbAutoExit.Text = "AutoExit";
            cbAutoExit.UseVisualStyleBackColor = true;
            // 
            // SettingsPage
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(168, 193);
            Controls.Add(cbAutoExit);
            Controls.Add(chShowBrowserOnClaimProgress);
            Controls.Add(cbRunOnAppStartup);
            Controls.Add(cbRunOnWindowsStartup);
            Controls.Add(btnSave);
            Controls.Add(btnhideform);
            Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 162);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "SettingsPage";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SettingsPage";
            MouseDown += FormMouseDown;
            MouseMove += FormMouseMove;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnhideform;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbRunOnWindowsStartup;
        private System.Windows.Forms.CheckBox cbRunOnAppStartup;
        private System.Windows.Forms.CheckBox chShowBrowserOnClaimProgress;
        private System.Windows.Forms.CheckBox cbAutoExit;
    }
}