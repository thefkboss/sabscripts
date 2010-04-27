using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SABSync
{
    class Program
    {
        private static Logger logger = new Logger();

        static void Main()
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName name = assembly.GetName();

                logger.Log("=====================================================================");
                logger.Log("Starting {0} v{1} - Build Date: {2:D}",
                           name.Name, name.Version, File.GetLastWriteTime(assembly.Location));
                logger.Log("Current System Time: {0}", DateTime.Now);
                logger.Log("=====================================================================");

                logger.DeleteLogs();

                var job = new SyncJob();
                job.Start();

                sw.Stop();
                logger.Log("=====================================================================");
                logger.Log("Process successfully completed. Duration {0:f1}s", sw.Elapsed.TotalSeconds);
                logger.Log(DateTime.Now.ToString());
            }
            catch (Exception e)
            {
                logger.Log("Error: {0}", e.Message);
            }
        }
    }
}
