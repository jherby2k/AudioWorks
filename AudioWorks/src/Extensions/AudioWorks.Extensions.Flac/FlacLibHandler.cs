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

namespace AudioWorks.Extensions.Flac
{
    [PrerequisiteHandlerExport]
    public sealed class FlacLibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86");

            var flacLibrary = Path.Combine(nativeLibraryPath, "libFLAC.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<FlacLibHandler>();

            if (!File.Exists(flacLibrary))
            {
                logger.LogWarning("libFLAC could not be found.");
                return false;
            }

            logger.LogInformation("Using libFLAC version {0}.",
                FileVersionInfo.GetVersionInfo(flacLibrary).FileVersion);

            // Prefix the PATH variable with the correct architecture-specific directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());
#endif
            return true;
        }
    }
}
