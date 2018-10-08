using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDataCatalog
{
    public class DataCatalogHelpers
    {
        private const string ApiVersion1 = "2016-03-30";
        private const string ApiVersionRelationships = "2017-06-30-Preview";

        private string CatalogName { get; }
        private string Token { get; }

        public DataCatalogHelpers(string catalogName, string token)
        {
            CatalogName = catalogName;
            Token = token;
        }

        public string Search(IEnumerable<string> searchTerms, IEnumerable<string> facets = null)
        {
            var uriCommand = "/search/search?" +
                                ProcessParameter("searchTerms", " ", searchTerms) +
                                ProcessParameter("&facets", ",", facets) +
                                "&count=100&api-version=" + ApiVersion1;
            var result = new HttpHelpers().Execute(CatalogName, uriCommand, Token);
            return result;
        }

        public string Relationships(string fromAssetId = null, string toAssetId = null)
        {
            var uriCommand = "relationships/find/all?" +
                             ProcessParameters(new Dictionary<string, string> {{ "fromAssetId", fromAssetId }, { "toAssetId", toAssetId }}) +
                             "&api-version=" + ApiVersionRelationships;
            var result = new HttpHelpers().Execute(CatalogName, uriCommand, Token);
            return result;
        }

        private static string ProcessParameters(IDictionary<string, string> parameters)
        {
            var output = string.Join("&", parameters.Where(p => p.Value != null).Select(p => $"{p.Key}={p.Value}"));
            return output;
        }

        private static string ProcessParameter(string parameterName, string separator, IEnumerable<string> parameterValues)
        {
            var output = string.Empty;
            if (parameterValues != null)
                output = $"{parameterName}={string.Join(separator, parameterValues)}";
            return output;
        }
    }
}
