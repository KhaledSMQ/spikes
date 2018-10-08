using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TraceLoggingExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
			var dt = DateTime.Now;
			Trace.TraceInformation("This is an informational message " + dt);
			Trace.TraceWarning("This is a warning message " + dt);
			Trace.TraceError("This is an error message " + dt);
		    var listeners = Trace.Listeners;
		    var list = ListAllTraceSourcesAndListeners();
		    foreach (var listener in list)
		        Console.WriteLine(listener);
			Console.WriteLine("Done. Press Enter to exit.");
			Console.ReadLine();
		}

	    private static IEnumerable<string> ListAllTraceSourcesAndListeners()
	    {
            var sources = (List<WeakReference>)
                typeof(System.Diagnostics.TraceSource).GetField("tracesources",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);

            var list = new List<string>();

            foreach (WeakReference source in sources)
            {
                var target = source.Target;
                if (target != null)
                {
                    var traceSource = (TraceSource)source.Target;
                    var listeners = traceSource.Listeners;
                    foreach (TraceListener listener in listeners)
                        list.Add(traceSource.Name + " " + listener.Name + " " + listener.GetType());
                }
            }

            return list;
        }
    }
}
