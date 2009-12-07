using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace TVMove
{
    class Program
    {
        static void Main(string[] args)
        {
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory  from Config File
            string tempDir = ConfigurationSettings.AppSettings["tempDir"].ToString(); //Temp Directory from Config File
            string shows = ConfigurationSettings.AppSettings["shows"].ToString(); // TV Shows from Config File
            shows = shows.ToLower(); //Convert Shows from user to Lower-Case (SABnzbd may have odd Case structure)
            string showPath = args[0]; //Get showPath from first CMD Line Argument
            string showInfo = args[2]; //Get showName from third CMD Line Argument
            string[] fileNameArray = showInfo.Split('-'); //Split showInfo into sections
            string showName = fileNameArray[0].Trim(); //Show Name is first string in fileNameArray
            string showNumber = fileNameArray[1].Trim(); //Show Number (Season + Episode) is the second string  in fileNameArray
            string episodeTitle = fileNameArray[2].Trim(); //Episode Title is the third string in fileNameArray
            string[] showNumberArray = showNumber.Split('x'); //Split showNumber
            int seasonNumber = Convert.ToInt32(showNumberArray[0].Trim()); //Create int with seasonNumber
            int episodeNumber = Convert.ToInt32(showNumberArray[1].Trim()); //Create int with episodeNumber
            string seasonNum; //Invoke here for use later
            string episodeNum; //Invoke here for use later
            string logFile = logDir + @"\TVMove.txt"; // Log File
            File.AppendAllText(logFile, "\n");
            File.AppendAllText(logFile, "#######################################################################\n");
            File.AppendAllText(logFile, "Show Name is: " + showName + "\n");
            File.AppendAllText(logFile, "Show Path is: " + showPath + "\n");

            if (seasonNumber < 10) //Convert seasonNumber to seasonNum with leading zero if required
            {
                seasonNum = "0" + Convert.ToString(seasonNumber);
            }
            else
            {
                seasonNum = Convert.ToString(seasonNumber);
            }

            if (episodeNumber < 10)//Convert episodeNumber to episodeNum with leading zero if required
            {
                episodeNum = "0" + Convert.ToString(episodeNumber);
            }
            else
            {
                episodeNum = Convert.ToString(episodeNumber);
            }

            string fileName = showName + " - S" + seasonNum + "E" + episodeNum + " - " + episodeTitle + ".avi"; //Create string for file name (only supports "Show Name - S01E01 - Episode Name.avi" at thsi time)
            string fileFullPath = showPath + "\\" + fileName; //Path to downloaded file as based on path (from SAB) + fileName
            string fileTempPath = tempDir + "\\" + fileName; //Path to temp file as supplied by user + fileName

            if (shows.Contains(showName.ToLower()))
            {
                File.AppendAllText(logFile, "Show is wanted, copying to: " + tempDir + "\n");
                File.Copy(fileFullPath, fileTempPath); //Copy file to tempDir
            }
        }
    }
}
