using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace SemanticLoggingExperiments.Extensions
{
    public static class EventListenerExtensions
    {
        public static void DisableEvents(this EventListener eventListener, string eventSourceName)
        {
            CastToObservableEventListener(eventListener).DisableEvents(eventSourceName);
        }

        public static bool EnableEvents(this EventListener eventListener, string eventSourceName, EventLevel level)
        {
            return CastToObservableEventListener(eventListener).EnableEvents(eventSourceName, level);
        }

        public static bool EnableEvents(this EventListener eventListener, string eventSourceName, EventLevel level, EventKeywords matchAnyKeyword)
        {
            return CastToObservableEventListener(eventListener).EnableEvents(eventSourceName, level, matchAnyKeyword);
        }

        public static bool EnableEvents(this EventListener eventListener, string eventSourceName, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
        {
            return CastToObservableEventListener(eventListener).EnableEvents(eventSourceName, level, matchAnyKeyword, arguments);
        }

        private static ObservableEventListener CastToObservableEventListener(EventListener eventListener)
        {
            Guard.ArgumentNotNull(eventListener, "eventListener");

            var observableEventListener = eventListener as ObservableEventListener;

            if (observableEventListener == null)
            {
                throw new ArgumentException("The argument must be an instance of ObservableEventListener.", "eventListener");
            }

            return observableEventListener;
        }
    }
}
