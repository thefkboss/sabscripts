using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace TVConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory from app.config
            string tempDir = ConfigurationSettings.AppSettings["tempDir"].ToString(); // Temp Directory from app.cpnfig
            string outputDir = ConfigurationSettings.AppSettings["outputDir"].ToString(); //Output Directory from app.config
            string handBrakeLocation = ConfigurationSettings.AppSettings["handBrakeLocation"].ToString(); //HandBrake location from app.config
            string atomicParsleyLocation = ConfigurationSettings.AppSettings["atomicParsleyLocation"].ToString(); //Atomic Parsley from app.config
            string episodeNameFormat =  ConfigurationSettings.AppSettings["episodeNameFormat"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)

            string[] filesToConvert = Directory.GetFiles(tempDir, "*.avi"); //Get all files from tempDir
            
            foreach (string fileToConvert in filesToConvert) //Foreach file found, run HandBrake + Atomic Parsley
            {
                string[] fileNameSplit = fileToConvert.Split('-'); //Break apart File Name (Full Path)
                string showNamePath = fileNameSplit[0].Trim(); //Path + Show Name
                string[] showNamePathSplit = showNamePath.Split('\\'); //Split Path + Show Name on \
                int showNamePathLength = showNamePathSplit.Length; //Get Length of last string array
                int showNameInt = showNamePathLength - 1; //Subtrack one from Array Length to get last position (Array starts at 0)
                string showName = showNamePathSplit[showNameInt]; //Set showName to last string in showNamePathSplit array
                string seasonEpisode = fileNameSplit[1].Trim(); //Season and Episode from fileNameSplit
                string episodeNameWithExt = fileNameSplit[2].Trim(); //Episode Name from fileNameSplit
                string[] episodeNameSplit = episodeNameWithExt.Split('.'); //Split on . (between name & extension)
                string episodeName = episodeNameSplit[0]; //Episode Name is first string in Array
                string[] seasonEpisodeSplit = seasonEpisode.Split('E'); //Split appart season number and episode number
                int seasonNumber = Convert.ToInt32(seasonEpisodeSplit[0].TrimStart('S')); //Season Number is first String, also remove the S from the start (store as int to drop leading zero)
                string episodeNumber = seasonEpisodeSplit[1]; //Store episode number as string
                string outputFile = outputDir + "\\" + showName + " - " + seasonEpisode + " - " + episodeName + ".mp4"; //Build outputFile string
                string handBrakeCommands = "-i \"" + fileToConvert + "\" -o \"" + outputFile + "\" --preset=\"iPhone & iPod Touch\""; //Commands for Handbrake
                string handBrakeFile = handBrakeLocation + "\\handbrakeCLI.exe"; //Path to handbrake.exe
                Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit
                File.Delete(fileToConvert); //Delete Converted File from tempDir
                
                //episode both none
                if (episodeNameFormat.Contains("none")) //If only episode name should be in episode name field
                { 
                    string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
                }

                else if (episodeNameFormat.Contains("episode")) //If Episode Number + Name should be in Episode Title (Number - Title)
                {
                    string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
                }

                else if (episodeNameFormat.Contains("both")) //If Season/Episode Number + Name should be in Episode Title (SeasonNumber'x'EpisodeNumber - Title)
                {
                    string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + seasonNumber + "x" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
                }

                else //handle as none (default)
                {
                    string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
                }
            }
        }
    }
}
