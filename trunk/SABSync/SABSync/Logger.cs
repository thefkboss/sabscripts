using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace SABSync
{
    public class Logger
    {
        private readonly string _logPath;
        private bool _verboseLogging;
        private readonly int _deleteLogs;

        public Logger()
        {
            _verboseLogging = Convert.ToBoolean(ConfigurationManager.AppSettings["verboseLogging"]);
            _deleteLogs = Convert.ToInt32(ConfigurationManager.AppSettings["deleteLogs"]);

            var assembly = Assembly.GetExecutingAssembly();
            _logPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "log");

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            try
            {
                var fileName = string.Format("{0:MM.dd-HH-mm}.txt", DateTime.Now);
                using (StreamWriter sw = File.AppendText(Path.Combine(_logPath, fileName)))
                {
                    sw.WriteLine(message);
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Log(string message, params object[] args)
        {
            Log(string.Format(message, args));
        }


        // Delete log files older than _deleteLogs days.  Zero keeps forever.
        public void DeleteLogs()
        {
            if (_deleteLogs <= 0) return;

            int deletedCount = 0;

            foreach (var logFileName in Directory.GetFiles(_logPath, "*.txt"))
            {
                var logFileInfo = new FileInfo(logFileName);

                if (logFileInfo.LastWriteTime < DateTime.Now.AddDays(-_deleteLogs))
                {
                    Log("Deleting Log File: " + Path.GetFileName(logFileName));
                    File.Delete(logFileName);
                    deletedCount++;
                }
            }

            if (deletedCount > 0)
                Log(Environment.NewLine);
        }
    }
}