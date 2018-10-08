using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using SemanticLoggingExperiments.Extensions;
using ConsoleLog = SemanticLoggingExperiments.Extensions.ConsoleLog;
using EventTextFormatter = SemanticLoggingExperiments.Extensions.EventTextFormatter;
using RollingFlatFileLog = SemanticLoggingExperiments.Extensions.RollingFlatFileLog;

namespace SemanticLoggingExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
            //var eventLogListener = new EventLogEventListener("SemanticLogging");
            var p = new Program();
		    Console.WriteLine("Initial sources:");
		    p.ShowSources();
            //p.LogToConsole();
            p.LogToConsoleAdvanced();
            //p.LogToEventLog();
            //p.LogToEventLogAdvanced();
            //p.LogToFile();
            //p.LogToFileAdvanced();
            Console.WriteLine("Final sources:");
            p.ShowSources();
            Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

	    private void LogToConsole()
	    {
            var listener = ConsoleLog.CreateListener();
            listener.EnableEvents(MyEventSource.Log, EventLevel.LogAlways, EventKeywords.All);

            MyEventSource.Log.MethodOne("aaabbbccc", "ddd", "eee");
            MyEventSource.Log.MethodTwo(3.14);
            MyEventSource.Log.MethodThree("def");

            listener.DisableEvents(MyEventSource.Log);
            listener.Dispose();
        }

        private void LogToConsoleAdvanced()
        {
            var listener = ConsoleLog.CreateListener(new EventTextFormatter() /*new XmlEventTextFormatter()*/);
            //listener.EnableEvents(EventSourceSelfDescribing.Log, EventLevel.LogAlways, Keywords.All);
//            Extensions.EventListenerExtensions.EnableEvents(listener, "SelfDescribingEventSource", EventLevel.LogAlways, EventKeywords.All);
            listener.EnableEvents("SelfDescribingEventSource", EventLevel.LogAlways, EventKeywords.All);
            //listener.EnableEvents("System.Threading.Tasks.TplEventSource", EventLevel.LogAlways, EventKeywords.All);

            EventSourceSelfDescribing.Log.MethodOne("aaabbbccc", new[] { "ddd", "eee" });
            EventSourceSelfDescribing.Log.ActivityStart("Starting child activity");
            EventSourceSelfDescribing.Log.MethodTwo(3.14, new Dictionary<string, int> { { "twentyone", 21 }, { "twentytwo", 22 } });
            EventSourceSelfDescribing.Log.ActivityStop("Stopping child activity");
            EventSourceSelfDescribing.Log.MethodThree("def", new MyEventData { PropertyOne = "p1", Texts = new[] { "text1", "text2" } });

            //listener.DisableEvents("System.Threading.Tasks.TplEventSource");
            listener.DisableEvents("SelfDescribingEventSource");
            listener.Dispose();
        }

        private void LogToEventLog()
	    {
	        var listener = new EventLogEventListener(/*"MyEventLog",*/ "MySource2");
            listener.EnableEvents(MyEventSource.Log, EventLevel.LogAlways, EventKeywords.All);

            MyEventSource.Log.MethodOne("aaabbbccc", "ddd", "eee");
            MyEventSource.Log.MethodTwo(3.14);
            MyEventSource.Log.MethodThree("def");

            listener.DisableEvents(MyEventSource.Log);
            listener.Dispose();
        }

        private void LogToEventLogAdvanced()
        {
            var listener = new EventLogEventListener(/*"MyEventLog",*/ "MySource2Advanced");
            listener.EnableEvents(EventSourceSelfDescribing.Log, EventLevel.LogAlways, EventKeywords.All);

            EventSourceSelfDescribing.Log.MethodOne("aaabbbccc", new[] { "ddd", "eee" });
            EventSourceSelfDescribing.Log.ActivityStart("Starting child activity");
            EventSourceSelfDescribing.Log.MethodTwo(3.14, new Dictionary<string, int> { { "twentyone", 21 }, { "twentytwo", 22 } });
            EventSourceSelfDescribing.Log.ActivityStop("Stopping child activity");
            EventSourceSelfDescribing.Log.MethodThree("def", new MyEventData { PropertyOne = "p1", Texts = new[] { "text1", "text2" } });

            listener.DisableEvents(EventSourceSelfDescribing.Log);
            listener.Dispose();
        }

        private void LogToFile()
        {
            var listener = RollingFlatFileLog.CreateListener("logfile.txt", 5, "yyyyMMddHHmmss",
                Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollFileExistsBehavior.Overwrite,
                Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollInterval.Day, maxArchivedFiles: 5, formatter: new EventTextFormatter());
            listener.EnableEvents(MyEventSource.Log, EventLevel.LogAlways, EventKeywords.All);

            foreach (var i in Enumerable.Range(1, 1000))
            {
                MyEventSource.Log.MethodOne("aaabbbccc", "ddd", "eee");
                MyEventSource.Log.MethodTwo(3.14);
                MyEventSource.Log.MethodThree("def");
            }

            listener.DisableEvents(MyEventSource.Log);
            listener.Dispose();
        }

        private void LogToFileAdvanced()
        {
            var listener = RollingFlatFileLog.CreateListener("logfileadv.txt", 5, "yyyyMMddHHmmss",
                Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollFileExistsBehavior.Overwrite,
                Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollInterval.Day, maxArchivedFiles: 5, formatter: new EventTextFormatter());
            listener.EnableEvents(EventSourceSelfDescribing.Log, EventLevel.LogAlways, EventKeywords.All);

            foreach (var i in Enumerable.Range(1, 1000))
            {
                EventSourceSelfDescribing.Log.MethodOne("aaabbbccc", new[] { "ddd", "eee" });
                EventSourceSelfDescribing.Log.ActivityStart("Starting child activity");
                EventSourceSelfDescribing.Log.MethodTwo(3.14, new Dictionary<string, int> { { "twentyone", 21 }, { "twentytwo", 22 } });
                EventSourceSelfDescribing.Log.ActivityStop("Stopping child activity");
                EventSourceSelfDescribing.Log.MethodThree("def", new MyEventData { PropertyOne = "p1", Texts = new[] { "text1", "text2" } });
            }

            listener.DisableEvents(EventSourceSelfDescribing.Log);
            listener.Dispose();
        }

	    private void ShowSources()
	    {
	        var sources = EventSource.GetSources();
	        foreach (var source in sources)
	            Console.WriteLine("Source: {0}, {1}", source.Name, source.Guid);
	    }
    }
}
