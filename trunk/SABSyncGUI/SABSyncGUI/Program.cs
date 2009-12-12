using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace SABSyncGUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SABSyncGUI());

            string exePath = @"C:\Test\SABSync.exe";
            Configuration sabSyncConfig = ConfigurationManager.OpenExeConfiguration(exePath);

            string tvRootDir = sabSyncConfig.AppSettings.Settings["tvRoot"].Value;
        }

        public static string TestMethod()
        {
            string exePath = @"C:\Test\SABSync.exe";
            Configuration sabSyncConfig = ConfigurationManager.OpenExeConfiguration(exePath);

            string name = sabSyncConfig.AppSettings.Settings["tvRoot"].Value;

            return name;
        }
    }
}
