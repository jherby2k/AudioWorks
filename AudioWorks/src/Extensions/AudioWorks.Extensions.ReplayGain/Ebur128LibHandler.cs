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

namespace AudioWorks.Extensions.ReplayGain
{
    [PrerequisiteHandlerExport]
    public sealed class Ebur128LibHandler : IPrerequisiteHandler
    {
        const string _ebur128Lib = "ebur128";
        const string _linuxLibVersion = "1";

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac, we need to add the full file name, but on Windows it resolves the file extension properly
        static readonly string _ebur128LibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_ebur128Lib}.dylib"
                : _ebur128Lib);

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<Ebur128LibHandler>();

            try
            {
                logger.LogDebug("Attempting to load {_ebur128LibFullPath}", _ebur128LibFullPath);

                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                LibEbur128.GetVersion(out var major, out var minor, out var patch);

                logger.LogInformation("Using ebur128 version {major}.{minor}.{patch}.", major, minor, patch);
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The ebur128 library could not be found.");
                return false;
            }

            return true;
        }

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != _ebur128Lib) return IntPtr.Zero;

            // On Linux, use the system-provided library.
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? NativeLibrary.Load($"{_ebur128Lib}.so.{_linuxLibVersion}", assembly, searchPath)
                : NativeLibrary.Load(_ebur128LibFullPath);
        }
    }
}
