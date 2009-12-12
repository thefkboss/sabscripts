namespace SABSyncGUI
{
    partial class SABSyncGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveButton = new System.Windows.Forms.Button();
            this.txtTvRoot = new System.Windows.Forms.TextBox();
            this.lblTvRoot = new System.Windows.Forms.Label();
            this.tvRootBrowse = new System.Windows.Forms.Button();
            this.tvRootDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtTvTemplate = new System.Windows.Forms.TextBox();
            this.lblTvTemplate = new System.Windows.Forms.Label();
            this.txtTvDailyTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblSabInfo = new System.Windows.Forms.Label();
            this.txtSabInfo = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtNzbDir = new System.Windows.Forms.TextBox();
            this.lblNzbDir = new System.Windows.Forms.Label();
            this.nzbDirBrowse = new System.Windows.Forms.Button();
            this.nzbDirDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtVideoExt = new System.Windows.Forms.TextBox();
            this.lblVideoExt = new System.Windows.Forms.Label();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.txtRssConfig = new System.Windows.Forms.TextBox();
            this.lblRssConfig = new System.Windows.Forms.Label();
            this.btnRssConfig = new System.Windows.Forms.Button();
            this.txtIgnoreSeasons = new System.Windows.Forms.TextBox();
            this.lblIgnoreSeasons = new System.Windows.Forms.Label();
            this.chkReplaceChars = new System.Windows.Forms.CheckBox();
            this.lblPriorityDesc = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(318, 458);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTvRoot
            // 
            this.txtTvRoot.Location = new System.Drawing.Point(118, 19);
            this.txtTvRoot.Name = "txtTvRoot";
            this.txtTvRoot.Size = new System.Drawing.Size(250, 20);
            this.txtTvRoot.TabIndex = 1;
            this.txtTvRoot.TextChanged += new System.EventHandler(this.txtTvRoot_TextChanged);
            // 
            // lblTvRoot
            // 
            this.lblTvRoot.AutoSize = true;
            this.lblTvRoot.Location = new System.Drawing.Point(65, 26);
            this.lblTvRoot.Name = "lblTvRoot";
            this.lblTvRoot.Size = new System.Drawing.Size(50, 13);
            this.lblTvRoot.TabIndex = 2;
            this.lblTvRoot.Text = "TV Root:";
            this.lblTvRoot.Click += new System.EventHandler(this.label1_Click);
            // 
            // tvRootBrowse
            // 
            this.tvRootBrowse.Location = new System.Drawing.Point(374, 17);
            this.tvRootBrowse.Name = "tvRootBrowse";
            this.tvRootBrowse.Size = new System.Drawing.Size(24, 23);
            this.tvRootBrowse.TabIndex = 3;
            this.tvRootBrowse.Text = "...";
            this.tvRootBrowse.UseVisualStyleBackColor = true;
            this.tvRootBrowse.Click += new System.EventHandler(this.tvRootBrowse_Click);
            this.tvRootBrowse.MouseHover += new System.EventHandler(this.btnTvRootBrowse_MouseHover);
            // 
            // tvRootDialog
            // 
            this.tvRootDialog.Description = "TV Root Folder";
            this.tvRootDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtTvTemplate
            // 
            this.txtTvTemplate.Location = new System.Drawing.Point(118, 46);
            this.txtTvTemplate.Name = "txtTvTemplate";
            this.txtTvTemplate.Size = new System.Drawing.Size(250, 20);
            this.txtTvTemplate.TabIndex = 4;
            // 
            // lblTvTemplate
            // 
            this.lblTvTemplate.AutoSize = true;
            this.lblTvTemplate.Location = new System.Drawing.Point(44, 53);
            this.lblTvTemplate.Name = "lblTvTemplate";
            this.lblTvTemplate.Size = new System.Drawing.Size(71, 13);
            this.lblTvTemplate.TabIndex = 5;
            this.lblTvTemplate.Text = "TV Template:";
            this.lblTvTemplate.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // txtTvDailyTemplate
            // 
            this.txtTvDailyTemplate.Location = new System.Drawing.Point(118, 72);
            this.txtTvDailyTemplate.Name = "txtTvDailyTemplate";
            this.txtTvDailyTemplate.Size = new System.Drawing.Size(250, 20);
            this.txtTvDailyTemplate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "TV Daily Template:";
            this.label1.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblApiKey);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtApiKey);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.lblSabInfo);
            this.groupBox1.Controls.Add(this.txtSabInfo);
            this.groupBox1.Location = new System.Drawing.Point(24, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 129);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SABnzbd Settings";
            this.groupBox1.Enter += new System.EventHandler(this.grpSabSettings_Enter);
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(50, 104);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 15;
            this.lblApiKey.Text = "API Key:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(42, 78);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Password:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(104, 97);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(200, 20);
            this.txtApiKey.TabIndex = 13;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(104, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 12;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 52);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 11;
            this.lblUsername.Text = "Username:";
            this.lblUsername.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(104, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 10;
            this.txtUsername.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lblSabInfo
            // 
            this.lblSabInfo.AutoSize = true;
            this.lblSabInfo.Location = new System.Drawing.Point(9, 26);
            this.lblSabInfo.Name = "lblSabInfo";
            this.lblSabInfo.Size = new System.Drawing.Size(89, 13);
            this.lblSabInfo.TabIndex = 9;
            this.lblSabInfo.Text = "Hostname + Port:";
            // 
            // txtSabInfo
            // 
            this.txtSabInfo.Location = new System.Drawing.Point(104, 19);
            this.txtSabInfo.Name = "txtSabInfo";
            this.txtSabInfo.Size = new System.Drawing.Size(200, 20);
            this.txtSabInfo.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 490);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(404, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "Hello";
            // 
            // txtNzbDir
            // 
            this.txtNzbDir.Location = new System.Drawing.Point(104, 312);
            this.txtNzbDir.Name = "txtNzbDir";
            this.txtNzbDir.Size = new System.Drawing.Size(250, 20);
            this.txtNzbDir.TabIndex = 10;
            this.txtNzbDir.TextChanged += new System.EventHandler(this.txtNzbDir_TextChanged);
            // 
            // lblNzbDir
            // 
            this.lblNzbDir.AutoSize = true;
            this.lblNzbDir.Location = new System.Drawing.Point(21, 319);
            this.lblNzbDir.Name = "lblNzbDir";
            this.lblNzbDir.Size = new System.Drawing.Size(77, 13);
            this.lblNzbDir.TabIndex = 11;
            this.lblNzbDir.Text = "NZB Directory:";
            // 
            // nzbDirBrowse
            // 
            this.nzbDirBrowse.Location = new System.Drawing.Point(360, 310);
            this.nzbDirBrowse.Name = "nzbDirBrowse";
            this.nzbDirBrowse.Size = new System.Drawing.Size(24, 23);
            this.nzbDirBrowse.TabIndex = 12;
            this.nzbDirBrowse.Text = "...";
            this.nzbDirBrowse.UseVisualStyleBackColor = true;
            this.nzbDirBrowse.Click += new System.EventHandler(this.nzbDirBrowse_Click);
            // 
            // nzbDirDialog
            // 
            this.nzbDirDialog.Description = "NZB Import Folder";
            this.nzbDirDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtVideoExt
            // 
            this.txtVideoExt.Location = new System.Drawing.Point(118, 98);
            this.txtVideoExt.Name = "txtVideoExt";
            this.txtVideoExt.Size = new System.Drawing.Size(250, 20);
            this.txtVideoExt.TabIndex = 13;
            // 
            // lblVideoExt
            // 
            this.lblVideoExt.AutoSize = true;
            this.lblVideoExt.Location = new System.Drawing.Point(21, 105);
            this.lblVideoExt.Name = "lblVideoExt";
            this.lblVideoExt.Size = new System.Drawing.Size(91, 13);
            this.lblVideoExt.TabIndex = 14;
            this.lblVideoExt.Text = "Video Extensions:";
            // 
            // txtPriority
            // 
            this.txtPriority.Location = new System.Drawing.Point(104, 338);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Size = new System.Drawing.Size(26, 20);
            this.txtPriority.TabIndex = 15;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(57, 345);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 16;
            this.lblPriority.Text = "Priority:";
            // 
            // txtRssConfig
            // 
            this.txtRssConfig.Location = new System.Drawing.Point(104, 364);
            this.txtRssConfig.Name = "txtRssConfig";
            this.txtRssConfig.Size = new System.Drawing.Size(250, 20);
            this.txtRssConfig.TabIndex = 17;
            // 
            // lblRssConfig
            // 
            this.lblRssConfig.AutoSize = true;
            this.lblRssConfig.Location = new System.Drawing.Point(14, 371);
            this.lblRssConfig.Name = "lblRssConfig";
            this.lblRssConfig.Size = new System.Drawing.Size(84, 13);
            this.lblRssConfig.TabIndex = 18;
            this.lblRssConfig.Text = "RSS Config File:";
            // 
            // btnRssConfig
            // 
            this.btnRssConfig.Location = new System.Drawing.Point(360, 362);
            this.btnRssConfig.Name = "btnRssConfig";
            this.btnRssConfig.Size = new System.Drawing.Size(24, 23);
            this.btnRssConfig.TabIndex = 19;
            this.btnRssConfig.Text = "...";
            this.btnRssConfig.UseVisualStyleBackColor = true;
            this.btnRssConfig.Click += new System.EventHandler(this.btnRssConfig_Click);
            // 
            // txtIgnoreSeasons
            // 
            this.txtIgnoreSeasons.AcceptsReturn = true;
            this.txtIgnoreSeasons.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtIgnoreSeasons.Location = new System.Drawing.Point(104, 390);
            this.txtIgnoreSeasons.Multiline = true;
            this.txtIgnoreSeasons.Name = "txtIgnoreSeasons";
            this.txtIgnoreSeasons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIgnoreSeasons.Size = new System.Drawing.Size(250, 60);
            this.txtIgnoreSeasons.TabIndex = 20;
            // 
            // lblIgnoreSeasons
            // 
            this.lblIgnoreSeasons.AutoSize = true;
            this.lblIgnoreSeasons.Location = new System.Drawing.Point(2, 393);
            this.lblIgnoreSeasons.Name = "lblIgnoreSeasons";
            this.lblIgnoreSeasons.Size = new System.Drawing.Size(96, 13);
            this.lblIgnoreSeasons.TabIndex = 21;
            this.lblIgnoreSeasons.Text = "Seasons to Ignore:";
            // 
            // chkReplaceChars
            // 
            this.chkReplaceChars.AutoSize = true;
            this.chkReplaceChars.Location = new System.Drawing.Point(104, 456);
            this.chkReplaceChars.Name = "chkReplaceChars";
            this.chkReplaceChars.Size = new System.Drawing.Size(120, 17);
            this.chkReplaceChars.TabIndex = 22;
            this.chkReplaceChars.Text = "Replace Characters";
            this.chkReplaceChars.UseVisualStyleBackColor = true;
            // 
            // lblPriorityDesc
            // 
            this.lblPriorityDesc.AutoSize = true;
            this.lblPriorityDesc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPriorityDesc.Location = new System.Drawing.Point(137, 344);
            this.lblPriorityDesc.Name = "lblPriorityDesc";
            this.lblPriorityDesc.Size = new System.Drawing.Size(127, 13);
            this.lblPriorityDesc.TabIndex = 23;
            this.lblPriorityDesc.Text = "Low=-1 Normal=0 High=1";
            // 
            // SABSyncGUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(404, 512);
            this.Controls.Add(this.lblPriorityDesc);
            this.Controls.Add(this.chkReplaceChars);
            this.Controls.Add(this.lblIgnoreSeasons);
            this.Controls.Add(this.txtIgnoreSeasons);
            this.Controls.Add(this.btnRssConfig);
            this.Controls.Add(this.lblRssConfig);
            this.Controls.Add(this.txtRssConfig);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.txtPriority);
            this.Controls.Add(this.lblVideoExt);
            this.Controls.Add(this.txtVideoExt);
            this.Controls.Add(this.nzbDirBrowse);
            this.Controls.Add(this.lblNzbDir);
            this.Controls.Add(this.txtNzbDir);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTvDailyTemplate);
            this.Controls.Add(this.lblTvTemplate);
            this.Controls.Add(this.txtTvTemplate);
            this.Controls.Add(this.tvRootBrowse);
            this.Controls.Add(this.lblTvRoot);
            this.Controls.Add(this.txtTvRoot);
            this.Controls.Add(this.saveButton);
            this.Name = "SABSyncGUI";
            this.Text = "SABSync GUI";
            this.Load += new System.EventHandler(this.SABSyncGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox txtTvRoot;
        private System.Windows.Forms.Label lblTvRoot;
        private System.Windows.Forms.Button tvRootBrowse;
        private System.Windows.Forms.FolderBrowserDialog tvRootDialog;
        private System.Windows.Forms.TextBox txtTvTemplate;
        private System.Windows.Forms.Label lblTvTemplate;
        private System.Windows.Forms.TextBox txtTvDailyTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblSabInfo;
        private System.Windows.Forms.TextBox txtSabInfo;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtNzbDir;
        private System.Windows.Forms.Label lblNzbDir;
        private System.Windows.Forms.Button nzbDirBrowse;
        private System.Windows.Forms.FolderBrowserDialog nzbDirDialog;
        private System.Windows.Forms.TextBox txtVideoExt;
        private System.Windows.Forms.Label lblVideoExt;
        private System.Windows.Forms.TextBox txtPriority;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox txtRssConfig;
        private System.Windows.Forms.Label lblRssConfig;
        private System.Windows.Forms.Button btnRssConfig;
        private System.Windows.Forms.TextBox txtIgnoreSeasons;
        private System.Windows.Forms.Label lblIgnoreSeasons;
        private System.Windows.Forms.CheckBox chkReplaceChars;
        private System.Windows.Forms.Label lblPriorityDesc;

    }
}

