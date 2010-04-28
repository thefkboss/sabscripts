using System;
using System.Collections.Generic;
using System.Linq;

namespace SABSync
{
    public class NzbInfo
    {
        public NzbInfo()
        {
            Sites = Config.NzbSites;
            Qualities = Config.DownloadQuality;
        }

        public NzbInfo(IList<NzbSite> sites, string[] qualities)
        {
            Sites = sites;
            Qualities = qualities;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Site { get; set; }
        public string Link { get; set; }
        private IList<NzbSite> Sites { get; set; }
        private string[] Qualities { get; set; }

        public bool IsValidQuality()
        {
            NzbSite site = Sites.Where(s => s.Name == Site).SingleOrDefault();
            if (site == null || site.UseQuality)
                return Qualities.Any(quality => Title.ToLower().Contains(quality));
            return true;
        }

        public bool IsPassworded()
        {
            return Title.EndsWith("(Passworded)", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}