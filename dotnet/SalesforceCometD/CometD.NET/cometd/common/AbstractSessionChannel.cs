using System;
using System.Collections.Generic;
using System.Linq;
using Cometd.Bayeux;
using Cometd.Bayeux.Client;
using Cometd;
using Cometd.Client;

namespace Cometd.Common
{
    public abstract partial class AbstractClientSession
    {
        /// <summary> <p>A channel scoped to a {@link ClientSession}.</p></summary>
        public abstract class AbstractSessionChannel : IClientSessionChannel
        {
            //protected Logger logger = Log.getLogger(GetType().FullName);
            private ChannelId _id;
            private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();
            private readonly List<IMessageListener> _subscriptions = new List<IMessageListener>();
            private readonly List<IClientSessionChannelListener> _listeners = new List<IClientSessionChannelListener>();

            public string Id => _id.ToString();
            public bool DeepWild => _id.DeepWild;
            public bool Meta => _id.isMeta();
            public bool Service => _id.isService();
            public bool Wild => _id.Wild;
            public ChannelId ChannelId => _id;
            public ICollection<string> AttributeNames => _attributes.Keys;
            public abstract IClientSession Session { get; }
            public bool HasSubscriptions => _subscriptions.Any();

            public AbstractSessionChannel(ChannelId id)
            {
                _id = id;
            }

            public void addListener(IClientSessionChannelListener listener)
            {
                _listeners.Add(listener);
            }

            public void removeListener(IClientSessionChannelListener listener)
            {
                _listeners.Remove(listener);
            }

            protected abstract void sendSubscribe();
            protected abstract void sendUnsubscribe();
            public abstract void publish(object param1);
            public abstract void publish(object param1, string param2);

            public void subscribe(IMessageListener listener)
            {
                _subscriptions.Add(listener);

                if (_subscriptions.Count == 1)
                    sendSubscribe();
            }

            public void unsubscribe(IMessageListener listener)
            {
                _subscriptions.Remove(listener);

                if (_subscriptions.Count == 0)
                    sendUnsubscribe();
            }

            public void unsubscribe()
            {
                foreach (var listener in new List<IMessageListener>(_subscriptions))
                    unsubscribe(listener);
            }

            public void resetSubscriptions()
            {
                _subscriptions.Clear();
                /*foreach (var listener in new List<IMessageListener>(_subscriptions))
                {
                    unsubscribe(listener);
                }*/
            }

            public void notifyMessageListeners(IMessage message)
            {
                foreach (var listener in _listeners)
                {
                    if (!(listener is IMessageListener messageListener))
                        continue;

                    try
                    {
                        messageListener.onMessage(this, message);
                    }
                    catch (Exception x)
                    {
                        LogHelper.Log($"AbstractSessionChannel: Error notifying message listeners: {x}");
                    }
                }

                var list = new List<IMessageListener>(_subscriptions);
                foreach (var listener in list)
                {
                    if (listener == null)
                        continue;

                    if (message.Data == null)
                        continue;

                    try
                    {
                        listener.onMessage(this, message);
                    }
                    catch (Exception x)
                    {
                        LogHelper.Log($"AbstractSessionChannel: Error notifying channel listeners: {x}");
                    }
                }
            }

            public void setAttribute(string name, object val)
            {
                _attributes[name] = val;
            }

            public object getAttribute(string name)
            {
                _attributes.TryGetValue(name, out var obj);
                return obj;
            }

            public object removeAttribute(String name)
            {
                var old = getAttribute(name);
                _attributes.Remove(name);
                return old;
            }

            public override string ToString()
            {
                return _id.ToString();
            }
        }
    }
}