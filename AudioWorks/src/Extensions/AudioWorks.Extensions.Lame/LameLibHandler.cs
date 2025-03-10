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
        const string _lameLib = "mp3lame";
        const string _linuxLibVersion = "0";

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac we need to add the full file name, but on Windows it resolves the file properly
        static readonly string _lameLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? $"lib{_lameLib}.dylib"
            : _lameLib);

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<LameLibHandler>();

            try
            {
                logger.LogDebug("Attempting to load {_lameLibFullPath}", _lameLibFullPath);

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

        static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != _lameLib) return nint.Zero;

            // On Linux, use the system-provided library.
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? NativeLibrary.Load($"{_lameLib}.so.{_linuxLibVersion}", assembly, searchPath)
                : NativeLibrary.Load(_lameLibFullPath);
        }
    }
}
