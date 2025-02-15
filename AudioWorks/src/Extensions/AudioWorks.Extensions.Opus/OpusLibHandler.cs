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

namespace AudioWorks.Extensions.Opus
{
    [PrerequisiteHandlerExport]
    public sealed class OpusLibHandler : IPrerequisiteHandler
    {
        const string _oggLib = "ogg";
        const string _opusLib = "opus";
        const string _opusEncLib = "opusenc";
        const int _linuxOggLibVersion = 0;
        const int _linuxOpusLibVersion = 0;

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<OpusLibHandler>();

            try
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                logger.LogInformation("Using Opus version {version}.",
                    (Marshal.PtrToStringAnsi(LibOpus.GetVersion()) ?? "<unknown>")
                    .Replace("libopus ", string.Empty, StringComparison.Ordinal));
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The Opus library could not be found.");
                return false;
            }

            return true;
        }

        static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            libraryName switch
            {
                // On Linux, use the system-provided libogg and libopus
                _oggLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_oggLib}.so.{_linuxOggLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(GetLibFullPath(_oggLib)),
                _opusLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_opusLib}.so.{_linuxOpusLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(GetLibFullPath(_opusLib)),
                _opusEncLib => NativeLibrary.Load(GetLibFullPath(_opusEncLib)),
                _ => nint.Zero
            };

        static string GetLibFullPath(string libraryName) =>
            Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                "runtimes",
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                    ? "win-x86"
                    : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                        ? "linux-x64"
                        : RuntimeInformation.RuntimeIdentifier,
                "native",
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? $"lib{libraryName}.dylib"
                    : libraryName);
    }
}
