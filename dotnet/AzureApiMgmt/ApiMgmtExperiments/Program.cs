using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiMgmtExperiments
{
    class Program
    {
        private static HttpClient Client { get; set; }
        private const string SubscriptionId = "e4ae04ea-0883-48be-9e34-cb47224d36a4";
        private const string ResourceGroupName = "RT_Platform-DevTest";
        private const string ServiceName = "genapimpocdev";

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.ReadLine();
        }

        private void Run()
        {
            PrepareClient();
            Console.WriteLine("Users:" + GetData("users").Result);
            Console.WriteLine("Subscriptions:" + GetData("subscriptions").Result);
            Console.WriteLine("Products:" + GetData("products").Result);
            Console.WriteLine("APIs:" + GetData("apis").Result);
        }

        /*private async Task<string> AccessApiMgmt()
        {
            using (var client = new HttpClient())
            {
                var token = GenerateToken();
                //var token =
                //    "integration&201708300400&i0OWlJUl/vJn5zs+qOnpKovgoW5nvE1czZxYhEqlBCQvI4FfPh2XjN+TyD+kZLedPTtZ040Gpcdz01B9Qe8djQ==";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SharedAccessSignature", token);

                var url = GetBaseUrl(serviceName, subscriptionId, resourceGroupName) + "/users?api-version=2017-03-01";
                var response = await client.GetStringAsync(url);
                return response;
            }
        }*/

        private static void PrepareClient()
        {
            Client = new HttpClient();
            var token = GenerateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SharedAccessSignature", token);
        }

        private static string GenerateToken()
        {
            // the following values are taken from the credentials section of the security page in the publisher portal
            // id is the value of the identifier text box
            // key is the value of the primary or secondary key text boxes
            var id = "integration";
            var key = "Pkm77KgGEwmLwb0Nrtjd10ReiVlt0vVkMoVl+dMwMQaMC/RuVK3wSXHEYybBkWG/XD82CrksMFTwg7+d5w4gIg==";

            var tomorrow = DateTime.UtcNow.AddDays(1);
            // we need to truncate to a resolution of one minute
            var expiry = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, tomorrow.Hour, tomorrow.Minute, 0, DateTimeKind.Utc);

            using (var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var dataToSign = id + "\n" + expiry.ToString("O", CultureInfo.InvariantCulture);
                var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                var signature = Convert.ToBase64String(hash);
                var exp = expiry.ToString("yyyyMMddHHmm");
                var encodedToken = $"{id}&{exp}&{signature}";
                return encodedToken;
            }
        }

        private static string GetUrl(string relativeUrl, string apiVersion = "2017-03-01")
        {
            var url = "https://" + $"{ServiceName}.management.azure-api.net/subscriptions/{SubscriptionId}/resourcegroups/{ResourceGroupName}/providers/Microsoft.ApiManagement/service/{ServiceName}/{relativeUrl}?api-version={apiVersion}";
            return url;
        }

        private static async Task<string> GetData(string relativeUrl)
        {
            var url = GetUrl(relativeUrl);
            var response = await Client.GetStringAsync(url);
            return response;
        }
    }
}
