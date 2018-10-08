using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace spike_salesforce_cometd.Salesforce
{
    public class SalesforceAuthenticationExecutor
    {
        private readonly string _loginUri = ConfigurationManager.AppSettings["salesforce:LoginURI"];
        private readonly string _userName = ConfigurationManager.AppSettings["salesforce:UserName"];
        private readonly string _password = ConfigurationManager.AppSettings["salesforce:Password"];
        private readonly string _clientId = ConfigurationManager.AppSettings["salesforce:ClientId"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["salesforce:ClientSecret"];

        public Dictionary<string, string> Login()
        {
            string jsonResponse;

            using (var client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret},
                    {"username", _userName},
                    {"password", _password}
                });

                request.Headers.Add("X-PrettyPrint", "1");

                jsonResponse = client.PostAsync(_loginUri, request).Result.Content.ReadAsStringAsync().Result;
            }

            Trace.TraceInformation($"Salesforce: authenticated: {jsonResponse}");
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
        }

        static SalesforceAuthenticationExecutor()
        {
            // SF requires 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}