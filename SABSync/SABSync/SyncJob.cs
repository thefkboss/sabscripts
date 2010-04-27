using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
// TODO: http://eyeung003.blogspot.com/2010/03/c-net-35-syndication.html
using Rss;

namespace SABSync
{
    public class SyncJob
    {
        private readonly Logger _logger = new Logger();
        private DirectoryInfo _nzbDir;
        private string _ignoreSeasons;
        private string _tvTemplate;
        private string _tvDailyTemplate;
        private List<string> _wantedShowNames = new List<string>();
        private bool _sabReplaceChars;
        private bool _downloadPropers;
        private string[] _downloadQuality;
        private readonly List<string> Queued = new List<string>();
        private readonly List<string> Summary = new List<string>();

        private SyncJobConfig Config { get; set; }

        public SyncJob()
        {
            Config = new SyncJobConfig();
        }

        public void Start()
        {
            try
            {
                LoadConfig();
                
                Log("Watching {0} shows", _wantedShowNames.Count);
                Log("_ignoreSeasons: {0}", _ignoreSeasons);

                foreach (FeedInfo feedInfo in Config.Feeds)
                {
                    Log("Downloading feed {0} from {1}", feedInfo.Name, feedInfo.Url);

                    RssFeed feed = RssFeed.Read(feedInfo.Url);
                    foreach (RssItem item in feed.Channels[0].Items)
                    {
                        ProcessRssItem(feed.Url, item);
                    }
                }

                LogSummary();
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                Log(ex.ToString(), true);
            }
        }

        private void LoadConfig()
        {
            Log("Loading configuration...");

            LoadWantedShows();

            _ignoreSeasons = ConfigurationManager.AppSettings["ignoreSeasons"]; //Get _ignoreSeasons from app.config

            _tvTemplate = ConfigurationManager.AppSettings["tvTemplate"]; //Get _tvTemplate from app.config
            if (String.IsNullOrEmpty(_tvTemplate))
                throw new ApplicationException("Undefined tvTemplate");

            _tvDailyTemplate = ConfigurationManager.AppSettings["tvDailyTemplate"];
            if (String.IsNullOrEmpty(_tvDailyTemplate))
                throw new ApplicationException("tvDailyTemplate");

            _sabReplaceChars = Convert.ToBoolean(ConfigurationManager.AppSettings["sabReplaceChars"]); //Get sabReplaceChars from app.config
            _downloadQuality = ConfigurationManager.AppSettings["downloadQuality"].Trim(';', ' ').Split(';'); //Get _downloadQuality from app.config
            _downloadPropers = Convert.ToBoolean(ConfigurationManager.AppSettings["downloadPropers"]); //Get downloadProper from app.config

            _nzbDir = new DirectoryInfo(ConfigurationManager.AppSettings["nzbDir"]); //Get _nzbDir from app.config
        }

        private void LoadWantedShows()
        {
            foreach (DirectoryInfo rootFolder in Config.TvRootFolders)
            {
                if (Config.VerboseLogging)
                    Log("TVRoot Directory: {0}", rootFolder);

                foreach (var show in rootFolder.GetDirectories())
                {
                    if (Config.VerboseLogging)
                        Log("Adding show to wanted shows list: " + show);

                    if (!_wantedShowNames.Contains(show.ToString()))
                        _wantedShowNames.Add(show.ToString());
                }
            }
        }

        private void ProcessRssItem(string url, RssItem item)
        {
            if (item.Title.EndsWith("(Passworded)", StringComparison.InvariantCultureIgnoreCase))
            {
                Log("Skipping Passworded Report {0}", item.Title);
                return;
            }

            Int64 reportId = 0;
            string nzbId = null;
            string rssTitle = null;
            string nzbSite = null;
            string downloadLink = null;

            string urlLower = url.ToLower();

            if (urlLower.Contains("newzbin.com"))
            {
                reportId = Convert.ToInt64(Regex.Match(item.Link.AbsolutePath, @"\d{7,10}").Value);
                rssTitle = item.Title;
                nzbSite = "newzbin";
            }

            else if (urlLower.Contains("nzbs.org"))
            {
                nzbId = Regex.Match(item.Link.ToString(), @"\d{5,10}").Value;
                rssTitle = item.Title;
                nzbSite = "nzbsDotOrg";
                downloadLink = item.Link.ToString();
                downloadLink = downloadLink.Replace("&", "%26");
            }

            else if (urlLower.Contains("tvnzb.com"))
            {
                nzbId = Regex.Match(item.Link.ToString(), @"\d{5,10}").Value;
                rssTitle = item.Title;
                nzbSite = "tvnzb";
                downloadLink = item.Link.ToString();
                downloadLink = downloadLink.Replace("&", "%26");
            }

            else if (urlLower.Contains("nzbmatrix.com"))
            {
                // TODO: use HttpUtility.ParseQueryString();
                // https://nzbmatrix.com/api-nzb-download.php?id=626526
                nzbId = Regex.Match(item.Link.ToString(), @"\d{6,10}").Value;
                rssTitle = item.Title;
                nzbSite = "nzbmatrix";
                downloadLink = item.Link.ToString();
                downloadLink = downloadLink.Replace("&", "%26");
            }

            else if (urlLower.Contains("nzbsrus.com"))
            {
                nzbId = Regex.Match(item.Link.ToString(), @"\d{6,10}").Value;
                rssTitle = item.Title;
                nzbSite = "nzbsrus";
                downloadLink = item.Link.ToString();
                downloadLink = downloadLink.Replace("&", "%26");
            }

            else
            {
                nzbId = Regex.Match(item.Link.ToString(), @"\d{6,10}").Value;
                rssTitle = item.Title;
                nzbSite = "unknown";
                downloadLink = item.Link.ToString();
                downloadLink = downloadLink.Replace("&", "%26");
            }

            //Check if Show is Wanted

            if (nzbSite == "newzbin")
            {
                if (IsEpisodeWanted(rssTitle, reportId))
                {
                    string queueResponse = AddToQueue(reportId);
                    Queued.Add(rssTitle + ": " + queueResponse);
                }
            }

            else if (nzbSite == "nzbsDotOrg")
            {
                if (IsEpisodeWanted(rssTitle, nzbId))
                {
                    string titleFix = GetTitleFix(rssTitle);
                    string queueResponse = AddToQueue(rssTitle, downloadLink, titleFix);
                    Queued.Add(titleFix + ": " + queueResponse);
                }
            }

            else if (nzbSite == "tvnzb")
            {
                bool qualityWanted = false;
                for (int i = 0; i < _downloadQuality.Length; i++)
                {
                    if (rssTitle.ToLower().Contains(_downloadQuality[i]))
                        qualityWanted = true;
                }

                if (qualityWanted)
                {
                    if (IsEpisodeWanted(rssTitle, nzbId))
                    {
                        string titleFix = GetTitleFix(rssTitle);
                        string queueResponse = AddToQueue(rssTitle, downloadLink, titleFix);
                        Queued.Add(titleFix + ": " + queueResponse);
                    }
                }
            }

            else if (nzbSite == "nzbmatrix")
            {
                if (IsEpisodeWanted(rssTitle, nzbId))
                {
                    string titleFix = GetTitleFix(rssTitle);
                    string queueResponse = AddToQueue(rssTitle, downloadLink, titleFix);
                    Queued.Add(titleFix + ": " + queueResponse);
                }
            }

            else if (nzbSite == "nzbsrus")
            {
                if (IsEpisodeWanted(rssTitle, nzbId))
                {
                    string titleFix = GetTitleFix(rssTitle);
                    string queueResponse = AddToQueue(rssTitle, downloadLink, titleFix);
                    Queued.Add(titleFix + ": " + queueResponse);
                }
            }

            else
            {
                bool qualityWanted = false;
                for (int i = 0; i < _downloadQuality.Length; i++)
                {
                    if (rssTitle.ToLower().Contains(_downloadQuality[i]))
                        qualityWanted = true;
                }

                if (qualityWanted)
                {
                    if (IsEpisodeWanted(rssTitle, nzbId))
                    {
                        string titleFix = GetTitleFix(rssTitle);
                        string queueResponse = AddToQueue(rssTitle, downloadLink, titleFix);
                        Queued.Add(rssTitle + ": " + queueResponse);
                    }
                }
            }
        }

        private void LogSummary()
        {
            foreach (var logItem in Summary)
            {
                Log(logItem);
            }

            if (Summary.Count != 0)
                Log(Environment.NewLine);

            foreach (var item in Queued)
            {
                Log("Queued for download: " + item);
            }

            if (Queued.Count != 0)
                Log(Environment.NewLine);

            Log("Number of reports added to the queue: " + Queued.Count);
        }

        private string GetEpisodeDir(string showName, int seasonNumber, int episodeNumber, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode Dir");

            showName = CleanString(showName);

            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

            string path = Path.GetDirectoryName(tvDir + "\\" + _tvTemplate);

            path = path.Replace(".%ext", "");
            path = path.Replace("%sn", snReplace);
            path = path.Replace("%s.n", sDotNReplace);
            path = path.Replace("%s_n", sUnderNReplace);
            path = path.Replace("%0s", zeroSReplace);
            path = path.Replace("%s", sReplace);
            path = path.Replace("%0e", zeroEReplace);
            path = path.Replace("%e", eReplace);

            return path;
        }

        private string GetEpisodeFileMask(int seasonNumber, int episodeNumber, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode File Mask");

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

            string fileName = Path.GetFileName(tvDir + "\\" + _tvTemplate);

            fileName = fileName.Replace(".%ext", "");
            fileName = fileName.Replace("%en", "*");
            fileName = fileName.Replace("%e.n", "*");
            fileName = fileName.Replace("%e_n", "*");
            fileName = fileName.Replace("%sn", "*");
            fileName = fileName.Replace("%s.n", "*");
            fileName = fileName.Replace("%s_n", "*");
            fileName = fileName.Replace("%0s", zeroSReplace);
            fileName = fileName.Replace("%s", sReplace);
            fileName = fileName.Replace("%0e", zeroEReplace);
            fileName = fileName.Replace("%e", eReplace);

            return fileName;
        }

        private string GetEpisodeDir(string showName, int year, int month, int day, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode Dir");

            string path = Path.GetDirectoryName(tvDir + "\\" + _tvDailyTemplate);

            showName = CleanString(showName);

            string tReplace = showName;
            string dotTReplace = showName.Replace(' ', '.');
            string underTReplace = showName.Replace(' ', '_');
            string yearReplace = Convert.ToString(year);
            string zeroMReplace = String.Format("{0:00}", month);
            string mReplace = Convert.ToString(month);
            string zeroDReplace = String.Format("{0:00}", day);
            string dReplace = Convert.ToString(day);

            path = path.Replace(".%ext", "");
            path = path.Replace("%t", tReplace);
            path = path.Replace("%.t", dotTReplace);
            path = path.Replace("%_t", underTReplace);
            path = path.Replace("%y", yearReplace);
            path = path.Replace("%0m", zeroMReplace);
            path = path.Replace("%m", mReplace);
            path = path.Replace("%0d", zeroDReplace);
            path = path.Replace("%d", dReplace);

            //Trim path down to just season and episode file mask (for shows that do not have episode name) ie. [*S01E01*] instead of [* - S01E01 - *]
            path = path.TrimEnd(' ', '*', '.', '-', '_');
            path = path.TrimStart(' ', '*', '.', '-', '_');
            path = "*" + path + "*";

            return path;
        } //Ends GetDailyShowNamingScheme

        private string GetEpisodeFileMask(int year, int month, int day, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode File Mask");

            string fileMask = Path.GetFileName(tvDir + "\\" + _tvDailyTemplate);

            string yearReplace = Convert.ToString(year);
            string zeroMReplace = String.Format("{0:00}", month);
            string mReplace = Convert.ToString(month);
            string zeroDReplace = String.Format("{0:00}", day);
            string dReplace = Convert.ToString(day);

            fileMask = fileMask.Replace(".%ext", "*");
            fileMask = fileMask.Replace("%desc", "*");
            fileMask = fileMask.Replace("%.desc", "*");
            fileMask = fileMask.Replace("%_desc", "*");
            fileMask = fileMask.Replace("%t", "*");
            fileMask = fileMask.Replace("%.t", "*");
            fileMask = fileMask.Replace("%_t", "*");
            fileMask = fileMask.Replace("%y", yearReplace);
            fileMask = fileMask.Replace("%0m", zeroMReplace);
            fileMask = fileMask.Replace("%m", mReplace);
            fileMask = fileMask.Replace("%0d", zeroDReplace);
            fileMask = fileMask.Replace("%d", dReplace);

            return fileMask;
        } //Ends GetDailyShowNamingScheme

        private string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = { "\\", "/", "<", ">", "?", "*", ":", "|", "\"" };
            string[] goodCharacters = { "+", "+", "{", "}", "!", "@", "-", "#", "`" };

            for (int i = 0; i < badCharacters.Length; i++)
            {
                if (_sabReplaceChars)
                {
                    result = result.Replace(badCharacters[i], goodCharacters[i]);
                }
                else
                {
                    result = result.Replace(badCharacters[i], "");
                }
            }

            return result.Trim();
        }

        private string CleanUrlString(string name)
        {
            string result = name;
            string[] badCharacters = { "%", "<", ">", "#", "{", "}", "|", "\\", "^", "`", "[", "]", "`", ";", "/", "?", ":", "@", "=", "&", "$" };
            string[] goodCharacters = { "%25", "%3C", "%3E", "%23", "%7B", "%7D", "%7C", "%5C", "%5E", "%7E", "%5B", "%5D", "%60", "%3B", "%2F", "%3F", "%3A", "%40", "%3D", "%26", "%24" };


            for (int i = 0; i < badCharacters.Length; i++)
            {
                if (_sabReplaceChars)
                {
                    result = result.Replace(badCharacters[i], goodCharacters[i]);
                }
                else
                {
                    result = result.Replace(badCharacters[i], "");
                }
            }

            return result.Trim();
        }

        private bool IsShowWanted(string showName)
        {
            foreach (var di in _wantedShowNames)
            {
                if (string.Equals(di, CleanString(showName),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    Log("'{0}' is being watched.", showName);
                    return true;
                }
            }
            Log("'{0}' is not being watched.", showName);
            return false;
        } //Ends IsShowWanted

        private bool IsEpisodeWanted(string title, Int64 reportId)
        {
            Log("----------------------------------------------------------------");
            Log("Verifying '{0}'", title);

            try
            {
                if (title.Length > 80)
                {
                    title = title.Substring(0, 79);
                }

                string[] titleArray = title.Split('-');

                if (titleArray.Length == 3)
                {
                    string showName = titleArray[0].Trim();
                    string seasonEpisode = titleArray[1].Trim();

                    string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                    int seasonNumber;
                    int episodeNumber;

                    Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                    Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);

                    // Go through each video file extension
                    if (!IsShowWanted(showName))
                        return false;

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                        string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                        if (IsOnDisk(dir, fileMask))
                            return false;
                    }

                    if (IsSeasonIgnored(showName, seasonNumber))
                        return false;

                    if (IsInQueue(title, reportId))
                        return false;

                    if (InNzbArchive(title))
                        return false;

                    if (IsQueued(title))
                        return false;

                    return true;
                }

                if (titleArray.Length == 4)
                {
                    string showName;
                    string seasonEpisode;


                    if (titleArray[1].Contains("x"))
                    {
                        showName = titleArray[0].Trim();
                        seasonEpisode = titleArray[1].Trim();
                    }

                    else if (titleArray[2].Contains("x"))
                    {
                        showName = titleArray[0].Trim() + titleArray[1].Trim();
                        seasonEpisode = titleArray[2].Trim();
                    }

                    else
                    {
                        Log("Unsupported Title: {0}", title);
                        return false;
                    }

                    string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                    int seasonNumber;
                    int episodeNumber;

                    Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                    Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);

                    if (!IsShowWanted(showName))
                        return false;

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                        string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                        if (IsOnDisk(dir, fileMask))
                            return false;
                    }

                    if (IsSeasonIgnored(showName, seasonNumber))
                        return false;

                    if (IsInQueue(title, reportId))
                        return false;

                    if (InNzbArchive(title))
                        return false;

                    if (IsQueued(title))
                        return false;

                    return true;
                }


                //Daiy Episode
                if (titleArray.Length == 5)
                {
                    string showName = titleArray[0].Trim();
                    int year;
                    int month;
                    int day;

                    Int32.TryParse(titleArray[1], out year);
                    Int32.TryParse(titleArray[2], out month);
                    Int32.TryParse(titleArray[3], out day);

                    if (!IsShowWanted(showName))
                        return false;

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, year, month, day, tvDir);
                        string fileMask = GetEpisodeFileMask(year, month, day, tvDir);

                        if (IsOnDisk(dir, fileMask))
                            return false;
                    }

                    if (IsInQueue(title, reportId))
                        return false;

                    if (InNzbArchive(title))
                        return false;

                    if (IsQueued(title))
                        return false;

                    return true;
                }
            }
            catch (Exception e)
            {
                Log("Unsupported Title: {0} - {1}", title, e);
                return false;
            }

            Log("Unsupported Title: {0}", title);
            return false;
        }

        private bool IsEpisodeWanted(string title, string nzbId)
        {
            Log("----------------------------------------------------------------");
            Log("Verifying '{0}'", title);

            try
            {
                if (title.Length > 80)
                {
                    title = title.Substring(0, 79);
                }
                string[] titleSplitX = null;

                string patternMulti = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))[Ee](?<EpisodeTwo>(?:\d{1,2}))";
                string pattern = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";
                string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
                string patternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

                //Check for S01E01E02
                Match titleMatchMulti = Regex.Match(title, patternMulti);

                if (titleMatchMulti.Success)
                {
                    string[] titleSplitMulti = Regex.Split(title, patternMulti);
                    string showName = titleSplitMulti[0].Replace('.', ' ');
                    showName = showName.TrimEnd();
                    showName = ShowAlias(showName);

                    int seasonNumber = 0;
                    int episodeNumberOne = 0;
                    int episodeNumberTwo = 0;

                    Int32.TryParse(titleMatchMulti.Groups["Season"].Value, out seasonNumber);
                    Int32.TryParse(titleMatchMulti.Groups["EpisodeOne"].Value, out episodeNumberOne);
                    Int32.TryParse(titleMatchMulti.Groups["EpisodeTwo"].Value, out episodeNumberTwo);

                    if (!IsShowWanted(showName))
                        return false;

                    if (IsSeasonIgnored(showName, seasonNumber))
                        return false;

                    if (!IsQualityWanted(showName, title))
                        return false;

                    string episodeOneName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberOne);
                    string episodeTwoName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberTwo);
                    string titleFix = showName + " - " + seasonNumber + "x" + episodeNumberOne.ToString("D2") + "-" + seasonNumber + "x" + episodeNumberTwo.ToString("D2") + " - " + episodeOneName + " & " + episodeTwoName;

                    bool needProper = false;

                    if (_downloadPropers && title.Contains("PROPER"))
                    {
                        if (!IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                            needProper = true;
                    }


                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, seasonNumber, episodeNumberOne, tvDir);
                        string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumberOne, tvDir);

                        if (needProper)
                            DeleteForProper(dir, fileMask);

                        if (IsOnDisk(dir, fileMask))
                            return false;

                        if (IsOnDisk(dir, seasonNumber, episodeNumberOne))
                            return false;
                    }

                    if (IsInQueue(title, titleFix, nzbId))
                        return false;

                    if (InNzbArchive(title, titleFix))
                        return false;

                    if (IsQueued(titleFix))
                        return false;

                    return true;
                }

                //Check for S01E01
                Match titleMatch = Regex.Match(title, pattern);

                if (titleMatch.Success)
                {
                    string[] titleSplit = Regex.Split(title, pattern);
                    string showName = titleSplit[0].Replace('.', ' ');
                    showName = showName.TrimEnd();
                    showName = ShowAlias(showName);

                    int seasonNumber = 0;
                    int episodeNumber = 0;

                    Int32.TryParse(titleMatch.Groups["Season"].Value, out seasonNumber);
                    Int32.TryParse(titleMatch.Groups["Episode"].Value, out episodeNumber);

                    if (!IsShowWanted(showName))
                        return false;

                    string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
                    string titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;

                    bool needProper = false;

                    if (IsSeasonIgnored(showName, seasonNumber))
                        return false;

                    if (!IsQualityWanted(showName, title))
                        return false;

                    if (_downloadPropers && title.Contains("PROPER"))
                    {
                        if (!IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                            needProper = true;
                    }

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                        string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                        if (needProper)
                            DeleteForProper(dir, fileMask);

                        if (IsOnDisk(dir, fileMask))
                            return false;

                        if (IsOnDisk(dir, seasonNumber, episodeNumber))
                            return false;
                    }

                    if (IsInQueue(title, titleFix, nzbId))
                        return false;

                    if (InNzbArchive(title, titleFix))
                        return false;

                    if (IsQueued(titleFix))
                        return false;

                    return true;
                }

                //Check for 1x01
                Match titleMatchX = Regex.Match(title, patternX);

                if (titleMatchX.Success)
                {
                    titleSplitX = Regex.Split(title, patternX);
                    string showName = titleSplitX[0].Replace('.', ' ');
                    showName = showName.TrimEnd();
                    showName = ShowAlias(showName);

                    int seasonNumber = 0;
                    int episodeNumber = 0;

                    Int32.TryParse(titleMatchX.Groups["Season"].Value, out seasonNumber);
                    Int32.TryParse(titleMatchX.Groups["Episode"].Value, out episodeNumber);

                    if (!IsShowWanted(showName))
                        return false;

                    string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
                    string titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;

                    bool needProper = false;

                    if (IsSeasonIgnored(showName, seasonNumber))
                        return false;

                    if (!IsQualityWanted(showName, title))
                        return false;

                    if (_downloadPropers && title.Contains("PROPER"))
                    {
                        if (!IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                            needProper = true;
                    }

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                        string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                        if (needProper)
                            DeleteForProper(dir, fileMask);

                        if (IsOnDisk(dir, fileMask))
                            return false;

                        if (IsOnDisk(dir, seasonNumber, episodeNumber))
                            return false;
                    }

                    if (IsInQueue(title, titleFix, nzbId))
                        return false;

                    if (InNzbArchive(title, titleFix))
                        return false;

                    if (IsQueued(titleFix))
                        return false;

                    return true;
                }

                //Daily Show Title Check
                Match titleMatchDaily = Regex.Match(title, patternDaily);

                if (titleMatchDaily.Success)
                {
                    string[] titleSplitDaily = Regex.Split(title, patternDaily);
                    string showName = titleSplitDaily[0].Replace('.', ' ');
                    showName = showName.TrimEnd();
                    showName = ShowAlias(showName);

                    int year = 0;
                    int month = 0;
                    int day = 0;

                    Int32.TryParse(titleMatchDaily.Groups["Year"].Value, out year);
                    Int32.TryParse(titleMatchDaily.Groups["Month"].Value, out month);
                    Int32.TryParse(titleMatchDaily.Groups["Day"].Value, out day);

                    if (!IsShowWanted(showName))
                        return false;

                    string episodeName = TvDb.CheckTvDb(showName, year, month, day);
                    string titleFix = showName + " - " + year.ToString("D4") + "-" + month.ToString("D2") + "-" + day.ToString("D2") + " - " + episodeName;

                    bool needProper = false;

                    if (!IsQualityWanted(showName, title))
                        return false;

                    if (_downloadPropers && title.Contains("PROPER"))
                    {
                        if (!IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                            needProper = true;
                    }

                    foreach (var tvDir in Config.TvRootFolders)
                    {
                        string dir = GetEpisodeDir(showName, year, month, day, tvDir);
                        string fileMask = GetEpisodeFileMask(year, month, day, tvDir);

                        if (needProper)
                            DeleteForProper(dir, fileMask);

                        if (IsOnDisk(dir, fileMask))
                            return false;
                    }

                    if (IsInQueue(title, titleFix, nzbId))
                        return false;

                    if (InNzbArchive(title, titleFix))
                        return false;

                    if (IsQueued(titleFix))
                        return false;

                    return true;
                }
            }

            catch (Exception e)
            {
                Log("Unsupported Title: {0} - {1}", title, e);
                return false;
            }

            Log("Unsupported Title: {0}", title);
            return false;
        }

        private bool IsOnDisk(string dir, string fileMask)
        {
            if (!Directory.Exists(dir))
                return false;

            Log("Checking directory: {0} for [{1}]", dir, fileMask);

            foreach (var ext in Config.VideoExt)
            {
                var matchingFiles = Directory.GetFiles(dir, fileMask + ext);

                if (matchingFiles.Length != 0)
                {
                    Log("Episode on disk. '{0}'", true, matchingFiles[0]);
                    return true;
                }
            }
            return false;
        }

        private bool IsOnDisk(string dir, int seasonNumber, int episodeNumber)
        {
            if (!Directory.Exists(dir))
                return false;

            //Create list for formats (less code... I hope)
            List<string> formats = new List<string>();

            //Create Strings for addional searching for episodes and add to formats List
            formats.Add("*" + seasonNumber + "x" + episodeNumber.ToString("D2") + "*");
            formats.Add("*" + "S" + seasonNumber.ToString("D2") + "E" + episodeNumber.ToString("D2") + "*");
            formats.Add("*" + seasonNumber + episodeNumber.ToString("D2") + "*");

            foreach (var format in formats)
            {
                Log("Checking directory: {0} for [{1}]", dir, format);

                foreach (var ext in Config.VideoExt)
                {
                    var matchingFiles = Directory.GetFiles(dir, format + ext);

                    if (matchingFiles.Length != 0)
                    {
                        Log("Episode on disk. '{0}'", true, matchingFiles[0]);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsSeasonIgnored(string showName, int seasonNumber)
        {
            if (_ignoreSeasons.Contains(showName))
            {
                string[] showsSeasonIgnore = _ignoreSeasons.Trim(';', ' ').Split(';');
                foreach (string showSeasonIgnore in showsSeasonIgnore)
                {
                    if (Config.VerboseLogging)
                        Log("Checking Ignored Season for match: " + showSeasonIgnore);

                    string[] showNameIgnoreSplit = showSeasonIgnore.Split('=');
                    string showNameIgnore = showNameIgnoreSplit[0];
                    int seasonIgnore = Convert.ToInt32(showNameIgnoreSplit[1]);

                    if (showNameIgnore == showName)
                    {
                        if (seasonNumber <= seasonIgnore)
                        {
                            Log("Ignoring '{0}' Season '{1}'  ", showName, seasonNumber);
                            return true;
                        } //End if seasonNumber Less than or Equal to seasonIgnore
                    } //Ends if showNameIgnore equals showName
                } //Ends foreach loop for showsSeasonIgnore
            } //Ends if _ignoreSeasons contains showName
            return false; //If Show Name is not being ignored or that season is not ignored return false
        } //Ends IsSeasonIgnored

        private bool IsQualityWanted(string showName, string rssTitle)
        {
            foreach (var q in Config.ShowQualities)
            {
                if (showName.ToLower() == q.Name.ToLower())
                {
                    if (rssTitle.ToLower().Contains(q.Quality.ToLower()))
                    {
                        Log("Quality -{0}- is wanted for: {1}.", q.Quality, showName);
                        return true;
                    }
                    return false;
                }
            }

            foreach (var quality in _downloadQuality)
            {
                if (rssTitle.ToLower().Contains(quality.ToLower()))
                {
                    Log("Quality is wanted - Default");
                    return true;
                }
            }
            Log("Quality is not wanted");
            return false;
        }

        private bool IsInQueue(string rssTitle, Int64 reportId)
        {
            try
            {
                string queueRssUrl = String.Format(Config.SABRequest, "mode=queue&output=xml");
                string fetchName = String.Format("fetching msgid {0} from www.newzbin.com", reportId);

                XmlTextReader queueRssReader = new XmlTextReader(queueRssUrl);
                XmlDocument queueRssDoc = new XmlDocument();
                queueRssDoc.Load(queueRssReader);


                var queue = queueRssDoc.GetElementsByTagName(@"queue");
                var error = queueRssDoc.GetElementsByTagName(@"error");
                if (error.Count != 0)
                {
                    Log("Sab Queue Error: {0}", true, error[0].InnerText);
                }

                else if (queue.Count != 0)
                {
                    var slot = ((XmlElement)queue[0]).GetElementsByTagName("slot");

                    foreach (var s in slot)
                    {
                        XmlElement queueElement = (XmlElement)s;

                        //Queue is empty
                        if (String.IsNullOrEmpty(queueElement.InnerText))
                            return false;

                        string fileName = queueElement.GetElementsByTagName("filename")[0].InnerText.ToLower();

                        if (Config.VerboseLogging)
                            Log("Checking Queue Item for match: " + fileName);

                        if (fileName.ToLower() == CleanString(rssTitle).ToLower() || fileName == fetchName)
                        {
                            Log("Episode in queue - '{0}'", true, rssTitle);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("An Error has occurred while checking the queue. {0}", true, ex);
            }

            return false;
        } //Ends IsInQueue

        private bool IsInQueue(string rssTitle, string rssTitleFix, string nzbId)
        {
            try
            {
                Log("Checking Queue for: [{0}] or [{1}]", rssTitle, rssTitleFix);

                string queueRssUrl = String.Format(Config.SABRequest, "mode=queue&output=xml");

                XmlTextReader queueRssReader = new XmlTextReader(queueRssUrl);
                XmlDocument queueRssDoc = new XmlDocument();
                queueRssDoc.Load(queueRssReader);

                var queue = queueRssDoc.GetElementsByTagName(@"queue");
                var error = queueRssDoc.GetElementsByTagName(@"error");
                if (error.Count != 0)
                {
                    Log("Sab Queue Error: {0}", true, error[0].InnerText);
                }

                else if (queue.Count != 0)
                {
                    var slot = ((XmlElement)queue[0]).GetElementsByTagName("slot");

                    foreach (var s in slot)
                    {
                        XmlElement queueElement = (XmlElement)s;

                        //Queue is empty
                        if (String.IsNullOrEmpty(queueElement.InnerText))
                            return false;

                        string fileName = queueElement.GetElementsByTagName("filename")[0].InnerText.ToLower();

                        if (Config.VerboseLogging)
                            Log("Checking Queue Item for match: " + fileName);

                        if (fileName.ToLower() == CleanString(rssTitle).ToLower() || fileName.ToLower() == CleanString(rssTitleFix).ToLower() || fileName.ToLower().Contains(nzbId))
                        {
                            Log("Episode in queue - '{0}'", true, rssTitle);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("An Error has occurred while checking the queue. {0}", true, ex);
            }

            return false;
        } //Ends IsInQueue (Non-Newzbin)

        private bool IsQueued(string rssTitleFix)
        {
            //Checks Queued List for "Fixed" name, resolves issue when Item is added, but not properly renamed and it is found at another source.

            if (Queued.Contains(rssTitleFix + ": ok"))
                return true;

            return false;
        }

        private bool InNzbArchive(string rssTitle)
        {
            Log("Checking for Imported NZB for [{0}]", rssTitle);
            //return !File.Exists(_nzbDir + "\\" + rssTitle + ".nzb.gz");

            string nzbFileName = rssTitle.TrimEnd('.');
            nzbFileName = CleanString(nzbFileName);

            if (File.Exists(_nzbDir + "\\" + nzbFileName + ".nzb.gz"))
            {
                Log("Episode in archive: " + nzbFileName + ".nzb.gz", true);
                return true;
            }

            return false;
        }

        private bool InNzbArchive(string rssTitle, string rssTitleFix)
        {
            Log("Checking for Imported NZB for [{0}] or [{1}]", rssTitle, rssTitleFix);
            //return !File.Exists(_nzbDir + "\\" + rssTitle + ".nzb.gz");

            string nzbFileName = rssTitle.TrimEnd('.');
            nzbFileName = CleanString(nzbFileName);
            nzbFileName = nzbFileName.Replace('-', ' ');
            nzbFileName = nzbFileName.Replace('.', ' ');
            nzbFileName = nzbFileName.Replace('_', ' ');

            string nzbFileNameFix = rssTitleFix.TrimEnd('.');
            nzbFileNameFix = CleanString(nzbFileNameFix);

            foreach (var file in Directory.GetFiles(_nzbDir.ToString(), "*.nzb.gz"))
            {
                string foundFile = file.Replace(".nzb.gz", "");
                foundFile = foundFile.Replace('.', ' ');
                foundFile = foundFile.Replace('-', ' ');
                foundFile = foundFile.Replace('_', ' ');

                if (foundFile == _nzbDir.ToString().TrimEnd('\\').Replace('/', '\\') + "\\" + nzbFileName)
                {
                    Log("Episode in archive: '{0}'", true, nzbFileName + ".nzb.gz");
                    return true;
                }
            }

            if (File.Exists(_nzbDir + "\\" + nzbFileNameFix + ".nzb.gz"))
            {
                Log("Episode in archive: " + nzbFileName + ".nzb.gz", true);
                return true;
            }

            return false;
        }

        private string AddToQueue(Int64 reportId)
        {
            string nzbFileDownload = String.Format(Config.SABRequest, "mode=addid&name=" + reportId);
            Log("Adding report [{0}] to the queue.", reportId);
            WebClient client = new WebClient();
            string response = client.DownloadString(nzbFileDownload).Replace("\n", String.Empty);
            Log("Queue Response: [{0}]", response);
            return response;
        } // Ends AddToQueue

        private string AddToQueue(string rssTitle, string downloadLink, string titleFix)
        {
            titleFix = CleanString(titleFix);
            titleFix = CleanUrlString(titleFix);
            string nzbFileDownload = String.Format(Config.SABRequest, "mode=addurl&name=" + downloadLink + "&cat=tv&nzbname=" + titleFix);
            Log("DEBUG: " + nzbFileDownload);
            Log("Adding report [{0}] to the queue.", rssTitle);
            WebClient client = new WebClient();
            string response = client.DownloadString(nzbFileDownload).Replace("\n", String.Empty);
            Log("Queue Response: [{0}]", response);
            return response;
        } //Ends AddToQueue (Non-Newzbin)

        private string ShowAlias(string showName)
        {
            foreach (ShowAlias alias in Config.ShowAliases)
            {
                if (Config.VerboseLogging)
                    Log("Checking for alias: " + alias.BadName);

                if (showName.ToLower() == alias.BadName.ToLower())
                {
                    showName = alias.Alias;

                    if (Config.VerboseLogging)
                        Log("Alias found, new name is: " + showName);
                }
            }

            var patternYear = @"\s(?<Year>\d{4}\z)";
            var replaceYear = @" (${Year})";
            showName = Regex.Replace(showName, patternYear, replaceYear);

            var patternCountry = @"\s(?<Country>[A-Z]{2}\z)";
            var replaceCountry = @" (${Country})";
            showName = Regex.Replace(showName, patternCountry, replaceCountry);

            return showName;
        }

        private string GetTitleFix(string title)
        {
            if (Config.VerboseLogging)
                Log("Getting Fixed Title for: " + title);

            string titleFix = null;

            string[] titleSplitMulti = null;
            string[] titleSplit = null;
            string[] titleSplitX = null;
            string[] titleSplitDaily = null;

            string patternMulti = @"S(?<Season>(?:\d{1,2}))E(?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string pattern = @"S(?<Season>(?:\d{1,2}))E(?<Episode>(?:\d{1,2}))";
            string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
            string patternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

            //S01E01E02
            Match titleMatchMulti = Regex.Match(title, patternMulti);

            if (titleMatchMulti.Success)
            {
                titleSplitMulti = Regex.Split(title, patternMulti);
                string showName = titleSplitMulti[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber = 0;
                int episodeNumberOne = 0;
                int episodeNumberTwo = 0;

                Int32.TryParse(titleMatchMulti.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchMulti.Groups["EpisodeOne"].Value, out episodeNumberOne);
                Int32.TryParse(titleMatchMulti.Groups["EpisodeTwo"].Value, out episodeNumberTwo);

                string episodeOneName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberOne);
                string episodeTwoName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberTwo);
                titleFix = showName + " - " + seasonNumber + "x" + episodeNumberOne.ToString("D2") + "-" +
                                  seasonNumber + "x" + episodeNumberTwo.ToString("D2") + " - " + episodeOneName +
                                  " & " + episodeTwoName;
            }

            //S01E01
            Match titleMatch = Regex.Match(title, pattern);

            if (titleMatch.Success)
            {
                titleSplit = Regex.Split(title, pattern);
                string showName = titleSplit[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber = 0;
                int episodeNumber = 0;

                Int32.TryParse(titleMatch.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatch.Groups["Episode"].Value, out episodeNumber);

                string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
                titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;

            }

            //1x01
            Match titleMatchX = Regex.Match(title, patternX);

            if (titleMatchX.Success)
            {
                titleSplitX = Regex.Split(title, patternX);
                string showName = titleSplitX[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber = 0;
                int episodeNumber = 0;

                Int32.TryParse(titleMatchX.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchX.Groups["Episode"].Value, out episodeNumber);

                string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
                titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;
            }

            //Daily Show Title
            Match titleMatchDaily = Regex.Match(title, patternDaily);

            if (titleMatchDaily.Success)
            {
                titleSplitDaily = Regex.Split(title, patternDaily);
                string showName = titleSplitDaily[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int year = 0;
                int month = 0;
                int day = 0;

                Int32.TryParse(titleMatchDaily.Groups["Year"].Value, out year);
                Int32.TryParse(titleMatchDaily.Groups["Month"].Value, out month);
                Int32.TryParse(titleMatchDaily.Groups["Day"].Value, out day);

                string episodeName = TvDb.CheckTvDb(showName, year, month, day);
                titleFix = showName + " - " + year.ToString("D4") + "-" + month.ToString("D2") + "-" + day.ToString("D2") + " - " + episodeName;

            }
            if (Config.VerboseLogging)
                Log("Title Fix is: " + titleFix);

            return titleFix;
        }

        private void DeleteForProper(string dir, string fileMask)
        {
            //Delete old download to make room for proper!

            if (!Directory.Exists(dir))
                return;

            foreach (var ext in Config.VideoExt)
            {
                var matchingFiles = Directory.GetFiles(dir, fileMask + ext);

                if (matchingFiles.Length != 0)
                {
                    //Delete Matching File(s)
                    foreach (var m in matchingFiles)
                    {
                        Log("Deleting Episode on Disk for PROPER: " + m, true);
                        File.Delete(m);
                    }
                }
            }
        }

        internal void Log(string message)
        {
            _logger.Log(message);
        }

        internal void Log(string message, params object[] para)
        {
            Log(String.Format(message, para));
        }

        internal void Log(string message, bool showInSummary)
        {
            if (showInSummary) Summary.Add(message);
            Log(message);
        }

        internal void Log(string message, bool showInSummary, params object[] para)
        {
            Log(String.Format(message, para), showInSummary);
        }
    }
}
