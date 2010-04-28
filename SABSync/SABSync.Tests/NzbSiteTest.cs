using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class NzbSiteTest : AssertionHelper
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

        [Test]
        public void ParseId_GoodId_ReturnsId()
        {
            const string url = @"http://nzbmatrix.com/api-nzb-download.php?id=625437";

            Expect(TvNzbSite.ParseId(url), EqualTo("625437"));
        }

        // TODO: ParseId should be returning 5437.  Change logic to parse query string name/values
        // instead of looking for a bounded lenth string of digits.
        [Test]
        public void ParseId_BadId_ReturnsEmptyString()
        {
            const string url = @"http://nzbmatrix.com/api-nzb-download.php?id=5437";

            Expect(TvNzbSite.ParseId(url), EqualTo(string.Empty));
        }
    }
}
