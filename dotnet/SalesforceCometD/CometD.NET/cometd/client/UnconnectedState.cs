using System;
using System.Collections.Generic;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class UnconnectedState : BayeuxClientState
        {
            public UnconnectedState(BayeuxClient bayeuxClient, IDictionary<String, Object> handshakeFields, IDictionary<String, Object> advice, ClientTransport transport, String clientId, long backoff)
                : base(bayeuxClient, State.UNCONNECTED, handshakeFields, advice, transport, clientId, backoff)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.CONNECTED ||
                       newState.type == State.UNCONNECTED ||
                       newState.type == State.REHANDSHAKING ||
                       newState.type == State.DISCONNECTED;
            }

            public override void execute()
            {
                LogHelper.Log($"UnconnectedState: execute()...");

                bayeuxClient.scheduleConnect(Interval, backoff);

                LogHelper.Log($"UnconnectedState: execute() done.");
            }
        }
    }
}