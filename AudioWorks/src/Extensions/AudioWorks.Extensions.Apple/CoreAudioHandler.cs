/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

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
            var logger = LoggerManager.LoggerFactory.CreateLogger<CoreAudioHandler>();

            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Environment.GetEnvironmentVariable("CommonProgramFiles"),
                "Apple",
                "Apple Application Support");

            var coreAudioLibrary = Path.Combine(nativeLibraryPath, "CoreAudioToolbox.dll");

            if (!File.Exists(coreAudioLibrary))
            {
                logger.LogWarning("Missing CoreAudioToolbox.dll. Install Apple Application Support (part of iTunes).");
                return false;
            }

            // Prefix the PATH variable with the default Apple Application Support installation directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());

            logger.LogInformation("Using CoreAudio version {0}.",
                FileVersionInfo.GetVersionInfo(coreAudioLibrary).ProductVersion);
#endif
            return true;
        }
    }
}
