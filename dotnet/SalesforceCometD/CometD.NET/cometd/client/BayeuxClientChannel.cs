using System;
using Cometd.Bayeux;
using Cometd.Bayeux.Client;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        public class BayeuxClientChannel : AbstractSessionChannel
        {
            protected BayeuxClient _bayeuxClient;

            public BayeuxClientChannel(BayeuxClient bayeuxClient, ChannelId channelId)
                : base(channelId)
            {
                _bayeuxClient = bayeuxClient;
            }

            public override IClientSession Session => this as IClientSession;

            protected override void sendSubscribe()
            {
                var message = _bayeuxClient.newMessage();
                message.Channel = Channel_Fields.META_SUBSCRIBE;
                message[Message_Fields.SUBSCRIPTION_FIELD] = Id;
                _bayeuxClient.enqueueSend(message);
            }

            protected override void sendUnsubscribe()
            {
                var message = _bayeuxClient.newMessage();
                message.Channel = Channel_Fields.META_UNSUBSCRIBE;
                message[Message_Fields.SUBSCRIPTION_FIELD] = Id;
                _bayeuxClient.enqueueSend(message);
            }

            public override void publish(object data)
            {
                publish(data, null);
            }

            public override void publish(object data, string messageId)
            {
                var message = _bayeuxClient.newMessage();
                message.Channel = Id;
                message.Data = data;
                if (messageId != null)
                    message.Id = messageId;
                _bayeuxClient.enqueueSend(message);
            }
        }
    }
}