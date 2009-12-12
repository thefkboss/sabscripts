using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SABSyncGUI
{
    public partial class SABSyncGUI : Form
    {
        public SABSyncGUI()
        {
            InitializeComponent();
            UpdateBindedUi();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.TvRootPath = txtTvRoot.Text;
            Settings.TvTemplate = txtTvTemplate.Text;
            Settings.TvDailyTemplate = txtTvDailyTemplate.Text;
            Settings.VideoExt = txtVideoExt.Text;
            Settings.IgnoreSeasons = txtIgnoreSeasons.Text;
            Settings.NzbDir = txtNzbDir.Text;
            Settings.SabnzbdInfo = txtSabInfo.Text;
            Settings.Username = txtUsername.Text;
            Settings.Password = txtPassword.Text;
            Settings.ApiKey = txtApiKey.Text; 
            Settings.Priority = txtPriority.Text;
            Settings.RssConfig = txtRssConfig.Text;
            Settings.SabReplaceChars = Convert.ToString(chkReplaceChars.Checked);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tvRootBrowse_Click(object sender, EventArgs e)
        {
            ChangeTvRootFolder();
        }

        public void nzbDirBrowse_Click(object sender, EventArgs e)
        {
            ChangeNzbFolder();
        }

        private void txtTvRoot_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void UpdateBindedUi()
        {
            txtTvRoot.Text = Settings.TvRootPath;
            txtTvTemplate.Text = Settings.TvTemplate;
            txtTvDailyTemplate.Text = Settings.TvDailyTemplate;
            txtVideoExt.Text = Settings.VideoExt;
            txtIgnoreSeasons.Text = Settings.IgnoreSeasons;
            txtNzbDir.Text = Settings.NzbDir;
            txtSabInfo.Text = Settings.SabnzbdInfo;
            txtUsername.Text = Settings.Username;
            txtPassword.Text = Settings.Password;
            txtApiKey.Text = Settings.ApiKey;
            txtPriority.Text = Settings.Priority;
            txtRssConfig.Text = Settings.RssConfig;
            chkReplaceChars.Checked = Convert.ToBoolean(Settings.SabReplaceChars);

            //cmbXbmcStart.SelectedIndex = Convert.ToInt32(Settings.XbmcAutostart);
            //chkUpdateIfXbmcIsRunning.Checked = Settings.XbmcAutoShutdown;
            //txtXbmcStartArgs.Text = Settings.XbmcStartupArgs;
            //chkPreventStandby.Checked = Settings.PreventStandBy;
        }

        private void SABSyncGUI_Load(object sender, EventArgs e)
        {

        }

        private bool ChangeTvRootFolder()
        {
            bool result = false;

            tvRootDialog.ShowDialog(this);

            if (!String.IsNullOrEmpty(tvRootDialog.SelectedPath))
            {
                txtTvRoot.Text = tvRootDialog.SelectedPath;

                //Settings.TvRootPath = tvRootText.Text;

                result = true;
            }

            return result;
        }

        private bool ChangeNzbFolder()
        {
            bool result = false;

            nzbDirDialog.ShowDialog(this);

            if (!String.IsNullOrEmpty(nzbDirDialog.SelectedPath))
            {
                txtNzbDir.Text = nzbDirDialog.SelectedPath;

                //Settings.TvRootPath = tvRootText.Text;

                result = true;
            }

            return result;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void grpSabSettings_Enter(object sender, EventArgs e)
        {

        }

        private void btnTvRootBrowse_MouseHover(object sender, EventArgs e)
        {

            //statusStrip1.Text = "Click to Browse for the TV Root Directory";
            statusStrip1.Text = "Browse";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNzbDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void nzbDirDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnRssConfig_Click(object sender, EventArgs e)
        {
        OpenFileDialog fdlg = new OpenFileDialog();
        fdlg.Title = "C# Corner Open File Dialog" ;
        fdlg.InitialDirectory = @"c:\" ;
        fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*" ;
        fdlg.FilterIndex = 1 ;
        fdlg.RestoreDirectory = true ;

        if(fdlg.ShowDialog() == DialogResult.OK)
        {
            txtRssConfig.Text = fdlg.FileName ;
        }
        }
    }
}
