using System.Configuration;
using System.Diagnostics.Tracing;
using System.Web.Http;
using SpikesCo.Platform.Diagnostics.Logging.Semantic.ApplicationInsights;
using SpikesCo.Platform.Diagnostics.Logging.Semantic.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Owin;

namespace WebApi3
{
    public static class AppBuilderExtensions
    {
        private static EventListener EventListener { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "app")]
        public static void UseLogging(this IAppBuilder app, HttpConfiguration config)
        {
            const string instrumentationKeySettingName = "Telemetry.AI.InstrumentationKey";
            var instrumentationKey = ConfigurationManager.AppSettings[instrumentationKeySettingName];
            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            EventListener = ApplicationInsightsLog.CreateListener(instrumentationKey);
            EventListener.EnableEvents("SpikeCo-Platform-HostLifecycle", EventLevel.LogAlways, EventKeywords.All);
            EventListener.EnableEvents("MyCompany-Application1-WebApi3", EventLevel.LogAlways, EventKeywords.All);
        }
    }
}
