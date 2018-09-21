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

namespace AudioWorks.Extensions.Lame
{
    [PrerequisiteHandlerExport]
    public sealed class LameLibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86");

            var lameLibrary = Path.Combine(nativeLibraryPath, "libmp3lame.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<LameLibHandler>();

            if (!File.Exists(lameLibrary))
            {
                logger.LogWarning("libmp3lame could not be found.");
                return false;
            }

            logger.LogInformation("Using libmp3lame version {0}.",
                FileVersionInfo.GetVersionInfo(lameLibrary).ProductVersion);

            // Prefix the PATH variable with the correct architecture-specific directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());
#endif
            return true;
        }
    }
}
