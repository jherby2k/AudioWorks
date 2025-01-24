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
using AudioWorks.Extensions.Opus;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Vorbis
{
    [PrerequisiteHandlerExport]
    public sealed class OpusLibHandler : IPrerequisiteHandler
    {
        const string _oggLib = "ogg";
        const string _linuxOggLibVersion = "0";
        const string _opusLib = "opus";
        const string _linuxOpusLibVersion = "0";
        const string _opusEncLib = "opusenc";

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac we need to add the full file name, but on Windows it resolves the file properly
        static readonly string _oggLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_oggLib}.dylib"
                : _oggLib);

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac we need to add the full file name, but on Windows it resolves the file properly
        static readonly string _opusLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_opusLib}.dylib"
                : _opusLib);

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac and Linux we need to add the full file name, but on Windows it resolves the file properly
        static readonly string _opusEncLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_opusEncLib}.dylib"
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? $"lib{_opusEncLib}.so"
                    : _opusEncLib);

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

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            libraryName switch
            {
                // On Linux, use the system-provided libogg and libopus
                _oggLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_oggLib}.so.{_linuxOggLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(_oggLibFullPath),
                _opusLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_opusLib}.so.{_linuxOpusLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(_opusLibFullPath),
                _opusEncLib => NativeLibrary.Load(_opusEncLibFullPath),
                _ => IntPtr.Zero
            };
    }
}
