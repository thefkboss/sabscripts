using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace XBox360
{
    class Program
    {
        static void Main(string[] args)
        {
            string abgxOptions = ConfigurationSettings.AppSettings["abgxOptions"].ToString(); //abgx360 Options from Config File
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Log Directory  from Config File
            string failedDir = ConfigurationSettings.AppSettings["failedDir"].ToString(); //Failed Directory from Config File
            string wiiDir = ConfigurationSettings.AppSettings["wiiDir"].ToString(); //Wii Directory from Config File
            string passDir = ConfigurationSettings.AppSettings["passDir"].ToString(); //Pass Directory from Config File
            string gamePath = args[0]; //Get Game Path from CMD Line Arguments
            string gameName = args[2]; //Get Game Name from CMD Line Arguments
            bool gamePassed = true; //Boolean for Game Passed
            string failedGame = failedDir + @"\" + gameName; //Path to Failed Game Name
            string wiiGame = wiiDir + @"\" + gameName; //Path to Wii Game Directory
            string passedGame = passDir + @"\" + gameName; //Path to Passed Game Directory
            DirectoryInfo gamePathInfo = new DirectoryInfo(gamePath); //DirectoryInfo for Game Path
            DirectoryInfo failedGameInfo = new DirectoryInfo(failedGame); //DirectoryInfo for Failed Game Path
            DirectoryInfo wiiGameInfo = new DirectoryInfo(wiiGame); //DirectoryInfo for Wii Game Path
            DirectoryInfo passedGameInfo = new DirectoryInfo(passedGame); //DirectoryInfo for Passed Game Path
            string logFile = logDir + @"\XBox360.txt"; // Log File
            string batFile = failedDir + "\\" + gameName + ".bat"; //Batch File for Failed Game
            File.AppendAllText(logFile, "\n");
            File.AppendAllText(logFile, "#######################################################################\n");
            File.AppendAllText(logFile, "Game name is: " + gameName + "\n");
            File.AppendAllText(logFile, "Game path is: " + gamePath + "\n");
            string[] isoFiles = Directory.GetFiles(gamePath, "*.iso"); //Create Array of ISO Files
            Array.Sort(isoFiles); //Sort ISO Files Alphabetically
            for (int isoFilesPos = 0; isoFilesPos < isoFiles.Length; isoFilesPos++) //Loop through ISO Files, Run abgx360 and check html for verification string
            {
                FileInfo isoFileInfo = new FileInfo(isoFiles[isoFilesPos]); //Create FileInfo for the ISO File
                long isoSize = isoFileInfo.Length; //Get SIze of ISO File
                File.AppendAllText(logFile, "ISO Size is: " + isoSize + "\n");
                if (isoSize > 5000000000) //Check to see if ISO File is big enough
                {
                    int isoFileNum = 1 + isoFilesPos; //int to see which ISO FIle it is
                    string abgxArgs = abgxOptions + " \"" + isoFiles[isoFilesPos] + "\" > \"" + gamePath + "\\abgx360-" + isoFileNum + ".html\""; //Args to Pass to abgx360
                    string abgxCommands = "/c abgx360.exe " + abgxArgs; //Commands to run abgx360 + Args through cmd
                    File.AppendAllText(logFile, "Running abgx360 on: " + isoFileInfo + "\n");
                    Process.Start("cmd.exe", abgxCommands).WaitForExit(); //Run abgx360 and wait for exit
                    Process.Start("cmd.exe", abgxCommands).WaitForExit(); //Run abgx360 and wait for exit a second time

                    Thread.Sleep(1000);

                    string htmlFile = gamePath + "\\abgx360-" + isoFileNum + ".html"; //Get full path to HTML file
                    string htmlFileContents = File.ReadAllText(htmlFile); //Read html file into string
                    if (!htmlFileContents.Contains("Verification was successful!")) //Check string for "Verification was successful!"
                    {
                        gamePassed = false;
                        break;
                    }
                }
                else
                {
                    File.AppendAllText(logFile, "Game ISO is too small\n");
                    Directory.Move(gamePath, wiiGame); //Move undersized Game to Wii Directory
                }
            }
            if (gamePassed)
            {
                if (passedGameInfo.FullName != gamePathInfo.FullName) //If game is not in already in the Passed Directory
                {
                    File.AppendAllText(logFile, "Game passed and is being moved to: " + passedGame + "\n");
                    Directory.Move(gamePath, passedGame); //Move Game to Passed Directory

                }
            }
            else
            {
                File.WriteAllText(batFile, "XBox360.exe \"" + failedGame + "\" 2 \"" + gameName + "\"");
                if (failedGameInfo.FullName != gamePathInfo.FullName) //If Game failed and is not in failed dir, move it
                {
                    File.AppendAllText(logFile, "Game Failed and is being moved to: " + failedGame + "\n");
                    Directory.Move(gamePath, failedGame);
                }
            }
        }
    }
}