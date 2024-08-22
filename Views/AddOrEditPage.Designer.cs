namespace AutoPixAiCreditClaimer.Views
{
    partial class AddOrEditPage
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.btnhideform.Location = new System.Drawing.Point(182, -1);
            this.btnhideform.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnhideform.Name = "btnhideform";
            this.btnhideform.Size = new System.Drawing.Size(56, 27);
            this.btnhideform.TabIndex = 4;
            this.btnhideform.Text = "X";
            this.btnhideform.UseVisualStyleBackColor = false;
            this.btnhideform.Click += new System.EventHandler(this.btnhideform_Click);
            this.btnhideform.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.btnhideform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(14, 39);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtName.MaxLength = 24;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(208, 21);
            this.txtName.TabIndex = 0;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(14, 90);
            this.txtMail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(208, 21);
            this.txtMail.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(14, 142);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(208, 21);
            this.txtPass.TabIndex = 2;
            // 
            // btnsave
            // 
            this.btnsave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnsave.Location = new System.Drawing.Point(58, 187);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(117, 27);
            this.btnsave.TabIndex = 3;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            this.btnsave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.btnsave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Email";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // AddOrEditPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 226);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnhideform);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AddOrEditPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddOrEditPage";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnhideform;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}