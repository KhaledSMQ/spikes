using Cometd.Bayeux.Client;
using Cometd.Client;

namespace spike_salesforce_cometd.Salesforce
{
    public class PushTopicConnector
    {
        private readonly BayeuxClient _client;
        private readonly string _channel;
        private readonly IMessageListener _listener;

        public PushTopicConnector(BayeuxClient client, string channel, IMessageListener listener)
        {
            _client = client;
            _channel = channel;
            _listener = listener;
        }

        public void Connect()
        {
            _client.RegisterListener(_channel, _listener);
            _client.handshake();
            _client.waitFor(1000, new[] { State.CONNECTED });
            //_client.getChannel(_channel).subscribe(_listener);
        }

        public void Disconnect()
        {
            _client.disconnect();
            _client.waitFor(1000, new[] { State.DISCONNECTED });
        }
    }
}