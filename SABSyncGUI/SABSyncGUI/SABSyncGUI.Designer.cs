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
            this.txtSabInfoPort = new System.Windows.Forms.TextBox();
            this.lblSabInfoPort = new System.Windows.Forms.Label();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblSabInfoHost = new System.Windows.Forms.Label();
            this.txtSabInfoHost = new System.Windows.Forms.TextBox();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnTestSab = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(318, 467);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTvRoot
            // 
            this.txtTvRoot.BackColor = System.Drawing.SystemColors.Control;
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
            this.lblTvRoot.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblTvRoot.Click += new System.EventHandler(this.label1_Click);
            this.lblTvRoot.MouseEnter += new System.EventHandler(this.lblTvRoot_MouseEnter);
            // 
            // tvRootBrowse
            // 
            this.tvRootBrowse.Location = new System.Drawing.Point(374, 17);
            this.tvRootBrowse.Name = "tvRootBrowse";
            this.tvRootBrowse.Size = new System.Drawing.Size(24, 23);
            this.tvRootBrowse.TabIndex = 3;
            this.tvRootBrowse.Text = "...";
            this.tvRootBrowse.UseVisualStyleBackColor = true;
            this.tvRootBrowse.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.tvRootBrowse.Click += new System.EventHandler(this.tvRootBrowse_Click);
            this.tvRootBrowse.MouseEnter += new System.EventHandler(this.btnTvRootBrowse_MouseEnter);
            // 
            // tvRootDialog
            // 
            this.tvRootDialog.Description = "TV Root Folder";
            this.tvRootDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtTvTemplate
            // 
            this.txtTvTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.txtTvTemplate.Location = new System.Drawing.Point(118, 46);
            this.txtTvTemplate.Name = "txtTvTemplate";
            this.txtTvTemplate.Size = new System.Drawing.Size(250, 20);
            this.txtTvTemplate.TabIndex = 2;
            // 
            // lblTvTemplate
            // 
            this.lblTvTemplate.AutoSize = true;
            this.lblTvTemplate.Location = new System.Drawing.Point(44, 53);
            this.lblTvTemplate.Name = "lblTvTemplate";
            this.lblTvTemplate.Size = new System.Drawing.Size(71, 13);
            this.lblTvTemplate.TabIndex = 5;
            this.lblTvTemplate.Text = "TV Template:";
            this.lblTvTemplate.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblTvTemplate.Click += new System.EventHandler(this.lblTvTemplate_Click);
            this.lblTvTemplate.MouseEnter += new System.EventHandler(this.lblTvTemplate_MouseEnter);
            // 
            // txtTvDailyTemplate
            // 
            this.txtTvDailyTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.txtTvDailyTemplate.Location = new System.Drawing.Point(118, 72);
            this.txtTvDailyTemplate.Name = "txtTvDailyTemplate";
            this.txtTvDailyTemplate.Size = new System.Drawing.Size(250, 20);
            this.txtTvDailyTemplate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "TV Daily Template:";
            this.label1.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.label1.Click += new System.EventHandler(this.lblTvDailyTemplate_Click);
            this.label1.MouseEnter += new System.EventHandler(this.lblTvDailyTemplate_MouseEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSabInfoPort);
            this.groupBox1.Controls.Add(this.lblSabInfoPort);
            this.groupBox1.Controls.Add(this.lblApiKey);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtApiKey);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.lblSabInfoHost);
            this.groupBox1.Controls.Add(this.txtSabInfoHost);
            this.groupBox1.Location = new System.Drawing.Point(24, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 129);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SABnzbd Settings";
            this.groupBox1.Enter += new System.EventHandler(this.grpSabSettings_Enter);
            // 
            // txtSabInfoPort
            // 
            this.txtSabInfoPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtSabInfoPort.Location = new System.Drawing.Point(265, 19);
            this.txtSabInfoPort.MaxLength = 5;
            this.txtSabInfoPort.Name = "txtSabInfoPort";
            this.txtSabInfoPort.Size = new System.Drawing.Size(39, 20);
            this.txtSabInfoPort.TabIndex = 6;
            // 
            // lblSabInfoPort
            // 
            this.lblSabInfoPort.AutoSize = true;
            this.lblSabInfoPort.Location = new System.Drawing.Point(230, 26);
            this.lblSabInfoPort.Name = "lblSabInfoPort";
            this.lblSabInfoPort.Size = new System.Drawing.Size(29, 13);
            this.lblSabInfoPort.TabIndex = 16;
            this.lblSabInfoPort.Text = "Port:";
            this.lblSabInfoPort.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblSabInfoPort.MouseEnter += new System.EventHandler(this.lblSabInfoPort_MouseEnter);
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(50, 104);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 15;
            this.lblApiKey.Text = "API Key:";
            this.lblApiKey.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblApiKey.MouseEnter += new System.EventHandler(this.lblApiKey_MouseEnter);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(42, 78);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Password:";
            this.lblPassword.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblPassword.MouseEnter += new System.EventHandler(this.lblPassword_MouseEnter);
            // 
            // txtApiKey
            // 
            this.txtApiKey.BackColor = System.Drawing.SystemColors.Control;
            this.txtApiKey.Location = new System.Drawing.Point(104, 97);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(200, 20);
            this.txtApiKey.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.Location = new System.Drawing.Point(104, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 52);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 11;
            this.lblUsername.Text = "Username:";
            this.lblUsername.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblUsername.MouseEnter += new System.EventHandler(this.lblUsername_MouseEnter);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Control;
            this.txtUsername.Location = new System.Drawing.Point(104, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 7;
            // 
            // lblSabInfoHost
            // 
            this.lblSabInfoHost.AutoSize = true;
            this.lblSabInfoHost.Location = new System.Drawing.Point(40, 26);
            this.lblSabInfoHost.Name = "lblSabInfoHost";
            this.lblSabInfoHost.Size = new System.Drawing.Size(58, 13);
            this.lblSabInfoHost.TabIndex = 9;
            this.lblSabInfoHost.Text = "Hostname:";
            this.lblSabInfoHost.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblSabInfoHost.MouseEnter += new System.EventHandler(this.lblSabInfoHostname_MouseEnter);
            // 
            // txtSabInfoHost
            // 
            this.txtSabInfoHost.BackColor = System.Drawing.SystemColors.Control;
            this.txtSabInfoHost.Location = new System.Drawing.Point(104, 19);
            this.txtSabInfoHost.Name = "txtSabInfoHost";
            this.txtSabInfoHost.Size = new System.Drawing.Size(120, 20);
            this.txtSabInfoHost.TabIndex = 5;
            // 
            // txtNzbDir
            // 
            this.txtNzbDir.BackColor = System.Drawing.SystemColors.Control;
            this.txtNzbDir.Location = new System.Drawing.Point(104, 286);
            this.txtNzbDir.Name = "txtNzbDir";
            this.txtNzbDir.Size = new System.Drawing.Size(250, 20);
            this.txtNzbDir.TabIndex = 10;
            // 
            // lblNzbDir
            // 
            this.lblNzbDir.AutoSize = true;
            this.lblNzbDir.Location = new System.Drawing.Point(21, 293);
            this.lblNzbDir.Name = "lblNzbDir";
            this.lblNzbDir.Size = new System.Drawing.Size(77, 13);
            this.lblNzbDir.TabIndex = 11;
            this.lblNzbDir.Text = "NZB Directory:";
            this.lblNzbDir.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblNzbDir.MouseEnter += new System.EventHandler(this.lblNzbDir_MouseEnter);
            // 
            // nzbDirBrowse
            // 
            this.nzbDirBrowse.Location = new System.Drawing.Point(360, 284);
            this.nzbDirBrowse.Name = "nzbDirBrowse";
            this.nzbDirBrowse.Size = new System.Drawing.Size(24, 23);
            this.nzbDirBrowse.TabIndex = 12;
            this.nzbDirBrowse.Text = "...";
            this.nzbDirBrowse.UseVisualStyleBackColor = true;
            this.nzbDirBrowse.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.nzbDirBrowse.Click += new System.EventHandler(this.nzbDirBrowse_Click);
            this.nzbDirBrowse.MouseEnter += new System.EventHandler(this.btnNzbDir_MouseEnter);
            // 
            // nzbDirDialog
            // 
            this.nzbDirDialog.Description = "NZB Import Folder";
            this.nzbDirDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtVideoExt
            // 
            this.txtVideoExt.BackColor = System.Drawing.SystemColors.Control;
            this.txtVideoExt.Location = new System.Drawing.Point(118, 98);
            this.txtVideoExt.Name = "txtVideoExt";
            this.txtVideoExt.Size = new System.Drawing.Size(250, 20);
            this.txtVideoExt.TabIndex = 4;
            // 
            // lblVideoExt
            // 
            this.lblVideoExt.AutoSize = true;
            this.lblVideoExt.Location = new System.Drawing.Point(21, 105);
            this.lblVideoExt.Name = "lblVideoExt";
            this.lblVideoExt.Size = new System.Drawing.Size(91, 13);
            this.lblVideoExt.TabIndex = 14;
            this.lblVideoExt.Text = "Video Extensions:";
            this.lblVideoExt.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblVideoExt.MouseEnter += new System.EventHandler(this.lblVideoExt_MouseEnter);
            // 
            // txtPriority
            // 
            this.txtPriority.BackColor = System.Drawing.SystemColors.Control;
            this.txtPriority.Location = new System.Drawing.Point(104, 312);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Size = new System.Drawing.Size(26, 20);
            this.txtPriority.TabIndex = 11;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(57, 319);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 16;
            this.lblPriority.Text = "Priority:";
            this.lblPriority.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblPriority.MouseEnter += new System.EventHandler(this.lblPriority_MouseEnter);
            // 
            // txtRssConfig
            // 
            this.txtRssConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtRssConfig.Location = new System.Drawing.Point(104, 338);
            this.txtRssConfig.Name = "txtRssConfig";
            this.txtRssConfig.Size = new System.Drawing.Size(250, 20);
            this.txtRssConfig.TabIndex = 12;
            // 
            // lblRssConfig
            // 
            this.lblRssConfig.AutoSize = true;
            this.lblRssConfig.Location = new System.Drawing.Point(14, 345);
            this.lblRssConfig.Name = "lblRssConfig";
            this.lblRssConfig.Size = new System.Drawing.Size(84, 13);
            this.lblRssConfig.TabIndex = 18;
            this.lblRssConfig.Text = "RSS Config File:";
            this.lblRssConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblRssConfig.MouseEnter += new System.EventHandler(this.lblRssConfig_MouseEnter);
            // 
            // btnRssConfig
            // 
            this.btnRssConfig.Location = new System.Drawing.Point(360, 336);
            this.btnRssConfig.Name = "btnRssConfig";
            this.btnRssConfig.Size = new System.Drawing.Size(24, 23);
            this.btnRssConfig.TabIndex = 19;
            this.btnRssConfig.Text = "...";
            this.btnRssConfig.UseVisualStyleBackColor = true;
            this.btnRssConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.btnRssConfig.Click += new System.EventHandler(this.btnRssConfig_Click);
            this.btnRssConfig.MouseEnter += new System.EventHandler(this.btnRssConfig_MouseEnter);
            // 
            // txtIgnoreSeasons
            // 
            this.txtIgnoreSeasons.AcceptsReturn = true;
            this.txtIgnoreSeasons.BackColor = System.Drawing.SystemColors.Control;
            this.txtIgnoreSeasons.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtIgnoreSeasons.Location = new System.Drawing.Point(104, 364);
            this.txtIgnoreSeasons.Multiline = true;
            this.txtIgnoreSeasons.Name = "txtIgnoreSeasons";
            this.txtIgnoreSeasons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIgnoreSeasons.Size = new System.Drawing.Size(250, 72);
            this.txtIgnoreSeasons.TabIndex = 13;
            // 
            // lblIgnoreSeasons
            // 
            this.lblIgnoreSeasons.AutoSize = true;
            this.lblIgnoreSeasons.Location = new System.Drawing.Point(2, 367);
            this.lblIgnoreSeasons.Name = "lblIgnoreSeasons";
            this.lblIgnoreSeasons.Size = new System.Drawing.Size(96, 13);
            this.lblIgnoreSeasons.TabIndex = 21;
            this.lblIgnoreSeasons.Text = "Seasons to Ignore:";
            this.lblIgnoreSeasons.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.lblIgnoreSeasons.MouseEnter += new System.EventHandler(this.lblIgnoreSeasons_MouseEnter);
            // 
            // chkReplaceChars
            // 
            this.chkReplaceChars.AutoSize = true;
            this.chkReplaceChars.Location = new System.Drawing.Point(104, 443);
            this.chkReplaceChars.Name = "chkReplaceChars";
            this.chkReplaceChars.Size = new System.Drawing.Size(120, 17);
            this.chkReplaceChars.TabIndex = 14;
            this.chkReplaceChars.Text = "Replace Characters";
            this.chkReplaceChars.UseVisualStyleBackColor = true;
            this.chkReplaceChars.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.chkReplaceChars.MouseEnter += new System.EventHandler(this.checkBoxSabReplace_MouseEnter);
            // 
            // lblPriorityDesc
            // 
            this.lblPriorityDesc.AutoSize = true;
            this.lblPriorityDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriorityDesc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPriorityDesc.Location = new System.Drawing.Point(137, 318);
            this.lblPriorityDesc.Name = "lblPriorityDesc";
            this.lblPriorityDesc.Size = new System.Drawing.Size(149, 13);
            this.lblPriorityDesc.TabIndex = 23;
            this.lblPriorityDesc.Text = "Low=-1 Normal=0 High=1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(404, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(257, 17);
            this.statusStripLabel.Text = "SABSync GUI - Mouse over labels for more info!";
            // 
            // btnTestSab
            // 
            this.btnTestSab.Location = new System.Drawing.Point(12, 467);
            this.btnTestSab.Name = "btnTestSab";
            this.btnTestSab.Size = new System.Drawing.Size(64, 23);
            this.btnTestSab.TabIndex = 25;
            this.btnTestSab.Text = "Test SAB";
            this.btnTestSab.UseVisualStyleBackColor = true;
            this.btnTestSab.MouseLeave += new System.EventHandler(this.statusBarClear);
            this.btnTestSab.Click += new System.EventHandler(this.btnTestSab_Click);
            this.btnTestSab.MouseEnter += new System.EventHandler(this.btnTestSab_MouseEnter);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(222, 467);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 23);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // SABSyncGUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(404, 522);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnTestSab);
            this.Controls.Add(this.statusStrip1);
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
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTvDailyTemplate);
            this.Controls.Add(this.lblTvTemplate);
            this.Controls.Add(this.txtTvTemplate);
            this.Controls.Add(this.tvRootBrowse);
            this.Controls.Add(this.lblTvRoot);
            this.Controls.Add(this.txtTvRoot);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SABSyncGUI";
            this.Text = "SABSync GUI";
            this.Load += new System.EventHandler(this.SABSyncGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Label lblSabInfoHost;
        private System.Windows.Forms.TextBox txtSabInfoHost;
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.TextBox txtSabInfoPort;
        private System.Windows.Forms.Label lblSabInfoPort;
        private System.Windows.Forms.Button btnTestSab;
        private System.Windows.Forms.Button btnReset;

    }
}

