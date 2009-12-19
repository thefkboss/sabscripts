using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace TVMove
{
    class Program
    {
        private static bool _updateXbmc = Convert.ToBoolean(ConfigurationManager.AppSettings["updateXbmc"]); //Get updateXbmc Bool from App.Config
        private static string _filenameTemplate = ConfigurationManager.AppSettings["filenameTemplate"]; //Get _tvTemplate from app.config
        private static string _videoExt = ConfigurationManager.AppSettings["videoExt"]; //Get _tvTemplate from app.config

        static void Main(string[] args)
        {
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory  from Config File
            string tempDir = ConfigurationSettings.AppSettings["tempDir"].ToString(); //Temp Directory from Config File
            string shows = ConfigurationSettings.AppSettings["shows"].ToString(); // TV Shows from Config File
            shows = shows.ToLower(); //Convert Shows from user to Lower-Case (SABnzbd may have odd Case structure)

            string showPath = args[0]; //Get showPath from first CMD Line Argument
            string showInfo = args[2]; //Get showName from third CMD Line Argument
            string[] fileNameArray = showInfo.Split('-'); //Split showInfo into sections

            string showName = null;
            string seasonEpisode = null;
            string episodeName = null;
            int seasonNumber = 0;
            int episodeNumber = 0;

            string logFile = logDir + @"\TVMove.txt"; // Log File

            File.AppendAllText(logFile, "\n");
            File.AppendAllText(logFile, "#######################################################################\n");
            File.AppendAllText(logFile, "Show Name is: " + showName + "\n");
            File.AppendAllText(logFile, "Show Path is: " + showPath + "\n");

            if (fileNameArray.Length == 3)
            {
                showName = fileNameArray[0].Trim(); //Show Name is first string in fileNameArray
                seasonEpisode = fileNameArray[1].Trim(); //Show Number (Season + Episode) is the second string  in fileNameArray
                episodeName = fileNameArray[2].Trim(); //Episode Title is the third string in fileNameArray
                string[] seasonEpisodeSplit = seasonEpisode.Split('x'); //Split showNumber
                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
            }

            if (fileNameArray.Length == 4)
            {
                if (fileNameArray[1].Contains("x"))
                {
                    showName = fileNameArray[0].Trim();
                    seasonEpisode = fileNameArray[1].Trim();
                    episodeName = fileNameArray[2] + fileNameArray[3];
                    episodeName.Trim();
                }

                else if (fileNameArray[2].Contains("x"))
                {
                    showName = fileNameArray[0] + fileNameArray[1];
                    showName.Trim();
                    seasonEpisode = fileNameArray[2].Trim();
                    episodeName = fileNameArray[3].Trim();
                }

                else
                {
                    File.AppendAllText(logFile, "Un-supported Episode \n"); //Log Un-Supported Episode
                }

                string[] seasonEpisodeSplit = seasonEpisode.Split('x');

                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
            }
            string fileNameNoExt = GetFilename(showName, seasonNumber, episodeNumber, episodeName);

            if (shows.Contains(showName.ToLower()))
            {
                File.AppendAllText(logFile, "Show is Wanted: " + showName + "\n");
                Console.WriteLine("Show is Wanted");
                string[] videoExtLoop = _videoExt.Split(';');
                foreach (string e in videoExtLoop)
                {
                    string testFilePath = showPath + "\\" + fileNameNoExt + e;
                    if (File.Exists(testFilePath))
                    {
                        string fileFullPath = showPath + "\\" + fileNameNoExt + e; //Path to downloaded file as based on path (from SAB) + fileName
                        string fileTempPath = tempDir + "\\" + fileNameNoExt + e; //Path to temp file as supplied by user + fileName
                        File.Copy(fileFullPath, fileTempPath, true); //Copy file to tempDir
                        File.AppendAllText(logFile, "Show was Copied to: " + tempDir + fileNameNoExt + e + "\n");
                    }
                }
            }
            if (_updateXbmc)
            {
                File.AppendAllText(logFile, "Attempting to Update XBMC\n");
                string xbmcUpdate = UpdateXbmc(showPath);
                File.AppendAllText(logFile, "XBMC Returned: " + xbmcUpdate + "\n");
            }
        }

        private static string GetFilename(string showName, int seasonNumber, int episodeNumber, string episodeName)
        {
            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');

            string enReplace = episodeName;
            string eDotNReplace = episodeName.Replace(' ', '.');
            string eUnderNReplace = episodeName.Replace(' ', '_');

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

            string path = _filenameTemplate;

            path = path.Replace(".%ext", "");
            path = path.Replace("%sn", snReplace);
            path = path.Replace("%s.n", sDotNReplace);
            path = path.Replace("%s_n", sUnderNReplace);
            path = path.Replace("%en", enReplace);
            path = path.Replace("%e.n", eDotNReplace);
            path = path.Replace("%e_n", eUnderNReplace);
            path = path.Replace("%0s", zeroSReplace);
            path = path.Replace("%s", sReplace);
            path = path.Replace("%0e", zeroEReplace);
            path = path.Replace("%e", eReplace);

            return path;
        }

        private static string UpdateXbmc(string showPath)
        {
            string downloadTvPath = ConfigurationManager.AppSettings["downloadTvPath"];
            string xbmcTvPath = ConfigurationManager.AppSettings["xbmcTvPath"];
            string xbmcInfo = ConfigurationManager.AppSettings["xbmcInfo"];
            string xbmcUsername = ConfigurationManager.AppSettings["xbmcUsername"];
            string xbmcPassword = ConfigurationManager.AppSettings["xbmcPassword"];
            string xbmcPath = null;

            if (downloadTvPath.ToLower() != xbmcTvPath.ToLower())
            {
                xbmcPath = showPath.Replace(downloadTvPath, xbmcTvPath);
                xbmcPath = xbmcPath.Replace('\\', '/');
            }

            else
            {
                xbmcPath = showPath;
            }

            Console.WriteLine(xbmcPath);

            try
            {
                string xbmcUrl = "http://" + xbmcInfo + "/xbmcCmds/xbmcHttp?command=ExecBuiltIn&parameter=XBMC.updatelibrary(video," + xbmcPath + ")";

                HttpWebRequest xbmcRequest = (HttpWebRequest)WebRequest.Create(xbmcUrl);
                xbmcRequest.Timeout = 10000;
                xbmcRequest.Credentials = new NetworkCredential(xbmcUsername, xbmcPassword);
                xbmcRequest.PreAuthenticate = true;

                HttpWebResponse xbmcRepsonse = (HttpWebResponse)xbmcRequest.GetResponse();
                xbmcRepsonse.Close();
            }
            catch
            {
                string errorMsg = "An error occured connecting to XBMC";
                return errorMsg;
            }

            string xbmcUpdating = "XBMC is Updating";
            return xbmcUpdating;
        }
    }
}
