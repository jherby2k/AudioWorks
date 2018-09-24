/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
#if LINUX
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
#if WINDOWS
using System.Reflection;
#endif
using System.Runtime.InteropServices;
#if WINDOWS
using System.Text;
#endif
using AudioWorks.Common;
using AudioWorks.Extensibility;
#if LINUX
using JetBrains.Annotations;
#endif
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
            var logger = LoggingManager.LoggerFactory.CreateLogger<FlacLibHandler>();

#if WINDOWS
            var nativeLibraryPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86");

            var flacLibrary = Path.Combine(nativeLibraryPath, "libFLAC.dll");

            if (!File.Exists(flacLibrary))
            {
                logger.LogWarning("Missing libFLAC.dll.");
                return false;
            }

            // Prefix the PATH variable with the correct architecture-specific directory
            Environment.SetEnvironmentVariable("PATH", new StringBuilder(nativeLibraryPath)
                .Append(Path.PathSeparator).Append(Environment.GetEnvironmentVariable("PATH"))
                .ToString());

            var module = SafeNativeMethods.LoadLibrary("libFLAC.dll");
#elif LINUX
            if (!VerifyLibrary("libFLAC.so.8"))
            {
                logger.LogWarning(
                    GetDistribution().Equals("Ubuntu", StringComparison.OrdinalIgnoreCase)
                        ? "Missing libFLAC.so.8. Run 'sudo apt-get install -y libflac8 && sudo updatedb' then restart AudioWorks."
                        : "Missing libFLAC.so.8.");
                return false;
            }

            var module = SafeNativeMethods.DlOpen("libFLAC.so.8", 2);
#endif
            try
            {
                logger.LogInformation("Using libFLAC version {0}.",
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
#if WINDOWS
                SafeNativeMethods.FreeLibrary(module);
#else
                SafeNativeMethods.DlClose(module);
#endif
            }

            return true;
        }
#if LINUX

        [Pure]
        static bool VerifyLibrary([NotNull] string libraryName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("locate", $"-r {libraryName}$")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        [NotNull]
        public static string GetDistribution()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo("lsb_release", "-i -s")
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                var result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result.Trim();
            }
            catch (FileNotFoundException)
            {
                // If lsb_release isn't available, the distribution is unknown
                return string.Empty;
            }
        }
#endif
    }
}
