using System.Diagnostics;
using Cometd.Bayeux;
using Cometd.Bayeux.Client;

namespace spike_salesforce_cometd.Salesforce
{
    public class AccountUpdateHandler : IMessageListener
    {
        public void onMessage(IClientSessionChannel channel, IMessage message)
        {
            Trace.TraceInformation($"AccountUpdateHandler: received on {channel}: {message}");
        }
    }
}