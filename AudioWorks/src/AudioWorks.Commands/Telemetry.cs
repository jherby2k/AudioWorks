using System.Collections.Generic;
using System.IO.IsolatedStorage;
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
        static readonly object _syncRoot = new object();

        static Telemetry()
        {
            TelemetryConfiguration.Active.InstrumentationKey = _instrumentationKey;
        }

        internal static void TrackFirstLaunch()
        {
            var store = IsolatedStorageFile.GetUserStoreForAssembly();
            lock (_syncRoot)
            {
                if (store.FileExists("FIRST_LAUNCH_TELEMETRY_GATHERED")) return;
                store.CreateFile("FIRST_LAUNCH_TELEMETRY_GATHERED");
            }

            TrackEvent("FirstLaunch");
        }

        static void TrackEvent([NotNull] string eventName, [CanBeNull] IDictionary<string, string> properties = null)
        {
            //TODO Allow telemetry to be disabled.

            if (_client == null)
            {
                _client = new TelemetryClient();
                _client.Context.Device.OperatingSystem = RuntimeInformation.OSDescription;
                _client.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _client.Context.Properties["Framework"] = RuntimeInformation.FrameworkDescription;
                _client.Context.Properties["Architecture"] = RuntimeInformation.ProcessArchitecture.ToString();
            }

            _client.TrackEvent(eventName, properties);
        }
    }
}