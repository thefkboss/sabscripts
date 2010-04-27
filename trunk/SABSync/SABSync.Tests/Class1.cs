using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class Class1 : AssertionHelper
    {
        [Test]
        public void EqualsTest()
        {
            Expect(0, EqualTo(0));
        }
    }
}
