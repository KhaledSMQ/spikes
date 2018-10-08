using System;
using System.Collections.Generic;
using Cometd.Bayeux;
using Cometd;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class DisconnectTransportListener : PublishTransportListener
        {
            public DisconnectTransportListener(BayeuxClient bayeuxClient)
                : base(bayeuxClient)
            {
            }

            protected override void onFailure(Exception x, IList<IMessage> messages)
            {
                LogHelper.Log($"DisconnectTransportListener: onFailure() exception: {x}");

                bayeuxClient.updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new DisconnectedState(bayeuxClient, oldState.transport);
                    });
                base.onFailure(x, messages);
            }

            protected override void processMessage(IMutableMessage message)
            {
                LogHelper.Log($"DisconnectTransportListener: processMessage()");

                if (Channel_Fields.META_DISCONNECT.Equals(message.Channel))
                    bayeuxClient.processDisconnect(message);
                else
                    base.processMessage(message);
            }
        }
    }
}