#if WINDOWS
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using AudioWorks.Common;
#endif
using AudioWorks.Extensibility;
#if WINDOWS
using Microsoft.Extensions.Logging;
#endif

namespace AudioWorks.Extensions.Apple
{
    [PrerequisiteHandlerExport]
    public sealed class CoreAudioHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Environment.GetEnvironmentVariable("CommonProgramFiles"),
                "Apple",
                "Apple Application Support");

            var coreAudioLibrary = Path.Combine(nativeLibraryPath, "CoreAudioToolbox.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<CoreAudioHandler>();

            if (!File.Exists(coreAudioLibrary))
            {
                logger.LogWarning(
                    "CoreAudio could not be found. Install Apple Application Support (part of iTunes) for AAC and Apple Lossless codecs.");
                return false;
            }

            logger.LogInformation("Using CoreAudio version {0}.",
                FileVersionInfo.GetVersionInfo(coreAudioLibrary).ProductVersion);

            // Prefix the PATH variable with the default Apple Application Support installation directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());
#endif
            return true;
        }
    }
}
