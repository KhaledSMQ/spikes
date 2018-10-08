using System;
using System.Collections.Generic;
using Cometd.Bayeux;
using Cometd;
using Cometd.Client.Transport;
using Cometd.Common;

namespace Cometd.Client
{
    public partial class BayeuxClient
    {
        abstract public class BayeuxClientState
        {
            public State type;
            public IDictionary<String, Object> handshakeFields;
            public IDictionary<String, Object> advice;
            public ClientTransport transport;
            public String clientId;
            public long backoff;
            protected BayeuxClient bayeuxClient;

            public BayeuxClientState(BayeuxClient bayeuxClient, State type, IDictionary<String, Object> handshakeFields,
                IDictionary<String, Object> advice, ClientTransport transport, String clientId, long backoff)
            {
                this.bayeuxClient = bayeuxClient;
                this.type = type;
                this.handshakeFields = handshakeFields;
                this.advice = advice;
                this.transport = transport;
                this.clientId = clientId;
                this.backoff = backoff;
            }

            public long Interval
            {
                get
                {
                    long result = 0;
                    if (advice != null && advice.ContainsKey(Message_Fields.INTERVAL_FIELD))
                        result = ObjectConverter.ToInt64(advice[Message_Fields.INTERVAL_FIELD], result);

                    return result;
                }
            }

            public void send(ITransportListener listener, IMutableMessage message)
            {
                IList<IMutableMessage> messages = new List<IMutableMessage>
                {
                    message
                };
                send(listener, messages);
            }

            public void send(ITransportListener listener, IList<IMutableMessage> messages)
            {
                LogHelper.Log($"BayeuxClientState: send()");

                foreach (var message in messages)
                {
                    if (message.Id == null)
                        message.Id = bayeuxClient.newMessageId();
                    if (clientId != null)
                        message.ClientId = clientId;

                    if (!bayeuxClient.extendSend(message))
                        messages.Remove(message);
                }
                if (messages.Count > 0)
                {
                    transport.send(listener, messages);
                }
            }

            public long nextBackoff()
            {
                return Math.Min(backoff + bayeuxClient.BackoffIncrement, bayeuxClient.MaxBackoff);
            }

            public abstract bool isUpdateableTo(BayeuxClientState newState);

            public virtual void enter(State oldState)
            {
            }

            public abstract void execute();

            public State Type => type;

            public override string ToString()
            {
                return type.ToString();
            }
        }
    }
}