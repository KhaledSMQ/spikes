using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Cometd.Client;
using Cometd.Client.Transport;

namespace spike_salesforce_cometd.Salesforce
{
    public class BayeuxClientBuilder
    {
        private readonly int _readTimeout = 120 * 1000;
        private readonly string _streamingUri = ConfigurationManager.AppSettings["salesforce:StreamingURI"];

        public BayeuxClient CreateClient(string authToken, string instanceUrl)
        {
            return CreateClient(BuildEndpointName(instanceUrl), CreateTransports(authToken));
        }

        private IList<ClientTransport> CreateTransports(string authToken)
        {
            var options = new Dictionary<string, object> {{ClientTransport.TIMEOUT_OPTION, _readTimeout}};
            var headers = new NameValueCollection { { "Authorization", "OAuth " + authToken } };

            var transport = new LongPollingTransport(options);
            transport.AddHeaders(headers);

            return new List<ClientTransport> {transport};
        }

        private string BuildEndpointName(string instanceUrl)
        {
            var serverUri = new Uri(instanceUrl);
            return $"{serverUri.Scheme}://{serverUri.Host}{_streamingUri}";
        }

        private static BayeuxClient CreateClient(string endpoint, IList<ClientTransport> transports)
        {
            return new BayeuxClient(endpoint, transports);
        }
    }
}