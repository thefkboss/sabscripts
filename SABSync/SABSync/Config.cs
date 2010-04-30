using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace SABSync
{
    public class Config
    {
        private static Logger logger = new Logger();
        private IList<FeedInfo> _feeds;
        private IList<string> _myShows;
        private DirectoryInfo _nzbDir;
        private string _sabRequest;
        private IList<ShowAlias> _showAliases;
        private IList<ShowQuality> _showQualities;
        private string _tvDailyTemplate;
        private IList<DirectoryInfo> _tvRootFolders;
        private string _tvTemplate;

        public Config() : this(ConfigurationManager.AppSettings) {}

        public Config(NameValueCollection settings)
        {
            Settings = settings;

            DownloadPropers = Convert.ToBoolean(Settings["downloadPropers"] ?? "false");
            DownloadQuality = (Settings["downloadQuality"] ?? string.Empty).Trim(';', ' ').Split(';');
            IgnoreSeasons = Settings["ignoreSeasons"];
            SabReplaceChars = Convert.ToBoolean(Settings["sabReplaceChars"] ?? "false");
            VerboseLogging = Convert.ToBoolean(Settings["verboseLogging"] ?? "false");
            VideoExt = (Settings["videoExt"] ?? string.Empty).Trim(';', ' ').Split(';');
        }

        public bool DownloadPropers { get; set; }

        public string[] DownloadQuality { get; set; }

        public IList<FeedInfo> Feeds
        {
            get { return _feeds ?? (_feeds = GetFeeds()); }
            set { _feeds = value; }
        }

        public string IgnoreSeasons { get; set; }

        public IList<string> MyShows
        {
            get { return _myShows ?? (_myShows = GetMyShows()); }
            set { _myShows = value; }
        }

        public DirectoryInfo NzbDir
        {
            get { return _nzbDir ?? (_nzbDir = GetNzbDir()); }
            set { _nzbDir = value; }
        }

        public string SabRequest
        {
            get { return _sabRequest ?? (_sabRequest = GetSabRequest()); }
            set { _sabRequest = value; }
        }

        public bool SabReplaceChars { get; set; }

        public IList<ShowAlias> ShowAliases
        {
            get { return _showAliases ?? (_showAliases = GetShowAliases()); }
            set { _showAliases = value; }
        }

        public IList<ShowQuality> ShowQualities
        {
            get { return _showQualities ?? (_showQualities = GetShowQualities()); }
            set { _showQualities = value; }
        }

        public string TvDailyTemplate
        {
            get { return _tvDailyTemplate ?? (_tvDailyTemplate = GetTvDailyTemplate()); }
            set { _tvDailyTemplate = value; }
        }

        public IList<DirectoryInfo> TvRootFolders
        {
            get { return _tvRootFolders ?? (_tvRootFolders = GetTvRootFolders()); }
            set { _tvRootFolders = value; }
        }

        public string TvTemplate
        {
            get { return _tvTemplate ?? (_tvTemplate = GetTvTemplate()); }
            set { _tvTemplate = value; }
        }

        public bool VerboseLogging { get; set; }

        public string[] VideoExt { get; set; }

        public NameValueCollection Settings { get; private set; }

        private string GetTvTemplate()
        {
            if (string.IsNullOrEmpty(GetSetting("tvTemplate")))
                throw new ApplicationException("Configuration missing: tvTemplate");
            return GetSetting("tvTemplate");
        }

        private string GetTvDailyTemplate()
        {
            if (string.IsNullOrEmpty(GetSetting("tvDailyTemplate")))
                throw new ApplicationException("Configuration missing: tvDailyTemplate");
            return Settings["tvDailyTemplate"];
        }

        private DirectoryInfo GetNzbDir()
        {
            string path = Settings["nzbDir"];
            if (string.IsNullOrEmpty(path)) return null;

            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
                throw new ApplicationException(string.Format("Configuration: Invalid nzbDir folder: {0}", folder));

            return folder;
        }

        private IList<string> GetMyShows()
        {
            var list = new List<string>();
            foreach (DirectoryInfo rootFolder in TvRootFolders)
            {
                if (VerboseLogging)
                    logger.Log("TVRoot Directory: {0}", rootFolder);

                foreach (DirectoryInfo show in rootFolder.GetDirectories())
                {
                    if (VerboseLogging)
                        logger.Log("Adding show to wanted shows list: " + show);

                    if (!list.Contains(show.ToString()))
                        list.Add(show.ToString());
                }
            }
            return list;
        }

        private string GetSabRequest()
        {
            string sabnzbdInfo = GetSetting("sabnzbdInfo");
            string priority = GetSetting("priority");
            string apiKey = GetSetting("apiKey");
            string username = GetSetting("username");
            string password = GetSetting("password");
            return string.Format(
                "http://{0}/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
        }

        private IList<ShowAlias> GetShowAliases()
        {
            string path = Settings["alias"];
            if (string.IsNullOrEmpty(path)) 
                return new List<ShowAlias>();

            var aliasConfigFile = GetValidFile(GetSetting("alias"), "Alias");

            var list = new List<ShowAlias>();
            int line = 0;
            foreach (string aliasLine in File.ReadAllLines(aliasConfigFile.FullName))
            {
                line++;
                string[] parts = aliasLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Alias configuration at line {0}.", line));

                list.Add(new ShowAlias {BadName = parts[0], Alias = parts[1]});
            }
            return list;
        }

        private IList<FeedInfo> GetFeeds()
        {
            var rssConfigFile = GetValidFile(GetSetting("rss"), "RSS");

            logger.Log("Loading RSS feed list from {0}", rssConfigFile);

            var list = new List<FeedInfo>();
            foreach (string line in File.ReadAllLines(rssConfigFile.FullName))
            {
                string[] part = line.Split('|');
                bool isTwoPart = part.Length > 1;
                list.Add(new FeedInfo
                {
                    Name = isTwoPart ? part[0] : "UN-NAMED",
                    Url = isTwoPart ? part[1] : part[0]
                });
            }
            return list;
        }

        private string GetSetting(string key)
        {
            string value = Settings[key];
            if (value == null)
                throw new ApplicationException("Configuration missing: " + key);
            return value;
        }

        private IList<ShowQuality> GetShowQualities()
        {
            string path = Settings["quality"];
            if (string.IsNullOrEmpty(path))
                return new List<ShowQuality>();

            FileInfo qualityFile = GetValidFile(path,"Quality");

            var list = new List<ShowQuality>();
            int line = 0;
            foreach (string qualityLine in File.ReadAllLines(qualityFile.FullName))
            {
                line++;
                string[] parts = qualityLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Quality configuration at line {0}.", line));
                list.Add(new ShowQuality {Name = parts[0], Quality = parts[1]});
            }
            return list;
        }

        private static FileInfo GetValidFile(string path, string fileType) 
        {
            var file = new FileInfo(path);
            if (!file.Exists)
                throw new ApplicationException(string.Format("Invalid {0} file path. {1}", fileType, path));
            return file;
        }

        private IList<DirectoryInfo> GetTvRootFolders()
        {
            var list = new List<DirectoryInfo>();
            string[] paths = GetSetting("tvRoot").Trim(';').Split(';');
            foreach (string path in paths)
            {
                var folder = new DirectoryInfo(path);
                if (!folder.Exists)
                    throw new ApplicationException("Invalid TV Root folder: " + folder);
                list.Add(folder);
            }
            return list;
        }
    }
}