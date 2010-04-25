using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
 
namespace TVMove
{
    class Program
    {
        private static bool _updateXbmc = Convert.ToBoolean(ConfigurationManager.AppSettings["updateXbmc"]); //Get updateXbmc Bool from App.Config
        private static string _videoExt = ConfigurationManager.AppSettings["videoExt"]; //Get _tvTemplate from app.config
        private static string _logDir = ConfigurationManager.AppSettings["logDir"]; //Log Directory  from Config File
        private static string _logFile = _logDir + @"\TVMove.txt"; // Log File

        static void Main(string[] args)
        {
            //string logDir = ConfigurationManager.AppSettings["logDir"]; //Log Directory  from Config File

            string tempDir = ConfigurationManager.AppSettings["tempDir"]; //Temp Directory from Config File
            string shows = ConfigurationManager.AppSettings["shows"]; // TV Shows from Config File

            string showPath = args[0]; //Get showPath from first CMD Line Argument
            string showInfo = args[2]; //Get showName from third CMD Line Argument

            string showName = null;
            string[] titleSplitSs = null;
            string[] titleSplitX = null;
            int seasonNumber = 0;
            int episodeNumber = 0;

            //Log(Environment.NewLine);
            Log("#######################################################################");
            Log("Start Time: " + DateTime.Now);
            Log("Show Info from SAB is: " + showInfo);
            Log("Show Path is: " + showPath);

            string patternX = @"(?<Season>(?:\d{1,2}))[xX](?<Episode>(?:\d{1,2}))";
            string patternSs = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";

            Match titleMatchX = Regex.Match(showInfo, patternX);
            Match titleMatchSs = Regex.Match(showInfo, patternSs);

            if (titleMatchX.Success)
            {
                titleSplitX = Regex.Split(showInfo, patternX);
                showName = titleSplitX[0].TrimEnd('.', ' ', '-', '_');

                Int32.TryParse(titleMatchX.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchX.Groups["Episode"].Value, out episodeNumber);
            }

            else if (titleMatchSs.Success)
            {
                titleSplitSs = Regex.Split(showInfo, patternSs);
                showName = titleSplitSs[0].TrimEnd('.', ' ', '-', '_');

                Int32.TryParse(titleMatchSs.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchSs.Groups["Episode"].Value, out episodeNumber);
            }

            else
            {
                Log("Episode formatting is unsupported");
                return;
            }

            showName = showName.Replace('.', ' ');
            Log("Show Name is: " + showName);

            //Create list for formats (less code... I hope)
            List<string> formats = new List<string>();

            //Create Strings for addional searching for episodes and add to formats List
            formats.Add("*" + seasonNumber + "x" + episodeNumber.ToString("D2") + "*");
            formats.Add("*" + "S" + seasonNumber.ToString("D2") + "E" + episodeNumber.ToString("D2") + "*");
            formats.Add("*" + seasonNumber + episodeNumber.ToString("D2") + "*");

            if (shows.ToLower().Contains(showName.ToLower()))
            {
                Log("Show is wanted: " + showName);

                foreach (var format in formats)
                {
                    foreach (var ext in _videoExt)
                    {
                        var matchingFiles = Directory.GetFiles(showPath, format + ext);

                        if (matchingFiles.Length != 0)
                        {
                            Log("Episode Found, copying: " + matchingFiles[0]);
                            string destFileName = Path.GetFileName(matchingFiles[0]);
                            string destinationFile = tempDir + "\\" + destFileName;
                            File.Copy(matchingFiles[0], destinationFile, true);
                            Log("Episode copied to: " + destinationFile);
                        }

                    }
                }
            }

            if (_updateXbmc)
            {
                Log("Attempting to Update XBMC");
                string xbmcUpdate = UpdateXbmc(showPath, showInfo);
            }
            //Console.ReadKey();
        }
        
        private static string UpdateXbmc(string showPath, string showInfo)
        {
            bool xbmcOsWindows = Convert.ToBoolean(ConfigurationManager.AppSettings["xbmcOsWindows"]);

            string downloadTvPath = ConfigurationManager.AppSettings["downloadTvPath"];
            string xbmcTvPath = ConfigurationManager.AppSettings["xbmcTvPath"];
            string xbmcHost = ConfigurationManager.AppSettings["xbmcHost"];
            int xbmcPort = 9777;
            string xbmcPortS = ConfigurationManager.AppSettings["xbmcPort"];
            int.TryParse(xbmcPortS, out xbmcPort);
            string xbmcPath = null;
            bool notifyXbmc = Convert.ToBoolean(ConfigurationManager.AppSettings["notifyXbmc"]);
            bool cleanLibrary = Convert.ToBoolean(ConfigurationManager.AppSettings["cleanLibrary"]);

            string xbmcUpdating = null;

            if (downloadTvPath.ToLower() != xbmcTvPath.ToLower())
            {
                xbmcPath = showPath.Replace(downloadTvPath, xbmcTvPath);
                if (!xbmcOsWindows)
                {
                   xbmcPath = xbmcPath.Replace('\\', '/'); 
                }
            }

            else
            {
                xbmcPath = showPath;
            }

            Console.WriteLine(xbmcPath);

            if (!XBMC.EventClient.Current.Connected)
                XBMC.EventClient.Current.Connect(xbmcHost, xbmcPort);

            if (XBMC.EventClient.Current.Connected)
            {
                string xbmcLibraryUpdate = "UpdateLibrary(video," + xbmcPath + ")";

                XBMC.EventClient.Current.SendAction(xbmcLibraryUpdate, "");
                if (notifyXbmc)
                    XBMC.EventClient.Current.SendNotification("TV Show Downloaded", showInfo, XBMC.IconType.ICON_PNG, "sabnzbd");
                if (cleanLibrary)
                    XBMC.EventClient.Current.SendAction("CleanLibrary(video)", "");
                xbmcUpdating = "XBMC is Updating";
                return xbmcUpdating;
            }

            xbmcUpdating = "Could not connect to XBMC";
            return xbmcUpdating;
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
            try
            {
                using (StreamWriter sw = File.AppendText(_logFile))
                {
                    sw.WriteLine(message);
                }
            }
            catch { }
        }
    }
}
