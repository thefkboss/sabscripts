using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;

namespace SABSyncGUI
{
    public partial class SABSyncGUI : Form
    {
        public SABSyncGUI()
        {
            InitializeComponent();
            UpdateBindedUi();

        }

        private void UpdateBindedUi()
        {
            string[] sabInfoSplit = Settings.SabnzbdInfo.Split(':');

            string hostname = null;
            string port = null;
            if (sabInfoSplit.Length == 2)
            {
                hostname = sabInfoSplit[0];
                port = sabInfoSplit[1];
            }
            else
            {
                hostname = "127.0.0.1";
                port = "8080";
            }

            txtTvRoot.Text = Settings.TvRootPath;
            txtTvTemplate.Text = Settings.TvTemplate;
            txtTvDailyTemplate.Text = Settings.TvDailyTemplate;
            txtVideoExt.Text = Settings.VideoExt;
            txtIgnoreSeasons.Text = Settings.IgnoreSeasons;
            txtNzbDir.Text = Settings.NzbDir;
            txtSabInfoHost.Text = hostname;
            txtSabInfoPort.Text = port;
            txtUsername.Text = Settings.Username;
            txtPassword.Text = Settings.Password;
            txtApiKey.Text = Settings.ApiKey;
            txtPriority.Text = Settings.Priority;
            txtRssConfig.Text = Settings.RssConfig;
            txtAliasConfig.Text = Settings.AliasConfig;
            txtQualityConfig.Text = Settings.QualityConfig;
            txtDownloadQuality.Text = Settings.DownloadQuality;
            chkReplaceChars.Checked = Convert.ToBoolean(Settings.SabReplaceChars);
            chkVerboseLogging.Checked = Convert.ToBoolean(Settings.VerboseLogging);
            chkDownloadPropers.Checked = Convert.ToBoolean(Settings.DownloadPropers);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.TvRootPath = txtTvRoot.Text;
            Settings.TvTemplate = txtTvTemplate.Text;
            Settings.TvDailyTemplate = txtTvDailyTemplate.Text;
            Settings.VideoExt = txtVideoExt.Text;
            Settings.IgnoreSeasons = txtIgnoreSeasons.Text;
            Settings.NzbDir = txtNzbDir.Text;
            Settings.SabnzbdInfo = SabInfoJoin(txtSabInfoHost.Text, txtSabInfoPort.Text);
            Settings.Username = txtUsername.Text;
            Settings.Password = txtPassword.Text;
            Settings.ApiKey = txtApiKey.Text; 
            Settings.Priority = txtPriority.Text;
            Settings.RssConfig = txtRssConfig.Text;
            Settings.AliasConfig = txtAliasConfig.Text;
            Settings.QualityConfig = txtQualityConfig.Text;
            Settings.DownloadQuality = txtDownloadQuality.Text;
            Settings.SabReplaceChars = Convert.ToString(chkReplaceChars.Checked);
            Settings.VerboseLogging = Convert.ToString(chkVerboseLogging.Checked);
            Settings.DownloadPropers = Convert.ToString(chkDownloadPropers.Checked);
            statusStripLabel.Text = "Settings have been saved!";
        }

        private static string TestConnection(string hostname, string port, string apiKey, string username, string password)
        {
            string versionRssUrl = "http://" + hostname + ":" + port + "/api?mode=queue&output=xml&apikey=" + apiKey + "&ma_username=" + username + "&ma_password=" + password;
            string sabVersion = null;
            try
            {
                HttpWebRequest versionRssRequest = (HttpWebRequest)WebRequest.Create(versionRssUrl);
                versionRssRequest.Timeout = 10000;
                HttpWebResponse versionRssResponse = (HttpWebResponse)versionRssRequest.GetResponse();
                Stream versionDoc = versionRssResponse.GetResponseStream();
                XmlTextReader versionReader = new XmlTextReader(versionDoc);
                XmlDocument versionRssDoc = new XmlDocument();
                versionRssDoc.Load(versionReader);

                var version = versionRssDoc.GetElementsByTagName(@"version");
                var error = versionRssDoc.GetElementsByTagName(@"error");

                if (error.Count != 0)
                {
                    string errorMsg = "Error connecting to SAB: " + error[0].InnerText;
                    return errorMsg;
                }

                if (version.Count != 0)
                {
                    sabVersion = version[0].InnerText;
                }
                versionRssResponse.Close();
            }

            catch
            {
                string errorMsg = "Timed out connecting to SABnzbd, check your settings";
                return errorMsg;
            }
            string result = "Successfully Connected to SABnzbd! Version: " + sabVersion;
            return result;
        }

        private void statusBarClear(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABSync GUI - Mouse over labels for more info!";
        }

        private void chkReplaceChars_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SAB is Replacing Illegal Characters = Checked";
        }

        private void chkVerboseLogging_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Check to enable Verbose Logging for SABSync";
        }

        private void chkDownloadPropers_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Check to enable SABSync to download PROPER releases";
        }

        private void tvRootBrowse_Click(object sender, EventArgs e)
        {
            ChangeTvRootFolder();
        }

        public void nzbDirBrowse_Click(object sender, EventArgs e)
        {
            ChangeNzbFolder();
        }

        private string SabInfoJoin(string hostname, string port)
        {
            string sabInfo = hostname + ":" + port;
            return sabInfo;
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

        private void btnTvRootBrowse_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for the TV Root Directory";
        }

        private void lblTvRoot_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Where are your TV Shows Located?";
        }

        private void btnNzbDir_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for NZB Directory";
        }

        private void lblNzbDir_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Imported NZB Directory";
        }

        private void lblTvTemplate_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Copy+Paste from SAB's TV Sorting Config";
        }

        private void lblTvDailyTemplate_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Copy+Paste from SAB's Daily TV Sorting Config";
        }

        private void lblVideoExt_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Extensions that should be considered Video Files";
        }

        private void lblSabInfoHostname_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Hostname/IP Address for SABnzbd";
        }

        private void lblSabInfoPort_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Port for SABnzbd";
        }

        private void lblUsername_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd Username";
        }

        private void lblPassword_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd Password";
        }

        private void lblApiKey_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd API Key (Config-General)";
        }

        private void lblPriority_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "The Priority NZBs should be added with";
        }

        private void lblIgnoreSeasons_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Shows & Seasons to Ignore";
        }

        private void btnRssConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for RSS Config";
        }

        private void lblRssConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "RSS Configuration File";
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

        private void btnTestSab_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Test current settings to connect to SABnzbd";
        }

        private void btnTestSab_Click(object sender, EventArgs e)
        {
            string response = TestConnection(txtSabInfoHost.Text, txtSabInfoPort.Text, txtApiKey.Text, txtUsername.Text, txtPassword.Text);
            statusStripLabel.Text = response;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            UpdateBindedUi();
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Reload settings from Config File";
        }

        private void btnAliasConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtRssConfig.Text = fdlg.FileName;
            }
        }

        private void btnAliasConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for Alias onfig";
        }

        private void lblAliasConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Alias Configuration File";
        }

        private void btnQualityConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtRssConfig.Text = fdlg.FileName;
            }
        }

        private void btnQualityConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for Quality Config";
        }

        private void lblQualityConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Quality Configuration File";
        }

        private void lblDownloadQuality_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Default Download Quality for Episodes - xvid;720p (one or both)";
        }
    }
}
