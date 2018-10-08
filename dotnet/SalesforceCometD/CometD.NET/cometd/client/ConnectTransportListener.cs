using System;
using System.Collections.Generic;
using Cometd.Bayeux;
using Cometd;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class ConnectTransportListener : PublishTransportListener
        {
            public ConnectTransportListener(BayeuxClient bayeuxClient)
                : base(bayeuxClient)
            {
            }

            protected override void onFailure(Exception x, IList<IMessage> messages)
            {
                LogHelper.Log($"ConnectTransportListener: onFailure() exception: {x}");

                bayeuxClient.updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new UnconnectedState(bayeuxClient, oldState.handshakeFields, oldState.advice, oldState.transport, oldState.clientId, oldState.nextBackoff());
                    });
                base.onFailure(x, messages);
            }

            protected override void processMessage(IMutableMessage message)
            {
                LogHelper.Log($"ConnectTransportListener: processMessage()");

                if (Channel_Fields.META_CONNECT.Equals(message.Channel))
                    bayeuxClient.processConnect(message);
                else
                    base.processMessage(message);
            }
        }
    }
}