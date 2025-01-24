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

namespace AudioWorks.Extensions.Flac
{
    [PrerequisiteHandlerExport]
    public sealed class FlacLibHandler : IPrerequisiteHandler
    {
        const string _flacLib = "FLAC";
        const string _linuxLibVersion = "8";

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac we need to add the full file name, but on Windows it resolves the file properly
        static readonly string _flacLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_flacLib}.dylib"
                : _flacLib);

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<FlacLibHandler>();

            try
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                //TODO log the loaded version
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The FLAC library could not be found.");
                return false;
            }

            return true;
        }

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != _flacLib) return IntPtr.Zero;

            // On Linux, use the system-provided library.
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? NativeLibrary.Load($"{_flacLib}.so.{_linuxLibVersion}", assembly, searchPath)
                : NativeLibrary.Load(_flacLibFullPath);
        }
    }
}
