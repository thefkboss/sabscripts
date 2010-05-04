using System;
using System.Xml;

namespace SABSync
{
    public interface ITvDbService
    {
        string CheckTvDb(string showName, int seasonNumber, int episodeNumber);
        string CheckTvDb(string showName, int year, int month, int day);
        string GetSeriesId(string seriesName);
        string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber);
    }

    public class TvDbService : ITvDbService
    {
        private const string TvDbApiKey = "5D2D188E86E07F4F";
        private static readonly Logger Logger = new Logger();
        private int _lastDay;
        private string _lastEpisodeName;
        private int _lastEpisodeNumber;
        private int _lastMonth;
        private int _lastSeasonNumber;
        private string _lastShowName;
        private int _lastYear;

        #region ITvDbService Members

        public string CheckTvDb(string showName, int seasonNumber, int episodeNumber)
        {
            if (showName == _lastShowName &&
                seasonNumber == _lastSeasonNumber && episodeNumber == _lastEpisodeNumber)
                return _lastEpisodeName;

            string episodeName = null;
            string seriesId = GetSeriesId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, seasonNumber, episodeNumber);

            _lastShowName = showName;
            _lastSeasonNumber = seasonNumber;
            _lastEpisodeNumber = episodeNumber;
            _lastEpisodeName = episodeName;

            return episodeName;
        }

        public string CheckTvDb(string showName, int year, int month, int day)
        {
            if (showName == _lastShowName &&
                year == _lastYear && month == _lastMonth && day == _lastDay)
                return _lastEpisodeName;

            string episodeName = null;
            string seriesId = GetSeriesId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, year, month, day);

            _lastShowName = showName;
            _lastYear = year;
            _lastMonth = month;
            _lastDay = day;
            _lastEpisodeName = episodeName;

            return episodeName;
        }

        public string GetSeriesId(string seriesName)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/GetSeries.php?seriesname={0}", seriesName);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList series = ((XmlElement) data[0]).GetElementsByTagName("Series");

                    if (series.Count == 0)
                    {
                        Logger.Log("No Series Found");
                        return null;
                    }

                    foreach (object s in series)
                    {
                        var tvDbElement = (XmlElement) s;

                        string tvDbShowName =
                            tvDbElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvDbShowName.ToLower() == seriesName.ToLower())
                        {
                            return
                                tvDbElement.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                        }
                    }
                    var tvDbElementLast = (XmlElement) series.Item(0);
                    return tvDbElementLast.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }
            return null;
        }

        public string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/{0}/series/{1}/default/{2}/{3}",
                    TvDbApiKey, seriesId, seasonNumber, episodeNumber);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList episode = ((XmlElement) data[0]).GetElementsByTagName("Episode");

                    if (episode.Count == 0)
                    {
                        Logger.Log("Episode Not Found");
                        return null;
                    }

                    var tvDbElement = (XmlElement) episode.Item(0);
                    string episodeName = tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                    Logger.Log("Episode Name is: " + episodeName);
                    return episodeName;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return null;
        }

        #endregion

        private static string GetEpisodeName(string seriesId, int year, int month, int day)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/{0}/series/{1}/all/",
                    TvDbApiKey, seriesId);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                string rssAirDate = year.ToString("D4") + "-" + month.ToString("D2") + "-" +
                    day.ToString("D2");

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList episodes = ((XmlElement) data[0]).GetElementsByTagName("Episode");

                    if (episodes.Count == 0)
                    {
                        Logger.Log("Episode Not Found");
                        return null;
                    }

                    foreach (object e in episodes)
                    {
                        var tvDbElement = (XmlElement) e;
                        string tvDbAirDate = tvDbElement.GetElementsByTagName("FirstAired")[0].InnerText;

                        if (tvDbAirDate == rssAirDate)
                        {
                            return tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return "unknown";
        }
    }
}