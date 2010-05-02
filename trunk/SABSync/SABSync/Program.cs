using System;
using System.Diagnostics;

namespace SABSync
{
    internal class Program
    {
        private static readonly Logger Logger = new Logger();

        private static void Main()
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Logger.Log("=====================================================================");
                Logger.Log("Starting {0} v{1} - Build Date: {2:D}", App.Name, App.Version, App.BuildDate);
                Logger.Log("Current System Time: {0}", DateTime.Now);
                Logger.Log("=====================================================================");

                Logger.DeleteLogs();

                var job = new SyncJob();
                job.Start();

                sw.Stop();
                Logger.Log("=====================================================================");
                Logger.Log("Process successfully completed. Duration {0:f1}s", sw.Elapsed.TotalSeconds);
                Logger.Log("{0}", DateTime.Now);
            }
            catch (Exception e)
            {
                Logger.Log("Error: {0}", e.Message);
                Logger.Log(e.ToString());
            }
        }
    }
}