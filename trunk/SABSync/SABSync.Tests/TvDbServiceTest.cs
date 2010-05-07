using System;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class TvDbServiceTest : AssertionHelper
    {
        [Test][Ignore("Uses live TheTvDb service")]
        public void CheckTvDb()
        {
            // "The Daily Show 2010 05 06 Mario Batali";
            const string showName = "The Daily Show";
            DateTime firstAired = DateTime.Parse("2010-05-06");

            var s = new TvDbService();
            string episodeName = s.CheckTvDb(showName, firstAired);

            Expect(episodeName, Is.EqualTo("Mario Batali"));
        }
    }
}