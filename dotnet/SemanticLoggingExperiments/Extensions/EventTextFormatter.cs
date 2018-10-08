using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;

namespace SemanticLoggingExperiments.Extensions
{
    public class EventTextFormatter : IEventTextFormatter
    {
        private const string NameValueFormat = "[{0} : {1}] ";

        public void WriteEvent(EventEntry eventEntry, TextWriter writer)
        {
            writer.Write(
                "{0}, {1}, {2}, {3} : {4}, {5} : {6}, {7} : {8}, {9} : {10}",
                eventEntry.GetFormattedTimestamp(null),
                eventEntry.Schema.Level,
                eventEntry.EventId,
                PropertyNames.Message,
                eventEntry.FormattedMessage,
                PropertyNames.Payload,
                FormatPayload(eventEntry),
                PropertyNames.ProcessId,
                eventEntry.ProcessId,
                PropertyNames.ThreadId,
                eventEntry.ThreadId);

            if (eventEntry.ActivityId != Guid.Empty)
            {
                writer.Write(", {0} : {1}", PropertyNames.ActivityId, eventEntry.ActivityId);
            }

            if (eventEntry.RelatedActivityId != Guid.Empty)
            {
                writer.Write(", {0} : {1}", PropertyNames.RelatedActivityId, eventEntry.RelatedActivityId);
            }

            writer.WriteLine();
        }

        private static string FormatPayload(EventEntry entry)
        {
            var eventSchema = entry.Schema;
            var sb = new StringBuilder();

            for (var i = 0; i < entry.Payload.Count; i++)
            {
                try
                {
                    var name = eventSchema.Payload[i];
                    var value = PayloadItemFormatter.Format(entry.Payload[i]);
                    sb.AppendFormat(NameValueFormat, name, value);
                }
                catch (Exception e)
                {
                    sb.AppendFormat(NameValueFormat, "Exception", string.Format(CultureInfo.CurrentCulture, "Cannot serialize the payload: {0}", e.Message));
                }
            }

            return sb.ToString();
        }

    }
}
