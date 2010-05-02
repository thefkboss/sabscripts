using System;
using System.Collections.Specialized;
using System.Xml;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class SyncJobTest : AssertionHelper
    {
        private Config Config { get; set; }

        public SyncJobTest()
        {
            Config = new Config(new NameValueCollection
            {
                {"tvroot", @"..\..\TV;..\..\TV2;"},
                {"rss", @"rss.fileuri.txt"},
            });
        }

        [Test]
        public void Start()
        {
            //var job = new SyncJob(Config);
            //job.Start();
        }
    }
}