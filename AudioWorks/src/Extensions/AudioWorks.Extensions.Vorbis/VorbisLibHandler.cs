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

namespace AudioWorks.Extensions.Vorbis
{
    [PrerequisiteHandlerExport]
    public sealed class VorbisLibHandler : IPrerequisiteHandler
    {
        const string _oggLib = "ogg";
        const string _vorbisLib = "vorbis";
        const string _vorbisEncLib = "vorbisenc";
        const int _linuxOggLibVersion = 0;
        const int _linuxVorbisLibVersion = 0;
        const int _linuxVorbisEncLibVersion = 2;

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

        static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            libraryName switch
            {
                // On Linux, use the system-provided library
                _oggLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_oggLib}.so.{_linuxOggLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(GetLibFullPath(_oggLib)),
                _vorbisLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_vorbisLib}.so.{_linuxVorbisLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(GetLibFullPath(_vorbisLib)),
                _vorbisEncLib => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? NativeLibrary.Load($"{_vorbisEncLib}.so.{_linuxVorbisEncLibVersion}", assembly, searchPath)
                    : NativeLibrary.Load(GetLibFullPath(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? _vorbisLib : _vorbisEncLib)),
                _ => nint.Zero
            };

        static string GetLibFullPath(string libraryName) =>
            Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                "runtimes",
                RuntimeInformation.RuntimeIdentifier,
                "native",
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? $"lib{libraryName}.dylib"
                    : libraryName);
    }
}
