using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cometd.Bayeux;
using Cometd.Bayeux.Client;

namespace Cometd.Common
{
    /// <summary> <p>Partial implementation of {@link ClientSession}.</p>
    /// <p>It handles extensions and batching, and provides utility methods to be used by subclasses.</p>
    /// </summary>
    public abstract partial class AbstractClientSession : IClientSession
    {
        // @@ax: WARNING Should implement thread safety, as in http://msdn.microsoft.com/en-us/library/3azh197k.aspx
        private List<IExtension> _extensions = new List<IExtension>();
        private Dictionary<String, Object> _attributes = new Dictionary<String, Object>();
        private Dictionary<String, AbstractSessionChannel> _channels = new Dictionary<String, AbstractSessionChannel>();
        private Dictionary<string, IList<IMessageListener>> _listenersPerChannel = new Dictionary<string, IList<IMessageListener>>();
        private int _batch;
        private int _idGen = 0;

        public ICollection<String> AttributeNames => _attributes.Keys;

        public IEnumerable<IMessageListener> GetRegisteredListeners(string channelName)
        {
            if (_listenersPerChannel.TryGetValue(channelName, out var listeners))
            {
                return listeners;
            }

            return new IMessageListener[]{ };
        }

        public abstract bool Handshook { get; }
        public abstract String Id { get; }
        public abstract bool Connected { get; }

        protected Dictionary<String, AbstractSessionChannel> Channels => _channels;
        protected abstract void sendBatch();
        protected bool Batching => _batch > 0;

        public abstract void handshake(IDictionary<String, Object> template);
        public abstract void handshake();
        public abstract void disconnect();

        protected AbstractClientSession()
        {
            _listenersPerChannel = new Dictionary<string, IList<IMessageListener>>();
        }

        protected String newMessageId()
        {
            return Convert.ToString(_idGen++);
        }

        public void addExtension(IExtension extension)
        {
            _extensions.Add(extension);
        }

        public void removeExtension(IExtension extension)
        {
            _extensions.Remove(extension);
        }

        public IClientSessionChannel getChannel(String channelId)
        {
            AbstractSessionChannel channel;
            _channels.TryGetValue(channelId, out channel);

            if (channel == null)
            {
                ChannelId id = newChannelId(channelId);
                AbstractSessionChannel new_channel = newChannel(id);

                if (_channels.ContainsKey(channelId))
                    channel = _channels[channelId];
                else
                    _channels[channelId] = new_channel;

                if (channel == null)
                    channel = new_channel;
            }
            return channel;
        }

        public void startBatch()
        {
            _batch++;
        }

        public bool endBatch()
        {
            if (--_batch == 0)
            {
                sendBatch();
                return true;
            }
            return false;
        }

        public void batch(BatchDelegate batch)
        {
            startBatch();
            try
            {
                batch();
            }
            finally
            {
                endBatch();
            }
        }

        public Object getAttribute(String name)
        {
            Object obj;
            _attributes.TryGetValue(name, out obj);
            return obj;
        }

        public Object removeAttribute(String name)
        {
            try
            {
                Object old = _attributes[name];
                _attributes.Remove(name);
                return old;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void setAttribute(String name, Object val)
        {
            _attributes[name] = val;
        }

        public void RegisterListener(string channelName, IMessageListener listener)
        {
            LogHelper.Log($"AbstractClientSession: RegisterListener() for channel {channelName}.");

            if (!_listenersPerChannel.TryGetValue(channelName, out var listeners))
            {
                listeners = new List<IMessageListener>();
                _listenersPerChannel[channelName] = listeners;
            }

            listeners.Add(listener);
        }

        public void Resubscribe()
        {
            LogHelper.Log($"AbstractClientSession: Resubscribe()...");

            foreach (var channelName in _listenersPerChannel.Keys)
            {
                var channel = getChannel(channelName) as AbstractSessionChannel;

                // at the moment, we do a crude check if the channel already has
                // any subscriptions; if not, it indicates subscriptions were
                // probably reset, so we need to resubscribe
                var needToResubscribe = channel?.HasSubscriptions != true;

                LogHelper.Log($"AbstractClientSession: Do we need to resubscribe to channel {channelName}? {needToResubscribe}");

                if (needToResubscribe)
                {
                    if (_listenersPerChannel.TryGetValue(channelName, out var listeners))
                    {
                        if (listeners.Any())
                        {
                            foreach (var listener in listeners)
                                channel.subscribe(listener);

                            LogHelper.Log($"AbstractClientSession: Listeners for {channelName} subscribed.");
                        }
                        else
                            LogHelper.Log($"AbstractClientSession: No listeners to subscribe for {channelName}.");
                    }
                }
            }

            LogHelper.Log($"AbstractClientSession: Resubscribe() done.");
        }

        public void resetSubscriptions()
        {
            foreach (KeyValuePair<String, AbstractSessionChannel> channel in _channels)
            {
                channel.Value.resetSubscriptions();
            }
        }

        /// <summary> <p>Receives a message (from the server) and process it.</p>
        /// <p>Processing the message involves calling the receive {@link ClientSession.Extension extensions}
        /// and the channel {@link ClientSessionChannel.ClientSessionChannelListener listeners}.</p>
        /// </summary>
        /// <param name="message">the message received.
        /// </param>
        /// <param name="mutable">the mutable version of the message received
        /// </param>
        public void receive(IMutableMessage message)
        {
            String id = message.Channel;
            if (id == null)
            {
                throw new ArgumentException("Bayeux messages must have a channel, " + message);
            }

            if (!extendRcv(message))
                return;

            AbstractSessionChannel channel = (AbstractSessionChannel) getChannel(id);
            ChannelId channelId = channel.ChannelId;

            channel.notifyMessageListeners(message);

            foreach (String channelPattern in channelId.Wilds)
            {
                ChannelId channelIdPattern = newChannelId(channelPattern);
                if (channelIdPattern.matches(channelId))
                {
                    AbstractSessionChannel wildChannel = (AbstractSessionChannel) getChannel(channelPattern);
                    wildChannel.notifyMessageListeners(message);
                }
            }
        }

        protected bool extendSend(IMutableMessage message)
        {
            if (message.Meta)
            {
                foreach (IExtension extension in _extensions)
                    if (!extension.sendMeta(this, message))
                        return false;
            }
            else
            {
                foreach (IExtension extension in _extensions)
                    if (!extension.send(this, message))
                        return false;
            }
            return true;
        }

        protected bool extendRcv(IMutableMessage message)
        {
            if (message.Meta)
            {
                foreach (IExtension extension in _extensions)
                    if (!extension.rcvMeta(this, message))
                        return false;
            }
            else
            {
                foreach (IExtension extension in _extensions)
                    if (!extension.rcv(this, message))
                        return false;
            }
            return true;
        }

        protected abstract ChannelId newChannelId(String channelId);
        protected abstract AbstractSessionChannel newChannel(ChannelId channelId);
    }
}
