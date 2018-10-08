using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using Cometd.Bayeux;
using Cometd;
using Cometd.Client.Transport;
using Cometd.Common;


namespace Cometd.Client
{
    /// <summary> </summary>
    public partial class BayeuxClient : AbstractClientSession, IBayeux
    {
        public const String BACKOFF_INCREMENT_OPTION = "backoffIncrement";
        public const String MAX_BACKOFF_OPTION = "maxBackoff";
        public const String BAYEUX_VERSION = "1.0";

        //private Logger logger;
        private TransportRegistry transportRegistry = new TransportRegistry();
        private Dictionary<String, Object> options = new Dictionary<String, Object>();
        private BayeuxClientState bayeuxClientState;
        private Queue<IMutableMessage> messageQueue = new Queue<IMutableMessage>();
        private CookieCollection cookieCollection = new CookieCollection();
        private ITransportListener handshakeListener;
        private ITransportListener connectListener;
        private ITransportListener disconnectListener;
        private ITransportListener publishListener;
        private long backoffIncrement;
        private long maxBackoff;
        private static Mutex stateUpdateInProgressMutex = new Mutex();
        private int stateUpdateInProgress;
        private AutoResetEvent stateChanged = new AutoResetEvent(false);

        private delegate BayeuxClientState BayeuxClientStateUpdater_createDelegate(BayeuxClientState oldState);
        private delegate void BayeuxClientStateUpdater_postCreateDelegate();

        public long BackoffIncrement => backoffIncrement;
        public long MaxBackoff => maxBackoff;
        public override String Id => bayeuxClientState.clientId;
        public override bool Connected => isConnected(bayeuxClientState);
        public override bool Handshook => isHandshook(bayeuxClientState);
        public bool Disconnected => isDisconnected(bayeuxClientState);
        public IList<String> AllowedTransports => transportRegistry.AllowedTransports;
        public ICollection<String> KnownTransportNames => transportRegistry.KnownTransports;
        public ICollection<String> OptionNames => options.Keys;
        public IDictionary<String, Object> Options => options;

        protected State CurrentState => bayeuxClientState.type;

        public BayeuxClient(String url, IList<ClientTransport> transports)
        {
            //logger = Log.getLogger(GetType().FullName + "@" + this.GetHashCode());
            //Console.WriteLine(GetType().FullName + "@" + this.GetHashCode());

            handshakeListener = new HandshakeTransportListener(this);
            connectListener = new ConnectTransportListener(this);
            disconnectListener = new DisconnectTransportListener(this);
            publishListener = new PublishTransportListener(this);

            if (transports == null || transports.Count == 0)
                throw new ArgumentException("Transport cannot be null");

            foreach (ClientTransport t in transports)
                transportRegistry.Add(t);

            foreach (String transportName in transportRegistry.KnownTransports)
            {
                ClientTransport clientTransport = transportRegistry.getTransport(transportName);
                if (clientTransport is HttpClientTransport)
                {
                    HttpClientTransport httpTransport = (HttpClientTransport)clientTransport;
                    httpTransport.setURL(url);
                    httpTransport.setCookieCollection(cookieCollection);
                }
            }

            bayeuxClientState = new DisconnectedState(this, null);
        }

        public String getCookie(String name)
        {
            Cookie cookie = cookieCollection[name];
            if (cookie != null)
                return cookie.Value;
            return null;
        }

        public void setCookie(String name, String val)
        {
            setCookie(name, val, -1);
        }

        public void setCookie(String name, String val, int maxAge)
        {
            Cookie cookie = new Cookie(name, val, null, null);
            if (maxAge > 0)
            {
                cookie.Expires = DateTime.Now;
                cookie.Expires.AddMilliseconds(maxAge);
            }
            cookieCollection.Add(cookie);
        }

        private bool isConnected(BayeuxClientState bayeuxClientState)
        {
            return bayeuxClientState.type == State.CONNECTED;
        }

        private bool isHandshook(BayeuxClientState bayeuxClientState)
        {
            return bayeuxClientState.type == State.CONNECTING || bayeuxClientState.type == State.CONNECTED || bayeuxClientState.type == State.UNCONNECTED;
        }

        private bool isHandshaking(BayeuxClientState bayeuxClientState)
        {
            return bayeuxClientState.type == State.HANDSHAKING || bayeuxClientState.type == State.REHANDSHAKING;
        }

        private bool isDisconnected(BayeuxClientState bayeuxClientState)
        {
            return bayeuxClientState.type == State.DISCONNECTING || bayeuxClientState.type == State.DISCONNECTED;
        }

        public override void handshake()
        {
            handshake(null);
        }

        public override void handshake(IDictionary<String, Object> handshakeFields)
        {
            initialize();

            IList<String> allowedTransports = AllowedTransports;
            // Pick the first transport for the handshake, it will renegotiate if not right
            ClientTransport initialTransport = transportRegistry.getTransport(allowedTransports[0]);
            initialTransport.init();
            //Console.WriteLine("Using initial transport {0} from {1}", initialTransport.Name, Print.List(allowedTransports));

            updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new HandshakingState(this, handshakeFields, initialTransport);
                    });
        }

        public State handshake(int waitMs)
        {
            return handshake(null, waitMs);
        }

        public State handshake(IDictionary<String, Object> template, int waitMs)
        {
            handshake(template);
            ICollection<State> states = new List<State>();
            states.Add(State.CONNECTING);
            states.Add(State.DISCONNECTED);
            return waitFor(waitMs, states);
        }

        protected bool sendHandshake()
        {
            LogHelper.Log($"BayeauxClient: sendHandshake()...");

            BayeuxClientState bayeuxClientState = this.bayeuxClientState;

            if (isHandshaking(bayeuxClientState))
            {
                IMutableMessage message = newMessage();
                if (bayeuxClientState.handshakeFields != null)
                    foreach (KeyValuePair<String, Object> kvp in bayeuxClientState.handshakeFields)
                        message.Add(kvp.Key, kvp.Value);

                message.Channel = Channel_Fields.META_HANDSHAKE;
                message[Message_Fields.SUPPORTED_CONNECTION_TYPES_FIELD] = AllowedTransports;
                message[Message_Fields.VERSION_FIELD] = BayeuxClient.BAYEUX_VERSION;
                if (message.Id == null)
                    message.Id = newMessageId();

                //Console.WriteLine("Handshaking with extra fields {0}, transport {1}", Print.Dictionary(bayeuxClientState.handshakeFields), Print.Dictionary(bayeuxClientState.transport as IDictionary<String, Object>));
                bayeuxClientState.send(handshakeListener, message);
                LogHelper.Log($"BayeauxClient: sendHandshake() returning true.");
                return true;
            }
            LogHelper.Log($"BayeauxClient: sendHandshake() returning false.");
            return false;
        }

        public State waitFor(int waitMs, ICollection<State> states)
        {
            var statesToLog = states?.Select(st => st.ToString()) ?? new[]{ "" };
            LogHelper.Log($"BayeauxClient: waitFor() waitMs: {waitMs}, states: {string.Join(", ", statesToLog)}.");

            DateTime stop = DateTime.Now.AddMilliseconds(waitMs);
            int duration = waitMs;

            State s = CurrentState;
            if (states.Contains(s))
            {
                LogHelper.Log($"BayeauxClient: waitFor() returning {s} (1).");
                return s;
            }

            while (stateChanged.WaitOne(duration))
            {
                if (stateUpdateInProgress == 0)
                {
                    s = CurrentState;
                    if (states.Contains(s))
                    {
                        LogHelper.Log($"BayeauxClient: waitFor() returning {s} (2).");
                        return s;
                    }
                }

                duration = (int)(stop - DateTime.Now).TotalMilliseconds;
                if (duration <= 0) break;
            }

            s = CurrentState;
            if (states.Contains(s))
            {
                LogHelper.Log($"BayeauxClient: waitFor() returning {s} (3).");
                return s;
            }

            LogHelper.Log($"BayeauxClient: waitFor() returning {State.INVALID}.");

            return State.INVALID;
        }

        protected bool sendConnect()
        {
            LogHelper.Log($"BayeauxClient: sendConnect()...");

            BayeuxClientState bayeuxClientState = this.bayeuxClientState;
            if (isHandshook(bayeuxClientState))
            {
                IMutableMessage message = newMessage();
                message.Channel = Channel_Fields.META_CONNECT;
                message[Message_Fields.CONNECTION_TYPE_FIELD] = bayeuxClientState.transport.Name;
                if (bayeuxClientState.type == State.CONNECTING || bayeuxClientState.type == State.UNCONNECTED)
                {
                    // First connect after handshake or after failure, add advice
                    message.getAdvice(true)["timeout"] = 0;
                }
                bayeuxClientState.send(connectListener, message);
                LogHelper.Log($"BayeauxClient: sendConnect() returning true.");
                return true;
            }

            LogHelper.Log($"BayeauxClient: sendConnect() returning false.");

            return false;
        }

        protected override ChannelId newChannelId(String channelId)
        {
            // Save some parsing by checking if there is already one
            AbstractSessionChannel channel;
            Channels.TryGetValue(channelId, out channel);
            return channel == null ? new ChannelId(channelId) : channel.ChannelId;
        }

        protected override AbstractSessionChannel newChannel(ChannelId channelId)
        {
            return new BayeuxClientChannel(this, channelId);
        }

        protected override void sendBatch()
        {
            BayeuxClientState bayeuxClientState = this.bayeuxClientState;
            if (isHandshaking(bayeuxClientState))
                return;

            IList<IMutableMessage> messages = takeMessages();
            if (messages.Count > 0)
                sendMessages(messages);
        }

        protected bool sendMessages(IList<IMutableMessage> messages)
        {
            BayeuxClientState bayeuxClientState = this.bayeuxClientState;
            if (bayeuxClientState.type == State.CONNECTING || isConnected(bayeuxClientState))
            {
                bayeuxClientState.send(publishListener, messages);
                return true;
            }
            else
            {
                failMessages(null, ObjectConverter.ToListOfIMessage(messages));
                return false;
            }
        }

        private int PendingMessages
        {
            get
            {
                int value = messageQueue.Count;

                var state = bayeuxClientState;
                var clientTransport = state.transport as ClientTransport;
                if(clientTransport != null)
                    value += clientTransport.isSending ? 1 : 0;

                return value;
            }
        }

        /// <summary>
        /// Wait for send queue to be emptied
        /// </summary>
        /// <param name="timeoutMS"></param>
        /// <returns>true if queue is empty, false if timed out</returns>
        public bool waitForEmptySendQueue(int timeoutMS)
        {
            LogHelper.Log($"BayeauxClient: waitForEmptySendQueue() timeoutMS: {timeoutMS}.");

            if (PendingMessages == 0)
            {
                LogHelper.Log($"BayeauxClient: waitForEmptySendQueue() returning true (1).");
                return true;
            }

            DateTime start = DateTime.Now;

            while ((DateTime.Now - start).TotalMilliseconds < timeoutMS)
            {
                if (PendingMessages == 0)
                {
                    LogHelper.Log($"BayeauxClient: waitForEmptySendQueue() returning true (2).");
                    return true;
                }

                System.Threading.Thread.Sleep(100);
            }

            LogHelper.Log($"BayeauxClient: waitForEmptySendQueue() returning false.");
            return false;
        }

        private IList<IMutableMessage> takeMessages()
        {
            IList<IMutableMessage> queue = new List<IMutableMessage>(messageQueue);
            messageQueue.Clear();
            return queue;
        }

        public override void disconnect()
        {
            LogHelper.Log($"BayeauxClient: disconnect()");
            updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        if (isConnected(oldState))
                            return new DisconnectingState(this, oldState.transport, oldState.clientId);
                        else
                            return new DisconnectedState(this, oldState.transport);
                    });
        }

        public void abort()
        {
            LogHelper.Log($"BayeauxClient: abort()");

            updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new AbortedState(this, oldState.transport);
                    });
        }

        protected void processHandshake(IMutableMessage handshake)
        {
            LogHelper.Log($"BayeauxClient: processHandshake() successful? {handshake.Successful}");

            if (handshake.Successful)
            {
                // @@ax: I think this should be able to return a list of objects?
                Object serverTransportObject;
                handshake.TryGetValue(Message_Fields.SUPPORTED_CONNECTION_TYPES_FIELD, out serverTransportObject);
                IList<Object> serverTransports = serverTransportObject as IList<Object>;
                //Console.WriteLine("Supported transport: {0}", serverTransport);
                //IList<Object> serverTransports = new List<Object>();
                //serverTransports.Add(serverTransport);
                IList<ClientTransport> negotiatedTransports = transportRegistry.Negotiate(serverTransports, BAYEUX_VERSION);
                ClientTransport newTransport = negotiatedTransports.Count == 0 ? null : negotiatedTransports[0];
                if (newTransport == null)
                {
                    updateBayeuxClientState(
                            delegate(BayeuxClientState oldState)
                            {
                                return new DisconnectedState(this, oldState.transport);
                            },
                            delegate()
                            {
                                receive(handshake);
                            });

                    // Signal the failure
                    String error = "405:c" + transportRegistry.AllowedTransports + ",s" + serverTransports.ToString() + ":no transport";

                    handshake.Successful = false;
                    handshake[Message_Fields.ERROR_FIELD] = error;
                    // TODO: also update the advice with reconnect=none for listeners ?
                }
                else
                {
                    updateBayeuxClientState(
                            delegate(BayeuxClientState oldState)
                            {
                                if (newTransport != oldState.transport)
                                {
                                    oldState.transport.reset();
                                    newTransport.init();
                                }

                                String action = getAdviceAction(handshake.Advice, Message_Fields.RECONNECT_RETRY_VALUE);
                                if (Message_Fields.RECONNECT_RETRY_VALUE.Equals(action))
                                    return new ConnectingState(this, oldState.handshakeFields, handshake.Advice, newTransport, handshake.ClientId);
                                else if (Message_Fields.RECONNECT_NONE_VALUE.Equals(action))
                                    return new DisconnectedState(this, oldState.transport);

                                return null;
                            },
                            delegate()
                            {
                                receive(handshake);
                            });
                }
            }
            else
            {
                updateBayeuxClientState(
                        delegate(BayeuxClientState oldState)
                        {
                            String action = getAdviceAction(handshake.Advice, Message_Fields.RECONNECT_HANDSHAKE_VALUE);
                            if (Message_Fields.RECONNECT_HANDSHAKE_VALUE.Equals(action) || Message_Fields.RECONNECT_RETRY_VALUE.Equals(action))
                                return new RehandshakingState(this, oldState.handshakeFields, oldState.transport, oldState.nextBackoff());
                            else if (Message_Fields.RECONNECT_NONE_VALUE.Equals(action))
                                return new DisconnectedState(this, oldState.transport);
                            return null;
                        },
                        delegate()
                        {
                            receive(handshake);
                        });
            }
        }

        protected void processConnect(IMutableMessage connect)
        {
            LogHelper.Log($"BayeauxClient: processConnect()");

            updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        IDictionary<String, Object> advice = connect.Advice;
                        if (advice == null)
                            advice = oldState.advice;

                        String action = getAdviceAction(advice, Message_Fields.RECONNECT_RETRY_VALUE);
                        if (connect.Successful)
                        {
                            if (Message_Fields.RECONNECT_RETRY_VALUE.Equals(action))
                                return new ConnectedState(this, oldState.handshakeFields, advice, oldState.transport, oldState.clientId);
                            else if (Message_Fields.RECONNECT_NONE_VALUE.Equals(action))
                                // This case happens when the connect reply arrives after a disconnect
                                // We do not go into a disconnected state to allow normal processing of the disconnect reply
                                return new DisconnectingState(this, oldState.transport, oldState.clientId);
                        }
                        else
                        {
                            if (Message_Fields.RECONNECT_HANDSHAKE_VALUE.Equals(action))
                                return new RehandshakingState(this, oldState.handshakeFields, oldState.transport, 0);
                            else if (Message_Fields.RECONNECT_RETRY_VALUE.Equals(action))
                                return new UnconnectedState(this, oldState.handshakeFields, advice, oldState.transport, oldState.clientId, oldState.nextBackoff());
                            else if (Message_Fields.RECONNECT_NONE_VALUE.Equals(action))
                                return new DisconnectedState(this, oldState.transport);
                        }

                        return null;
                    },
                delegate()
                {
                    receive(connect);
                });
        }

        protected void processDisconnect(IMutableMessage disconnect)
        {
            LogHelper.Log($"BayeauxClient: processDisconnect()");

            updateBayeuxClientState(
                    delegate(BayeuxClientState oldState)
                    {
                        return new DisconnectedState(this, oldState.transport);
                    },
                    delegate()
                    {
                        receive(disconnect);
                    });
        }

        protected void processMessage(IMutableMessage message)
        {
            // logger.debug("Processing message {}", message);
            receive(message);
        }

        private String getAdviceAction(IDictionary<String, Object> advice, String defaultResult)
        {
            String action = defaultResult;
            if (advice != null && advice.ContainsKey(Message_Fields.RECONNECT_FIELD))
                action = ((String)advice[Message_Fields.RECONNECT_FIELD]);
            return action;
        }

        protected bool scheduleHandshake(long interval, long backoff)
        {
            LogHelper.Log($"BayeauxClient: scheduleHandshake() with interval: {interval} and backoff: {backoff}");

            return scheduleAction("handshake", () =>
                    //delegate(object sender, ElapsedEventArgs e)
                    {
                        sendHandshake();
                    }
                    , interval, backoff);
        }

        protected bool scheduleConnect(long interval, long backoff)
        {
            LogHelper.Log($"BayeauxClient: scheduleConnect() with interval: {interval} and backoff: {backoff}");

            return scheduleAction("connect", () =>
                    //delegate(object sender, ElapsedEventArgs e)
                    {
                        sendConnect();
                    }
                    , interval, backoff);
        }

        //private bool scheduleAction(ElapsedEventHandler action, long interval, long backoff)
        private bool scheduleAction(string actionName, Action action, long interval, long backoff)
        {
            LogHelper.Log($"BayeauxClient: scheduleAction() actionName: {actionName} with interval: {interval} and backoff: {backoff}");

            /*System.Timers.Timer timer = new System.Timers.Timer(); // @@ax: What about support for multiple timers?
            timer.Elapsed += action;
            long wait = interval + backoff;
            if (wait <= 0) wait = 1;
            timer.Interval = wait;
            timer.AutoReset = false;
            timer.Enabled = true;*/

            Task.Run(() =>
            {
                var wait = interval + backoff;
                if (wait <= 0) wait = 1;
                Task.Delay((int) wait);
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    LogHelper.Log($"BayeauxClient: scheduleAction() failed. actionName: {actionName}, interval: {interval}, backoff: {backoff}, error: {e}");
                }
            }).ConfigureAwait(false);

            return true;
        }

        public ITransport getTransport(String transport)
        {
            return transportRegistry.getTransport(transport);
        }

        public void setDebugEnabled(bool debug)
        {
            // ... todo
        }

        public bool isDebugEnabled()
        {
            return false;
        }

        protected void initialize()
        {
            LogHelper.Log($"BayeauxClient: initialize()");

            Int64 backoffIncrement = ObjectConverter.ToInt64(getOption(BACKOFF_INCREMENT_OPTION), 1000L);
            this.backoffIncrement = backoffIncrement;

            Int64 maxBackoff = ObjectConverter.ToInt64(getOption(MAX_BACKOFF_OPTION), 30000L);
            this.maxBackoff = maxBackoff;
        }

        protected void terminate()
        {
            LogHelper.Log($"BayeauxClient: terminate()");

            IList<IMutableMessage> messages = takeMessages();
            failMessages(null, ObjectConverter.ToListOfIMessage(messages));
        }

        public Object getOption(String qualifiedName)
        {
            Object obj;
            options.TryGetValue(qualifiedName, out obj);
            return obj;
        }

        public void setOption(String qualifiedName, Object val)
        {
            options[qualifiedName] = val;
        }

        protected IMutableMessage newMessage()
        {
            return new DictionaryMessage();
        }

        protected void enqueueSend(IMutableMessage message)
        {
            LogHelper.Log($"BayeauxClient: enqueueSend()");

            if (canSend())
            {
                IList<IMutableMessage> messages = new List<IMutableMessage>();
                messages.Add(message);
                bool sent = sendMessages(messages);
                //Console.WriteLine("{0} message {1}", sent?"Sent":"Failed", message);
            }
            else
            {
                messageQueue.Enqueue(message);
                //Console.WriteLine("Enqueued message {0} (batching: {1})", message, this.Batching);
            }
        }

        private bool canSend()
        {
            return !isDisconnected(this.bayeuxClientState) && !this.Batching && !isHandshaking(this.bayeuxClientState);
        }

        protected void failMessages(Exception x, IList<IMessage> messages)
        {
            LogHelper.Log($"BayeauxClient: failMessages()...");

            foreach (IMessage message in messages)
            {
                IMutableMessage failed = newMessage();
                failed.Id = message.Id;
                failed.Successful = false;
                failed.Channel = message.Channel;
                failed["message"] = messages;
                if (x != null)
                    failed["exception"] = x;

                var text = string.Join(",", failed.Select(m => m.ToString()));
                LogHelper.Log($"BayeauxClient: failMessages(). Failed message(s): {text}");

                receive(failed);
            }
        }

        public void onSending(IList<IMessage> messages)
        {
        }

        public void onMessages(IList<IMutableMessage> messages)
        {
        }

        public virtual void onFailure(Exception x, IList<IMessage> messages)
        {
            LogHelper.Log($"BayeauxClient: onFailure() exception {x}");

            //Console.WriteLine("{0}", x.ToString());
        }

        private void updateBayeuxClientState(BayeuxClientStateUpdater_createDelegate create)
        {
            updateBayeuxClientState(create, null);
        }

        private void updateBayeuxClientState(BayeuxClientStateUpdater_createDelegate create, BayeuxClientStateUpdater_postCreateDelegate postCreate)
        {
            LogHelper.Log($"BayeauxClient: updateBayeuxClientState()...");

            stateUpdateInProgressMutex.WaitOne();
            ++stateUpdateInProgress;
            stateUpdateInProgressMutex.ReleaseMutex();

            BayeuxClientState newState = null;
            BayeuxClientState oldState = bayeuxClientState;

            newState = create(oldState);

            LogHelper.Log($"BayeauxClient: updateBayeuxClientState() old state: {oldState}, new state: {newState}");

            if (newState == null)
                throw new SystemException();

            if (!oldState.isUpdateableTo(newState))
            {
                LogHelper.Log($"BayeauxClient: updateBayeuxClientState() State is not updatable from {oldState} to {newState}");
                return;
            }

            bayeuxClientState = newState;

            postCreate?.Invoke();

            if (oldState.Type != newState.Type)
                newState.enter(oldState.Type);

            LogHelper.Log($"BayeauxClient: updateBayeuxClientState() executing on {newState}...");
            newState.execute();
            LogHelper.Log($"BayeauxClient: updateBayeuxClientState() executed on {newState}.");

            // Notify threads waiting in waitFor()
            stateUpdateInProgressMutex.WaitOne();
            --stateUpdateInProgress;

            if (stateUpdateInProgress == 0)
                stateChanged.Set();
            stateUpdateInProgressMutex.ReleaseMutex();

            LogHelper.Log($"BayeauxClient: updateBayeuxClientState() done.");
        }

        public String dump()
        {
            return "";
        }
    }
}
