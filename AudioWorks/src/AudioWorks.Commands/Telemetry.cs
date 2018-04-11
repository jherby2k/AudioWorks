using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace AudioWorks.Commands
{
    static class Telemetry
    {
        [NotNull] const string _instrumentationKey = "f36d131d-ef59-4b6a-8788-403d0ef14927";
        [CanBeNull] static TelemetryClient _client;

        static Telemetry()
        {
            TelemetryConfiguration.Active.InstrumentationKey = _instrumentationKey;
        }

        internal static void TrackEvent([NotNull] string eventName, [CanBeNull] IDictionary<string, string> properties = null)
        {
            // TODO check if enabled

            if (_client == null)
                _client = new TelemetryClient();

            _client.TrackEvent(eventName, properties);
        }

        internal static void TrackFirstLaunch()
        {
            TrackEvent("FirstLaunch", new Dictionary<string, string>
            {
                ["Version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                ["Framework"] = RuntimeInformation.FrameworkDescription,
                ["Architecture"] = RuntimeInformation.ProcessArchitecture.ToString(),
                ["OSDescription"] = RuntimeInformation.OSDescription
            });
        }
    }
}