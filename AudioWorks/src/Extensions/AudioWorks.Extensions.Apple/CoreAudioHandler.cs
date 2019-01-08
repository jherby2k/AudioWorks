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
#if NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif
using System.Runtime.Loader;
using AudioWorks.Common;
#endif
using AudioWorks.Extensibility;
#if WINDOWS
using JetBrains.Annotations;
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

            var libPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Environment.GetEnvironmentVariable("CommonProgramFiles"),
                "Apple",
                "Apple Application Support");

#if NETSTANDARD2_0
            // On Full Framework, AssemblyLoadContext isn't available, so we add the directory to PATH
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.Ordinal))
                Environment.SetEnvironmentVariable("PATH",
                    $"{libPath}{Path.PathSeparator}{Environment.GetEnvironmentVariable("PATH")}");
            else
                AddUnmanagedLibraryPath(libPath);
#else
            AddUnmanagedLibraryPath(libPath);
#endif

            var coreAudioLibrary = Path.Combine(libPath, "CoreAudioToolbox.dll");

            if (!File.Exists(coreAudioLibrary))
            {
                logger.LogWarning("Missing CoreAudioToolbox.dll. Install Apple Application Support (part of iTunes).");
                return false;
            }

            logger.LogInformation("Using CoreAudio version {0}.",
                FileVersionInfo.GetVersionInfo(coreAudioLibrary).ProductVersion);
#endif

            return true;
        }
#if WINDOWS

        static void AddUnmanagedLibraryPath([NotNull] string libPath)
        {
            ((ExtensionLoadContext) AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()))
                .AddUnmanagedLibraryPath(libPath);
        }
#endif
    }
}
