using System;
using System.Collections.Generic;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class ConnectedState : BayeuxClientState
        {
            public ConnectedState(BayeuxClient bayeuxClient, IDictionary<String, Object> handshakeFields, IDictionary<String, Object> advice, ClientTransport transport, String clientId)
                : base(bayeuxClient, State.CONNECTED, handshakeFields, advice, transport, clientId, 0)
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

            public override void enter(State oldState)
            {
                if (oldState != State.CONNECTED)
                {
                    bayeuxClient.Resubscribe();                    
                }

                base.enter(oldState);
            }

            public override void execute()
            {
                LogHelper.Log($"ConnectedState: execute()...");

                bayeuxClient.scheduleConnect(Interval, backoff);

                LogHelper.Log($"ConnectedState: execute() done.");
            }
        }
    }
}