using System;
using PipesAndFilters;

namespace Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            var fetcher = new UrlWithNavigationFetcher();
            var contents = fetcher.Fetch(new Uri("http://finance.yahoo.com"));
            Console.ReadKey();
        }
    }
}
