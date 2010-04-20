using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace TVConvert
{
    class Program
    {
        private static string _logDir = ConfigurationManager.AppSettings["logDir"].ToString(); //Log Directory from app.config
        private static string _logFile = _logDir + @"\TVConvert.txt";
        private static string _tempDir = ConfigurationManager.AppSettings["tempDir"].ToString(); // Temp Directory from app.cpnfig
        private static string _outputDir = ConfigurationManager.AppSettings["outputDir"].ToString(); //Output Directory from app.config
        private static string _handBrakeLocation = ConfigurationManager.AppSettings["handBrakeLocation"].ToString(); //HandBrake location from app.config
        private static string _handBrakePreset = ConfigurationManager.AppSettings["handBrakePreset"].ToString(); //HandBrake location from app.config
        private static string _atomicParsleyLocation = ConfigurationManager.AppSettings["atomicParsleyLocation"].ToString(); //Atomic Parsley from app.config
        private static string _episodeNameFormat = ConfigurationManager.AppSettings["episodeNameFormat"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)
        private static string _videoExt = ConfigurationManager.AppSettings["videoExt"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)

        static void Main(string[] args)
        {
            string[] videoExt = _videoExt.Split(';');
            foreach (string e in videoExt)
            {
                string fileExtSearch = "*" + e;
                string[] filesToConvert = Directory.GetFiles(_tempDir, fileExtSearch); //Get all files from tempDir
                foreach (string fileToConvert in filesToConvert) //Foreach file found, run HandBrake + Atomic Parsley
                {
                    string fileNameToConvert = Path.GetFileNameWithoutExtension(fileToConvert);

                    string showName = GetShowName(fileNameToConvert);
                    int seasonNumber = GetSeasonNumber(fileNameToConvert);
                    int episodeNumber = GetEpisodeNumber(fileNameToConvert);
                    string episodeName = GetEpisodeName(fileNameToConvert);

                    if (showName != null && seasonNumber > 0 && episodeNumber > 0 && episodeName != null)
                    {
                        Log("Running Handbrake on: " + fileToConvert);
                        string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                        Log("Deleting: " + fileToConvert);
                        File.Delete(fileToConvert);
                        Log("Running Atomic Parsley on: " + outputFile);
                        RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                    }

                    else
                    {
                        //Run HandBrake, no Atomic Parsley
                        Log("Unable to get episode specific information for: " + fileToConvert);
                        Log("Running Handbrake on: " + fileToConvert);
                        RunHandbrake(fileToConvert, fileNameToConvert);
                        Log("Deleting: " + fileToConvert);
                        File.Delete(fileToConvert);
                    }
                }
            }
        }

        private static string RunHandbrake(string inputFile, string inputFileName)
        {
            string handBreakReplace = _handBrakePreset.Replace(" AND ", " & ");
            string outputFile = _outputDir + "\\" + inputFileName + ".mp4";
            string handBrakeCommands = "-i \"" + inputFile + "\" -o \"" + outputFile + "\" --preset=\"" + handBreakReplace + "\""; //Commands for Handbrake
            string handBrakeFile = _handBrakeLocation + "\\handbrakeCLI.exe"; //Path to handbrake.exe
            Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit
            return outputFile;
        }

        private static string RunAtomicParsley(string showName, int seasonNumber, int episodeNumber, string episodeName, string outputFile)
        {
            if (_episodeNameFormat.Contains("none")) //If only episode name should be in episode name field
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else if (_episodeNameFormat.Contains("episode")) //If Episode Number + Name should be in Episode Title (Number - Title)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else if (_episodeNameFormat.Contains("both")) //If Season/Episode Number + Name should be in Episode Title (SeasonNumber'x'EpisodeNumber - Title)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + seasonNumber + "x" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else //handle as none (default)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }
            return outputFile;
        }

        private static string GetShowName(string fileName)
        {
            string showName = null;
            string[] titleSplitSs = null;
            string[] titleSplitX = null;

            string patternSs = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";

            Match titleMatchSs = Regex.Match(fileName, patternSs);

            if (titleMatchSs.Success)
            {
                titleSplitSs = Regex.Split(fileName, patternSs);

                showName = titleSplitSs[0].TrimEnd('.', ' ', '-', '_');
                return showName;
            }

            Match titleMatchX = Regex.Match(fileName, patternX);

            if (titleMatchX.Success)
            {
                titleSplitX = Regex.Split(fileName, patternX);
                showName = titleSplitX[0].TrimEnd('.', ' ', '-', '_');
                return showName;
            }
            return showName;
        }

        private static int GetSeasonNumber(string fileName)
        {
            int seasonNumber = 0;

            string patternSs = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";

            Match titleMatchSs = Regex.Match(fileName, patternSs);

            if (titleMatchSs.Success)
            {
                Int32.TryParse(titleMatchSs.Groups["Season"].Value, out seasonNumber);
                return seasonNumber;
            }

            Match titleMatchX = Regex.Match(fileName, patternX);

            if (titleMatchX.Success)
            {
                Int32.TryParse(titleMatchX.Groups["Season"].Value, out seasonNumber);
                return seasonNumber;
            }
            return seasonNumber;
        }

        private static int GetEpisodeNumber(string fileName)
        {
            int episodeNumber = 0;

            string patternSs = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";

            Match titleMatchSs = Regex.Match(fileName, patternSs);

            if (titleMatchSs.Success)
            {
                Int32.TryParse(titleMatchSs.Groups["Episode"].Value, out episodeNumber);
                return episodeNumber;
            }

            Match titleMatchX = Regex.Match(fileName, patternX);

            if (titleMatchX.Success)
            {
                Int32.TryParse(titleMatchX.Groups["Episode"].Value, out episodeNumber);
                return episodeNumber;
            }
            return episodeNumber;
        }

        private static string GetEpisodeName(string fileName)
        {
            string episodeName = null;

            string[] titleSplitSs = null;
            string[] titleSplitX = null;

            string patternSs = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";

            Match titleMatchSs = Regex.Match(fileName, patternSs);

            if (titleMatchSs.Success)
            {
                titleSplitSs = Regex.Split(fileName, patternSs);

                episodeName = titleSplitSs[3].TrimEnd('.', ' ', '-', '_').TrimStart('.', ' ', '-', '_');
                return episodeName;
            }

            Match titleMatchX = Regex.Match(fileName, patternX);

            if (titleMatchX.Success)
            {
                titleSplitX = Regex.Split(fileName, patternX);
                episodeName = titleSplitX[3].TrimEnd('.', ' ', '-', '_').TrimStart('.', ' ', '-', '_');
                return episodeName;
            }
            return null;
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

        private static void Log(string message, params object[] para)
        {

            Log(String.Format(message, para));
        }
    }
}