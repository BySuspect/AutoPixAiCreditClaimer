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
            this.btnhideform = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbRunOnWindowsStartup = new System.Windows.Forms.CheckBox();
            this.cbRunOnAppStartup = new System.Windows.Forms.CheckBox();
            this.chShowBrowserOnClaimProgress = new System.Windows.Forms.CheckBox();
            this.cbScrollAutomation = new System.Windows.Forms.CheckBox();
            this.cbAutoExit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnhideform
            // 
            this.btnhideform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnhideform.BackColor = System.Drawing.Color.White;
            this.btnhideform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnhideform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnhideform.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnhideform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhideform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnhideform.ForeColor = System.Drawing.Color.Blue;
            this.btnhideform.Location = new System.Drawing.Point(104, -2);
            this.btnhideform.Margin = new System.Windows.Forms.Padding(4);
            this.btnhideform.Name = "btnhideform";
            this.btnhideform.Size = new System.Drawing.Size(64, 28);
            this.btnhideform.TabIndex = 4;
            this.btnhideform.Text = "X";
            this.btnhideform.UseVisualStyleBackColor = false;
            this.btnhideform.Click += new System.EventHandler(this.btnhideform_Click);
            this.btnhideform.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.btnhideform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(45, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbRunOnWindowsStartup
            // 
            this.cbRunOnWindowsStartup.AutoSize = true;
            this.cbRunOnWindowsStartup.Location = new System.Drawing.Point(12, 46);
            this.cbRunOnWindowsStartup.Name = "cbRunOnWindowsStartup";
            this.cbRunOnWindowsStartup.Size = new System.Drawing.Size(149, 21);
            this.cbRunOnWindowsStartup.TabIndex = 5;
            this.cbRunOnWindowsStartup.Text = "Start With Windows";
            this.cbRunOnWindowsStartup.UseVisualStyleBackColor = true;
            // 
            // cbRunOnAppStartup
            // 
            this.cbRunOnAppStartup.AutoSize = true;
            this.cbRunOnAppStartup.Location = new System.Drawing.Point(12, 73);
            this.cbRunOnAppStartup.Name = "cbRunOnAppStartup";
            this.cbRunOnAppStartup.Size = new System.Drawing.Size(148, 21);
            this.cbRunOnAppStartup.TabIndex = 5;
            this.cbRunOnAppStartup.Text = "Run With App Start";
            this.cbRunOnAppStartup.UseVisualStyleBackColor = true;
            // 
            // chShowBrowserOnClaimProgress
            // 
            this.chShowBrowserOnClaimProgress.AutoSize = true;
            this.chShowBrowserOnClaimProgress.Location = new System.Drawing.Point(12, 100);
            this.chShowBrowserOnClaimProgress.Name = "chShowBrowserOnClaimProgress";
            this.chShowBrowserOnClaimProgress.Size = new System.Drawing.Size(116, 21);
            this.chShowBrowserOnClaimProgress.TabIndex = 5;
            this.chShowBrowserOnClaimProgress.Text = "Show Browser";
            this.chShowBrowserOnClaimProgress.UseVisualStyleBackColor = true;
            // 
            // cbScrollAutomation
            // 
            this.cbScrollAutomation.AccessibleDescription = "";
            this.cbScrollAutomation.AutoSize = true;
            this.cbScrollAutomation.Enabled = false;
            this.cbScrollAutomation.Location = new System.Drawing.Point(12, 154);
            this.cbScrollAutomation.Name = "cbScrollAutomation";
            this.cbScrollAutomation.Size = new System.Drawing.Size(95, 21);
            this.cbScrollAutomation.TabIndex = 5;
            this.cbScrollAutomation.Text = "PageScroll";
            this.cbScrollAutomation.UseVisualStyleBackColor = true;
            this.cbScrollAutomation.Visible = false;
            // 
            // cbAutoExit
            // 
            this.cbAutoExit.AutoSize = true;
            this.cbAutoExit.Location = new System.Drawing.Point(12, 127);
            this.cbAutoExit.Name = "cbAutoExit";
            this.cbAutoExit.Size = new System.Drawing.Size(78, 21);
            this.cbAutoExit.TabIndex = 5;
            this.cbAutoExit.Text = "AutoExit";
            this.cbAutoExit.UseVisualStyleBackColor = true;
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 229);
            this.Controls.Add(this.cbAutoExit);
            this.Controls.Add(this.cbScrollAutomation);
            this.Controls.Add(this.chShowBrowserOnClaimProgress);
            this.Controls.Add(this.cbRunOnAppStartup);
            this.Controls.Add(this.cbRunOnWindowsStartup);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnhideform);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsPage";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnhideform;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbRunOnWindowsStartup;
        private System.Windows.Forms.CheckBox cbRunOnAppStartup;
        private System.Windows.Forms.CheckBox chShowBrowserOnClaimProgress;
        private System.Windows.Forms.CheckBox cbScrollAutomation;
        private System.Windows.Forms.CheckBox cbAutoExit;
    }
}