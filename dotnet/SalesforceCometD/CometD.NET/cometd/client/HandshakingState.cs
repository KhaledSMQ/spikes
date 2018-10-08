using System;
using System.Collections.Generic;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class HandshakingState : BayeuxClientState
        {
            public HandshakingState(BayeuxClient bayeuxClient, IDictionary<String, Object> handshakeFields, ClientTransport transport)
                : base(bayeuxClient, State.HANDSHAKING, handshakeFields, null, transport, null, 0)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.REHANDSHAKING ||
                       newState.type == State.CONNECTING ||
                       newState.type == State.DISCONNECTED;
            }

            public override void enter(State oldState)
            {
                // Always reset the subscriptions when a handshake has been requested.
                bayeuxClient.resetSubscriptions();
            }

            public override void execute()
            {
                LogHelper.Log($"HandshakingState: execute()...");

                // The state could change between now and when sendHandshake() runs;
                // in this case the handshake message will not be sent and will not
                // be failed, because most probably the client has been disconnected.
                bayeuxClient.sendHandshake();

                LogHelper.Log($"HandshakingState: execute() done.");
            }
        }
    }
}