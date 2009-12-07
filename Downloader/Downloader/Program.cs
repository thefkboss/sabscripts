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
        static void Main(string[] args)
        {
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Get logDir from app.config
            string tvDir = ConfigurationSettings.AppSettings["tvDir"].ToString(); //Get tvDir from app.config
            string nzbDir = ConfigurationSettings.AppSettings["nzbDir"].ToString(); //Get nzbDir from app.config
            string sabnzbdInfo = ConfigurationSettings.AppSettings["sabnzbdInfo"].ToString(); //Get sabnzbdInfo from app.config
            string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString(); //Get apiKey from app.config
            string rssUrl = ConfigurationSettings.AppSettings["rssUrl"].ToString(); //Get rssUrl from app.config
            string ignoreSeasons = ConfigurationSettings.AppSettings["ignoreSeasons"].ToString(); //Get rssUrl from app.config
            string priority = ConfigurationSettings.AppSettings["priority"].ToString(); //Get rssUrl from app.config

            string queueRssUrl = "http://" + sabnzbdInfo + "/api?mode=queue&output=xml&apikey=" + apiKey;

            XmlTextReader rssReader;
            XmlDocument rssDoc;
            XmlNode nodeRss = null;
            XmlNode nodeChannel = null;
            XmlNode nodeItem = null;

            bool wantedSeason = true;
            bool wantedEpisode = false;
            bool notInQueue = true;
            bool notInNzbArchive = false;

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

                string showName = null;
                string seasonEpisode = null;
                int seasonNumberInt = 0;
                string seasonNumber = null;
                string episodeNumber = null;
                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    nodeItem = nodeChannel.ChildNodes[i];
                    string rssTitle = nodeItem["title"].InnerText;
                    string[] titleArray = rssTitle.Split('-');
                    if (titleArray.Length == 3)
                    {
                        showName = titleArray[0].Trim();
                        showName = showName.Replace(':', '-');
                        seasonEpisode = titleArray[1].Trim();
                        string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                        seasonNumberInt = Convert.ToInt32(seasonEpisodeSplit[0]);
                        episodeNumber = seasonEpisodeSplit[1];
                        if (seasonNumberInt < 10)
                        {
                            seasonNumber = "0" + seasonNumberInt;
                        }
                        else
                        {
                            seasonNumber = Convert.ToString(seasonNumberInt);
                        }
                    }

                    //Is Show Wanted?
                    foreach (string wantedShowNamePath in wantedShowNames)
                    {
                        string wantedShowName = Path.GetFileName(wantedShowNamePath);
                        if (showName == wantedShowName)
                        {
                            Console.WriteLine("WANTED: " + showName);
                            //Console.ReadKey();

                            //Ignoring These Seasons for this show

                            if (ignoreSeasons.Contains(showName))
                            {
                                Console.WriteLine("Checking for Ignore");
                                string[] showsSeasonIgnore = ignoreSeasons.Split(';');
                                foreach (string showSeasonIgnore in showsSeasonIgnore)
                                {
                                    string[] showNameIgnoreSplit = showSeasonIgnore.Split('=');
                                    string showNameIgnore = showNameIgnoreSplit[0];
                                    if (showNameIgnore == showName)
                                    {
                                        string ignoreSeasonsCount = showNameIgnoreSplit[1];
                                        string[] ignoreSeasonsCountSplit = ignoreSeasonsCount.Split(',');
                                        string seasonNumberShort = Convert.ToString(seasonNumberInt);

                                        foreach (string ignoreSeasonNumber in ignoreSeasonsCountSplit)
                                        {
                                            if (seasonNumberShort == ignoreSeasonNumber)
                                            {
                                                Console.WriteLine("Ignoring Season");
                                                wantedSeason = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not Ignoring Season");
                                            }
                                        }
                                    }
                                }
                            }

                            //Is Episode Needed?
                            if (wantedSeason == true)
                            {
                                string showNameSeasonDir = wantedShowNamePath + "\\Season " + seasonNumber;
                                string nameOnDisk = showName + " - S" + seasonNumber + "E" + episodeNumber;
                                Console.WriteLine(showNameSeasonDir);
                                if (Directory.Exists(showNameSeasonDir)) //Determine if Season XX Folder exists
                                {
                                    Console.WriteLine("Season folder found");
                                    string[] episodesOnDisk = Directory.GetFiles(showNameSeasonDir);

                                    if (episodesOnDisk.Length == 0) //If Season folder contains no files
                                    {
                                        wantedEpisode = true; //Set wantedEpisode to true
                                    }

                                    else //If Season folder has files
                                    {
                                        foreach (string episodeOnDiskPath in episodesOnDisk) //Loop through Files in Folder
                                        {
                                            string episodeOnDisk = Path.GetFileNameWithoutExtension(episodeOnDiskPath);
                                            Console.WriteLine(episodeOnDisk);

                                            if (!episodeOnDisk.Contains(nameOnDisk)) //If Filename is NOT found then download
                                            {
                                                wantedEpisode = true; //Set wantedEpisode to true
                                            }
                                        }
                                    }
                                }


                                else //If Season XX folder does not exist - Possibly a New Season
                                {
                                    wantedEpisode = true; //Set wantedEpisode to true
                                }
                            }

                            //Check Queue for pending download
                            if (wantedEpisode == true)
                            {
                                XmlTextReader queueRssReader;
                                XmlDocument queueRssDoc = null;
                                XmlNode nodeQueue = null;
                                XmlNode nodeSlots = null;
                                XmlNode nodeSlot = null;

                                queueRssReader = new XmlTextReader(queueRssUrl);
                                queueRssDoc = new XmlDocument();
                                queueRssDoc.Load(queueRssReader);

                                //Loop for the <queue> tag
                                for (int i2 = 0; i2 < queueRssDoc.ChildNodes.Count; i2++)
                                {
                                    //If it is the queue tag
                                    if (queueRssDoc.ChildNodes[i2].Name == "queue")
                                    {
                                        //queue tag found
                                        nodeQueue = queueRssDoc.ChildNodes[i2];
                                        //Console.WriteLine("Queue FOUND");
                                    }
                                }

                                //Loop for the <slots> tag
                                for (int i2 = 0; i2 < nodeQueue.ChildNodes.Count; i2++)
                                {
                                    //If is it the slots tag
                                    if (nodeQueue.ChildNodes[i2].Name == "slots")
                                    {
                                        //Slots tag found
                                        nodeSlots = nodeQueue.ChildNodes[i2];
                                        //Console.WriteLine("Slots FOUND");
                                    }
                                }

                                //Loop for filename
                                for (int i2 = 0; i2 < nodeSlots.ChildNodes.Count; i2++)
                                {
                                    if (nodeSlots.ChildNodes[i2].Name == "slot")
                                    {
                                        nodeSlot = nodeSlots.ChildNodes[i2];
                                        string queueFilename = nodeSlot["filename"].InnerText;
                                        //Console.WriteLine(queueFilename);

                                        if (queueFilename == rssTitle)
                                        {
                                            Console.WriteLine("Already in queue!");
                                            notInQueue = false;
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("Not in queue = " + notInQueue);
                            //Check NZB Imported dir (Possible failed extraction)
                            if (notInQueue == true)
                            {
                                Console.WriteLine("Checking for Imported NZB");
                                string[] nzbsOnDisk = Directory.GetFiles(nzbDir,"*.nzb.gz");

                                foreach (string nzbOnDiskPath in nzbsOnDisk) //Loop through NZBs in Folder
                                {
                                    string nzbOnDisk = Path.GetFileNameWithoutExtension(nzbOnDiskPath);
                                    //Console.WriteLine(nzbOnDisk);
                                    //string rssTitleNzb = rssTitle + ".nzb";
                                    if (!nzbOnDisk.Contains(rssTitle)) //If Filename is NOT found then download
                                    {
                                        notInNzbArchive = true; //Set notInNzbArchive to true
                                        Console.WriteLine("NZB File Not Found");
                                    }
                                }
                            }


                            if (wantedSeason == true && wantedEpisode == true && notInQueue == true && notInNzbArchive == true)
                            {
                                reportId = nodeItem["report:id"].InnerText; //Get ReportID
                                Console.WriteLine("No Files found: download");
                                nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&priority=" + priority + "&apikey=" + apiKey; //Create URL String
                                Console.WriteLine(nzbFileDownload);
                                //Send Newzbin Report to SABnzbd
                                HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload); //Create Request
                                HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse(); //Execute Request
                                addNzbResponse.Close();
                            }

                        } //Ends if show is wanted
                    } //Ends foreach wantedShowName in wantedShowNames
                }
            } //Ends Loop for title
            Console.ReadKey(); //Wait for User Input before Exit (Testing Only)
        } //Ends Public Static Void Main
    } //Ends Class
} //Ends Namespace
