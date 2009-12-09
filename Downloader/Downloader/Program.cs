using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Net;
using Rss;

namespace Downloader
{
    internal struct Report
    {
        public Int64 ReportId;
        public string Title;
    }

    class Program
    {
        private static string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Get logDir from app.config
        private static string tvDir = ConfigurationSettings.AppSettings["tvDir"].ToString(); //Get tvDir from app.config
        private static string nzbDir = ConfigurationSettings.AppSettings["nzbDir"].ToString(); //Get nzbDir from app.config
        private static string sabnzbdInfo = ConfigurationSettings.AppSettings["sabnzbdInfo"].ToString(); //Get sabnzbdInfo from app.config
        private static string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString(); //Get apiKey from app.config
        private static string rssUrl = ConfigurationSettings.AppSettings["rssUrl"].ToString(); //Get rssUrl from app.config
        private static string ignoreSeasons = ConfigurationSettings.AppSettings["ignoreSeasons"].ToString(); //Get rssUrl from app.config
        private static string priority = ConfigurationSettings.AppSettings["priority"].ToString(); //Get rssUrl from app.config
        private static string tvSorting = ConfigurationSettings.AppSettings["tvSorting"].ToString(); //Get tvSorting from app.config
        private static string tvDailySorting = ConfigurationSettings.AppSettings["tvDailySorting"].ToString(); //Get tvDailySorting from app.config
        private static string videoExtConfig = ConfigurationSettings.AppSettings["videoExt"].ToString(); //Get videoExt from app.config
        private static string username = ConfigurationSettings.AppSettings["username"].ToString(); //Get rssUrl from app.config
        private static string password = ConfigurationSettings.AppSettings["password"].ToString(); //Get tvSorting from app.config

        private static string[] videoExt = videoExtConfig.Split(';');

        private static List<Report> GetReports()
        {
            Console.WriteLine("Downloading feed from {0}", rssUrl);

            RssFeed feed = RssFeed.Read(rssUrl);
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
                    Console.WriteLine("{0}:{1}", currentReport.ReportId, currentReport.Title);
                }
                else
                {
                    Console.WriteLine("Skipping {0}", item.Title);
                }
            }

            Console.WriteLine(Environment.NewLine + "Download Completed. Total of {0} reports found", reports.Count);
            return reports;
        }


        static void Main()
        {

            List<Report> reports = GetReports();

            Console.WriteLine("Watching {0} shows", Directory.GetDirectories(tvDir).Length);
            Console.WriteLine("ignoreSeasons: {0}", ignoreSeasons);

            foreach (var report in reports)
            {
                if (IsEpisodeWannted(report.Title, report.ReportId))
                {
                    AddToQueue(report.ReportId);
                }
            }

            //Thread.Sleep(10000);
            Console.ReadKey();
        } //Ends Main Method

        private static string GetShowNamingScheme(string showName, int seasonNumber, int episodeNumber, string episodeName)
        {
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

            //Console.WriteLine("Show Name: " + snReplace);
            //Console.WriteLine("Show.Name: " + sDotNReplace);
            //Console.WriteLine("Show_Name: " + sUnderNReplace);
            //Console.WriteLine("0 Season: " + zeroSReplace);
            //Console.WriteLine("Season: " + sReplace);
            //Console.WriteLine("0 Episode: " + zeroEReplace);
            //Console.WriteLine("Episode: " + eReplace);

            string tvSortingRename = tvSorting;

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
        } //Ends GetShowNamingScheme method

        private static string GetDailyShowNamingScheme(string showName, int year, int month, int day, string episodeName)
        {


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
            //string decadeReplace = 
            //string decaseZeroReplace =

            //Console.WriteLine("Show Name: " + snReplace);
            //Console.WriteLine("Show.Name: " + sDotNReplace);
            //Console.WriteLine("Show_Name: " + sUnderNReplace);
            //Console.WriteLine("0 Season: " + zeroSReplace);
            //Console.WriteLine("Season: " + sReplace);
            //Console.WriteLine("0 Episode: " + zeroEReplace);
            //Console.WriteLine("Episode: " + eReplace);

            string tvDailySortingRename = tvDailySorting;

            tvDailySortingRename = tvDailySortingRename.Replace(".%ext", "");
            tvDailySortingRename = tvDailySortingRename.Replace("%desc", "");
            tvDailySortingRename = tvDailySortingRename.Replace("%.desc", "");
            tvDailySortingRename = tvDailySortingRename.Replace("%_desc", "");
            tvDailySortingRename = tvDailySortingRename.Replace("%y", yearReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%t", tReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%.t", dotTReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%_t", underTReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%0s", zeroMReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%s", mReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%0e", zeroDReplace);
            tvDailySortingRename = tvDailySortingRename.Replace("%e", dReplace);

            return tvDailySortingRename;
        } //Ends GetDailyShowNamingScheme


        private static bool IsShowWanted(string wantedShowName)
        {


            string[] wantedShowNames = Directory.GetDirectories(tvDir);

            foreach (var item in wantedShowNames)
            {
                DirectoryInfo di = new DirectoryInfo(item);

                if (di.Name.ToLower() == wantedShowName.ToLower())
                {
                    return true;
                }
            }
            Console.WriteLine("'{0}' is not being watched.", wantedShowName);
            return false;
        } //Ends IsShowWanted

        private static bool IsEpisodeWannted(string title, Int64 reportId)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Verifying '{0}'", title);
            string[] titleArray = title.Split('-');

            if (titleArray.Length == 3)
            {
                string showName = titleArray[0].Trim();

                string seasonEpisode = titleArray[1].Trim();
                string episodeName = titleArray[2].Trim();

                string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                int seasonNumber = 0;
                int episodeNumber;

                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);

                string path = GetShowNamingScheme(showName, seasonNumber, episodeNumber, episodeName);

                foreach (var s in videoExt)
                {
                    if (File.Exists(path + s))
                    {
                        Console.WriteLine("Episode is in disk. '{0}'", path + s);
                        return false;
                    }

                }

                if (IsSeasonIgnored(showName, seasonNumber))
                    return false;

                if (!IsShowWanted(showName))
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
                    Console.WriteLine("Unsupported Title: {0}", title);
                    return false;
                }

                string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                int seasonNumber = 0;
                int episodeNumber;

                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);

                string path = GetShowNamingScheme(showName, seasonNumber, episodeNumber, episodeName);

                foreach (var s in videoExt)
                {
                    if (File.Exists(path + s))
                    {
                        Console.WriteLine("Episode is in disk. '{0}'", path + s);
                        return false;
                    }

                }

                if (IsSeasonIgnored(showName, seasonNumber))
                    return false;

                if (!IsShowWanted(showName))
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
                int year = 2000;
                int month = 12;
                int day = 24;

                Int32.TryParse(titleArray[1], out year);
                Int32.TryParse(titleArray[2], out month);
                Int32.TryParse(titleArray[3], out day);
                
                string episodeName = titleArray[4].Trim();

                string path = GetDailyShowNamingScheme(showName, year, month, day, episodeName);

                foreach (var s in videoExt)
                {
                    if (File.Exists(path + s))
                    {
                        Console.WriteLine("Episode is in disk. '{0}'", path + s);
                        return false;
                    }

                }

                if (!IsShowWanted(showName))
                    return false;

                if (IsInQueue(title, reportId))
                    return false;

                if (InNzbArchive(title))
                    return false;

                return true;

            }

            Console.WriteLine("Unsupported Title: {0}", title);
            return false;
        }

        private static bool IsSeasonIgnored(string showName, int seasonNumber)
        {
            if (ignoreSeasons.Contains(showName))
            {
                string[] showsSeasonIgnore = ignoreSeasons.Split(';');
                foreach (string showSeasonIgnore in showsSeasonIgnore)
                {
                    string[] showNameIgnoreSplit = showSeasonIgnore.Split('=');
                    string showNameIgnore = showNameIgnoreSplit[0];
                    int seasonIgnore = Convert.ToInt32(showNameIgnoreSplit[1]);

                    if (showNameIgnore == showName)
                    {
                        if (seasonNumber <= seasonIgnore)
                        {
                            Console.WriteLine("Ignoring '{0}' Season '{1}'  ", showName, seasonNumber);
                            return false;
                        } //End if seasonNumber Less than or Equal to seasonIgnore
                    } //Ends if showNameIgnore equals showName
                } //Ends foreach loop for showsSeasonIgnore
            } //Ends if ignoreSeasons contains showName
            return false; //If Show Name is not being ignored or that season is not ignored return false
        } //Ends IsSeasonIgnored

        private static bool IsInQueue(string rssTitle, Int64 reportId)
        {
            try
            {
                string queueRssUrl = "http://" + sabnzbdInfo + "/api?mode=queue&output=xml&apikey=" + apiKey + "&ma_username=" + username + "&ma_password=" + password;
                string fetchName = String.Format("fetching msgid {0} from www.newzbin.com", reportId);

                XmlTextReader queueRssReader = new XmlTextReader(queueRssUrl);
                XmlDocument queueRssDoc = new XmlDocument();
                queueRssDoc.Load(queueRssReader);

                var queue = (XmlElement)queueRssDoc.GetElementsByTagName(@"queue")[0];
                var slots = queue.GetElementsByTagName("slots");

                foreach (var s in slots)
                {
                    XmlElement queueElement = (XmlElement)s;

                    //Queue is empty
                    if (!String.IsNullOrEmpty(queueElement.InnerText))
                        return false;

                    string fileName = queueElement.GetElementsByTagName("filename")[0].InnerText.ToLower();
                    
                    if (fileName == rssTitle.ToLower() || fileName == fetchName)
                    {
                        Console.WriteLine("Already in queue - '{0}'", rssTitle);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error has occurred while checking the queue. {0}", ex);
            }

            return false;

        } //Ends IsInQueue

        private static bool InNzbArchive(string rssTitle)
        {
            Console.WriteLine("Checking for Imported NZB for [{0}]", rssTitle);
            //return !File.Exists(nzbDir + "\\" + rssTitle + ".nzb.gz");

            string nzbFileName = rssTitle.TrimEnd('.');

            if (File.Exists(nzbDir + "\\" + nzbFileName + ".nzb.gz"))
            {
                Console.WriteLine("NZB in already in archive: " + nzbFileName + ".nzb.gz");
                return true;
            }

            return false;
        }

        private static void AddToQueue(Int64 reportId)
        {
            string nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&priority=" + priority + "&apikey=" + apiKey + "&ma_username=" + username + "&ma_password=" + password; //Create URL String
            Console.WriteLine("Adding report [{0}] to the queueu.", reportId);
            //Send Newzbin Report to SABnzbd
            HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload); //Create Request
            HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse(); //Execute Request
            addNzbResponse.Close();
            Console.WriteLine("Report added to queue.");
        } // Ends AddToQueue
    }
}

//Ends Namespace
