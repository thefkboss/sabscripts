using System;
using System.Collections.Generic;
using System.Text;

namespace XBMCNotify
{
    /// <summary>
    /// http://wiki.sabnzbd.org/user-scripts
    /// 1 	The final directory of the job (full path)
    //  2 	The name of the NZB file
    //  3 	Clean version of the job name (no path info and ".nzb" removed)
    //  4 	Newzbin report number (may be empty
    //  5 	Newzbin or user-defined category
    //  6 	Group that the NZB was posted in e.g. alt.binaries.x
    /// </summary>
    public class SabParameters
    {
        public string FinalJobDirectory { get; set; }
        public string NZBFileName { get; set; }
        public string CleanedJobName { get; set; }
        public string NewzbinReportNumber { get; set; }
        public string NewzbinCategory { get; set; }
        public string UsenetGroup { get; set; }

        public static SabParameters ParseCommandLine(string[] args)
        {

            SabParameters p = new SabParameters();
            p.FinalJobDirectory = args[0];
            p.NZBFileName = args[1];
            p.CleanedJobName = args[2];
            p.NewzbinReportNumber = args[3];
            p.NewzbinCategory = args[4];
            p.UsenetGroup = args[5];

            return p;
        }
    }
}
