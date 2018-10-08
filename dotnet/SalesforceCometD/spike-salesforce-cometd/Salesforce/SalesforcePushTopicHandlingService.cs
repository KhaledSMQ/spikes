using System.Configuration;

namespace spike_salesforce_cometd.Salesforce
{
    public class SalesforcePushTopicHandlingService
    {
        public static void Startup()
        {
            var authenticationResults = new SalesforceAuthenticationExecutor().Login();

            var client = new BayeuxClientBuilder().CreateClient(authenticationResults["access_token"], authenticationResults["instance_url"]);

            var connector = new PushTopicConnector(client, ConfigurationManager.AppSettings["salesforce:Channel"], new AccountUpdateHandler());
            connector.Connect();
        }
    }
}