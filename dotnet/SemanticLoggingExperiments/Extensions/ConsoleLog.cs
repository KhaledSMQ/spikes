using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;

namespace SemanticLoggingExperiments.Extensions
{
    public static class ConsoleLog
    {
        public static SinkSubscription<ConsoleSink> LogToConsole(this IObservable<EventEntry> eventStream, IEventTextFormatter formatter = null, IConsoleColorMapper colorMapper = null)
        {
            formatter = formatter ?? new EventTextFormatter();
            colorMapper = colorMapper ?? new DefaultConsoleColorMapper();

            var sink = new ConsoleSink(formatter, colorMapper);

            var subscription = eventStream.Subscribe(sink);

            return new SinkSubscription<ConsoleSink>(subscription, sink);
        }

        public static EventListener CreateListener(IEventTextFormatter formatter = null, IConsoleColorMapper colorMapper = null)
        {
            var listener = new ObservableEventListener();
            listener.LogToConsole(formatter, colorMapper);
            return listener;
        }
    }
}
