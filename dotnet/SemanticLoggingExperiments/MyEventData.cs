using System.Diagnostics.Tracing;

namespace SemanticLoggingExperiments
{
    [EventData]
    public class MyEventData
    {
        public string PropertyOne { get; set; }
        public string[] Texts { get; set; }
    }
}
