using System.Reflection;

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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnTestSab = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAliasConfig = new System.Windows.Forms.Button();
            this.lblAliasConfig = new System.Windows.Forms.Label();
            this.txtAliasConfig = new System.Windows.Forms.TextBox();
            this.btnQualityConfig = new System.Windows.Forms.Button();
            this.lblQualityConfig = new System.Windows.Forms.Label();
            this.txtQualityConfig = new System.Windows.Forms.TextBox();
            this.chkVerboseLogging = new System.Windows.Forms.CheckBox();
            this.chkDownloadPropers = new System.Windows.Forms.CheckBox();
            this.lblDownloadQuality = new System.Windows.Forms.Label();
            this.txtDownloadQuality = new System.Windows.Forms.TextBox();
            this.tabControlGUI = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.btn120Days = new System.Windows.Forms.Button();
            this.btn60Days = new System.Windows.Forms.Button();
            this.btn30Days = new System.Windows.Forms.Button();
            this.txtDeleteLogs = new System.Windows.Forms.TextBox();
            this.lblDeleteLogs = new System.Windows.Forms.Label();
            this.btnPriorityHigh = new System.Windows.Forms.Button();
            this.btnPriorityNormal = new System.Windows.Forms.Button();
            this.btnPriorityLow = new System.Windows.Forms.Button();
            this.btnTvRootClear = new System.Windows.Forms.Button();
            this.btnClearDQ = new System.Windows.Forms.Button();
            this.btnHd = new System.Windows.Forms.Button();
            this.btnSd = new System.Windows.Forms.Button();
            this.tabConfigs = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnResetConfig = new System.Windows.Forms.Button();
            this.txtQualityDotConfig = new System.Windows.Forms.TextBox();
            this.lblQualityDotConfig = new System.Windows.Forms.Label();
            this.txtAliasDotConfig = new System.Windows.Forms.TextBox();
            this.lblAliasDotConfig = new System.Windows.Forms.Label();
            this.txtRssDotConfig = new System.Windows.Forms.TextBox();
            this.lblRssDotConfig = new System.Windows.Forms.Label();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this.btnRunSabSync = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnCreateTask = new System.Windows.Forms.Button();
            this.txtWinPassword = new System.Windows.Forms.TextBox();
            this.lblRepeatTask = new System.Windows.Forms.Label();
            this.txtWinUsername = new System.Windows.Forms.TextBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblWinPassword = new System.Windows.Forms.Label();
            this.chkVisbile = new System.Windows.Forms.CheckBox();
            this.lblWinUsername = new System.Windows.Forms.Label();
            this.btnTestSabSync = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControlGUI.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabConfigs.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            this.groupBoxTest.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(357, 560);
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
            this.txtTvRoot.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvRoot.Location = new System.Drawing.Point(115, 7);
            this.txtTvRoot.Name = "txtTvRoot";
            this.txtTvRoot.Size = new System.Drawing.Size(271, 22);
            this.txtTvRoot.TabIndex = 1;
            // 
            // lblTvRoot
            // 
            this.lblTvRoot.AutoSize = true;
            this.lblTvRoot.Location = new System.Drawing.Point(62, 14);
            this.lblTvRoot.Name = "lblTvRoot";
            this.lblTvRoot.Size = new System.Drawing.Size(50, 13);
            this.lblTvRoot.TabIndex = 2;
            this.lblTvRoot.Text = "TV Root:";
            this.lblTvRoot.MouseEnter += new System.EventHandler(this.lblTvRoot_MouseEnter);
            this.lblTvRoot.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // tvRootBrowse
            // 
            this.tvRootBrowse.Location = new System.Drawing.Point(452, 6);
            this.tvRootBrowse.Name = "tvRootBrowse";
            this.tvRootBrowse.Size = new System.Drawing.Size(24, 23);
            this.tvRootBrowse.TabIndex = 3;
            this.tvRootBrowse.Text = "...";
            this.tvRootBrowse.UseVisualStyleBackColor = true;
            this.tvRootBrowse.Click += new System.EventHandler(this.tvRootBrowse_Click);
            this.tvRootBrowse.MouseEnter += new System.EventHandler(this.btnTvRootBrowse_MouseEnter);
            this.tvRootBrowse.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // tvRootDialog
            // 
            this.tvRootDialog.Description = "TV Root Folder";
            this.tvRootDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtTvTemplate
            // 
            this.txtTvTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.txtTvTemplate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvTemplate.Location = new System.Drawing.Point(115, 34);
            this.txtTvTemplate.Name = "txtTvTemplate";
            this.txtTvTemplate.Size = new System.Drawing.Size(331, 22);
            this.txtTvTemplate.TabIndex = 2;
            // 
            // lblTvTemplate
            // 
            this.lblTvTemplate.AutoSize = true;
            this.lblTvTemplate.Location = new System.Drawing.Point(41, 41);
            this.lblTvTemplate.Name = "lblTvTemplate";
            this.lblTvTemplate.Size = new System.Drawing.Size(71, 13);
            this.lblTvTemplate.TabIndex = 5;
            this.lblTvTemplate.Text = "TV Template:";
            this.lblTvTemplate.MouseEnter += new System.EventHandler(this.lblTvTemplate_MouseEnter);
            this.lblTvTemplate.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtTvDailyTemplate
            // 
            this.txtTvDailyTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.txtTvDailyTemplate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvDailyTemplate.Location = new System.Drawing.Point(115, 60);
            this.txtTvDailyTemplate.Name = "txtTvDailyTemplate";
            this.txtTvDailyTemplate.Size = new System.Drawing.Size(331, 22);
            this.txtTvDailyTemplate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "TV Daily Template:";
            this.label1.MouseEnter += new System.EventHandler(this.lblTvDailyTemplate_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.statusBarClear);
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
            this.groupBox1.Location = new System.Drawing.Point(21, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 129);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SABnzbd Settings";
            // 
            // txtSabInfoPort
            // 
            this.txtSabInfoPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtSabInfoPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSabInfoPort.Location = new System.Drawing.Point(359, 19);
            this.txtSabInfoPort.MaxLength = 5;
            this.txtSabInfoPort.Name = "txtSabInfoPort";
            this.txtSabInfoPort.Size = new System.Drawing.Size(51, 22);
            this.txtSabInfoPort.TabIndex = 6;
            // 
            // lblSabInfoPort
            // 
            this.lblSabInfoPort.AutoSize = true;
            this.lblSabInfoPort.Location = new System.Drawing.Point(324, 26);
            this.lblSabInfoPort.Name = "lblSabInfoPort";
            this.lblSabInfoPort.Size = new System.Drawing.Size(29, 13);
            this.lblSabInfoPort.TabIndex = 16;
            this.lblSabInfoPort.Text = "Port:";
            this.lblSabInfoPort.MouseEnter += new System.EventHandler(this.lblSabInfoPort_MouseEnter);
            this.lblSabInfoPort.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(50, 104);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 15;
            this.lblApiKey.Text = "API Key:";
            this.lblApiKey.MouseEnter += new System.EventHandler(this.lblApiKey_MouseEnter);
            this.lblApiKey.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(42, 78);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Password:";
            this.lblPassword.MouseEnter += new System.EventHandler(this.lblPassword_MouseEnter);
            this.lblPassword.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtApiKey
            // 
            this.txtApiKey.BackColor = System.Drawing.SystemColors.Control;
            this.txtApiKey.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApiKey.Location = new System.Drawing.Point(104, 97);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(306, 22);
            this.txtApiKey.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(104, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(306, 22);
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
            this.lblUsername.MouseEnter += new System.EventHandler(this.lblUsername_MouseEnter);
            this.lblUsername.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Control;
            this.txtUsername.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(104, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(306, 22);
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
            this.lblSabInfoHost.MouseEnter += new System.EventHandler(this.lblSabInfoHostname_MouseEnter);
            this.lblSabInfoHost.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtSabInfoHost
            // 
            this.txtSabInfoHost.BackColor = System.Drawing.SystemColors.Control;
            this.txtSabInfoHost.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSabInfoHost.Location = new System.Drawing.Point(104, 19);
            this.txtSabInfoHost.Name = "txtSabInfoHost";
            this.txtSabInfoHost.Size = new System.Drawing.Size(214, 22);
            this.txtSabInfoHost.TabIndex = 5;
            // 
            // txtNzbDir
            // 
            this.txtNzbDir.BackColor = System.Drawing.SystemColors.Control;
            this.txtNzbDir.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNzbDir.Location = new System.Drawing.Point(115, 274);
            this.txtNzbDir.Name = "txtNzbDir";
            this.txtNzbDir.Size = new System.Drawing.Size(316, 22);
            this.txtNzbDir.TabIndex = 10;
            // 
            // lblNzbDir
            // 
            this.lblNzbDir.AutoSize = true;
            this.lblNzbDir.Location = new System.Drawing.Point(32, 281);
            this.lblNzbDir.Name = "lblNzbDir";
            this.lblNzbDir.Size = new System.Drawing.Size(77, 13);
            this.lblNzbDir.TabIndex = 11;
            this.lblNzbDir.Text = "NZB Directory:";
            this.lblNzbDir.MouseEnter += new System.EventHandler(this.lblNzbDir_MouseEnter);
            this.lblNzbDir.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // nzbDirBrowse
            // 
            this.nzbDirBrowse.Location = new System.Drawing.Point(437, 274);
            this.nzbDirBrowse.Name = "nzbDirBrowse";
            this.nzbDirBrowse.Size = new System.Drawing.Size(24, 23);
            this.nzbDirBrowse.TabIndex = 12;
            this.nzbDirBrowse.Text = "...";
            this.nzbDirBrowse.UseVisualStyleBackColor = true;
            this.nzbDirBrowse.Click += new System.EventHandler(this.nzbDirBrowse_Click);
            this.nzbDirBrowse.MouseEnter += new System.EventHandler(this.btnNzbDir_MouseEnter);
            this.nzbDirBrowse.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // nzbDirDialog
            // 
            this.nzbDirDialog.Description = "NZB Import Folder";
            this.nzbDirDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtVideoExt
            // 
            this.txtVideoExt.BackColor = System.Drawing.SystemColors.Control;
            this.txtVideoExt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideoExt.Location = new System.Drawing.Point(115, 86);
            this.txtVideoExt.Name = "txtVideoExt";
            this.txtVideoExt.Size = new System.Drawing.Size(331, 22);
            this.txtVideoExt.TabIndex = 4;
            // 
            // lblVideoExt
            // 
            this.lblVideoExt.AutoSize = true;
            this.lblVideoExt.Location = new System.Drawing.Point(18, 93);
            this.lblVideoExt.Name = "lblVideoExt";
            this.lblVideoExt.Size = new System.Drawing.Size(91, 13);
            this.lblVideoExt.TabIndex = 14;
            this.lblVideoExt.Text = "Video Extensions:";
            this.lblVideoExt.MouseEnter += new System.EventHandler(this.lblVideoExt_MouseEnter);
            this.lblVideoExt.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtPriority
            // 
            this.txtPriority.BackColor = System.Drawing.SystemColors.Control;
            this.txtPriority.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPriority.Location = new System.Drawing.Point(115, 300);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Size = new System.Drawing.Size(77, 22);
            this.txtPriority.TabIndex = 11;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(68, 307);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 16;
            this.lblPriority.Text = "Priority:";
            this.lblPriority.MouseEnter += new System.EventHandler(this.lblPriority_MouseEnter);
            this.lblPriority.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtRssConfig
            // 
            this.txtRssConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtRssConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRssConfig.Location = new System.Drawing.Point(115, 326);
            this.txtRssConfig.Name = "txtRssConfig";
            this.txtRssConfig.Size = new System.Drawing.Size(316, 22);
            this.txtRssConfig.TabIndex = 12;
            // 
            // lblRssConfig
            // 
            this.lblRssConfig.AutoSize = true;
            this.lblRssConfig.Location = new System.Drawing.Point(25, 333);
            this.lblRssConfig.Name = "lblRssConfig";
            this.lblRssConfig.Size = new System.Drawing.Size(84, 13);
            this.lblRssConfig.TabIndex = 18;
            this.lblRssConfig.Text = "RSS Config File:";
            this.lblRssConfig.MouseEnter += new System.EventHandler(this.lblRssConfig_MouseEnter);
            this.lblRssConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // btnRssConfig
            // 
            this.btnRssConfig.Location = new System.Drawing.Point(437, 326);
            this.btnRssConfig.Name = "btnRssConfig";
            this.btnRssConfig.Size = new System.Drawing.Size(24, 23);
            this.btnRssConfig.TabIndex = 19;
            this.btnRssConfig.Text = "...";
            this.btnRssConfig.UseVisualStyleBackColor = true;
            this.btnRssConfig.Click += new System.EventHandler(this.btnRssConfig_Click);
            this.btnRssConfig.MouseEnter += new System.EventHandler(this.btnRssConfig_MouseEnter);
            this.btnRssConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtIgnoreSeasons
            // 
            this.txtIgnoreSeasons.AcceptsReturn = true;
            this.txtIgnoreSeasons.BackColor = System.Drawing.SystemColors.Control;
            this.txtIgnoreSeasons.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtIgnoreSeasons.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIgnoreSeasons.Location = new System.Drawing.Point(115, 407);
            this.txtIgnoreSeasons.Multiline = true;
            this.txtIgnoreSeasons.Name = "txtIgnoreSeasons";
            this.txtIgnoreSeasons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIgnoreSeasons.Size = new System.Drawing.Size(316, 72);
            this.txtIgnoreSeasons.TabIndex = 13;
            // 
            // lblIgnoreSeasons
            // 
            this.lblIgnoreSeasons.AutoSize = true;
            this.lblIgnoreSeasons.Location = new System.Drawing.Point(13, 410);
            this.lblIgnoreSeasons.Name = "lblIgnoreSeasons";
            this.lblIgnoreSeasons.Size = new System.Drawing.Size(96, 13);
            this.lblIgnoreSeasons.TabIndex = 21;
            this.lblIgnoreSeasons.Text = "Seasons to Ignore:";
            this.lblIgnoreSeasons.MouseEnter += new System.EventHandler(this.lblIgnoreSeasons_MouseEnter);
            this.lblIgnoreSeasons.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // chkReplaceChars
            // 
            this.chkReplaceChars.AutoSize = true;
            this.chkReplaceChars.Location = new System.Drawing.Point(115, 514);
            this.chkReplaceChars.Name = "chkReplaceChars";
            this.chkReplaceChars.Size = new System.Drawing.Size(120, 17);
            this.chkReplaceChars.TabIndex = 14;
            this.chkReplaceChars.Text = "Replace Characters";
            this.chkReplaceChars.UseVisualStyleBackColor = true;
            this.chkReplaceChars.MouseEnter += new System.EventHandler(this.chkReplaceChars_MouseEnter);
            this.chkReplaceChars.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(494, 22);
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
            this.btnTestSab.Location = new System.Drawing.Point(45, 563);
            this.btnTestSab.Name = "btnTestSab";
            this.btnTestSab.Size = new System.Drawing.Size(64, 23);
            this.btnTestSab.TabIndex = 25;
            this.btnTestSab.Text = "Test SAB";
            this.btnTestSab.UseVisualStyleBackColor = true;
            this.btnTestSab.Click += new System.EventHandler(this.btnTestSab_Click);
            this.btnTestSab.MouseEnter += new System.EventHandler(this.btnTestSab_MouseEnter);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(250, 560);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 23);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.MouseEnter += new System.EventHandler(this.btnReset_MouseEnter);
            this.btnReset.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // btnAliasConfig
            // 
            this.btnAliasConfig.Location = new System.Drawing.Point(437, 352);
            this.btnAliasConfig.Name = "btnAliasConfig";
            this.btnAliasConfig.Size = new System.Drawing.Size(24, 23);
            this.btnAliasConfig.TabIndex = 29;
            this.btnAliasConfig.Text = "...";
            this.btnAliasConfig.UseVisualStyleBackColor = true;
            this.btnAliasConfig.Click += new System.EventHandler(this.btnAliasConfig_Click);
            this.btnAliasConfig.MouseEnter += new System.EventHandler(this.btnAliasConfig_MouseEnter);
            this.btnAliasConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblAliasConfig
            // 
            this.lblAliasConfig.AutoSize = true;
            this.lblAliasConfig.Location = new System.Drawing.Point(25, 360);
            this.lblAliasConfig.Name = "lblAliasConfig";
            this.lblAliasConfig.Size = new System.Drawing.Size(84, 13);
            this.lblAliasConfig.TabIndex = 28;
            this.lblAliasConfig.Text = "Alias Config File:";
            this.lblAliasConfig.MouseEnter += new System.EventHandler(this.lblAliasConfig_MouseEnter);
            this.lblAliasConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtAliasConfig
            // 
            this.txtAliasConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtAliasConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasConfig.Location = new System.Drawing.Point(116, 352);
            this.txtAliasConfig.Name = "txtAliasConfig";
            this.txtAliasConfig.Size = new System.Drawing.Size(316, 22);
            this.txtAliasConfig.TabIndex = 27;
            // 
            // btnQualityConfig
            // 
            this.btnQualityConfig.Location = new System.Drawing.Point(437, 378);
            this.btnQualityConfig.Name = "btnQualityConfig";
            this.btnQualityConfig.Size = new System.Drawing.Size(24, 23);
            this.btnQualityConfig.TabIndex = 32;
            this.btnQualityConfig.Text = "...";
            this.btnQualityConfig.UseVisualStyleBackColor = true;
            this.btnQualityConfig.Click += new System.EventHandler(this.btnQualityConfig_Click);
            this.btnQualityConfig.MouseEnter += new System.EventHandler(this.btnQualityConfig_MouseEnter);
            this.btnQualityConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblQualityConfig
            // 
            this.lblQualityConfig.AutoSize = true;
            this.lblQualityConfig.Location = new System.Drawing.Point(15, 385);
            this.lblQualityConfig.Name = "lblQualityConfig";
            this.lblQualityConfig.Size = new System.Drawing.Size(94, 13);
            this.lblQualityConfig.TabIndex = 31;
            this.lblQualityConfig.Text = "Quality Config File:";
            this.lblQualityConfig.MouseEnter += new System.EventHandler(this.lblQualityConfig_MouseEnter);
            this.lblQualityConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtQualityConfig
            // 
            this.txtQualityConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtQualityConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQualityConfig.Location = new System.Drawing.Point(116, 378);
            this.txtQualityConfig.Name = "txtQualityConfig";
            this.txtQualityConfig.Size = new System.Drawing.Size(316, 22);
            this.txtQualityConfig.TabIndex = 30;
            // 
            // chkVerboseLogging
            // 
            this.chkVerboseLogging.AutoSize = true;
            this.chkVerboseLogging.Location = new System.Drawing.Point(115, 537);
            this.chkVerboseLogging.Name = "chkVerboseLogging";
            this.chkVerboseLogging.Size = new System.Drawing.Size(106, 17);
            this.chkVerboseLogging.TabIndex = 33;
            this.chkVerboseLogging.Text = "Verbose Logging";
            this.chkVerboseLogging.UseVisualStyleBackColor = true;
            this.chkVerboseLogging.MouseEnter += new System.EventHandler(this.chkVerboseLogging_MouseEnter);
            this.chkVerboseLogging.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // chkDownloadPropers
            // 
            this.chkDownloadPropers.AutoSize = true;
            this.chkDownloadPropers.Location = new System.Drawing.Point(250, 514);
            this.chkDownloadPropers.Name = "chkDownloadPropers";
            this.chkDownloadPropers.Size = new System.Drawing.Size(127, 17);
            this.chkDownloadPropers.TabIndex = 34;
            this.chkDownloadPropers.Text = "Download PROPERs";
            this.chkDownloadPropers.UseVisualStyleBackColor = true;
            this.chkDownloadPropers.MouseEnter += new System.EventHandler(this.chkDownloadPropers_MouseEnter);
            this.chkDownloadPropers.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblDownloadQuality
            // 
            this.lblDownloadQuality.AutoSize = true;
            this.lblDownloadQuality.Location = new System.Drawing.Point(16, 491);
            this.lblDownloadQuality.Name = "lblDownloadQuality";
            this.lblDownloadQuality.Size = new System.Drawing.Size(93, 13);
            this.lblDownloadQuality.TabIndex = 36;
            this.lblDownloadQuality.Text = "Download Quality:";
            this.lblDownloadQuality.MouseEnter += new System.EventHandler(this.lblDownloadQuality_MouseEnter);
            this.lblDownloadQuality.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtDownloadQuality
            // 
            this.txtDownloadQuality.BackColor = System.Drawing.SystemColors.Control;
            this.txtDownloadQuality.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownloadQuality.Location = new System.Drawing.Point(115, 484);
            this.txtDownloadQuality.Name = "txtDownloadQuality";
            this.txtDownloadQuality.Size = new System.Drawing.Size(106, 22);
            this.txtDownloadQuality.TabIndex = 35;
            // 
            // tabControlGUI
            // 
            this.tabControlGUI.Controls.Add(this.tabGeneral);
            this.tabControlGUI.Controls.Add(this.tabConfigs);
            this.tabControlGUI.Controls.Add(this.tabSchedule);
            this.tabControlGUI.Location = new System.Drawing.Point(2, 2);
            this.tabControlGUI.Name = "tabControlGUI";
            this.tabControlGUI.SelectedIndex = 0;
            this.tabControlGUI.Size = new System.Drawing.Size(492, 615);
            this.tabControlGUI.TabIndex = 37;
            this.tabControlGUI.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.btn120Days);
            this.tabGeneral.Controls.Add(this.btn60Days);
            this.tabGeneral.Controls.Add(this.btn30Days);
            this.tabGeneral.Controls.Add(this.txtDeleteLogs);
            this.tabGeneral.Controls.Add(this.lblDeleteLogs);
            this.tabGeneral.Controls.Add(this.btnPriorityHigh);
            this.tabGeneral.Controls.Add(this.btnPriorityNormal);
            this.tabGeneral.Controls.Add(this.btnPriorityLow);
            this.tabGeneral.Controls.Add(this.btnTvRootClear);
            this.tabGeneral.Controls.Add(this.btnClearDQ);
            this.tabGeneral.Controls.Add(this.btnHd);
            this.tabGeneral.Controls.Add(this.btnSd);
            this.tabGeneral.Controls.Add(this.txtTvTemplate);
            this.tabGeneral.Controls.Add(this.lblDownloadQuality);
            this.tabGeneral.Controls.Add(this.saveButton);
            this.tabGeneral.Controls.Add(this.txtDownloadQuality);
            this.tabGeneral.Controls.Add(this.txtTvRoot);
            this.tabGeneral.Controls.Add(this.chkDownloadPropers);
            this.tabGeneral.Controls.Add(this.lblTvRoot);
            this.tabGeneral.Controls.Add(this.chkVerboseLogging);
            this.tabGeneral.Controls.Add(this.tvRootBrowse);
            this.tabGeneral.Controls.Add(this.btnQualityConfig);
            this.tabGeneral.Controls.Add(this.lblTvTemplate);
            this.tabGeneral.Controls.Add(this.lblQualityConfig);
            this.tabGeneral.Controls.Add(this.txtTvDailyTemplate);
            this.tabGeneral.Controls.Add(this.txtQualityConfig);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.btnAliasConfig);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.lblAliasConfig);
            this.tabGeneral.Controls.Add(this.txtNzbDir);
            this.tabGeneral.Controls.Add(this.txtAliasConfig);
            this.tabGeneral.Controls.Add(this.lblNzbDir);
            this.tabGeneral.Controls.Add(this.btnReset);
            this.tabGeneral.Controls.Add(this.nzbDirBrowse);
            this.tabGeneral.Controls.Add(this.btnTestSab);
            this.tabGeneral.Controls.Add(this.txtVideoExt);
            this.tabGeneral.Controls.Add(this.lblVideoExt);
            this.tabGeneral.Controls.Add(this.txtPriority);
            this.tabGeneral.Controls.Add(this.chkReplaceChars);
            this.tabGeneral.Controls.Add(this.lblPriority);
            this.tabGeneral.Controls.Add(this.lblIgnoreSeasons);
            this.tabGeneral.Controls.Add(this.txtRssConfig);
            this.tabGeneral.Controls.Add(this.txtIgnoreSeasons);
            this.tabGeneral.Controls.Add(this.lblRssConfig);
            this.tabGeneral.Controls.Add(this.btnRssConfig);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(484, 589);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General Config";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // btn120Days
            // 
            this.btn120Days.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn120Days.Location = new System.Drawing.Point(403, 533);
            this.btn120Days.Margin = new System.Windows.Forms.Padding(0);
            this.btn120Days.Name = "btn120Days";
            this.btn120Days.Size = new System.Drawing.Size(29, 20);
            this.btn120Days.TabIndex = 48;
            this.btn120Days.Text = "120";
            this.btn120Days.UseVisualStyleBackColor = true;
            this.btn120Days.Click += new System.EventHandler(this.btn120Days_Click);
            // 
            // btn60Days
            // 
            this.btn60Days.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn60Days.Location = new System.Drawing.Point(378, 533);
            this.btn60Days.Margin = new System.Windows.Forms.Padding(0);
            this.btn60Days.Name = "btn60Days";
            this.btn60Days.Size = new System.Drawing.Size(23, 20);
            this.btn60Days.TabIndex = 47;
            this.btn60Days.Text = "60";
            this.btn60Days.UseVisualStyleBackColor = true;
            this.btn60Days.Click += new System.EventHandler(this.btn60Days_Click);
            // 
            // btn30Days
            // 
            this.btn30Days.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn30Days.Location = new System.Drawing.Point(353, 533);
            this.btn30Days.Margin = new System.Windows.Forms.Padding(0);
            this.btn30Days.Name = "btn30Days";
            this.btn30Days.Size = new System.Drawing.Size(23, 20);
            this.btn30Days.TabIndex = 46;
            this.btn30Days.Text = "30";
            this.btn30Days.UseVisualStyleBackColor = true;
            this.btn30Days.Click += new System.EventHandler(this.btn30Days_Click);
            // 
            // txtDeleteLogs
            // 
            this.txtDeleteLogs.BackColor = System.Drawing.SystemColors.Control;
            this.txtDeleteLogs.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeleteLogs.Location = new System.Drawing.Point(318, 532);
            this.txtDeleteLogs.MaxLength = 4;
            this.txtDeleteLogs.Name = "txtDeleteLogs";
            this.txtDeleteLogs.Size = new System.Drawing.Size(30, 22);
            this.txtDeleteLogs.TabIndex = 45;
            // 
            // lblDeleteLogs
            // 
            this.lblDeleteLogs.AutoSize = true;
            this.lblDeleteLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteLogs.Location = new System.Drawing.Point(247, 538);
            this.lblDeleteLogs.Name = "lblDeleteLogs";
            this.lblDeleteLogs.Size = new System.Drawing.Size(67, 13);
            this.lblDeleteLogs.TabIndex = 44;
            this.lblDeleteLogs.Text = "Delete Logs:";
            this.lblDeleteLogs.MouseEnter += new System.EventHandler(this.lblDeleteLogs_MouseEnter);
            this.lblDeleteLogs.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // btnPriorityHigh
            // 
            this.btnPriorityHigh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriorityHigh.Location = new System.Drawing.Point(324, 301);
            this.btnPriorityHigh.Name = "btnPriorityHigh";
            this.btnPriorityHigh.Size = new System.Drawing.Size(52, 20);
            this.btnPriorityHigh.TabIndex = 43;
            this.btnPriorityHigh.Text = "High";
            this.btnPriorityHigh.UseVisualStyleBackColor = true;
            this.btnPriorityHigh.Click += new System.EventHandler(this.btnPriorityHigh_Click);
            // 
            // btnPriorityNormal
            // 
            this.btnPriorityNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriorityNormal.Location = new System.Drawing.Point(263, 301);
            this.btnPriorityNormal.Name = "btnPriorityNormal";
            this.btnPriorityNormal.Size = new System.Drawing.Size(52, 20);
            this.btnPriorityNormal.TabIndex = 42;
            this.btnPriorityNormal.Text = "Normal";
            this.btnPriorityNormal.UseVisualStyleBackColor = true;
            this.btnPriorityNormal.Click += new System.EventHandler(this.btnPriorityNormal_Click);
            // 
            // btnPriorityLow
            // 
            this.btnPriorityLow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriorityLow.Location = new System.Drawing.Point(202, 301);
            this.btnPriorityLow.Name = "btnPriorityLow";
            this.btnPriorityLow.Size = new System.Drawing.Size(52, 20);
            this.btnPriorityLow.TabIndex = 41;
            this.btnPriorityLow.Text = "Low";
            this.btnPriorityLow.UseVisualStyleBackColor = true;
            this.btnPriorityLow.Click += new System.EventHandler(this.btnPriorityLow_Click);
            // 
            // btnTvRootClear
            // 
            this.btnTvRootClear.Location = new System.Drawing.Point(392, 6);
            this.btnTvRootClear.Name = "btnTvRootClear";
            this.btnTvRootClear.Size = new System.Drawing.Size(54, 23);
            this.btnTvRootClear.TabIndex = 40;
            this.btnTvRootClear.Text = "Clear";
            this.btnTvRootClear.UseVisualStyleBackColor = true;
            this.btnTvRootClear.Click += new System.EventHandler(this.btnTvRootClear_Click);
            // 
            // btnClearDQ
            // 
            this.btnClearDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDQ.Location = new System.Drawing.Point(311, 485);
            this.btnClearDQ.Name = "btnClearDQ";
            this.btnClearDQ.Size = new System.Drawing.Size(120, 20);
            this.btnClearDQ.TabIndex = 39;
            this.btnClearDQ.Text = "Clear Quality";
            this.btnClearDQ.UseVisualStyleBackColor = true;
            this.btnClearDQ.Click += new System.EventHandler(this.btnClearDQ_Click);
            // 
            // btnHd
            // 
            this.btnHd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHd.Location = new System.Drawing.Point(269, 486);
            this.btnHd.Name = "btnHd";
            this.btnHd.Size = new System.Drawing.Size(31, 20);
            this.btnHd.TabIndex = 38;
            this.btnHd.Text = "HD";
            this.btnHd.UseVisualStyleBackColor = true;
            this.btnHd.Click += new System.EventHandler(this.btnHd_Click);
            // 
            // btnSd
            // 
            this.btnSd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSd.Location = new System.Drawing.Point(231, 486);
            this.btnSd.Margin = new System.Windows.Forms.Padding(1);
            this.btnSd.Name = "btnSd";
            this.btnSd.Size = new System.Drawing.Size(30, 20);
            this.btnSd.TabIndex = 37;
            this.btnSd.Text = "SD";
            this.btnSd.UseVisualStyleBackColor = true;
            this.btnSd.Click += new System.EventHandler(this.btnSd_Click);
            // 
            // tabConfigs
            // 
            this.tabConfigs.Controls.Add(this.btnSaveConfig);
            this.tabConfigs.Controls.Add(this.btnResetConfig);
            this.tabConfigs.Controls.Add(this.txtQualityDotConfig);
            this.tabConfigs.Controls.Add(this.lblQualityDotConfig);
            this.tabConfigs.Controls.Add(this.txtAliasDotConfig);
            this.tabConfigs.Controls.Add(this.lblAliasDotConfig);
            this.tabConfigs.Controls.Add(this.txtRssDotConfig);
            this.tabConfigs.Controls.Add(this.lblRssDotConfig);
            this.tabConfigs.Location = new System.Drawing.Point(4, 22);
            this.tabConfigs.Name = "tabConfigs";
            this.tabConfigs.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigs.Size = new System.Drawing.Size(484, 589);
            this.tabConfigs.TabIndex = 1;
            this.tabConfigs.Text = "Config Files";
            this.tabConfigs.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(313, 531);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 23);
            this.btnSaveConfig.TabIndex = 27;
            this.btnSaveConfig.Text = "Save";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnResetConfig
            // 
            this.btnResetConfig.Location = new System.Drawing.Point(217, 531);
            this.btnResetConfig.Name = "btnResetConfig";
            this.btnResetConfig.Size = new System.Drawing.Size(64, 23);
            this.btnResetConfig.TabIndex = 28;
            this.btnResetConfig.Text = "Reset";
            this.btnResetConfig.UseVisualStyleBackColor = true;
            // 
            // txtQualityDotConfig
            // 
            this.txtQualityDotConfig.AcceptsReturn = true;
            this.txtQualityDotConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtQualityDotConfig.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQualityDotConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQualityDotConfig.Location = new System.Drawing.Point(7, 363);
            this.txtQualityDotConfig.Multiline = true;
            this.txtQualityDotConfig.Name = "txtQualityDotConfig";
            this.txtQualityDotConfig.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQualityDotConfig.Size = new System.Drawing.Size(381, 137);
            this.txtQualityDotConfig.TabIndex = 18;
            this.txtQualityDotConfig.WordWrap = false;
            // 
            // lblQualityDotConfig
            // 
            this.lblQualityDotConfig.AutoSize = true;
            this.lblQualityDotConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQualityDotConfig.Location = new System.Drawing.Point(7, 344);
            this.lblQualityDotConfig.Name = "lblQualityDotConfig";
            this.lblQualityDotConfig.Size = new System.Drawing.Size(102, 16);
            this.lblQualityDotConfig.TabIndex = 17;
            this.lblQualityDotConfig.Text = "Quality.config";
            this.lblQualityDotConfig.MouseEnter += new System.EventHandler(this.lblQualityDotConfig_MouseEnter);
            this.lblQualityDotConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtAliasDotConfig
            // 
            this.txtAliasDotConfig.AcceptsReturn = true;
            this.txtAliasDotConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtAliasDotConfig.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtAliasDotConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasDotConfig.Location = new System.Drawing.Point(7, 195);
            this.txtAliasDotConfig.Multiline = true;
            this.txtAliasDotConfig.Name = "txtAliasDotConfig";
            this.txtAliasDotConfig.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAliasDotConfig.Size = new System.Drawing.Size(381, 137);
            this.txtAliasDotConfig.TabIndex = 16;
            this.txtAliasDotConfig.WordWrap = false;
            this.txtAliasDotConfig.TextChanged += new System.EventHandler(this.txtAliasDotConfig_TextChanged);
            // 
            // lblAliasDotConfig
            // 
            this.lblAliasDotConfig.AutoSize = true;
            this.lblAliasDotConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAliasDotConfig.Location = new System.Drawing.Point(7, 176);
            this.lblAliasDotConfig.Name = "lblAliasDotConfig";
            this.lblAliasDotConfig.Size = new System.Drawing.Size(89, 16);
            this.lblAliasDotConfig.TabIndex = 15;
            this.lblAliasDotConfig.Text = "Alias.config";
            this.lblAliasDotConfig.MouseEnter += new System.EventHandler(this.lblAliasDotConfig_MouseEnter);
            this.lblAliasDotConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtRssDotConfig
            // 
            this.txtRssDotConfig.AcceptsReturn = true;
            this.txtRssDotConfig.BackColor = System.Drawing.SystemColors.Control;
            this.txtRssDotConfig.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRssDotConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRssDotConfig.Location = new System.Drawing.Point(7, 26);
            this.txtRssDotConfig.Multiline = true;
            this.txtRssDotConfig.Name = "txtRssDotConfig";
            this.txtRssDotConfig.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRssDotConfig.Size = new System.Drawing.Size(381, 137);
            this.txtRssDotConfig.TabIndex = 14;
            this.txtRssDotConfig.WordWrap = false;
            // 
            // lblRssDotConfig
            // 
            this.lblRssDotConfig.AutoSize = true;
            this.lblRssDotConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRssDotConfig.Location = new System.Drawing.Point(7, 7);
            this.lblRssDotConfig.Name = "lblRssDotConfig";
            this.lblRssDotConfig.Size = new System.Drawing.Size(85, 16);
            this.lblRssDotConfig.TabIndex = 0;
            this.lblRssDotConfig.Text = "RSS.config";
            this.lblRssDotConfig.MouseEnter += new System.EventHandler(this.lblRssDotConfig_MouseEnter);
            this.lblRssDotConfig.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.groupBoxTest);
            this.tabSchedule.Controls.Add(this.groupBoxSchedule);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedule.Size = new System.Drawing.Size(484, 589);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Test/Schedule";
            this.tabSchedule.UseVisualStyleBackColor = true;
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Controls.Add(this.btnRunSabSync);
            this.groupBoxTest.Controls.Add(this.txtOutput);
            this.groupBoxTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTest.Location = new System.Drawing.Point(8, 6);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(468, 380);
            this.groupBoxTest.TabIndex = 18;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Test SABSync";
            // 
            // btnRunSabSync
            // 
            this.btnRunSabSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunSabSync.Location = new System.Drawing.Point(159, 21);
            this.btnRunSabSync.Name = "btnRunSabSync";
            this.btnRunSabSync.Size = new System.Drawing.Size(114, 28);
            this.btnRunSabSync.TabIndex = 15;
            this.btnRunSabSync.Text = "Run SABSync";
            this.btnRunSabSync.UseVisualStyleBackColor = true;
            this.btnRunSabSync.Click += new System.EventHandler(this.btnRunSabSync_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(6, 54);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(456, 319);
            this.txtOutput.TabIndex = 9;
            this.txtOutput.WordWrap = false;
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Controls.Add(this.numMinutes);
            this.groupBoxSchedule.Controls.Add(this.txtResult);
            this.groupBoxSchedule.Controls.Add(this.btnCreateTask);
            this.groupBoxSchedule.Controls.Add(this.txtWinPassword);
            this.groupBoxSchedule.Controls.Add(this.lblRepeatTask);
            this.groupBoxSchedule.Controls.Add(this.txtWinUsername);
            this.groupBoxSchedule.Controls.Add(this.lblMinutes);
            this.groupBoxSchedule.Controls.Add(this.lblWinPassword);
            this.groupBoxSchedule.Controls.Add(this.chkVisbile);
            this.groupBoxSchedule.Controls.Add(this.lblWinUsername);
            this.groupBoxSchedule.Controls.Add(this.btnTestSabSync);
            this.groupBoxSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSchedule.Location = new System.Drawing.Point(8, 394);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(468, 192);
            this.groupBoxSchedule.TabIndex = 17;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule SABSync";
            // 
            // numMinutes
            // 
            this.numMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMinutes.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMinutes.Location = new System.Drawing.Point(155, 26);
            this.numMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numMinutes.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(36, 22);
            this.numMinutes.TabIndex = 3;
            this.numMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMinutes.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(6, 135);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(456, 50);
            this.txtResult.TabIndex = 6;
            // 
            // btnCreateTask
            // 
            this.btnCreateTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTask.Location = new System.Drawing.Point(112, 104);
            this.btnCreateTask.Name = "btnCreateTask";
            this.btnCreateTask.Size = new System.Drawing.Size(102, 28);
            this.btnCreateTask.TabIndex = 0;
            this.btnCreateTask.Text = "Create Task";
            this.btnCreateTask.UseVisualStyleBackColor = true;
            this.btnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
            this.btnCreateTask.MouseEnter += new System.EventHandler(this.btnCreateTask_MouseEnter);
            this.btnCreateTask.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtWinPassword
            // 
            this.txtWinPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWinPassword.Location = new System.Drawing.Point(102, 78);
            this.txtWinPassword.Name = "txtWinPassword";
            this.txtWinPassword.PasswordChar = '*';
            this.txtWinPassword.Size = new System.Drawing.Size(186, 22);
            this.txtWinPassword.TabIndex = 14;
            this.txtWinPassword.UseSystemPasswordChar = true;
            // 
            // lblRepeatTask
            // 
            this.lblRepeatTask.AutoSize = true;
            this.lblRepeatTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepeatTask.Location = new System.Drawing.Point(11, 28);
            this.lblRepeatTask.Name = "lblRepeatTask";
            this.lblRepeatTask.Size = new System.Drawing.Size(150, 16);
            this.lblRepeatTask.TabIndex = 2;
            this.lblRepeatTask.Text = "Repeat Task Every: ";
            this.lblRepeatTask.MouseEnter += new System.EventHandler(this.lblRepeatTask_MouseEnter);
            this.lblRepeatTask.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // txtWinUsername
            // 
            this.txtWinUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWinUsername.Location = new System.Drawing.Point(103, 52);
            this.txtWinUsername.Name = "txtWinUsername";
            this.txtWinUsername.Size = new System.Drawing.Size(185, 22);
            this.txtWinUsername.TabIndex = 13;
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinutes.Location = new System.Drawing.Point(197, 28);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(54, 16);
            this.lblMinutes.TabIndex = 4;
            this.lblMinutes.Text = "Minutes";
            // 
            // lblWinPassword
            // 
            this.lblWinPassword.AutoSize = true;
            this.lblWinPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinPassword.Location = new System.Drawing.Point(16, 78);
            this.lblWinPassword.Name = "lblWinPassword";
            this.lblWinPassword.Size = new System.Drawing.Size(80, 16);
            this.lblWinPassword.TabIndex = 12;
            this.lblWinPassword.Text = "Password:";
            this.lblWinPassword.MouseEnter += new System.EventHandler(this.lblWinPassword_MouseEnter);
            this.lblWinPassword.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // chkVisbile
            // 
            this.chkVisbile.AutoSize = true;
            this.chkVisbile.Checked = true;
            this.chkVisbile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisbile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVisbile.Location = new System.Drawing.Point(315, 28);
            this.chkVisbile.Name = "chkVisbile";
            this.chkVisbile.Size = new System.Drawing.Size(136, 20);
            this.chkVisbile.TabIndex = 5;
            this.chkVisbile.Text = "Console Visible";
            this.chkVisbile.UseVisualStyleBackColor = true;
            this.chkVisbile.MouseEnter += new System.EventHandler(this.chkVisbile_MouseEnter);
            this.chkVisbile.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // lblWinUsername
            // 
            this.lblWinUsername.AutoSize = true;
            this.lblWinUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinUsername.Location = new System.Drawing.Point(13, 52);
            this.lblWinUsername.Name = "lblWinUsername";
            this.lblWinUsername.Size = new System.Drawing.Size(83, 16);
            this.lblWinUsername.TabIndex = 11;
            this.lblWinUsername.Text = "Username:";
            this.lblWinUsername.MouseEnter += new System.EventHandler(this.lblWinUsername_MouseEnter);
            this.lblWinUsername.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // btnTestSabSync
            // 
            this.btnTestSabSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestSabSync.Location = new System.Drawing.Point(290, 104);
            this.btnTestSabSync.Name = "btnTestSabSync";
            this.btnTestSabSync.Size = new System.Drawing.Size(161, 28);
            this.btnTestSabSync.TabIndex = 8;
            this.btnTestSabSync.Text = "Run Scheduled Task";
            this.btnTestSabSync.UseVisualStyleBackColor = true;
            this.btnTestSabSync.Click += new System.EventHandler(this.btnTestSabSync_Click);
            this.btnTestSabSync.MouseEnter += new System.EventHandler(this.btnTestSabSync_MouseEnter);
            this.btnTestSabSync.MouseLeave += new System.EventHandler(this.statusBarClear);
            // 
            // SABSyncGUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(494, 642);
            this.Controls.Add(this.tabControlGUI);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SABSyncGUI";
            this.Text = "SABSync 0.4.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlGUI.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabConfigs.ResumeLayout(false);
            this.tabConfigs.PerformLayout();
            this.tabSchedule.ResumeLayout(false);
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.TextBox txtSabInfoPort;
        private System.Windows.Forms.Label lblSabInfoPort;
        private System.Windows.Forms.Button btnTestSab;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAliasConfig;
        private System.Windows.Forms.Label lblAliasConfig;
        private System.Windows.Forms.TextBox txtAliasConfig;
        private System.Windows.Forms.Button btnQualityConfig;
        private System.Windows.Forms.Label lblQualityConfig;
        private System.Windows.Forms.TextBox txtQualityConfig;
        private System.Windows.Forms.CheckBox chkVerboseLogging;
        private System.Windows.Forms.CheckBox chkDownloadPropers;
        private System.Windows.Forms.Label lblDownloadQuality;
        private System.Windows.Forms.TextBox txtDownloadQuality;
        private System.Windows.Forms.TabControl tabControlGUI;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabConfigs;
        private System.Windows.Forms.Label lblRssDotConfig;
        private System.Windows.Forms.TextBox txtRssDotConfig;
        private System.Windows.Forms.TextBox txtAliasDotConfig;
        private System.Windows.Forms.Label lblAliasDotConfig;
        private System.Windows.Forms.TextBox txtQualityDotConfig;
        private System.Windows.Forms.Label lblQualityDotConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnResetConfig;
        private System.Windows.Forms.Button btnHd;
        private System.Windows.Forms.Button btnSd;
        private System.Windows.Forms.Button btnClearDQ;
        private System.Windows.Forms.Button btnTvRootClear;
        private System.Windows.Forms.Button btnPriorityLow;
        private System.Windows.Forms.Button btnPriorityHigh;
        private System.Windows.Forms.Button btnPriorityNormal;
        private System.Windows.Forms.TextBox txtDeleteLogs;
        private System.Windows.Forms.Label lblDeleteLogs;
        private System.Windows.Forms.Button btn30Days;
        private System.Windows.Forms.Button btn60Days;
        private System.Windows.Forms.Button btn120Days;
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.Label lblRepeatTask;
        private System.Windows.Forms.Button btnCreateTask;
        private System.Windows.Forms.NumericUpDown numMinutes;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.CheckBox chkVisbile;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnTestSabSync;
        private System.Windows.Forms.TextBox txtWinPassword;
        private System.Windows.Forms.TextBox txtWinUsername;
        private System.Windows.Forms.Label lblWinPassword;
        private System.Windows.Forms.Label lblWinUsername;
        private System.Windows.Forms.Button btnRunSabSync;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.GroupBox groupBoxTest;

    }
}

