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

namespace AudioWorks.Extensions.Lame
{
    [PrerequisiteHandlerExport]
    public sealed class LameLibHandler : IPrerequisiteHandler
    {
        const string lameLib = "libmp3lame";

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<LameLibHandler>();

            try
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                logger.LogInformation("Using LAME version {version}.",
                    Marshal.PtrToStringAnsi(LibMp3Lame.GetVersion()));
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The LAME library could not be found.");
                return false;
            }

            return true;
        }

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            // Load from the RID-specific folder unless this is 32-bit Windows
            // On Linux, use the system-provided library
            if (libraryName == lameLib && !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return NativeLibrary.Load(Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                        ? "win-x86"
                        : RuntimeInformation.RuntimeIdentifier,
                    lameLib));

            return IntPtr.Zero;
        }
    }
}
