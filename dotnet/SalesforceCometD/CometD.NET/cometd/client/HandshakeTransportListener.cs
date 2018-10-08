using System;
using System.Collections.Generic;
using Cometd.Bayeux;
using Cometd;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class HandshakeTransportListener : PublishTransportListener
        {
            public HandshakeTransportListener(BayeuxClient bayeuxClient)
                : base(bayeuxClient)
            {
            }

            protected override void onFailure(Exception x, IList<IMessage> messages)
            {
                LogHelper.Log($"HandshakeTransportListener: onFailure: {x}");

                bayeuxClient.updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new RehandshakingState(bayeuxClient, oldState.handshakeFields, oldState.transport, oldState.nextBackoff());
                    });
                base.onFailure(x, messages);
            }

            protected override void processMessage(IMutableMessage message)
            {
                LogHelper.Log($"HandshakeTransportListener: processMessage()");

                if (Channel_Fields.META_HANDSHAKE.Equals(message.Channel))
                    bayeuxClient.processHandshake(message);
                else
                    base.processMessage(message);
            }
        }
    }
}