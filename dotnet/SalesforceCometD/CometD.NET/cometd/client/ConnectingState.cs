using System;
using System.Collections.Generic;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class ConnectingState : BayeuxClientState
        {
            public ConnectingState(BayeuxClient bayeuxClient, IDictionary<String, Object> handshakeFields, IDictionary<String, Object> advice, ClientTransport transport, String clientId)
                : base(bayeuxClient, State.CONNECTING, handshakeFields, advice, transport, clientId, 0)
            {
            }

            public override bool isUpdateableTo(BayeuxClientState newState)
            {
                return newState.type == State.CONNECTED ||
                       newState.type == State.UNCONNECTED ||
                       newState.type == State.REHANDSHAKING ||
                       newState.type == State.DISCONNECTING ||
                       newState.type == State.DISCONNECTED;
            }

            public override void execute()
            {
                LogHelper.Log($"ConnectingState: execute()...");

                // Send the messages that may have queued up before the handshake completed
                bayeuxClient.sendBatch();
                bayeuxClient.scheduleConnect(Interval, backoff);

                LogHelper.Log($"ConnectingState: execute() done.");
            }
        }
    }
}