using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace SABSyncGUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SABSyncGUI());
        }


    }
}
