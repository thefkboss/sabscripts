// TODO: add validation of app.config elements

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace SABSync
{
    public class SyncJobConfig
    {
        private static readonly Logger Log = new Logger();
        private static readonly NameValueCollection Settings = ConfigurationManager.AppSettings;

        public SyncJobConfig()
        {
            Log.Log("Loading configuration...");
            TvRootFolders = GetTvRootFolders();
            VerboseLogging = Convert.ToBoolean(Settings["verboseLogging"]);
            Feeds = GetFeeds();
            SabRequest = GetSabRequest();
            ShowAliases = GetShowAliases();
            ShowQualities = GetShowQualities();
            VideoExt = Settings["videoExt"].Trim(';', ' ').Split(';');
            NzbDir = new DirectoryInfo(ConfigurationManager.AppSettings["nzbDir"]);
            DownloadPropers = Convert.ToBoolean(ConfigurationManager.AppSettings["downloadPropers"]);
            DownloadQuality = ConfigurationManager.AppSettings["downloadQuality"].Trim(';', ' ').Split(';');
            SabReplaceChars = Convert.ToBoolean(ConfigurationManager.AppSettings["sabReplaceChars"]);
            IgnoreSeasons = ConfigurationManager.AppSettings["ignoreSeasons"];
            TvDailyTemplate = ConfigurationManager.AppSettings["tvDailyTemplate"];
            if (string.IsNullOrEmpty(TvDailyTemplate))
                throw new ApplicationException("Configuration missing: tvDailyTemplate");
            TvTemplate = ConfigurationManager.AppSettings["tvTemplate"];
            if (string.IsNullOrEmpty(TvTemplate))
                throw new ApplicationException("Configuration missing: tvTemplate");
        }

        public bool DownloadPropers { get; set; }
        public string[] DownloadQuality { get; set; }
        public IList<FeedInfo> Feeds { get; set; }
        public string IgnoreSeasons { get; set; }
        public DirectoryInfo NzbDir { get; set; }
        public string SabRequest { get; set; }
        public bool SabReplaceChars { get; set; }
        public IList<ShowAlias> ShowAliases { get; set; }
        public IList<ShowQuality> ShowQualities { get; set; }
        public string TvDailyTemplate { get; set; }
        public IList<DirectoryInfo> TvRootFolders { get; set; }
        public string TvTemplate { get; set; }
        public bool VerboseLogging { get; set; }
        public string[] VideoExt { get; set; }

        private static string GetSabRequest()
        {
            string sabnzbdInfo = Settings["sabnzbdInfo"];
            string priority = Settings["priority"];
            string apiKey = Settings["apiKey"];
            string username = Settings["username"];
            string password = Settings["password"];
            return string.Format(
                "http://{0}/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
        }

        private static IList<ShowAlias> GetShowAliases()
        {
            var aliasConfigFile = new FileInfo(Settings["alias"]);
            if (!aliasConfigFile.Exists)
                throw new ApplicationException("Invalid Alias file path. " + aliasConfigFile);

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

        private static IList<FeedInfo> GetFeeds()
        {
            var rssConfigFile = new FileInfo(Settings["rss"]);
            if (!rssConfigFile.Exists)
                throw new ApplicationException("Invalid RSS file path. " + rssConfigFile);

            Log.Log("Loading RSS feed list from {0}", rssConfigFile);

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

        private static IList<ShowQuality> GetShowQualities()
        {
            var qualityFile = new FileInfo(Settings["quality"]);
            if (!qualityFile.Exists)
                throw new ApplicationException("Invalid Quality file path. " + qualityFile);

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

        private static IList<DirectoryInfo> GetTvRootFolders()
        {
            const string tvRootKey = "tvRoot";
            string tvRoot = Settings[tvRootKey];
            if (tvRoot == null)
                throw new ApplicationException("Configuration missing: " + tvRootKey);

            var list = new List<DirectoryInfo>();
            string[] paths = tvRoot.Trim(';').Split(';');
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