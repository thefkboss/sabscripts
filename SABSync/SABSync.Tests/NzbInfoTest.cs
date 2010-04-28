using System.Collections.Generic;
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

        [Test]
        public void SiteUsesQuality_IsValidQuality_ReturnsTrue()
        {
            var qualities = new[] { "xvid" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "tvnzb",
                Title = "Show S01E01 Episode Xvid",
            };
            
            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void SiteUsesQuality_IsInvalidQuality_ReturnsFalse()
        {
            var qualities = new[] { "xvid" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "tvnzb",
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.False);
        }

        [Test]
        public void SiteUsesQualityMultipleQualities_IsValidQuality_ReturnsTrue()
        {
            var qualities = new[] { "xvid", "720p" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "tvnzb",
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void SiteDoesNotUseQuality_IsAnyQuality_ReturnsTrue()
        {
            var qualities = new[] { "xvid" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "nzbmatrix",
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }

        [Test]
        public void SiteNotFound_IsInvalidQuality_ReturnsFalse()
        {
            var qualities = new[] { "xvid" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "unknown",
                Title = "Show S01E01 Episode 720p",
            };

            Expect(nzb.IsValidQuality(), Is.False);
        }

        [Test]
        public void SiteNotFound_IsValidQuality_ReturnsTrue()
        {
            var qualities = new[] { "xvid" };
            var nzb = new NzbInfo(_sites, qualities)
            {
                Site = "unknown",
                Title = "Show S01E01 Episode Xvid",
            };

            Expect(nzb.IsValidQuality(), Is.True);
        }
    }
}
