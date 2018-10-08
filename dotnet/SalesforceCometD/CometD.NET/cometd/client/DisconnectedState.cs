using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class DisconnectedState : BayeuxClientState
        {
            public DisconnectedState(BayeuxClient bayeuxClient, ClientTransport transport)
                : base(bayeuxClient, State.DISCONNECTED, null, null, transport, null, 0)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.HANDSHAKING;
            }

            public override void execute()
            {
                LogHelper.Log($"DisconnectedState: execute()...");

                transport.reset();
                bayeuxClient.terminate();

                LogHelper.Log($"DisconnectedState: execute() done.");
            }
        }
    }
}