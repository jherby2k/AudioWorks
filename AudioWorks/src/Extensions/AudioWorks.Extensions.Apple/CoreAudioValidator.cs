using System;
using System.Diagnostics;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Apple
{
    [PrerequisiteValidatorExport]
    public sealed class CoreAudioValidator : IPrerequisiteValidator
    {
        public bool HasPrerequisites()
        {
#if WINDOWS
            var coreAudioLibrary = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Environment.GetEnvironmentVariable("CommonProgramFiles"),
                "Apple",
                "Apple Application Support",
                "CoreAudioToolbox.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<CoreAudioValidator>();

            if (!File.Exists(coreAudioLibrary))
            {
                logger.LogWarning(
                    "Core Audio Toolbox not found. Install Apple Application Support (included with iTunes) for AAC and Apple Lossless support.");
                return false;
            }

            logger.LogInformation("Core Audio Toolbox version {0} is installed.",
                FileVersionInfo.GetVersionInfo(coreAudioLibrary).FileVersion);
#endif
            return true;
        }
    }
}
