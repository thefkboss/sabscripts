using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TVConvertSvc
{
    class FileProcessor
    {
        private Queue<string> _convertQueue;
        private Thread _convertThread;
        private EventWaitHandle _waitHandle;

        private static string _logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory from app.config
        private static string _outputDir = ConfigurationSettings.AppSettings["outputDir"].ToString(); //Output Directory from app.config
        private static string _handBrakeLocation = ConfigurationSettings.AppSettings["handBrakeLocation"].ToString(); //HandBrake location from app.config
        private static string _atomicParsleyLocation = ConfigurationSettings.AppSettings["atomicParsleyLocation"].ToString(); //Atomic Parsley from app.config
        private static string _episodeNameFormat = ConfigurationSettings.AppSettings["episodeNameFormat"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)
        private static string _videoExt = ConfigurationSettings.AppSettings["videoExt"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)
        private int loopCount = 0;
        private static string _logFile = _logDir + "\\TVConvertSvc.txt"; // Log File

        public FileProcessor()
        {
            _convertQueue = new Queue<string>();
            _waitHandle = new AutoResetEvent(true);
        }

        public void QueueInput(string filePath)
        {
            _convertQueue.Enqueue(filePath);

            File.AppendAllText(_logFile, DateTime.Now + " - Added to Queue: " + filePath + "\n");

            //Start thread if this is the first item
            if (_convertThread == null)
            {
                File.AppendAllText(_logFile, DateTime.Now + " - Starting to Process Queue \n");
                _convertThread = new Thread(new ThreadStart(Convert));
                _convertThread.Start();
            }

            //If Thread is waiting, start it
            else if (_convertThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
            {
                File.AppendAllText(_logFile, DateTime.Now + " - Restarting Processing of Queue \n");
                _waitHandle.Set();
            }
        }

        private void Convert()
        {
            while (true)
            {
                string filePath = GetQueueItem();

                if (filePath != null)
                {
                    File.AppendAllText(_logFile, DateTime.Now + " - Converting file: " + filePath + "\n");
                    ConvertFile(filePath);
                }

                    //Wait if no files are left
                else
                {
                    File.AppendAllText(_logFile, DateTime.Now + " - All files Converted, Sleeping \n");
                    _waitHandle.WaitOne();
                }
            }
        }

        public static void ConvertFile(string fileToConvert)
        {
            string fileExt = Path.GetExtension(fileToConvert);
            if (_videoExt.Contains(fileExt))
            {

                string fileNameToConvert = Path.GetFileNameWithoutExtension(fileToConvert);

                string[] fileNameSplit = fileNameToConvert.Split('-'); //Break apart File Name
                string showName = null;
                int seasonNumber = 0;
                int episodeNumber = 0;
                string episodeName = null;

                if (fileNameSplit.Length == 3)
                {
                    showName = fileNameSplit[0].Trim();
                    string seasonEpisode = fileNameSplit[1].Trim();
                    episodeName = fileNameSplit[2].Trim();

                    if (seasonEpisode.Contains("x"))
                    {
                        string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                        Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                        Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                    }

                    else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                    {
                        string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                        seasonEpisodeSplit[0].Replace("S", "");
                        Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                        Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                    }

                    string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                    File.Delete(fileToConvert);
                    RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                }

                else if (fileNameSplit.Length == 4)
                {
                    if (Regex.IsMatch(fileNameSplit[1], @"\d{1,2}x\d{1,2}") || Regex.IsMatch(fileNameSplit[1], @"S\d{1,2}E\d{1,2}"))
                    {
                        showName = fileNameSplit[0].Trim();
                        string seasonEpisode = fileNameSplit[1];
                        episodeName = fileNameSplit[2] + fileNameSplit[3];
                        episodeName.Trim();

                        if (seasonEpisode.Contains("x"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                        }

                        else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                            seasonEpisodeSplit[0].Replace("S", "");
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                        }

                        string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                        File.Delete(fileToConvert);
                        RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                    }

                    else if (Regex.IsMatch(fileNameSplit[2], @"\d{1,2}x\d{1,2}") || Regex.IsMatch(fileNameSplit[2], @"S\d{1,2}E\d{1,2}"))
                    {
                        showName = fileNameSplit[0] + fileNameSplit[1];
                        showName.Trim();
                        string seasonEpisode = fileNameSplit[2];
                        episodeName = fileNameSplit[3].Trim();

                        if (seasonEpisode.Contains("x"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                        }

                        else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                            seasonEpisodeSplit[0].Replace("S", "");
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                        }

                        string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                        File.Delete(fileToConvert);
                        RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                    }

                    else
                    {
                        //Run HandBrake, no Atomic Parsley
                        Console.WriteLine("Unsupported Format - 4 strings");
                        RunHandbrake(fileToConvert, fileNameToConvert);
                        File.Delete(fileToConvert);
                    }
                }

                else
                {
                    //Run HandBrake, no Atomic Parsley
                    Console.WriteLine("Unsupported Format - Wrong Split");
                    Console.WriteLine(fileToConvert);
                    RunHandbrake(fileToConvert, fileNameToConvert);
                    File.Delete(fileToConvert);
                }
            }

            else
            {
                Console.WriteLine("File Type is not supported");
            }
            File.AppendAllText(_logFile, DateTime.Now + " - Finished Converting: " + fileToConvert + "\n");
        }

        private static string RunHandbrake(string inputFile, string inputFileName)
        {
            //File.AppendAllText(_logFile, DateTime.Now + " - Running handbrake \n");
            //string outputFile = _outputDir + "\\" + inputFileName + ".mp4";
            //string handBrakeCommands = "-i \"" + inputFile + "\" -o \"" + outputFile + "\" --preset=\"iPhone & iPod Touch\""; //Commands for Handbrake
            //string handBrakeFile = _handBrakeLocation + "\\handbrakeCLI.exe"; //Path to handbrake.exe
            //Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit
            //return outputFile;

            File.AppendAllText(_logFile, DateTime.Now + " - Running handbrake \n");
            string outputFile = _outputDir + "\\" + inputFileName + ".mp4";
            string handBrakeCommands = "-i \"" + inputFile + "\" -o \"" + outputFile + "\" --preset=\"iPhone & iPod Touch\""; //Commands for Handbrake
            string handBrakeFile = _handBrakeLocation + "\\handbrakeCLI.exe"; //Path to handbrake.exe
            //Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit
            ProcessStartInfo handBrakeInfo = new ProcessStartInfo(handBrakeFile, handBrakeCommands);
            handBrakeInfo.UseShellExecute = false;
            handBrakeInfo.RedirectStandardOutput = true;
            handBrakeInfo.RedirectStandardError = true;

            try
            {
                Process handBrake = Process.Start(handBrakeInfo);
                handBrake.WaitForExit();
                StreamReader oReader = handBrake.StandardOutput;
                //StreamReader eReader = handBrake.StandardError;
                string sRes = oReader.ReadToEnd();
                oReader.Close();
                File.AppendAllText(@"C:\Logs\Output.txt", sRes);
            }

            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Logs\Exception.txt", ex.ToString());
            }
            return outputFile;

        }

        private static string RunAtomicParsley(string showName, int seasonNumber, int episodeNumber, string episodeName, string outputFile)
        {
            File.AppendAllText(_logFile, DateTime.Now + " - Running Atomic Parsley \n");
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

        private string GetQueueItem()
        {
            if (_convertQueue.Count > 0)
            {
                bool isFileClosed = false;
                string fileName = _convertQueue.Peek();

                isFileClosed = IsFileClosed(fileName);

                if (isFileClosed)
                    return _convertQueue.Dequeue();

                else if (_convertQueue.Count == 1)
                {
                    bool closedLoop = IsFileCloseLoop(fileName);
                    if (closedLoop)
                        return _convertQueue.Dequeue();

                    else
                    {
                        if (_convertQueue.Count > 1)
                        {
                            File.AppendAllText(_logFile, DateTime.Now + " - Moving to end of Queue: " + fileName + "\n");
                            string tempFileName = _convertQueue.Dequeue();
                            _convertQueue.Enqueue(tempFileName);
                            return null;
                        }
                        else
                        {
                            File.AppendAllText(_logFile, DateTime.Now + " - File is still open, Removing from Queue: " + fileName + "\n");
                            _convertQueue.Dequeue();
                            return null;
                        }
                    }
                }

                else if (_convertQueue.Count > 1)
                {
                    while (loopCount < 10)
                    {
                        File.AppendAllText(_logFile, DateTime.Now + " - Moving to end of Queue: " + fileName + "\n");
                        string tempFileName = _convertQueue.Dequeue();
                        _convertQueue.Enqueue(tempFileName);
                        loopCount++;
                        Thread.Sleep(1000);
                        return null;
                    }
                    loopCount = 0;
                    _convertQueue.Clear();
                    return null;
                }
                else
                    return null;
            }

            else
                return null;

        }

        private bool IsFileCloseLoop(string fileName)
        {
            bool isFileClosed = false;
            TimeSpan durationToRun = new TimeSpan(0, 5, 0);
            DateTime startTime = DateTime.Now;

            while (DateTime.Now - startTime < durationToRun && isFileClosed == false && _convertQueue.Count == 1)
            {
                isFileClosed = IsFileClosed(fileName);
                if (isFileClosed)
                    return true;
                Thread.Sleep(4000);
            }
            return isFileClosed;
        }

        static bool IsFileClosed(string fileName)
        {
            FileInfo fileNameInfo = new FileInfo(fileName);

            try
            {
                using (FileStream fs = fileNameInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                    return true;
                }
            }
            catch
            {

                return false;
            }
        }
    }
}