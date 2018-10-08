using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;

namespace SemanticLoggingExperiments.Extensions
{
    public static class RollingFlatFileLog
    {
        public static SinkSubscription<RollingFlatFileSink> LogToRollingFlatFile(this IObservable<EventEntry> eventStream, string fileName, int rollSizeKB, string timestampPattern, RollFileExistsBehavior rollFileExistsBehavior, RollInterval rollInterval, IEventTextFormatter formatter = null, int maxArchivedFiles = 0, bool isAsync = false)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = FileUtil.CreateRandomFileName();
            }

            var sink = new RollingFlatFileSink(fileName, rollSizeKB, timestampPattern, rollFileExistsBehavior, rollInterval, maxArchivedFiles, formatter ?? new Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters.EventTextFormatter(), isAsync);

            var subscription = eventStream.Subscribe(sink);

            return new SinkSubscription<RollingFlatFileSink>(subscription, sink);
        }

        public static EventListener CreateListener(string fileName, int rollSizeKB, string timestampPattern, RollFileExistsBehavior rollFileExistsBehavior, RollInterval rollInterval, IEventTextFormatter formatter = null, int maxArchivedFiles = 0, bool isAsync = false)
        {
            var listener = new ObservableEventListener();
            listener.LogToRollingFlatFile(fileName, rollSizeKB, timestampPattern, rollFileExistsBehavior, rollInterval, formatter, maxArchivedFiles, isAsync);
            return listener;
        }
    }
}
