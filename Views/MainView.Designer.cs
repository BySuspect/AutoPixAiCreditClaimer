namespace AutoPixAiCreditClaimer.Views
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ecitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnhideform = new System.Windows.Forms.Button();
            this.btnminiform = new System.Windows.Forms.Button();
            this.lvaccounts = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnaddnew = new System.Windows.Forms.Button();
            this.btnStartClaim = new System.Windows.Forms.Button();
            this.ClaimWorker = new System.ComponentModel.BackgroundWorker();
            this.btnSettings = new System.Windows.Forms.Button();
            this.runSingleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifymenu.SuspendLayout();
            this.listmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifymenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PixaiAutoCreditClaimer";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifymenu
            // 
            this.notifymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ecitToolStripMenuItem});
            this.notifymenu.Name = "notifymenu";
            this.notifymenu.Size = new System.Drawing.Size(94, 26);
            // 
            // ecitToolStripMenuItem
            // 
            this.ecitToolStripMenuItem.Name = "ecitToolStripMenuItem";
            this.ecitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.ecitToolStripMenuItem.Text = "Exit";
            this.ecitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.btnhideform.Location = new System.Drawing.Point(612, 0);
            this.btnhideform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnhideform.Name = "btnhideform";
            this.btnhideform.Size = new System.Drawing.Size(56, 26);
            this.btnhideform.TabIndex = 0;
            this.btnhideform.Text = "X";
            this.btnhideform.UseVisualStyleBackColor = false;
            this.btnhideform.Click += new System.EventHandler(this.btnhideform_Click);
            this.btnhideform.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.btnhideform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // btnminiform
            // 
            this.btnminiform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnminiform.BackColor = System.Drawing.Color.White;
            this.btnminiform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnminiform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnminiform.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnminiform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnminiform.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnminiform.ForeColor = System.Drawing.Color.Blue;
            this.btnminiform.Location = new System.Drawing.Point(571, 0);
            this.btnminiform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnminiform.Name = "btnminiform";
            this.btnminiform.Size = new System.Drawing.Size(41, 26);
            this.btnminiform.TabIndex = 0;
            this.btnminiform.Text = "⚊";
            this.btnminiform.UseVisualStyleBackColor = false;
            this.btnminiform.Click += new System.EventHandler(this.btnminiform_Click);
            this.btnminiform.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.btnminiform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // lvaccounts
            // 
            this.lvaccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.colname,
            this.colmail,
            this.colpass});
            this.lvaccounts.ContextMenuStrip = this.listmenu;
            this.lvaccounts.FullRowSelect = true;
            this.lvaccounts.GridLines = true;
            this.lvaccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvaccounts.HideSelection = false;
            this.lvaccounts.Location = new System.Drawing.Point(14, 49);
            this.lvaccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvaccounts.MultiSelect = false;
            this.lvaccounts.Name = "lvaccounts";
            this.lvaccounts.Size = new System.Drawing.Size(501, 133);
            this.lvaccounts.TabIndex = 0;
            this.lvaccounts.UseCompatibleStateImageBehavior = false;
            this.lvaccounts.View = System.Windows.Forms.View.Details;
            this.lvaccounts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.lvaccounts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // id
            // 
            this.id.DisplayIndex = 3;
            this.id.Text = "";
            this.id.Width = 0;
            // 
            // colname
            // 
            this.colname.DisplayIndex = 0;
            this.colname.Text = "Name";
            this.colname.Width = 90;
            // 
            // colmail
            // 
            this.colmail.DisplayIndex = 1;
            this.colmail.Text = "Email";
            this.colmail.Width = 177;
            // 
            // colpass
            // 
            this.colpass.DisplayIndex = 2;
            this.colpass.Text = "Password";
            this.colpass.Width = 150;
            // 
            // listmenu
            // 
            this.listmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runSingleToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.listmenu.Name = "listmenu";
            this.listmenu.Size = new System.Drawing.Size(131, 92);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // btnaddnew
            // 
            this.btnaddnew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnaddnew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnaddnew.Location = new System.Drawing.Point(522, 49);
            this.btnaddnew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnaddnew.Name = "btnaddnew";
            this.btnaddnew.Size = new System.Drawing.Size(133, 26);
            this.btnaddnew.TabIndex = 1;
            this.btnaddnew.Text = "Add New";
            this.btnaddnew.UseVisualStyleBackColor = true;
            this.btnaddnew.Click += new System.EventHandler(this.btnaddnew_Click);
            // 
            // btnStartClaim
            // 
            this.btnStartClaim.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnStartClaim.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartClaim.Location = new System.Drawing.Point(522, 113);
            this.btnStartClaim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartClaim.Name = "btnStartClaim";
            this.btnStartClaim.Size = new System.Drawing.Size(133, 26);
            this.btnStartClaim.TabIndex = 3;
            this.btnStartClaim.Text = "Start Claim";
            this.btnStartClaim.UseVisualStyleBackColor = true;
            this.btnStartClaim.Click += new System.EventHandler(this.btnStartClaim_Click);
            // 
            // ClaimWorker
            // 
            this.ClaimWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClaimWorker_DoWork);
            this.ClaimWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ClaimWorker_RunWorkerCompleted);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettings.Location = new System.Drawing.Point(522, 82);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(133, 26);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // runSingleToolStripMenuItem
            // 
            this.runSingleToolStripMenuItem.Name = "runSingleToolStripMenuItem";
            this.runSingleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.runSingleToolStripMenuItem.Text = "Run Single";
            this.runSingleToolStripMenuItem.Click += new System.EventHandler(this.runSingleToolStripMenuItem_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 211);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnStartClaim);
            this.Controls.Add(this.btnaddnew);
            this.Controls.Add(this.lvaccounts);
            this.Controls.Add(this.btnminiform);
            this.Controls.Add(this.btnhideform);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainPage";
            this.Text = "MainPage";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.notifymenu.ResumeLayout(false);
            this.listmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifymenu;
        private System.Windows.Forms.ToolStripMenuItem ecitToolStripMenuItem;
        private System.Windows.Forms.Button btnhideform;
        private System.Windows.Forms.Button btnminiform;
        private System.Windows.Forms.ListView lvaccounts;
        private System.Windows.Forms.ColumnHeader colname;
        private System.Windows.Forms.ColumnHeader colmail;
        private System.Windows.Forms.ColumnHeader colpass;
        private System.Windows.Forms.ContextMenuStrip listmenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnaddnew;
        private System.Windows.Forms.Button btnStartClaim;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker ClaimWorker;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ToolStripMenuItem runSingleToolStripMenuItem;
    }
}