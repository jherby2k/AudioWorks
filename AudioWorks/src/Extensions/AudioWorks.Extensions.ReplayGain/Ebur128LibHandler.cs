#if WINDOWS
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using AudioWorks.Common;
#endif
using AudioWorks.Extensibility;
#if WINDOWS
using Microsoft.Extensions.Logging;
#endif

namespace AudioWorks.Extensions.ReplayGain
{
    [PrerequisiteHandlerExport]
    public sealed class Ebur128LibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86");

            var ebur128Library = Path.Combine(nativeLibraryPath, "libebur128.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<Ebur128LibHandler>();

            if (!File.Exists(ebur128Library))
            {
                logger.LogWarning("libebur128 could not be found.");
                return false;
            }

            logger.LogInformation("Using libebur128 version {0}.",
                FileVersionInfo.GetVersionInfo(ebur128Library).ProductVersion);

            // Prefix the PATH variable with the correct architecture-specific directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());
#endif
            return true;
        }
    }
}
