using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace SemanticLoggingExperiments
{
    [EventSource(Name = "SelfDescribingEventSource")]
    public class EventSourceSelfDescribing : EventSource
    {
        public static readonly EventSourceSelfDescribing Log = new EventSourceSelfDescribing();

        private EventSourceSelfDescribing()
            : base(EventSourceSettings.EtwSelfDescribingEventFormat)
        { }

        [Event(1, Message = "This is MethodOne with params '{0}'", Level = EventLevel.Informational)]
        public void MethodOne(string text, string[] texts)
        {
            if (IsEnabled())
                WriteEvent(1, text, texts);
        }

        [Event(2, Message = "This is MethodTwo with param '{0}'", Level = EventLevel.Informational)]
        public void MethodTwo(double value, Dictionary<string, int> dict)
        {
            if (IsEnabled())
                WriteEvent(2, value, dict);
        }

        public void MethodThree(string text, MyEventData data)
        {
            if (IsEnabled())
                WriteEvent(3, text, data);
        }

        public void ActivityStart(string name)
        {
            if (IsEnabled())
                WriteEvent(4, name);
        }

        public void ActivityStop(string name)
        {
            if (IsEnabled())
                WriteEvent(5, name);
        }
    }
}
