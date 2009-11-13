using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            string logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); //Get logDir from app.config
            string movieDir = ConfigurationSettings.AppSettings["movieDir"].ToString(); //Get movieDir from app.config
            string hdMovieDir = ConfigurationSettings.AppSettings["hdMovieDir"].ToString(); //Get hdMovieDir from app.config
            string ipodMovieDir = ConfigurationSettings.AppSettings["ipodMovieDir"].ToString(); //Get ipodMovieDir from app.config
            string moviePath = args[0]; //Get moviePath from first CMD Line argument
            string movieName = args[2]; //Get movieName from third CMD Line argument
            string movieFileName = movieDir + "\\" + movieName + ".avi"; //Create movieFileName from movieDir + movieName
            string hdMovieMkvFileName = hdMovieDir + "\\" + movieName + ".mkv"; //Create hdMovieMkvFileName from hdMovieDir + movieName
            string hdMovieWmvFileName = hdMovieDir + "\\" + movieName + ".wmv"; //Create hdMovieWmvFileName from hdMovieDir + movieName
            string ipodMovieFileName = ipodMovieDir + "\\" + movieName + ".mp4";//Create ipodMovieFileName from ipodMovieDir + movieName
            string mencoderOptions = "-forceidx -ovc copy -oac copy -o"; //Options for mencoder (static)

            string[] filesSizeTest = Directory.GetFiles(moviePath, "*.*", SearchOption.AllDirectories); //Search moviePath for all Files, including sub-folders
            Array.Sort(filesSizeTest); //Sort Array :)

            foreach (string fileSizeTest in filesSizeTest) //Check for sample files that SAB may have missed
            {
                FileInfo fileSizeTestInfo = new FileInfo(fileSizeTest); //Create FileInfo
                if (fileSizeTestInfo.Length < 140000000) //If File is less than 140MB consider it a sample (For HD Samples)
                {
                    File.Delete(fileSizeTest); //Delete the sample file
                }
            }

            string[] aviFiles = Directory.GetFiles(moviePath, "*.avi", SearchOption.AllDirectories); //Search moviePath for AVI Files, including sub-folders
            Array.Sort(aviFiles); //Sort Array :)

            if (aviFiles.Length == 1) //If Only one AVI was found rename and move file
            {
                string aviFile = aviFiles[0]; //Set aviFile to first & only file in aviFiles Array
                FileInfo aviFileInfo = new FileInfo(aviFile); //Create file info for getting file size
                long aviSize = aviFileInfo.Length; //Create long to store AVI File Size
                if (aviSize > 150000000) //If avi is greater than 150MB then continue
                {
                    File.Move(aviFile, movieFileName); //Move/Rename File
                    Directory.Delete(moviePath, true); //Delete directory + all files
                    return; //Exit
                }
            }

            else if (aviFiles.Length == 2) //If two AVIs were found
            {
                string aviFileOne = aviFiles[0]; //Name aviFileOne to first file in Array
                string aviFileTwo = aviFiles[1]; //Name aviFileTwo to second file in Array
                FileInfo aviFileOneInfo = new FileInfo(aviFileOne); //Create FileInfo for aviFileOne
                FileInfo aviFileTwoInfo = new FileInfo(aviFileTwo); //Create FileInfo for aviFileTwo

                long aviOneSize = aviFileOneInfo.Length; //Create long to store file size
                long aviTwoSize = aviFileTwoInfo.Length; //Create long to store file size

                if (aviOneSize > 150000000 && aviTwoSize > 150000000) //Ensure that both files are actually
                {
                    string mencoderCommand = mencoderOptions + " \"" + movieFileName + "\" \"" + aviFileOne+ "\" \"" +aviFileTwoInfo + "\""; //Create string to hold commands to pass to mencoder
                    Process.Start("mencoder.exe", mencoderCommand).WaitForExit(); //Run mencoder on the two AVIs & wait for finish
                    Directory.Delete(moviePath, true); //Delete directory + all files
                    return; //Exit
                }

                else
                {
                    //Too many AVI Files found - will have to manually check...
                }
            }

            else
            {
                //Too Many Files... abort!
            }

            string[] mkvFiles = Directory.GetFiles(moviePath, "*.mkv", SearchOption.AllDirectories); //Search moviePath for MKV Files, including sub-folders
            Array.Sort(mkvFiles); //Sort Array :)

            if (mkvFiles.Length == 1) //Ensure only one MKV was found
            {
                string mkvFile = mkvFiles[0]; //Create mkvFile from first & only string in Array
                FileInfo mkvFileInfo = new FileInfo(mkvFile); //Create FileInfo

                if (mkvFileInfo.Length > 1200000000) //Ensure MKV is over 1200MB in size
                {
                    File.Move(mkvFile, hdMovieMkvFileName); //Move + Rename MKV File
                    Directory.Delete(moviePath, true); //Delete directory + contents
                    return; //Exit
                }
            }

            else
            {
                //Too Many MKVs! Abort!
            }

            string[] wmvFiles = Directory.GetFiles(moviePath, "*.wmv", SearchOption.AllDirectories); //Search moviePath for WMV Files, including sub-folders
            Array.Sort(wmvFiles); //Sort Array :)

            if (wmvFiles.Length == 1) //Ensure only one WMV was found
            {
                string wmvFile = wmvFiles[0]; //Create wmvFile from first & only string in Array
                FileInfo wmvFileInfo = new FileInfo(wmvFile); //Create FileInfo

                if (wmvFileInfo.Length > 1200000000) //Ensure WMV is over 1200MB in size
                {
                    File.Move(wmvFile, hdMovieWmvFileName); //Move + Rename WMV File
                    Directory.Delete(moviePath, true); //Delete directory + contents
                    return; //Exit
                }
            }

            else
            {
                //Too Many WMVs! Abort!
            }

            string[] mp4Files = Directory.GetFiles(moviePath, "*.mp4", SearchOption.AllDirectories); //Search moviePath for MP4 Files, including sub-folders
            Array.Sort(mp4Files); //Sort Array :)

            if (mp4Files.Length == 1) //Ensure only one MP4 was found
            {
                string mp4File = mp4Files[0]; //Create wmvFile from first & only string in Array
                FileInfo mp4FileInfo = new FileInfo(mp4File); //Create FileInfo

                if (mp4FileInfo.Length > 100000000) //Ensure MP4 is over 100MB in size
                {
                    File.Move(mp4File, ipodMovieFileName); //Move + Rename MP4 File
                    Directory.Delete(moviePath, true); //Delete directory + contents
                    return; //Exit
                }
            }

            else
            {
                //Too Many MP4s! Abort!
            }
        }
    }
}
