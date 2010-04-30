using System;
using System.Net;

namespace SABSync
{
    public class SabService
    {
        private static Logger logger = new Logger();

        public SabService()
        {
            Config = new Config();
        }

        private Config Config { get; set; }

        public string AddByUrl(NzbInfo nzb)
        {
            // TODO: create an sab action type
            string mode = "addurl";
            string name = nzb.Link;
            string cat = "tv";
            string nzbname = CleanUrlString(CleanString(nzb.Title));
            string action = string.Format("mode={0}&name={1}&cat={2}&nzbname={3}", mode, name, cat, nzbname);

            string request = string.Format(Config.SabRequest, action);
            logger.Log("Adding report [{0}] to the queue.", nzb.Title);

            return SendRequest(request);
        }

        public string AddByNewzbinId(NzbInfo nzb)
        {
            string mode = "addid";
            string name = Convert.ToInt64(nzb.Id).ToString();
            string action = string.Format("mode={0}&name={1}", mode, name);

            string request = string.Format(Config.SabRequest, action);
            logger.Log("Adding report [{0}] to the queue.", name);

            return SendRequest(request);
        }

        private static string SendRequest(string request)
        {
            logger.Log("DEBUG: " + request);

            var webClient = new WebClient();
            string response = webClient.DownloadString(request).Replace("\n", string.Empty);

            logger.Log("Queue Response: [{0}]", response);
            return response;
        }

        private string CleanUrlString(string name)
        {
            string result = name;
            string[] badCharacters =
                {
                    "%", "<", ">", "#", "{", "}", "|", "\\", "^", "`", "[", "]", "`", ";", "/", "?",
                    ":", "@", "=", "&", "$"
                };
            string[] goodCharacters =
                {
                    "%25", "%3C", "%3E", "%23", "%7B", "%7D", "%7C", "%5C", "%5E", "%7E", "%5B",
                    "%5D", "%60", "%3B", "%2F", "%3F", "%3A", "%40", "%3D", "%26", "%24"
                };

            for (int i = 0; i < badCharacters.Length; i++)
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");

            return result.Trim();
        }

        private string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = {"\\", "/", "<", ">", "?", "*", ":", "|", "\""};
            string[] goodCharacters = {"+", "+", "{", "}", "!", "@", "-", "#", "`"};

            for (int i = 0; i < badCharacters.Length; i++)
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");

            return result.Trim();
        }
    }
}