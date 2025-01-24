/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
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
            var logger = LoggerManager.LoggerFactory.CreateLogger<CoreAudioHandler>();

            // Core Audio is not available on Linux
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return false;

            try
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                //TODO log the loaded CoreAudio version
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The Apple Core Audio library could not be found.");
                return false;
            }

            return true;
        }

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != "CoreAudioToolbox") return IntPtr.Zero;

            // On Mac, it's a built-in framework
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return NativeLibrary.Load("/System/Library/Frameworks/AudioToolbox.framework/AudioToolbox");

            // On Windows, it may be installed with "Apple Application Support"
            var coreAudioPath = Path.Combine(
                Environment.GetEnvironmentVariable("CommonProgramFiles") ?? string.Empty,
                "Apple",
                "Apple Application Support",
                "CoreAudioToolbox.dll");

            // It could also be found inside the iTunes directory
            if (!File.Exists(coreAudioPath))
                coreAudioPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles") ?? string.Empty, "iTunes");

            return File.Exists(coreAudioPath)
                ? NativeLibrary.Load(coreAudioPath)
                : IntPtr.Zero;
        }
    }
}
