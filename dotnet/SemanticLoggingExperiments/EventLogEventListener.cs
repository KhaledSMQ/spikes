using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace SemanticLoggingExperiments
{
    public class EventLogEventListener : EventListener
    {
        private EventLog _log;

        public EventLogEventListener(string source)
            : this(null, source)
        { }

        public EventLogEventListener(string eventLogName, string source)
        {
            _log = new EventLog();
            if (!string.IsNullOrEmpty(eventLogName))
                _log.Log = eventLogName;
            if (!string.IsNullOrEmpty(source))
                _log.Source = source;
            //This code needs Admin permission. Create the Source by an install script and remove this code
            //from here.
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, eventLogName);
            }
        }

        public IDisposable Subscribe(IObserver<EventEntry> observer)
        {
            return null;
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            //var data = eventData.FlattenedPayload();  
            var data = eventData.Payload.Select(PayloadItemFormatter.Format);

            switch (eventData.Level)
            {
                case EventLevel.LogAlways:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Information,
                    eventData.EventId);
                    break;
                case EventLevel.Critical:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Error,
                    eventData.EventId);
                    break;
                case EventLevel.Error:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Error,
                    eventData.EventId);
                    break;
                /*case EventLevel.Informational:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Information,
                    eventData.EventId);
                    break;*/
                /*case EventLevel.Informational:
                    _log.WriteEvent(CreateEventInstance(EventLevel.Informational, eventData.EventId),
                        eventData.Payload);
                    break;*/
                case EventLevel.Informational:
                    _log.WriteEvent(CreateEventInstance(EventLevel.Informational, eventData.EventId),
                        data.ToArray());
                    break;
                case EventLevel.Verbose:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Information,
                    eventData.EventId);
                    break;
                case EventLevel.Warning:
                    _log.WriteEntry(FormatMessage(eventData),
                    EventLogEntryType.Warning,
                    eventData.EventId);
                    break;
            }
        }

        private string FormatMessage(EventWrittenEventArgs eventData)
        {
            if (eventData.Message == null)
                return string.Empty;

            var results = string.Format(eventData.Message, eventData.Payload.ToArray<object>());
            return results;
        }

        private static EventInstance CreateEventInstance(EventLevel severity, int id)
        {
            if (id > 65535)
            {
                id = 65535;
            }
            if (id < 0)
            {
                id = 0;
            }

            var eventInstance = new EventInstance(id, 0);
            if (severity == EventLevel.Error || severity == EventLevel.Critical)
            {
                eventInstance.EntryType = EventLogEntryType.Error;
            }
            else if (severity != EventLevel.Warning)
            {
                eventInstance.EntryType = EventLogEntryType.Information;
            }
            else
            {
                eventInstance.EntryType = EventLogEntryType.Warning;
            }

            return eventInstance;
        }
    }
}
