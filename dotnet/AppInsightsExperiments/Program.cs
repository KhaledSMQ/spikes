using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using SpikesCo.Platform.Diagnostics.Logging;
using Microsoft.ApplicationInsights;

namespace AppInsightsExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
            var p = new Program();
            p.StraightTrace();
            p.TraceViaSource();
		    p.TraceViaTelemetry();
			Console.WriteLine("Done. Press Enter to exit.");
			Console.ReadLine();
		}

	    private void StraightTrace()
	    {
            /*
            	<system.diagnostics>
		            <trace autoflush="true">
			            <listeners>
				            <add name="consoleListener" type="System.Diagnostics.ConsoleTraceListener">
					            <filter type="System.Diagnostics.EventTypeFilter" initializeData="Warning"/>
				            </add>
				            <add name="myAppInsightsListener"
					            type="Microsoft.ApplicationInsights.TraceListener.ApplicationInsightsTraceListener, Microsoft.ApplicationInsights.TraceListener"/>
			            </listeners>
		            </trace>
	            </system.diagnostics>
            */
            var dt = DateTime.Now;
            Trace.TraceInformation("This is an informational message " + dt);
            Trace.TraceWarning("This is a warning message " + dt);
            Trace.TraceError("This is an error message " + dt);
        }

	    private void TraceViaSource()
	    {
	        using (var trace = TraceActivity.Create("Tracing via source"))
	        {
                var dt = DateTime.Now;
                trace.Verbose(1001, "This is a verbose message sent via source " + dt);
                trace.Information(1002, "This is an informational message sent via source " + dt);
                trace.Warning(1003, "This is a warning message sent via source " + dt);
                trace.Error(1004, "This is an error message sent via source " + dt);
                trace.Critical(1005, "This is a critical message sent via source " + dt);
	            try
	            {
                    throw new DivideByZeroException("Test exception 1");
                }
                catch (Exception e)
	            {
                    trace.Warning(1006, e, "This is a warning message with exception sent via source " + dt);
                }
                try
                {
                    throw new ArgumentException("Test exception 2");
                }
                catch (Exception e)
                {
                    trace.Error(1004, e, "This is an error message sent via source " + dt);
                }
                try
                {
                    throw new ArgumentNullException("Test exception 3");
                }
                catch (Exception e)
                {
                    trace.Critical(1005, e, "This is a critical message sent via source " + dt);
                }
            }
        }

	    private void TraceViaTelemetry()
	    {
	        var text = new[] {"AAA", "BBB", "CCC", "DDD"};
	        var random = new Random();

	        var telemetry = new TelemetryClient();
	        telemetry.Context.InstrumentationKey = ConfigurationManager.AppSettings["AIKey"];

	        foreach (var i in Enumerable.Range(1, 20))
	        {
	            var properties = new Dictionary<string, string>();
                properties["Number"] = i.ToString();
	            properties["SomeProperty"] = text[random.Next(4)];
	            var metrics = new Dictionary<string, double>();
	            metrics["Score"] = i * 1.5;
                metrics["SomeMetric"] = i * 7.3 % 100;
                telemetry.TrackEvent("SampleEvent " + i, properties, metrics);
	        }

            try
            {
                throw new DivideByZeroException("Test exception 1");
            }
            catch (Exception e)
            {
                telemetry.TrackException(e, new Dictionary<string, string> { { "Prop1", "abc" }, { "Prop2", "def" } }, new Dictionary<string, double> { { "Metric1", 2.7 }, { "Metric2", 3.14 } });
            }
            try
            {
                throw new ArgumentException("Test exception 2");
            }
            catch (Exception e)
            {
                telemetry.TrackException(e, new Dictionary<string, string> { { "Prop1", "bcd" }, { "Prop2", "ef" }, { "Prop3", "ghij" } }, new Dictionary<string, double> { { "Metric1", 5.7 }, { "Metric2", 8.1 } });
            }
            try
            {
                throw new ArgumentNullException("Test exception 3");
            }
            catch (Exception e)
            {
                telemetry.TrackException(e, new Dictionary<string, string> { { "Prop1", "def" }, { "Prop2", "ghi" } }, new Dictionary<string, double> { { "Metric1", 9.7 }, { "Metric2", 13.14 } });
            }
        }
    }
}
