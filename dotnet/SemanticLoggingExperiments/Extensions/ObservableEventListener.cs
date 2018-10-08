using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema;

namespace SemanticLoggingExperiments.Extensions
{
    public sealed class ObservableEventListener : EventListener, IObservable<EventEntry>
    {
        private EventSourceSchemaCache schemaCache = EventSourceSchemaCache.Instance;
        private EventEntrySubject subject = new EventEntrySubject();
        private object deferredEnablePadlock = new object();
        private DeferredEnable deferredEnables;

        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Incorrect implementation is inherited from base class")]
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Calls the base class Dispose() and the local class Dispose(bool)")]
        public override void Dispose()
        {
            base.Dispose();
            this.subject.Dispose();
        }

        public void DisableEvents(string eventSourceName)
        {
            lock (this.deferredEnablePadlock)
            {
                Guard.ArgumentNotNullOrEmpty(eventSourceName, "eventSourceName");

                lock (this.deferredEnablePadlock)
                {
                    foreach (var eventSource in EventSource.GetSources())
                    {
                        if (string.Equals(eventSource.Name, eventSourceName, StringComparison.Ordinal))
                        {
                            this.DisableEvents(eventSource);

                            break;
                        }
                    }

                    // cleanup deferred enables
                    this.ConsumeDeferredEnable(eventSourceName, _ => { });
                }
            }
        }

        public bool EnableEvents(string eventSourceName, EventLevel level)
        {
            return this.EnableEvents(eventSourceName, level, EventKeywords.None);
        }

        public bool EnableEvents(string eventSourceName, EventLevel level, EventKeywords matchAnyKeyword)
        {
            return this.EnableEvents(eventSourceName, level, matchAnyKeyword, null);
        }

        public bool EnableEvents(string eventSourceName, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
        {
            Guard.ArgumentNotNullOrEmpty(eventSourceName, "eventSourceName");

            lock (this.deferredEnablePadlock)
            {
                foreach (var eventSource in EventSource.GetSources())
                {
                    if (string.Equals(eventSource.Name, eventSourceName, StringComparison.Ordinal))
                    {
                        this.EnableEvents(eventSource, level, matchAnyKeyword, arguments);

                        return true;
                    }
                }

                // remove any previous deferred enable for the same source name and add a new one
                this.ConsumeDeferredEnable(eventSourceName, _ => { });
                this.deferredEnables =
                    new DeferredEnable
                    {
                        EventSourceName = eventSourceName,
                        Level = level,
                        MatchAnyKeyword = matchAnyKeyword,
                        Arguments = arguments != null ? new Dictionary<string, string>(arguments) : null,
                        Next = this.deferredEnables
                    };

                return false;
            }
        }

        public IDisposable Subscribe(IObserver<EventEntry> observer)
        {
            return this.subject.Subscribe(observer);
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            Guard.ArgumentNotNull(eventData, "eventData");

            EventSchema schema = null;
            try
            {
                if (eventData.EventSource.Settings != EventSourceSettings.EtwSelfDescribingEventFormat)
                {
                    schema = this.schemaCache.GetSchema(eventData.EventId, eventData.EventSource);
                }
                else
                {
                    schema = new EventSchema(eventData.EventId, eventData.EventSource.Guid, eventData.EventSource.Name, eventData.Level,
                        eventData.Task, eventData.EventName, eventData.Opcode, eventData.Opcode.ToString(), eventData.Keywords, null, 0, eventData.PayloadNames);
                }
            }
            catch (Exception ex)
            {
                // TODO: should I notify all the observers or should I just publish a non-transient
                // error and not notify the rest of the listeners?
                // this.subject.OnError(ex);

                //SemanticLoggingEventSource.Log.ParsingEventSourceManifestFailed(eventData.EventSource.Name, eventData.EventId, ex.ToString());
                return;
            }

            var entry = EventEntry.Create(eventData, schema);

            this.subject.OnNext(entry);
        }

        /// <summary>
        /// Called for all existing event sources when the event listener is created and when a new event source is attached to the listener.
        /// </summary>
        /// <param name="eventSource">The event source.</param>
        /// <remarks>
        /// The listener processes any deferred enable events requests associated to the <paramref name="eventSource"/>.
        /// </remarks>
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);

            lock (this.deferredEnablePadlock)
            {
                this.ConsumeDeferredEnable(
                    eventSource.Name,
                    deferredEnable => this.EnableEvents(eventSource, deferredEnable.Level, deferredEnable.MatchAnyKeyword, deferredEnable.Arguments));
            }
        }

        private void ConsumeDeferredEnable(string eventSourceName, Action<DeferredEnable> action)
        {
            DeferredEnable previousEnable = null;
            for (var currentDeferredEnable = this.deferredEnables; currentDeferredEnable != null; currentDeferredEnable = currentDeferredEnable.Next)
            {
                if (string.Equals(currentDeferredEnable.EventSourceName, eventSourceName, StringComparison.Ordinal))
                {
                    // consume the deferred enable
                    action(currentDeferredEnable);

                    // remove the entry
                    if (previousEnable == null)
                    {
                        this.deferredEnables = currentDeferredEnable.Next;
                    }
                    else
                    {
                        previousEnable.Next = currentDeferredEnable.Next;
                    }

                    return;
                }

                previousEnable = currentDeferredEnable;
            }
        }

        private class DeferredEnable
        {
            public string EventSourceName;
            public EventLevel Level;
            public EventKeywords MatchAnyKeyword;
            public IDictionary<string, string> Arguments;
            public DeferredEnable Next;
        }
    }
}
