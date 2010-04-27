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

        public IList<FeedInfo> Feeds { get; set; }
        public string SABRequest { get; set; }
        public IList<ShowAlias> ShowAliases { get; set; }
        public IList<ShowQuality> ShowQualities { get; set; }
        public IList<DirectoryInfo> TvRootFolders { get; set; }
        public bool VerboseLogging { get; set; }
        public string[] VideoExt { get; set; }

        public SyncJobConfig()
        {
            TvRootFolders = GetTvRootFolders();
            VerboseLogging = Convert.ToBoolean(Settings["verboseLogging"]);
            Feeds = GetFeeds();
            SABRequest = GetSABRequest();
            ShowAliases = GetShowAliases();
            ShowQualities = GetShowQualities();
            VideoExt = Settings["videoExt"].Trim(';', ' ').Split(';');
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
                var parts = qualityLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Quality configuration at line {0}.", line));
                list.Add(new ShowQuality { Name = parts[0], Quality = parts[1] });
            }
            return list;
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
                var parts = aliasLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Alias configuration at line {0}.", line));

                list.Add(new ShowAlias {BadName = parts[0], Alias = parts[1]});
            }
            return list;
        }

        //private class ConfigParser<T>
        //{
        //    public IList<T> GetConfig(string path)
        //    {
                
        //    }
        //}

        private static string GetSABRequest()
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

        private static IList<DirectoryInfo> GetTvRootFolders()
        {
            const string tvRootKey = "tvRoot";
            var tvRoot = Settings[tvRootKey];
            if (tvRoot == null)
                throw new ApplicationException("Configuration missing: " + tvRootKey);

            var list = new List<DirectoryInfo>();
            var paths = tvRoot.Trim(';').Split(';');
            foreach (var path in paths)
            {
                var folder = new DirectoryInfo(path);
                if (!folder.Exists)
                    throw new ApplicationException("Invalid TV Root folder: " + folder);
                list.Add(folder);
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
            foreach (var line in File.ReadAllLines(rssConfigFile.FullName))
            {
                var part = line.Split('|');
                var isTwoPart = part.Length > 1;
                list.Add(new FeedInfo
                {
                    Name = isTwoPart ? part[0] : "UN-NAMED",
                    Url = isTwoPart ? part[1] : part[0]
                });
            }
            return list;
        }
    }
}