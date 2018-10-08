using System;
using System.Net;

namespace PipesAndFilters
{
    public class UrlFetcher : FetcherBase<Uri, string>
    {
        public override string Fetch(Uri uri)
        {
            var client = new WebClient();
            var contents = client.DownloadString(uri.ToString());
            return contents;
        }
    }
}
