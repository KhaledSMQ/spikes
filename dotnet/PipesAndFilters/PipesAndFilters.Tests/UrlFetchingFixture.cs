using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PipesAndFilters.Tests
{
    [TestClass]
    public class UrlFetchingFixture
    {
        [TestMethod]
        public void UseSimpleNavigation()
        {
            var fetcher = new UrlFetcher();
            var contents = fetcher.Fetch(new Uri("http://www.google.com"));
        }

        [TestMethod]
        public void UseComplexNavigation()
        {
            var fetcher = new UrlWithNavigationFetcher();
            var contents = fetcher.Fetch(new Uri("http://finance.yahoo.com"));
        }
    }
}
