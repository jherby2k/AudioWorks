﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if WINDOWS
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
#endif
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Apple
{
    [PrerequisiteHandlerExport]
    public sealed class CoreAudioHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
#if WINDOWS
            var logger = LoggerManager.LoggerFactory.CreateLogger<CoreAudioHandler>();

            var libPath = Path.Combine(
                Environment.GetEnvironmentVariable("CommonProgramFiles") ?? string.Empty,
                "Apple",
                "Apple Application Support");

            // As of iTunes 12.10.9.3 the library is found inside the iTunes directory
            if (!Directory.Exists(libPath))
                libPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles") ?? string.Empty, "iTunes");

            AddUnmanagedLibraryPath(libPath);
            try
            {
                foreach (var methodInfo in typeof(SafeNativeMethods).GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Static))
                    Marshal.Prelink(methodInfo);
            }
            catch (DirectoryNotFoundException)
            {
                logger.LogDebug("CoreAudioToolbox.dll could not be located.");
                return false;
            }
            catch (DllNotFoundException)
            {
                logger.LogDebug("CoreAudioToolbox.dll could not be located.");
                return false;
            }
            catch (EntryPointNotFoundException e)
            {
                logger.LogWarning(e, "An expected entry point in CoreAudioToolbox.dll was not found.");
                return false;
            }

            logger.LogInformation("Using CoreAudio version {version}.",
                FileVersionInfo.GetVersionInfo(Path.Combine(libPath, "CoreAudioToolbox.dll")).ProductVersion);

#endif
            return true;
        }
#if WINDOWS

        static void AddUnmanagedLibraryPath(string libPath) =>
            (AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()) as ExtensionLoadContext)?
            .AddUnmanagedLibraryPath(libPath);
#endif
    }
}
