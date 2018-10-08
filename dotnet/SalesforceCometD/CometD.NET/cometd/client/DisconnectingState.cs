using System;
using Cometd.Bayeux;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class DisconnectingState : BayeuxClientState
        {
            public DisconnectingState(BayeuxClient bayeuxClient, ClientTransport transport, String clientId)
                : base(bayeuxClient, State.DISCONNECTING, null, null, transport, clientId, 0)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.DISCONNECTED;
            }

            public override void execute()
            {
                LogHelper.Log($"DisconnectingState: execute()...");

                IMutableMessage message = bayeuxClient.newMessage();
                message.Channel = Channel_Fields.META_DISCONNECT;
                send(bayeuxClient.disconnectListener, message);

                LogHelper.Log($"DisconnectedState: execute() done.");
            }
        }
    }
}