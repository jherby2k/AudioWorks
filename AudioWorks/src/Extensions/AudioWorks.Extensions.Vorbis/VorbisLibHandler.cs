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
using System.Reflection;
using System.Text;
using AudioWorks.Common;
#endif
using AudioWorks.Extensibility;
#if WINDOWS
using Microsoft.Extensions.Logging;
#endif

namespace AudioWorks.Extensions.Vorbis
{
    [PrerequisiteHandlerExport]
    public sealed class VorbisLibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86");

            var oggLibrary = Path.Combine(nativeLibraryPath, "libogg.dll");
            var vorbisLibrary = Path.Combine(nativeLibraryPath, "libvorbis.dll");

            var logger = LoggingManager.LoggerFactory.CreateLogger<VorbisLibHandler>();

            if (!File.Exists(oggLibrary))
            {
                logger.LogWarning("libogg could not be found.");
                return false;
            }

            logger.LogInformation("Using libogg version {0}.",
                FileVersionInfo.GetVersionInfo(oggLibrary).ProductVersion);

            if (!File.Exists(vorbisLibrary))
            {
                logger.LogWarning("libvorbis could not be found.");
                return false;
            }

            logger.LogInformation("Using libvorbis version {0}.",
                FileVersionInfo.GetVersionInfo(vorbisLibrary).ProductVersion);

            // Prefix the PATH variable with the correct architecture-specific directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());
#endif
            return true;
        }
    }
}
