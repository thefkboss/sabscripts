using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Net;

namespace TestNamingScheme
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
            string queueRssUrl = "http://" + sabnzbdInfo + "/api?mode=queue&output=xml&apikey=" + apiKey;

            XmlTextReader rssReader;
            XmlDocument rssDoc;
            XmlNode nodeRss = null;
            XmlNode nodeChannel = null;
            XmlNode nodeItem = null;

            string[] wantedShowNames = Directory.GetDirectories(tvDir);
            int numberOfShows = wantedShowNames.Length;
            Console.WriteLine("Watching " + numberOfShows + " shows");

            rssReader = new XmlTextReader(rssUrl);
            rssDoc = new XmlDocument();

            rssDoc.Load(rssReader);

            bool showWanted = false;
            bool seasonWanted = false;
            bool neededEpisode = false;
            bool isNotQueued = false;
            bool nzbNotInArchive = false;

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
                string showName = null;
                string seasonEpisode = null;
                int seasonNumber = 0;
                int episodeNumber = 0;

                string tvShowFolder = null;
                string tvSeasonFolder = null;
                string tvFilename = null;
                string tvSortingArray = null;

                string reportId = null;

                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    nodeItem = nodeChannel.ChildNodes[i];
                    string rssTitle = nodeItem["title"].InnerText;
                    string[] titleArray = rssTitle.Split('-');


                    if (titleArray.Length == 3)
                    {
                        showName = titleArray[0].Trim();

                        seasonEpisode = titleArray[1].Trim();
                        string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                        seasonNumber = Convert.ToInt32(seasonEpisodeSplit[0]);
                        episodeNumber = Convert.ToInt32(seasonEpisodeSplit[1]);
                    }

                    else
                    {
                        showName = "Garbage Show";
                        seasonNumber = 0;
                        episodeNumber = 0;
                    }

                    tvSortingArray = GetShowNamingScheme(showName, seasonNumber, episodeNumber);
                    string[] tvSortingArraySplit = tvSortingArray.Split('\\');
                    tvShowFolder = tvSortingArraySplit[0];
                    tvSeasonFolder = tvSortingArraySplit[1];
                    tvFilename = tvSortingArraySplit[2];
                    tvShowFolder = tvShowFolder.Replace(':', '-');

                    //Console.WriteLine(tvShowFolder);

                    foreach (string wantedShowNamePath in wantedShowNames)
                    {
                        string wantedShowName = Path.GetFileName(wantedShowNamePath);

                        //Is Show Wanted?
                        showWanted = IsShowWanted(tvShowFolder, wantedShowName);

                        //Is Season Wanted?
                        if (showWanted == true)
                        {
                            seasonWanted = IsSeasonIgnored(showName, seasonNumber);
                        }

                        //Is Episode Needed?
                        if (showWanted == true && seasonWanted == true)
                        {
                            neededEpisode = IsEpisodeNeeded(tvDir, tvShowFolder, tvSeasonFolder, tvFilename);
                        }

                        //Check Queue for pending download
                        if (showWanted == true && neededEpisode == true)
                        {
                            isNotQueued = IsInQueue(rssTitle);
                        }

                        //Check NZB Imported dir (Possible failed extraction)
                        if (showWanted == true && isNotQueued == true)
                        {
                            nzbNotInArchive = InNzbArchive(rssTitle);
                        }

                        if (showWanted && seasonWanted && neededEpisode && isNotQueued && nzbNotInArchive)
                        {
                            reportId = nodeItem["report:id"].InnerText; //Get ReportID
                            AddToQueue(reportId);
                        }


                    } //Ends foreach wantedShowNamePath in wantedShowNames


                }//Ends if ChildNodes == item
            } //Ends Loop for Title, ReportID

            Console.ReadKey();
        } //Ends Main Method

        private static string GetShowNamingScheme(string showName, int seasonNumber, int episodeNumber)
        {
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

            string tvSortingRename = tvSorting;

            tvSortingRename = tvSortingRename.Replace(".%ext", "");
            tvSortingRename = tvSortingRename.Replace("%en", "");
            tvSortingRename = tvSortingRename.Replace("%e.n", "");
            tvSortingRename = tvSortingRename.Replace("%e_n", "");
            tvSortingRename = tvSortingRename.Replace("%sn", snReplace);
            tvSortingRename = tvSortingRename.Replace("%s.n", sDotNReplace);
            tvSortingRename = tvSortingRename.Replace("%s_n", sUnderNReplace);
            tvSortingRename = tvSortingRename.Replace("%0s", zeroSReplace);
            tvSortingRename = tvSortingRename.Replace("%s", sReplace);
            tvSortingRename = tvSortingRename.Replace("%0e", zeroEReplace);
            tvSortingRename = tvSortingRename.Replace("%e", eReplace);

            return tvSortingRename;
        } //Ends GetShowNamingScheme method

        private static bool IsShowWanted(string tvShowFolder, string wantedShowName)
        {
            if (tvShowFolder == wantedShowName)
            {
                Console.WriteLine("WANTED: " + tvShowFolder);
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

        private static bool IsEpisodeNeeded(string tvDir, string tvShowFolder, string tvSeasonFolder, string tvFilename)
        {
            //string showNameSeasonDir = tvDir + "\\" + tvSeasonFolder + "\\" + tvSeasonFolder;
            //string showNameSeasonDir = tvDir + "\\" + showName + "\\Season 0" + seasonNumber;
            string showNameSeasonDir = tvDir + "\\" + tvShowFolder + "\\" + tvSeasonFolder;
            string nameOnDisk = tvFilename;
            Console.WriteLine(showNameSeasonDir);

            //string nameOnDisk = showName + " - S" + seasonNumber + "E" + episodeNumber;
            //string nameOnDisk = tvFilename;

            if (Directory.Exists(showNameSeasonDir)) //Determine if Season XX Folder exists
            {
                Console.WriteLine("Season folder found");
                string[] episodesOnDisk = Directory.GetFiles(showNameSeasonDir);

                if (episodesOnDisk.Length == 0) //If Season folder contains no files
                {
                    Console.WriteLine("Episode is missing (no files found)");
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
            Console.WriteLine(rssTitle);
            Console.WriteLine("Episode not in Queue");
            return true;
        } //Ends IsInQueue

        private static bool InNzbArchive(string rssTitle)
        {
            Console.WriteLine("Checking for Imported NZB for [{0}]", rssTitle);
            //if (Directory.Exists(nzbDir + "\\" + rssTitle + ".nzb.gz"))
            //{
            //    return false;
            //}
            //return true;
            return !File.Exists(nzbDir + "\\" + rssTitle + ".nzb.gz");
        }

        private static bool AddToQueue(string reportId)
        {
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
