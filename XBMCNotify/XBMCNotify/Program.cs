using System;
using System.Collections.Generic;
using System.Text;

namespace XBMCNotify
{
    class Program
    {
        static void Main(string[] args)
        {
            SabParameters p = SabParameters.ParseCommandLine(args);

            string server = System.Configuration.ConfigurationSettings.AppSettings["Server"];
            int port = 9777;
            string sPort = System.Configuration.ConfigurationSettings.AppSettings["Port"];
            int.TryParse(sPort, out port);

            bool updateLibrary = false;
            bool cleanLibrary = false;
            string input = System.Configuration.ConfigurationSettings.AppSettings["UpdateLibrary"];
            bool.TryParse(input, out updateLibrary);
            input = System.Configuration.ConfigurationSettings.AppSettings["CleanLibrary"];
            bool.TryParse(input, out cleanLibrary);


            //XBMC.Default.EventClient ec = new XBMC.Default.EventClient();
            if (!XBMC.EventClient.Current.Connected) XBMC.EventClient.Current.Connect(server, port);

            if (XBMC.EventClient.Current.Connected)
            {
                XBMC.EventClient.Current.SendNotification(string.Format("Download Complete {0}", p.NewzbinCategory), string.Format("{0}", p.CleanedJobName), XBMC.IconType.ICON_PNG, "sabnzbd");
                if (cleanLibrary) XBMC.EventClient.Current.SendAction("CleanLibrary(video)", "");
                if (updateLibrary) XBMC.EventClient.Current.SendAction("UpdateLibrary(video)", "");
            }



        }
    }
}