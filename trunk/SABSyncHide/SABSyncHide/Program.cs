using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SABSyncHide
{
    class Program
    {
        static void Main(string[] args)
        {
            Process process = new Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = @"SABSync.exe";

            // Go
            process.Start();
        }
    }
}
