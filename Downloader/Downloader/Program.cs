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
            string sabnzbdInfo = ConfigurationSettings.AppSettings["sabnzbdInfo"].ToString(); //Get sabnzbdInfo from app.config
            string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString(); //Get apiKey from app.config
            string rssUrl = ConfigurationSettings.AppSettings["rssUrl"].ToString(); //Get rssUrl from app.config

            //string rssUrl = "https://www.newzbin.com/search/?go=1&ss=454082&fpn=p&feed=rss&fauth=NTcwMTIwLTE2M2UyNDFmNDk2NmI3Y2M3NjIzODMzMGUzMGQ3Y2YyMzVlNmM3YTk%3D";

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
                    //Channel tage found
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

                            //Is Episode Needed?
                            string showNameSeasonDir = wantedShowNamePath + "\\Season " + seasonNumber;
                            string nameOnDisk = showName + " - S" + seasonNumber + "E" + episodeNumber;
                            Console.WriteLine(showNameSeasonDir);
                            if (Directory.Exists(showNameSeasonDir))
                            {
                                Console.WriteLine("Season folder found");
                                string[] episodesOnDisk = Directory.GetFiles(showNameSeasonDir);

                                if (episodesOnDisk.Length == 0)
                                {
                                    string reportId = nodeItem["report:id"].InnerText; //Get ReportID
                                    Console.WriteLine("No Files found: download");
                                    string nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&apikey=" + apiKey; //Create URL String
                                    Console.WriteLine(nzbFileDownload);
                                    //Send Newzbin Report to SABnzbd
                                    HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload);
                                    HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse();
                                }
                                else
                                {
                                    foreach (string episodeOnDiskPath in episodesOnDisk)
                                    {
                                        string episodeOnDisk = Path.GetFileNameWithoutExtension(episodeOnDiskPath);
                                        Console.WriteLine(episodeOnDisk);
                                        //If Filename is NOT found then download
                                        if (!episodeOnDisk.Contains(nameOnDisk))
                                        {
                                            string reportId = nodeItem["report:id"].InnerText; //Get ReportID
                                            Console.WriteLine("File to be downloaded");

                                            string nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&apikey=" + apiKey; //Create URL String
                                            Console.WriteLine(nzbFileDownload);
                                            //Send Newzbin Report to SABnzbd
                                            HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload);
                                            HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Folder does not exist... New Season?
                                string reportId = nodeItem["report:id"].InnerText; //Get ReportID
                                Console.WriteLine("File will downloaded (new season)");

                                string nzbFileDownload = "http://" + sabnzbdInfo + "/api?mode=addid&name=" + reportId + "&apikey=" + apiKey; //Create URL String
                                Console.WriteLine(nzbFileDownload);
                                //Send Newzbin Report to SABnzbd
                                HttpWebRequest addNzbRequest = (HttpWebRequest)WebRequest.Create(nzbFileDownload);
                                HttpWebResponse addNzbResponse = (HttpWebResponse)addNzbRequest.GetResponse();
                            }
                        }
                    }
                    //string nzbFile = nodeItem["report:nzb"].InnerText;
                    //string reportId = nodeItem["report:id"].InnerText;
                    //Console.WriteLine(showName);
                    //Console.WriteLine(seasonNumber);
                    //Console.WriteLine(episodeNumber);
                    //Console.WriteLine(nameOnDisk);
                    //Console.WriteLine(title);
                    //Console.WriteLine(reportId);
                }
            }
            Console.ReadKey();

        }
    }
}
