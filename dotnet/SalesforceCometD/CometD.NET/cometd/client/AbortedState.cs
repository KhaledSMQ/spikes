using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class AbortedState : DisconnectedState
        {
            public AbortedState(BayeuxClient bayeuxClient, ClientTransport transport)
                : base(bayeuxClient, transport)
            {
            }

            public override void execute()
            {
                LogHelper.Log($"AbortedState: execute()...");

                transport.abort();
                base.execute();

                LogHelper.Log($"AbortedState: execute() done.");
            }
        }
    }
}