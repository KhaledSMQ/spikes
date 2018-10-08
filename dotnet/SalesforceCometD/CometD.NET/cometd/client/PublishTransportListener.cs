using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cometd.Bayeux;
using Cometd;
using Cometd.Client.Transport;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        private class PublishTransportListener : ITransportListener
        {
            protected BayeuxClient bayeuxClient;

            public PublishTransportListener(BayeuxClient bayeuxClient)
            {
                this.bayeuxClient = bayeuxClient;
            }

            public void onSending(IList<IMessage> messages)
            {
                var text = string.Join(",", messages.Select(message => message.ToString()));
                LogHelper.Log($"PublishTransportListener: onSending: {text}");
                bayeuxClient.onSending(messages);
            }

            public void onMessages(IList<IMutableMessage> messages)
            {
                var text = string.Join(",", messages.Select(message => message.ToString()));
                LogHelper.Log($"PublishTransportListener: onMessages: {text}");
                bayeuxClient.onMessages(messages);
                foreach (IMutableMessage message in messages)
                    processMessage(message);
            }

            public void onConnectException(Exception x, IList<IMessage> messages)
            {
                onFailure(x, messages);
            }

            public void onException(Exception x, IList<IMessage> messages)
            {
                onFailure(x, messages);
            }

            public void onExpire(IList<IMessage> messages)
            {
                onFailure(new TimeoutException("expired"), messages);
            }

            public void onProtocolError(String info, IList<IMessage> messages)
            {
                onFailure(new ProtocolViolationException(info), messages);
            }

            protected virtual void processMessage(IMutableMessage message)
            {
                bayeuxClient.processMessage(message);
            }

            protected virtual void onFailure(Exception x, IList<IMessage> messages)
            {
                LogHelper.Log($"PublishTransportListener: onFailure: {x}");
                bayeuxClient.onFailure(x, messages);
                bayeuxClient.failMessages(x, messages);
            }
        }
    }
}