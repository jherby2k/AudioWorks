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
using System.Runtime.Loader;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Vorbis
{
    [PrerequisiteHandlerExport]
    public sealed class VorbisLibHandler : IPrerequisiteHandler
    {
        const string _oggLib = "ogg";
        const string _linuxOggLibVersion = "1";
        const string _vorbisLib = "vorbis";
        const string _vorbisEncLib = "vorbisenc";

        // Use the RID-specific directory, except on 32-bit Windows
        // On Mac, we need to add the full file name, but on Windows it resolves the file extension properly
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
        // On Mac, we need to add the full file name, but on Windows/Linux it resolves the file extension properly
        static readonly string _vorbisLibFullPath = Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "runtimes",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Environment.Is64BitProcess
                ? "win-x86"
                : RuntimeInformation.RuntimeIdentifier,
            "native",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? $"lib{_vorbisLib}.dylib"
                : _vorbisLib);

        // On Windows, these functions are actually in vorbis.dll
        // On Mac, we need to add the full file name, but on Linux it resolves the file extension properly
        static readonly string _vorbisEncLibFullPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? _vorbisLibFullPath
            : Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                "runtimes",
                RuntimeInformation.RuntimeIdentifier,
                "native",
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? $"lib{_vorbisEncLib}.dylib"
                    : _vorbisEncLib);

        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<VorbisLibHandler>();

            try
            {
                NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

                logger.LogInformation("Using Vorbis version {version}.",
                    Marshal.PtrToStringAnsi(LibVorbis.GetVersion()));
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The Vorbis library could not be found.");
                return false;
            }

            return true;
        }

        static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            libraryName switch
            {
                // On Linux, use the system-provided libogg
                _oggLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_oggLib}.so.{_linuxOggLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(_oggLibFullPath),
                _vorbisLib or _vorbisEncLib => NativeLibrary.Load(_vorbisEncLibFullPath),
                _ => IntPtr.Zero
            };
    }
}
