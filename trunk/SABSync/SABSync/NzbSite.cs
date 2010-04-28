using System.Text.RegularExpressions;

namespace SABSync
{
    public class NzbSite
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Url { get; set; }
        public bool UseQuality { get; set; }

        public string ParseId(string url)
        {
            return Regex.Match(url, Pattern).Value;
        }
    }
}