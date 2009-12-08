using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Net;


namespace Downloader
{
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

        static void Main(string[] args)
        {
            //string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Get logDir from app.config
            //string tvDir = ConfigurationSettings.AppSettings["tvDir"].ToString(); //Get tvDir from app.config
            //string nzbDir = ConfigurationSettings.AppSettings["nzbDir"].ToString(); //Get nzbDir from app.config
            //string sabnzbdInfo = ConfigurationSettings.AppSettings["sabnzbdInfo"].ToString(); //Get sabnzbdInfo from app.config
            //string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString(); //Get apiKey from app.config
            //string rssUrl = ConfigurationSettings.AppSettings["rssUrl"].ToString(); //Get rssUrl from app.config
            //string ignoreSeasons = ConfigurationSettings.AppSettings["ignoreSeasons"].ToString(); //Get rssUrl from app.config
            //string priority = ConfigurationSettings.AppSettings["priority"].ToString(); //Get rssUrl from app.config

            string queueRssUrl = "http://" + sabnzbdInfo + "/api?mode=queue&output=xml&apikey=" + apiKey;

            XmlTextReader rssReader;
            XmlDocument rssDoc;
            XmlNode nodeRss = null;
            XmlNode nodeChannel = null;
            XmlNode nodeItem = null;

            bool showWanted = false;
            bool seasonWanted = false;
            bool neededEpisode = false;
            bool isQueued = true;
            bool nzbInArchive = true;
            bool addedToQueue = false;

            string showName = null;
            string showNameReplace = null;
            string showFolderReplace = null;
            string seasonEpisode = null;
            int seasonNumber = 0;
            int episodeNumber = 0;
            string reportId = null;
            string nzbFileDownload = null;
            

            string[] wantedShowNames = Directory.GetDirectories(tvDir);
            int numberOfShows = wantedShowNames.Length;
            Console.WriteLine("Watching " + numberOfShows + " shows");

            rssReader = new XmlTextReader(rssUrl);
            rssDoc = new XmlDocument();

            rssDoc.Load(rssReader);

            //Loop for the <rss> tag
            for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
            {
                //If it is the rss tag
                if (rssDoc.ChildNodes[i].Name == "rss")
                {
                    //rss tag found
                    nodeRss = rssDoc.ChildNodes[i];
                    //Console.WriteLine("RSS FOUND"); 
                }
            }

            //Loop for the <channel> tag
            for (int i = 0; i < nodeRss.ChildNodes.Count; i++)
            {
                //If is it the channel tag
                if (nodeRss.ChildNodes[i].Name == "channel")
                {
                    //Channel tag found
                    nodeChannel = nodeRss.ChildNodes[i];
                    //Console.WriteLine("Channel FOUND");
                }
            }

            //Loop for Title, ReportID
            for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
            {
                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    nodeItem = nodeChannel.ChildNodes[i];
                    string rssTitle = nodeItem["title"].InnerText;
                    string[] titleArray = rssTitle.Split('-');
                    if (titleArray.Length == 3)
                    {
                        showName = titleArray[0].Trim();
                        showNameReplace = showName.Replace(':', '-');
                        seasonEpisode = titleArray[1].Trim();
                        string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                        seasonNumber = Convert.ToInt32(seasonEpisodeSplit[0]);
                        episodeNumber = Convert.ToInt32(seasonEpisodeSplit[1]);
                    }

                    else
                    {
                        showName = "Garbage";
                        seasonNumber = 0;
                        episodeNumber = 0;
                    }
                    //Console.WriteLine(showName);
                    ////Get TV Show Folder Name
                    //string[] showNamingScheme = null;

                    ////Console.WriteLine(showName);
                    ////Console.WriteLine(seasonNumber);
                    ////Console.WriteLine(episodeNumber);

                    //showNamingScheme = GetShowNamingScheme(showName, seasonNumber, episodeNumber);
                    //string tvShowFolder = showNamingScheme[0];
                    //string tvSeasonFolder = showNamingScheme[1];
                    //string tvFilename = showNamingScheme[2];

                    //showFolderReplace = tvShowFolder.Replace(':', '-');

                    //Console.WriteLine(tvSeasonFolder);
                    //Console.WriteLine(tvShowFolder);
                    ////Console.WriteLine(wantedShowNamePath);
                    //Console.WriteLine(showFolderReplace);

                    

                    //Is this Show + Season + Episode Needed?
                    foreach (string wantedShowNamePath in wantedShowNames)
                    {
                        //Is Show Wanted?
                        showWanted = IsShowWanted(showNameReplace, wantedShowNamePath);

                        //Is Season Wanted?
                        if (showWanted == true)
                        {
                            seasonWanted = IsSeasonIgnored(showName, seasonNumber);
                            //Season wanted here! Redo Season Ignore Check First
                        }

                        //Is Episode Needed?
                        if (seasonWanted == true)
                        {
                            //neededEpisode = IsEpisodeNeeded(tvDir, tvShowFolder, tvSeasonFolder, tvFilename);
                            neededEpisode = IsEpisodeNeeded(tvDir, showName, seasonNumber, episodeNumber);
                        }




                        //Check Queue for pending download
                        //if (neededEpisode == true)
                        //{
                        //    isQueued = IsInQueue(rssTitle);
                        //}

                        ////Check NZB Imported dir (Possible failed extraction)
                        //if (isQueued == false)
                        //{
                        //    nzbInArchive = InNzbArchive(rssTitle);
                        //}


                        //if (seasonWanted == true && neededEpisode == true && isQueued == false && nzbInArchive == false)
                        //{
                        //    reportId = nodeItem["report:id"].InnerText; //Get ReportID
                        //    addedToQueue = AddToQueue(reportId);
                        //}

                        
                    } //Ends foreach wantedShowName in wantedShowNames
                }
            } //Ends Loop for title
            Console.ReadKey(); //Wait for User Input before Exit (Testing Only)
        } //Ends Public Static Void Main

        private static string[] GetShowNamingScheme(string showName, int seasonNumber, int episodeNumber)
        {
            string tvShowFolder = null;
            string tvSeasonFolder = null;
            string tvFilename = null;

            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');
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

            tvSorting = tvSorting.Replace(".%ext", "");
            tvSorting = tvSorting.Replace("%en", "");
            tvSorting = tvSorting.Replace("%e.n", "");
            tvSorting = tvSorting.Replace("%e_n", "");
            tvSorting = tvSorting.Replace("%sn", snReplace);
            tvSorting = tvSorting.Replace("%s.n", sDotNReplace);
            tvSorting = tvSorting.Replace("%s_n", sUnderNReplace);
            tvSorting = tvSorting.Replace("%0s", zeroSReplace);
            tvSorting = tvSorting.Replace("%s", sReplace);
            tvSorting = tvSorting.Replace("%0e", zeroEReplace);
            tvSorting = tvSorting.Replace("%e", eReplace);

            if (tvSorting.Contains("/"))
            {
                string[] tvSortingSplit = tvSorting.Split('/');

                if (tvSortingSplit.Length == 3)
                {
                    tvShowFolder = tvSortingSplit[0];
                    tvSeasonFolder = tvSortingSplit[1];
                    tvFilename = tvSortingSplit[2];
                }
            }

            else
            {
                string[] tvSortingSplit = tvSorting.Split('\\');

                if (tvSortingSplit.Length == 3)
                {
                    tvShowFolder = tvSortingSplit[0];
                    tvSeasonFolder = tvSortingSplit[1];
                    tvFilename = tvSortingSplit[2];
                }
            }

            //Console.WriteLine(tvShowFolder);
            string[] returnValues = new string[] {tvShowFolder, tvSeasonFolder, tvFilename};

            return returnValues;
        }

        private static bool IsShowWanted(string showNameReplace, string wantedShowNamePath)
        {
            //Console.WriteLine(showNameReplace);
            string wantedShowName = Path.GetFileName(wantedShowNamePath);
            if (showNameReplace == wantedShowName)
            {
                Console.WriteLine("WANTED: " + showNameReplace);
                return true;
            } //Ends if show is wanted
            return false;
        } //Ends IsShowWanted

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
                            Console.WriteLine("Ignoring this Season!");
                            return false;
                        } //End if seasonNumber Less than or Equal to seasonIgnore
                    } //Ends if showNameIgnore equals showName
                } //Ends foreach loop for showsSeasonIgnore
            } //Ends if ignoreSeasons contains showName
            Console.WriteLine("Not Ignoring this Season");
            return true; //If Show Name is not being ignored or that season is not ignored return false
        } //Ends IsSeasonIgnored

        private static bool IsEpisodeNeeded(string tvDir, string showName, int seasonNumber, int episodeNumber)
        {
            //string showNameSeasonDir = tvDir + "\\" + tvSeasonFolder + "\\" + tvSeasonFolder;

            string showNameSeasonDir = tvDir + "\\" + showName + "\\Season 0" + seasonNumber;
            string nameOnDisk = showName + " - S0" + seasonNumber + "E" + episodeNumber;

            //string nameOnDisk = showName + " - S" + seasonNumber + "E" + episodeNumber;
            //string nameOnDisk = tvFilename;

            //Console.WriteLine(tvDir);
            //Console.WriteLine(showName);
            Console.WriteLine(showNameSeasonDir);

            //Console.WriteLine(tvDir);
            //Console.WriteLine(showName);
            //Console.WriteLine(wantedShowNamePath);

            //Console.WriteLine(tvDir + "\\" + showName);

            if (Directory.Exists(showNameSeasonDir)) //Determine if Season XX Folder exists
            {
                Console.WriteLine("Season folder found");
                string[] episodesOnDisk = Directory.GetFiles(showNameSeasonDir);

                if (episodesOnDisk.Length == 0) //If Season folder contains no files
                {
                    Console.WriteLine("Episode is missing");
                    return true; //Set wantedEpisode to true
                }

                else //If Season folder has files
                {
                    foreach (string episodeOnDiskPath in episodesOnDisk) //Loop through Files in Folder
                    {
                        string episodeOnDisk = Path.GetFileNameWithoutExtension(episodeOnDiskPath);
                        Console.WriteLine(episodeOnDisk);

                        if (!episodeOnDisk.Contains(nameOnDisk)) //If Filename is NOT found then download
                        {
                            Console.WriteLine("Episode is missing");
                            return true;
                        }
                    }
                }
            }


            else //If Season XX folder does not exist - Possibly a New Season
            {
                Console.WriteLine("Season Folder Does Not Exist");
                return true; //Set wantedEpisode to true
            }
            return false;
        }

        private static bool IsInQueue(string rssTitle)
        {
            string queueRssUrl = "http://" + sabnzbdInfo + "/api?mode=queue&output=xml&apikey=" + apiKey;

            //Check Queue for pending download

            XmlTextReader queueRssReader;
            XmlDocument queueRssDoc = null;
            XmlNode nodeQueue = null;
            XmlNode nodeSlots = null;
            XmlNode nodeSlot = null;

            queueRssReader = new XmlTextReader(queueRssUrl);
            queueRssDoc = new XmlDocument();
            queueRssDoc.Load(queueRssReader);

            //Loop for the <queue> tag
            for (int i = 0; i < queueRssDoc.ChildNodes.Count; i++)
            {
                //If it is the queue tag
                if (queueRssDoc.ChildNodes[i].Name == "queue")
                {
                    //queue tag found
                    nodeQueue = queueRssDoc.ChildNodes[i];
                    //Console.WriteLine("Queue FOUND");
                }
            }

            //Loop for the <slots> tag
            for (int i = 0; i < nodeQueue.ChildNodes.Count; i++)
            {
                //If is it the slots tag
                if (nodeQueue.ChildNodes[i].Name == "slots")
                {
                    //Slots tag found
                    nodeSlots = nodeQueue.ChildNodes[i];
                    //Console.WriteLine("Slots FOUND");
                }
            }

            //Loop for filename
            for (int i = 0; i < nodeSlots.ChildNodes.Count; i++)
            {
                if (nodeSlots.ChildNodes[i].Name == "slot")
                {
                    nodeSlot = nodeSlots.ChildNodes[i];
                    string queueFilename = nodeSlot["filename"].InnerText;
                    //Console.WriteLine(queueFilename);

                    if (queueFilename == rssTitle)
                    {
                        Console.WriteLine("Already in queue!");
                        return true;
                    }
                }
            }
            Console.WriteLine("Episode not in Queue");
            return true;
        } //Ends IsInQueue

        private static bool InNzbArchive(string rssTitle)
        {
            Console.WriteLine("Checking for Imported NZB");
            string[] nzbsOnDisk = Directory.GetFiles(nzbDir, "*.nzb.gz");

            foreach (string nzbOnDiskPath in nzbsOnDisk) //Loop through NZBs in Folder
            {
                string nzbOnDisk = Path.GetFileNameWithoutExtension(nzbOnDiskPath);
                //Console.WriteLine(nzbOnDisk);
                //string rssTitleNzb = rssTitle + ".nzb";
                if (!nzbOnDisk.Contains(rssTitle)) //If Filename is NOT found then download
                {
                    Console.WriteLine("NZB File Not Found");
                    return false;
                }
            }
            return true;
        }

        private static bool AddToQueue(string reportId)
        {
            Console.WriteLine("No Files found: download");
            string nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&priority=" + priority + "&apikey=" + apiKey; //Create URL String
            Console.WriteLine(nzbFileDownload);
            //Send Newzbin Report to SABnzbd
            HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload); //Create Request
            HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse(); //Execute Request
            addNzbResponse.Close();
            Console.WriteLine("Adding to Queue!");
            return true;
        } // Ends AddToQueue

    } //Ends Class
} //Ends Namespace
