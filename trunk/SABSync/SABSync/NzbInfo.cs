using System;
using System.Collections.Generic;
using System.Linq;

namespace SABSync
{
    public class NzbInfo
    {
        public NzbInfo()
        {
            Qualities = Config.DownloadQuality;
        }

        public NzbInfo(string[] qualities)
        {
            Qualities = qualities;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public NzbSite Site { get; set; }
        public string Link { get; set; }
        private string[] Qualities { get; set; }

        public bool IsValidQuality()
        {
            if (Site == null || Site.UseQuality)
                return Qualities.Any(quality => Title.ToLower().Contains(quality));
            return true;
        }

        public bool IsPassworded()
        {
            return Title.EndsWith("(Passworded)", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}