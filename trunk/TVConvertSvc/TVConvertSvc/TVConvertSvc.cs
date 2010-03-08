using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using System.Configuration;

namespace TVConvertSvc
{
    public partial class TVConvertSvc : ServiceBase
    {
        public TVConvertSvc()
        {
            InitializeComponent();
        }

        private static string _logDir = ConfigurationSettings.AppSettings["logDir"].ToString(); // Log Directory from app.config
        private static string _watchDir = ConfigurationSettings.AppSettings["watchDir"].ToString(); // Temp Directory from app.config
        private static string _logFile = _logDir + "\\TVConvertSvc.txt"; // Log File
        static FileProcessor _processor; //Create file Processor (For the Queue)

        protected override void OnStart(string[] args)
        {
            File.AppendAllText(_logFile, "Starting TVConvert Service @ " + DateTime.Now + "\n");
            File.AppendAllText(_logFile, "Watching folder: " + _watchDir + "\n");
            _processor = new FileProcessor();
            InitializeWatcher();
        }

        protected override void OnStop()
        {
            File.AppendAllText(_logFile, "Shutting down TVConvert Service @ " + DateTime.Now + "\n");
        }

        static FileSystemWatcher dirWatcher;

        //Initialize Watcher
        static void InitializeWatcher()
        {
            try
            {

                dirWatcher = new FileSystemWatcher();
                dirWatcher.Path = _watchDir;

                dirWatcher.Created += new FileSystemEventHandler(dirWatcher_Created);
                dirWatcher.EnableRaisingEvents = true;
                dirWatcher.Filter = "*.*";
            }
            catch (Exception e)
            {
                File.AppendAllText(@"C:\Logs\Exception.txt", e.ToString());
            }
        }

        //Action when File is Created
        static void dirWatcher_Created(object sender, FileSystemEventArgs e)
        {
            //Add New File to Queue
            _processor.QueueInput(e.FullPath);
        }

    }
}
