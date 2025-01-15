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
#if OSX
using System.Diagnostics;
#endif
#if !WINDOWS
using System.Diagnostics.CodeAnalysis;
#endif
#if !LINUX
using System.IO;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
#if !LINUX
using System.Runtime.Loader;
#endif
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Flac
{
    [PrerequisiteHandlerExport]
    public sealed class FlacLibHandler : IPrerequisiteHandler
    {
#if !WINDOWS
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
#endif
        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<FlacLibHandler>();
#if WINDOWS

            var libPath = Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                Environment.Is64BitProcess ? "win-x64" : "win-x86");

            AddUnmanagedLibraryPath(libPath);
#elif OSX

            var libPath = Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                $"macos.{GetOSVersion()}-{GetArch()}");

            AddUnmanagedLibraryPath(libPath);
#endif

            try
            {
                foreach (var methodInfo in typeof(SafeNativeMethods).GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Static))
                    Marshal.Prelink(methodInfo);
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The FLAC library could not be found.");
                return false;
            }
            catch (EntryPointNotFoundException e)
            {
                logger.LogWarning(e, "An expected entry point in the FLAC library was not found.");
                return false;
            }

            var module = IntPtr.Zero;
            try
            {
#if WINDOWS
                module = SafeNativeMethods.LoadLibrary(Path.Combine(libPath, "libFLAC.dll"));
#elif OSX
                module = SafeNativeMethods.DlOpen(Path.Combine(libPath, "libFLAC.dylib"), 2);
#else // LINUX
                module = SafeNativeMethods.DlOpen("libFLAC.so.8", 2);
#endif
                logger.LogInformation("Using FLAC version {version}.",
                    Marshal.PtrToStringAnsi(
                        Marshal.PtrToStructure<IntPtr>(
#if WINDOWS
                            SafeNativeMethods.GetProcAddress(module, "FLAC__VENDOR_STRING"))));
#else
                            SafeNativeMethods.DlSym(module, "FLAC__VENDOR_STRING"))));
#endif
            }
            finally
            {
                if (module != IntPtr.Zero)
#if WINDOWS
                    SafeNativeMethods.FreeLibrary(module);
#else
                    SafeNativeMethods.DlClose(module);
#endif
            }

            return true;
        }

#if !LINUX
        static void AddUnmanagedLibraryPath(string libPath) =>
            (AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()) as ExtensionLoadContext)?
            .AddUnmanagedLibraryPath(libPath);
#endif
#if OSX

        static string GetOSVersion()
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo("sw_vers", "-productVersion")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.Start();
                var result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result.Trim()[..2];
            }
        }

        static string GetArch() => RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "x64";
#endif
    }
}
