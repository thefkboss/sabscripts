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
            
            foreach (string fileToConvert in filesToConvert)
            {
                string[] fileNameSplit = fileToConvert.Split('-');
                string showName = fileNameSplit[0].Trim();
                string seasonEpisode = fileNameSplit[1].Trim();
                string episodeNameWithExt = fileNameSplit[3].Trim();
                string[] episodeNameSplit = episodeNameWithExt.Split('.');
                string episodeName = episodeNameSplit[0];
                string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                int seasonNumber = Convert.ToInt32(seasonEpisodeSplit[0].TrimStart('S'));
                string episodeNumber =seasonEpisodeSplit[1];
                string outputFile = fileToConvert.Replace("avi", "mp4");
                
                string handBrakeCommands = "-i " + fileToConvert + " -o " + outputDir + "\\" + outputFile + "--preset=\"iPhone & iPod Touch\"";
                string handBrakeFile = handBrakeLocation + "\\handbrake.exe";

                Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit


                //episode both none
                if (episodeNameFormat.Contains("none"))
                {
                    string atomicParsleyCommands = outputFile + " --overwrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\"";
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe";

                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit();
                }

                else if (episodeNameFormat.Contains("episode"))
                {
                    string atomicParsleyCommands = outputFile + " --overwrite --title \"" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\"";
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe";

                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit();
                }

                else if (episodeNameFormat.Contains("both"))
                {
                    string atomicParsleyCommands = outputFile + " --overwrite --title \"" + seasonNumber + "x" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\"";
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe";

                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit();
                }

                else //handle as none (default)
                {
                    string atomicParsleyCommands = outputFile + " --overwrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\"";
                    string atomicParsleyFile = atomicParsleyLocation + "\\AtomicParsley.exe";

                    Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit();
                }
            }
        }
    }
}
