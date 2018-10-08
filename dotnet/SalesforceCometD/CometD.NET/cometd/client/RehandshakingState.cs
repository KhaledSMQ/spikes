using System;
using System.Collections.Generic;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class RehandshakingState : BayeuxClientState
        {
            public RehandshakingState(BayeuxClient bayeuxClient, IDictionary<String, Object> handshakeFields, ClientTransport transport, long backoff)
                : base(bayeuxClient, State.REHANDSHAKING, handshakeFields, null, transport, null, backoff)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.CONNECTING ||
                       newState.type == State.REHANDSHAKING ||
                       newState.type == State.DISCONNECTED;
            }

            public override void enter(State oldState)
            {
                // Reset the subscriptions if this is not a failure from a requested handshake.
                // Subscriptions may be queued after requested handshakes.
                if (oldState != State.HANDSHAKING)
                {
                    // Reset subscriptions if not queued after initial handshake
                    bayeuxClient.resetSubscriptions();
                }
            }

            public override void execute()
            {
                LogHelper.Log($"RehandshakingState: execute()...");

                bayeuxClient.scheduleHandshake(Interval, backoff);

                LogHelper.Log($"RehandshakingState: execute() done.");
            }
        }
    }
}