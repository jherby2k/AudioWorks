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
        const int _linuxLibMaxVersion = 14;
        const int _linuxLibMinVersion = 8;

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

                var module = nint.Zero;

                try
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        module = Kernel32.LoadLibrary(_flacLibFullPath);
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        module = LibDl.DlOpen(_flacLibFullPath, 1);
                    else // On Linux, use whichever system-provided library is available
                        module = LibDl.DlOpen($"lib{_flacLib}.so.14", 2);

                    logger.LogInformation("Using FLAC version {version}.",
                        Marshal.PtrToStringAnsi(Marshal.PtrToStructure<nint>(
                            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                            ? Kernel32.GetProcAddress(module, "FLAC__VENDOR_STRING")
                            : LibDl.DlSym(module, "FLAC__VENDOR_STRING"))));
                }
                finally
                {
                    if (module != nint.Zero)
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            Kernel32.FreeLibrary(module);
                        else
                            _ = LibDl.DlClose(module);
                }
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The FLAC library could not be found.");
                return false;
            }

            return true;
        }

        static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != _flacLib) return nint.Zero;

            // On Linux, use whichever system-provided library is available
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                for (var version = _linuxLibMaxVersion; version >= _linuxLibMinVersion; version--)
                    if (NativeLibrary.TryLoad($"{_flacLib}.so.{version}", assembly, searchPath, out var result))
                        return result;

            return NativeLibrary.Load(_flacLibFullPath);
        }
    }
}
