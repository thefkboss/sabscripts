using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Rss;

namespace Downloader
{
    internal struct Report
    {
        public Int64 ReportId;
        public string Title;
    }

    internal class Program
    {
        private static DirectoryInfo _tvRoot;
        private static DirectoryInfo _nzbDir;
        private static string _rssUrl;
        private static string _ignoreSeasons;
        private static string _tvTemplate;
        private static string _tvDailyTemplate;
        private static string[] _videoExt;
        private static DirectoryInfo[] _wantedShowNames;
        private static bool _sabReplaceChars;
        private static string _sabRequest;
        private static readonly List<string> Queued = new List<string>();
        private static readonly List<string> Summary = new List<string>();

        private static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                LoadConfig();
                List<Report> reports = GetReports();

                Log("Watching {0} shows", _wantedShowNames.Length);
                Log("_ignoreSeasons: {0}", _ignoreSeasons);

                foreach (var report in reports)
                {
                    if (IsEpisodeWannted(report.Title, report.ReportId))
                    {
                        string queueResponse = AddToQueue(report.ReportId);
                        Queued.Add(report.Title + ": " + queueResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                Log(ex.ToString(), true);
            }


            sw.Stop();
            Log(Environment.NewLine + "=============================================================");
            Log("Process successfully completed. Duration {0:##.#}s", sw.Elapsed.TotalSeconds);

            foreach (var item in Queued)
            {
                Log("Queued for download: " + item);
            }
            Log("Number of reports added to the queue: " + Queued.Count);

            foreach (var logItem in Summary)
            {
                Log(logItem);
            }

            Thread.Sleep(10000);
        }

        private static void LoadConfig()
        {
            Log("Loading configuration...");

            _tvRoot = new DirectoryInfo(ConfigurationManager.AppSettings["tvRoot"]); //Get _tvRoot from app.config
            if (!_tvRoot.Exists)
                throw new ApplicationException("Invalid TV Root folder. " + _tvRoot);


            _wantedShowNames = _tvRoot.GetDirectories();
            _rssUrl = ConfigurationManager.AppSettings["rssUrl"]; //Get _rssUrl from app.config
            _ignoreSeasons = ConfigurationManager.AppSettings["ignoreSeasons"]; //Get _rssUrl from app.config

            _videoExt = ConfigurationManager.AppSettings["videoExt"].Split(';'); //Get _videoExt from app.config

            _tvTemplate = ConfigurationManager.AppSettings["tvTemplate"]; //Get _tvTemplate from app.config
            if (String.IsNullOrEmpty(_tvTemplate))
                throw new ApplicationException("Undefined tvTemplate");


            _tvDailyTemplate = ConfigurationManager.AppSettings["tvDailyTemplate"];
                //Get _tvDailyTemplate from app.config
            if (String.IsNullOrEmpty(_tvTemplate))
                throw new ApplicationException("tvDailyTemplate");

            _sabReplaceChars = Convert.ToBoolean(ConfigurationManager.AppSettings["sabReplaceChars"]);


            //Generate template for a sab request.
            string sabnzbdInfo = ConfigurationManager.AppSettings["sabnzbdInfo"]; //Get sabnzbdInfo from app.config
            string priority = ConfigurationManager.AppSettings["priority"]; //Get _rssUrl from app.config
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string username = ConfigurationManager.AppSettings["username"]; //Get _rssUrl from app.config
            string password = ConfigurationManager.AppSettings["password"]; //Get _tvTemplate from app.config
            _sabRequest =
                string.Format("http://{0}/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                              sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
                //Create URL String

            _nzbDir = new DirectoryInfo(ConfigurationManager.AppSettings["nzbDir"]); //Get _nzbDir from app.config
        }


        private static List<Report> GetReports()
        {
            Log("Downloading feed from {0}", _rssUrl);

            RssFeed feed = RssFeed.Read(_rssUrl);
            RssChannel channel = feed.Channels[0];
            List<Report> reports = new List<Report>();

            foreach (RssItem item in channel.Items)
            {
                Report currentReport = new Report();
                if (!item.Title.ToLower().Contains("passworded"))
                {
                    currentReport.Title = item.Title;
                    currentReport.ReportId = Convert.ToInt64(Regex.Match(item.Link.AbsolutePath, @"\d{7,10}").Value);
                    reports.Add(currentReport);
                    Log("{0}:{1}", currentReport.ReportId, currentReport.Title);
                }
                else
                {
                    Log("Skipping {0}", item.Title);
                }
            }

            Log(Environment.NewLine + "Download Completed. Total of {0} reports found", reports.Count);
            return reports;
        }


        private static string GetShowNamingScheme(string showName, int seasonNumber, int episodeNumber,
                                                  string episodeName)
        {
            showName = CleanString(showName);
            episodeName = CleanString(episodeName);

            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');

            string enReplace = episodeName;
            string enDotNReplace = episodeName.Replace(' ', '.');
            string esUnderNReplace = episodeName.Replace(' ', '_');

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

            string tvSortingRename = _tvTemplate;

            tvSortingRename = tvSortingRename.Replace(".%ext", "");
            tvSortingRename = tvSortingRename.Replace("%en", enReplace);
            tvSortingRename = tvSortingRename.Replace("%e.n", enDotNReplace);
            tvSortingRename = tvSortingRename.Replace("%e_n", esUnderNReplace);
            tvSortingRename = tvSortingRename.Replace("%sn", snReplace);
            tvSortingRename = tvSortingRename.Replace("%s.n", sDotNReplace);
            tvSortingRename = tvSortingRename.Replace("%s_n", sUnderNReplace);
            tvSortingRename = tvSortingRename.Replace("%0s", zeroSReplace);
            tvSortingRename = tvSortingRename.Replace("%s", sReplace);
            tvSortingRename = tvSortingRename.Replace("%0e", zeroEReplace);
            tvSortingRename = tvSortingRename.Replace("%e", eReplace);

            return tvSortingRename;
        }

        private static string GetDailyShowNamingScheme(string showName, int year, int month, int day, string episodeName)
        {
            showName = CleanString(showName);
            episodeName = CleanString(episodeName);

            string tReplace = showName;
            string dotTReplace = showName.Replace(' ', '.');
            string underTReplace = showName.Replace(' ', '_');
            string yearReplace = Convert.ToString(year);
            string zeroMReplace = String.Format("{0:00}", month);
            string mReplace = Convert.ToString(month);
            string zeroDReplace = String.Format("{0:00}", day);
            string dReplace = Convert.ToString(day);
            string descReplace = episodeName;
            string dotDescReplace = episodeName.Replace(' ', '.');
            string underDescReplace = episodeName.Replace(' ', '_');

            string tvDailySortingR = _tvDailyTemplate;

            tvDailySortingR = tvDailySortingR.Replace(".%ext", "");
            tvDailySortingR = tvDailySortingR.Replace("%desc", descReplace);
            tvDailySortingR = tvDailySortingR.Replace("%.desc", dotDescReplace);
            tvDailySortingR = tvDailySortingR.Replace("%_desc", underDescReplace);
            tvDailySortingR = tvDailySortingR.Replace("%t", tReplace);
            tvDailySortingR = tvDailySortingR.Replace("%.t", dotTReplace);
            tvDailySortingR = tvDailySortingR.Replace("%_t", underTReplace);
            tvDailySortingR = tvDailySortingR.Replace("%y", yearReplace);
            tvDailySortingR = tvDailySortingR.Replace("%0m", zeroMReplace);
            tvDailySortingR = tvDailySortingR.Replace("%m", mReplace);
            tvDailySortingR = tvDailySortingR.Replace("%0d", zeroDReplace);
            tvDailySortingR = tvDailySortingR.Replace("%d", dReplace);

            return tvDailySortingR;
        } //Ends GetDailyShowNamingScheme

        private static string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = {"\\", "/", "<", ">", "?", "*", ":", "|", "\""};
            string[] goodCharacters = {"+", "+", "{", "}", "!", "@", "-", "#", "`"};


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

        private static bool IsShowWanted(string wantedShowName)
        {
            foreach (var di in _wantedShowNames)
            {
                if (di.Name.ToLower() == CleanString(wantedShowName).ToLower())
                {
                    return true;
                }
            }
            Log("'{0}' is not being watched.", wantedShowName);
            return false;
        } //Ends IsShowWanted

        private static bool IsEpisodeWannted(string title, Int64 reportId)
        {
            Log("----------------------------------------------------------------");
            Log("Verifying '{0}'", title);
            string[] titleArray = title.Split('-');

            if (titleArray.Length == 3)
            {
                string showName = titleArray[0].Trim();

                string seasonEpisode = titleArray[1].Trim();
                string episodeName = titleArray[2].Trim();

                string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                int seasonNumber;
                int episodeNumber;

                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);

                string path = GetShowNamingScheme(showName, seasonNumber, episodeNumber, episodeName);
                path = _tvRoot + "\\" + path;


                // Go through each video file extension
                if (!IsShowWanted(showName))
                    return false;

                if (IsOnDisk(path))
                    return false;

                if (IsSeasonIgnored(showName, seasonNumber))
                    return false;

                if (IsInQueue(title, reportId))
                    return false;

                if (InNzbArchive(title))
                    return false;

                return true;
            }

            if (titleArray.Length == 4)
            {
                string showName;

                string seasonEpisode;
                string episodeName;

                if (titleArray[1].Contains("x"))
                {
                    showName = titleArray[0].Trim();
                    seasonEpisode = titleArray[1].Trim();
                    episodeName = titleArray[2].Trim() + titleArray[3].Trim();
                }

                else if (titleArray[2].Contains("x"))
                {
                    showName = titleArray[0].Trim() + titleArray[1].Trim();
                    seasonEpisode = titleArray[2].Trim();
                    episodeName = titleArray[3].Trim();
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

                string path = GetShowNamingScheme(showName, seasonNumber, episodeNumber, episodeName);
                path = _tvRoot + "\\" + path;

                if (!IsShowWanted(showName))
                    return false;

                if (IsOnDisk(path))
                    return false;

                if (IsSeasonIgnored(showName, seasonNumber))
                    return false;

                if (IsInQueue(title, reportId))
                    return false;

                if (InNzbArchive(title))
                    return false;

                return true;
            }

            if (titleArray.Length == 5)
            {
                string showName = titleArray[0].Trim();
                int year;
                int month;
                int day;

                Int32.TryParse(titleArray[1], out year);
                Int32.TryParse(titleArray[2], out month);
                Int32.TryParse(titleArray[3], out day);

                string episodeName = titleArray[4].Trim();

                string path = GetDailyShowNamingScheme(showName, year, month, day, episodeName);
                path = _tvRoot + "\\" + path;


                if (!IsShowWanted(showName))
                    return false;

                if (IsOnDisk(path))
                    return false;

                if (IsInQueue(title, reportId))
                    return false;

                if (InNzbArchive(title))
                    return false;

                return true;
            }

            Log("Unsupported Title: {0}", title);
            return false;
        }

        private static bool IsOnDisk(string path)
        {
            foreach (var s in _videoExt)
            {
                if (File.Exists(path + s))
                {
                    Log("Episode is in disk. '{0}'", true, path + s);
                    return true;
                }
            }
            return false;
        }

        private static bool IsSeasonIgnored(string showName, int seasonNumber)
        {
            if (_ignoreSeasons.Contains(showName))
            {
                string[] showsSeasonIgnore = _ignoreSeasons.Split(';');
                foreach (string showSeasonIgnore in showsSeasonIgnore)
                {
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

        private static bool IsInQueue(string rssTitle, Int64 reportId)
        {
            try
            {
                string queueRssUrl = String.Format(_sabRequest, "mode=queue&output=xml");
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
                    var slot = ((XmlElement) queue[0]).GetElementsByTagName("slot");

                    foreach (var s in slot)
                    {
                        XmlElement queueElement = (XmlElement) s;

                        //Queue is empty
                        if (String.IsNullOrEmpty(queueElement.InnerText))
                            return false;

                        string fileName = queueElement.GetElementsByTagName("filename")[0].InnerText.ToLower();

                        if (fileName == CleanString(rssTitle).ToLower() || fileName == fetchName)
                        {
                            Log("Already in queue - '{0}'", true, rssTitle);
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

        private static bool InNzbArchive(string rssTitle)
        {
            Log("Checking for Imported NZB for [{0}]", rssTitle);
            //return !File.Exists(_nzbDir + "\\" + rssTitle + ".nzb.gz");

            string nzbFileName = rssTitle.TrimEnd('.');

            if (File.Exists(_nzbDir + "\\" + nzbFileName + ".nzb.gz"))
            {
                Log("NZB in already in archive: " + nzbFileName + ".nzb.gz", true);
                return true;
            }

            return false;
        }

        private static string AddToQueue(Int64 reportId)
        {
            string nzbFileDownload = String.Format(_sabRequest, "mode=addid&name=" + reportId);
            Log("Adding report [{0}] to the queue.", reportId);
            WebClient client = new WebClient();
            string response = client.DownloadString(nzbFileDownload).Replace("\n", String.Empty);
            Log("Queue Response: [{0}]", response);
            return response;
        } // Ends AddToQueue


        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static void Log(string message, params object[] para)
        {
            Log(String.Format(message, para));
        }

        private static void Log(string message, bool showInSummary)
        {
            if (showInSummary) Summary.Add(message);
            Log(message);
        }

        private static void Log(string message, bool showInSummary, params object[] para)
        {
            Log(String.Format(message, para), showInSummary);
        }
    }
}