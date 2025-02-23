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
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components);
            notifymenu = new ContextMenuStrip(components);
            ecitToolStripMenuItem = new ToolStripMenuItem();
            btnhideform = new Button();
            btnminiform = new Button();
            lvaccounts = new ListView();
            id = new ColumnHeader();
            colname = new ColumnHeader();
            colmail = new ColumnHeader();
            colpass = new ColumnHeader();
            listmenu = new ContextMenuStrip(components);
            runSingleToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            btnaddnew = new Button();
            btnStartClaim = new Button();
            ClaimWorker = new System.ComponentModel.BackgroundWorker();
            btnSettings = new Button();
            label1 = new Label();
            lblInfo = new Label();
            notifymenu.SuspendLayout();
            listmenu.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ContextMenuStrip = notifymenu;
            notifyIcon.Text = "PixaiAutoCreditClaimer";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // notifymenu
            // 
            notifymenu.ImageScalingSize = new Size(18, 18);
            notifymenu.Items.AddRange(new ToolStripItem[] { ecitToolStripMenuItem });
            notifymenu.Name = "notifymenu";
            notifymenu.Size = new Size(97, 26);
            // 
            // ecitToolStripMenuItem
            // 
            ecitToolStripMenuItem.Name = "ecitToolStripMenuItem";
            ecitToolStripMenuItem.Size = new Size(96, 22);
            ecitToolStripMenuItem.Text = "Exit";
            ecitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
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
            btnhideform.Location = new Point(612, 0);
            btnhideform.Margin = new Padding(4);
            btnhideform.Name = "btnhideform";
            btnhideform.Size = new Size(56, 26);
            btnhideform.TabIndex = 0;
            btnhideform.Text = "X";
            btnhideform.UseVisualStyleBackColor = false;
            btnhideform.Click += btnhideform_Click;
            btnhideform.MouseDown += FormMouseDown;
            btnhideform.MouseMove += FormMouseMove;
            // 
            // btnminiform
            // 
            btnminiform.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnminiform.BackColor = Color.White;
            btnminiform.BackgroundImageLayout = ImageLayout.None;
            btnminiform.Cursor = Cursors.Hand;
            btnminiform.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnminiform.FlatStyle = FlatStyle.Flat;
            btnminiform.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnminiform.ForeColor = Color.Blue;
            btnminiform.Location = new Point(571, 0);
            btnminiform.Margin = new Padding(4);
            btnminiform.Name = "btnminiform";
            btnminiform.Size = new Size(41, 26);
            btnminiform.TabIndex = 0;
            btnminiform.Text = "⚊";
            btnminiform.UseVisualStyleBackColor = false;
            btnminiform.Click += btnminiform_Click;
            btnminiform.MouseDown += FormMouseDown;
            btnminiform.MouseMove += FormMouseMove;
            // 
            // lvaccounts
            // 
            lvaccounts.Columns.AddRange(new ColumnHeader[] { id, colname, colmail, colpass });
            lvaccounts.ContextMenuStrip = listmenu;
            lvaccounts.FullRowSelect = true;
            lvaccounts.GridLines = true;
            lvaccounts.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvaccounts.Location = new Point(14, 49);
            lvaccounts.Margin = new Padding(4);
            lvaccounts.MultiSelect = false;
            lvaccounts.Name = "lvaccounts";
            lvaccounts.Size = new Size(501, 133);
            lvaccounts.TabIndex = 0;
            lvaccounts.UseCompatibleStateImageBehavior = false;
            lvaccounts.View = View.Details;
            lvaccounts.MouseDown += FormMouseDown;
            lvaccounts.MouseMove += FormMouseMove;
            // 
            // id
            // 
            id.DisplayIndex = 3;
            id.Text = "";
            id.Width = 0;
            // 
            // colname
            // 
            colname.DisplayIndex = 0;
            colname.Text = "Name";
            colname.Width = 90;
            // 
            // colmail
            // 
            colmail.DisplayIndex = 1;
            colmail.Text = "Email";
            colmail.Width = 177;
            // 
            // colpass
            // 
            colpass.DisplayIndex = 2;
            colpass.Text = "Password";
            colpass.Width = 150;
            // 
            // listmenu
            // 
            listmenu.ImageScalingSize = new Size(18, 18);
            listmenu.Items.AddRange(new ToolStripItem[] { runSingleToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem, refreshToolStripMenuItem });
            listmenu.Name = "listmenu";
            listmenu.Size = new Size(138, 92);
            // 
            // runSingleToolStripMenuItem
            // 
            runSingleToolStripMenuItem.Name = "runSingleToolStripMenuItem";
            runSingleToolStripMenuItem.Size = new Size(137, 22);
            runSingleToolStripMenuItem.Text = "Run Single";
            runSingleToolStripMenuItem.Click += runSingleToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(137, 22);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(137, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(137, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // btnaddnew
            // 
            btnaddnew.Anchor = AnchorStyles.Right;
            btnaddnew.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnaddnew.Location = new Point(522, 49);
            btnaddnew.Margin = new Padding(4);
            btnaddnew.Name = "btnaddnew";
            btnaddnew.Size = new Size(133, 26);
            btnaddnew.TabIndex = 1;
            btnaddnew.Text = "Add New";
            btnaddnew.UseVisualStyleBackColor = true;
            btnaddnew.Click += btnaddnew_Click;
            // 
            // btnStartClaim
            // 
            btnStartClaim.Anchor = AnchorStyles.Right;
            btnStartClaim.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartClaim.Location = new Point(522, 113);
            btnStartClaim.Margin = new Padding(4);
            btnStartClaim.Name = "btnStartClaim";
            btnStartClaim.Size = new Size(133, 26);
            btnStartClaim.TabIndex = 3;
            btnStartClaim.Text = "Start Claim";
            btnStartClaim.UseVisualStyleBackColor = true;
            btnStartClaim.Click += btnStartClaim_Click;
            // 
            // ClaimWorker
            // 
            ClaimWorker.DoWork += ClaimWorker_DoWork;
            ClaimWorker.RunWorkerCompleted += ClaimWorker_RunWorkerCompleted;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Right;
            btnSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSettings.Location = new Point(522, 82);
            btnSettings.Margin = new Padding(4);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(133, 26);
            btnSettings.TabIndex = 2;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 6);
            label1.Name = "label1";
            label1.Size = new Size(87, 17);
            label1.TabIndex = 4;
            label1.Text = "Working On:";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(107, 6);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(57, 17);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Nothing";
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 211);
            Controls.Add(lblInfo);
            Controls.Add(label1);
            Controls.Add(btnSettings);
            Controls.Add(btnStartClaim);
            Controls.Add(btnaddnew);
            Controls.Add(lvaccounts);
            Controls.Add(btnminiform);
            Controls.Add(btnhideform);
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "MainView";
            Text = "MainPage";
            Load += MainPage_Load;
            MouseDown += FormMouseDown;
            MouseMove += FormMouseMove;
            notifymenu.ResumeLayout(false);
            listmenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private Label label1;
        private Label lblInfo;
    }
}