using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class NzbInfoTest : AssertionHelper
    {
        private readonly List<NzbSite> _sites = new List<NzbSite>
        {
            new NzbSite {Name = "newzbin", Url = "newzbin.com", Pattern = @"\d{7,10}"},
            new NzbSite {Name = "nzbsDotOrg", Url = "nzbs.org", Pattern = @"\d{5,10}"},
            new NzbSite {Name = "tvnzb", Url = "tvnzb.com", Pattern = @"\d{5,10}", UseQuality = true},
            new NzbSite {Name = "nzbmatrix", Url = "nzbmatrix.com", Pattern = @"\d{6,10}"},
            new NzbSite {Name = "nzbsrus", Url = "nzbsrus.com", Pattern = @"\d{6,10}"},
        };

        private NzbSite TvNzbSite
        {
            get { return _sites.Single(s => s.Name == "tvnzb"); }
        }

        private NzbSite NzbMatrixSite
        {
            get { return _sites.Single(s => s.Name == "nzbmatrix"); }
        }

        private Config Xvid = new Config { DownloadQuality = new[] { "xvid" } };
        private Config Xvid720p = new Config { DownloadQuality = new[] { "xvid", "720p" } };

        [Test]
        public void IsValidQuality_SiteDoesNotUseQuality_ReturnsTrue()
        {
            var nzb = new NzbInfo(Xvid)
            {
                Site = NzbMatrixSite,
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void IsValidQuality_SiteNullInvalidQuality_ReturnsFalse()
        {
            var nzb = new NzbInfo(Xvid)
            {
                Site = null,
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.False);
        }

        [Test]
        public void IsValidQuality_SiteNullValidQuality_ReturnsTrue()
        {
            var nzb = new NzbInfo(Xvid)
            {
                Site = null,
                Title = "Show S01E01 Episode Xvid",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void IsValidQuality_SiteUsesQualityMultiQualityValidQuality_ReturnsTrue()
        {
            var nzb = new NzbInfo(Xvid720p)
            {
                Site = TvNzbSite,
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void IsValidQuality_SiteUsesQualityInvalidQuality_ReturnsFalse()
        {
            var nzb = new NzbInfo(Xvid)
            {
                Site = TvNzbSite,
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.False);
        }

        [Test]
        public void IsValidQuality_SiteUsesQualityValidQuality_ReturnsTrue()
        {
            var nzb = new NzbInfo(Xvid)
            {
                Site = TvNzbSite,
                Title = "Show S01E01 Episode Xvid",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }
    }
}