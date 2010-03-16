using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace XBox360
{
    class Program
    {
        private static string _logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory from Config File
        private static string _logFile = _logDir + @"\XBox360.txt"; // Log File 

        static void Main(string[] args)
        {
            bool checkIso = Convert.ToBoolean(ConfigurationSettings.AppSettings["checkIso"]);
            bool extractIso = Convert.ToBoolean(ConfigurationSettings.AppSettings["extractIso"]);
            string dlcDir = ConfigurationSettings.AppSettings["dlcDir"].ToString(); //DLC Directory from Config File
            string xblaDir = ConfigurationSettings.AppSettings["xblaDir"].ToString(); //XBLA Directory from Config File

            string gamePath = args[0]; //Get Game Path from CMD Line Arguments
            string gameName = args[2]; //Get Game Name from CMD Line Arguments

            //Move DLC Files instead of Failing them
            if (gameName.Contains("(DLC)"))
            {
                string dlcPath = dlcDir + @"\" + gameName; //Path to DLC Final Location
                Directory.Move(gamePath, dlcPath);
            }

            //Move XBLA Files instead of Failing them
            if (gameName.Contains("(XBLA)"))
            {
                string xblaPath = xblaDir + @"\" + gameName; //Path to XBLA Final Location
                Directory.Move(gamePath, xblaPath);
            }

            if (checkIso)
            {
                string newGamePath = CheckWithAbgx(gamePath, gameName);
                gamePath = newGamePath;

                if (gamePath == "wiigame")
                    return;
            }

            if (extractIso == true)
            {
                ExtractIso(gamePath, gameName);
            }
        }

        private static string CheckWithAbgx(string gamePath, string gameName)
        {
            string abgxOptions = ConfigurationSettings.AppSettings["abgxOptions"].ToString(); //abgx360 Options from Config File
            string failedDir = ConfigurationSettings.AppSettings["failedDir"].ToString(); //Failed Directory from Config File
            string wiiDir = ConfigurationSettings.AppSettings["wiiDir"].ToString(); //Wii Directory from Config File
            string passDir = ConfigurationSettings.AppSettings["passDir"].ToString(); //Pass Directory from Config File
            bool requireSSv2 = Convert.ToBoolean(ConfigurationSettings.AppSettings["requireSSv2"]);
            bool failedSs = false;
            bool gamePassed = true; //Boolean for Game Passed
            bool ssVersionTwo = true;
            string failedGame = failedDir + @"\" + gameName; //Path to Failed Game Name
            string wiiGame = wiiDir + @"\" + gameName; //Path to Wii Game Directory
            string passedGame = passDir + @"\" + gameName; //Path to Passed Game Directory
            DirectoryInfo gamePathInfo = new DirectoryInfo(gamePath); //DirectoryInfo for Game Path
            DirectoryInfo failedGameInfo = new DirectoryInfo(failedGame); //DirectoryInfo for Failed Game Path
            DirectoryInfo wiiGameInfo = new DirectoryInfo(wiiGame); //DirectoryInfo for Wii Game Path
            DirectoryInfo passedGameInfo = new DirectoryInfo(passedGame); //DirectoryInfo for Passed Game Path
            string batFile = failedDir + "\\" + gameName + ".bat"; //Batch File for Failed Game
            File.AppendAllText(_logFile, "\n");
            File.AppendAllText(_logFile, "#######################################################################\n");
            File.AppendAllText(_logFile, "Game name is: " + gameName + "\n");
            File.AppendAllText(_logFile, "Game path is: " + gamePath + "\n");
            string[] isoFiles = Directory.GetFiles(gamePath, "*.iso"); //Create Array of ISO Files
            Array.Sort(isoFiles); //Sort ISO Files Alphabetically
            for (int isoFilesPos = 0; isoFilesPos < isoFiles.Length; isoFilesPos++) //Loop through ISO Files, Run abgx360 and check html for verification string
            {
                string isoFile = isoFiles[isoFilesPos];

                if (IsoSizeCheck(isoFile)) //Check to see if ISO File is big enough
                {
                    int isoFileNum = 1 + isoFilesPos; //int to see which ISO File it is
                    RunAbgx(gamePath, isoFile, isoFileNum);

                    Thread.Sleep(1000);

                    string htmlFile = gamePath + "\\abgx360-" + isoFileNum + ".html"; //Get full path to HTML file
                    gamePassed = IsoPassed(htmlFile);
                    ssVersionTwo = CheckSsVersion(htmlFile);
                    if (requireSSv2 == true && ssVersionTwo == false)
                        failedSs = true;

                    if (gamePassed == false || failedSs == true)
                        break;
                }
                else
                {
                    MoveWiiGame(gamePath, wiiGame);
                    return "wiiGame";
                }
            }
            if (gamePassed)
            {
                if (!requireSSv2)
                {
                    if (passedGameInfo.FullName != gamePathInfo.FullName) //If game is not in already in the Passed Directory
                    {
                        File.AppendAllText(_logFile, "Game Passed, SSv1 not required, moving to: " + passedGame + "\n");
                        MovePassedGame(gamePath, passedGame); //Move Game to Passed Directory
                        return passedGame;
                    }
                }

                else
                {
                    if (failedSs)
                    {
                        if (failedGameInfo.FullName != gamePathInfo.FullName) //If Game failed and is not in failed dir, move it
                        {
                            File.AppendAllText(_logFile, "Game Passed, but SSv1 was Detected and is being moved to: " + failedGame + "\n");
                            MoveFailedGame(gamePath, failedGame);
                            return failedGame;
                        }
                    }

                    else
                    {
                        if (passedGameInfo.FullName != gamePathInfo.FullName) //If game is not in already in the Passed Directory
                        {
                            File.AppendAllText(_logFile, "Game passed and is being moved to: " + passedGame + "\n");
                            MovePassedGame(gamePath, passedGame); //Move Game to Passed Directory
                            return passedGame;
                        }
                    }


                }
            }
            else
            {
                File.WriteAllText(batFile, "XBox360.exe \"" + failedGame + "\" 2 \"" + gameName + "\"");
                if (failedGameInfo.FullName != gamePathInfo.FullName) //If Game failed and is not in failed dir, move it
                {
                    File.AppendAllText(_logFile, "Game Failed and is being moved to: " + failedGame + "\n");
                    MoveFailedGame(gamePath, failedGame);
                    return failedGame;
                }
            }
            return passedGame;
        }

        private static bool IsoPassed(string htmlFile)
        {
            string htmlFileContents = File.ReadAllText(htmlFile); //Read html file into string
            if (htmlFileContents.Contains("Verification was successful!")) //Check string for "Verification was successful!"
                return true;

            else
                return false;
        }

        private static bool CheckSsVersion(string htmlFile)
        {
            string htmlFileContents = File.ReadAllText(htmlFile); //Read html file into string
            if (htmlFileContents.Contains("SS Version: 2")) //Check string for "SS Version: 2"
                return true;

            else
                return false;
        }

        private static void RunAbgx(string gamePath, string isoFile, int isoFileNum)
        {
            string abgxOptions = ConfigurationSettings.AppSettings["abgxOptions"].ToString(); //abgx360 Options from Config File
            string abgxArgs = abgxOptions + " \"" + isoFile + "\" > \"" + gamePath + "\\abgx360-" + isoFileNum + ".html\""; //Args to Pass to abgx360
            string abgxCommands = "/c abgx360.exe " + abgxArgs; //Commands to run abgx360 + Args through cmd
            File.AppendAllText(_logFile, "Running abgx360 on: " + isoFile + "\n");
            Process.Start("cmd.exe", abgxCommands).WaitForExit(); //Run abgx360 and wait for exit
            Process.Start("cmd.exe", abgxCommands).WaitForExit(); //Run abgx360 and wait for exit a second time
        }

        private static bool IsoSizeCheck(string isoFile)
        {
            FileInfo isoFileInfo = new FileInfo(isoFile); //Create FileInfo for the ISO File
            long isoSize = isoFileInfo.Length; //Get Size of ISO File
            File.AppendAllText(_logFile, "ISO Size is: " + isoSize + "\n");

            if (isoSize > 5000000000)
                return true;

            else
                return false;
        }

        private static void MoveWiiGame(string gamePath, string wiiGame)
        {
            File.AppendAllText(_logFile, "Game ISO is too small\n");
            Directory.Move(gamePath, wiiGame); //Move undersized Game to Wii Directory
        }

        private static void MoveFailedGame(string gamePath, string failedGame)
        {
            Directory.Move(gamePath, failedGame);
        }

        private static void MovePassedGame(string gamePath, string passedGame)
        {
            File.AppendAllText(_logFile, "Game passed and is being moved to: " + passedGame + "\n");
            Directory.Move(gamePath, passedGame); //Move Game to Passed Directory
        }

        private static void ExtractIso(string gamePath, string gameName)
        {
            string extractDir = ConfigurationSettings.AppSettings["extractDir"].ToString(); //Extract Directory from Config File
            string[] isoFiles = Directory.GetFiles(gamePath, "*.iso"); //Create Array of ISO Files
            Array.Sort(isoFiles); //Sort ISO Files Alphabetically

            if (isoFiles.Length == 1)
            {
                string extractedGameDir = extractDir + @"\" + gameName;
                string isoFile = isoFiles[0];
                bool isoTooSmall = IsoSizeCheck(isoFile);

                RunExiso(isoFile, extractedGameDir);

                DeleteUpdates(extractedGameDir);
            }

            else if (isoFiles.Length == 2)
            {
                string isoFileOne = isoFiles[0];
                string isoFileTwo = isoFiles[1];
                string extractedGameDirOne = extractDir + @"\" + gameName + " Disk 1";
                string extractedGameDirTwo = extractDir + @"\" + gameName + " Disk 2";

                RunExiso(isoFileOne, extractedGameDirOne);
                RunExiso(isoFileTwo, extractedGameDirTwo);

                DeleteUpdates(extractedGameDirOne);
                DeleteUpdates(extractedGameDirTwo);
            }

            else if (isoFiles.Length == 3)
            {
                string isoFileOne = isoFiles[0];
                string isoFileTwo = isoFiles[1];
                string isoFileThree = isoFiles[2];
                string extractedGameDirOne = extractDir + @"\" + gameName + " Disk 1";
                string extractedGameDirTwo = extractDir + @"\" + gameName + " Disk 2";
                string extractedGameDirThree = extractDir + @"\" + gameName + " Disk 3";

                RunExiso(isoFileOne, extractedGameDirOne);
                RunExiso(isoFileTwo, extractedGameDirTwo);
                RunExiso(isoFileThree, extractedGameDirThree);

                DeleteUpdates(extractedGameDirOne);
                DeleteUpdates(extractedGameDirTwo);
                DeleteUpdates(extractedGameDirThree);
            }
        }

        private static void RunExiso(string isoFile, string extractedGameDir)
        {
            string exisoArgs = "-d \"" + extractedGameDir + "\" -x \"" + isoFile + "\"";
            string exisoCommands = "/c exiso.exe " + exisoArgs;
            Directory.CreateDirectory(extractedGameDir);
            Process.Start("cmd.exe", exisoCommands).WaitForExit(); //Run exiso and wait for exit
        }

        private static void DeleteUpdates(string extractedGameDir)
        {
            string updateFolder = extractedGameDir + @"\$SystemUpdate";
            if (Directory.Exists(updateFolder))
                Directory.Delete(updateFolder,true);

            else
                Console.WriteLine("No $SystemUpdate Folder found");
        }
    }
}