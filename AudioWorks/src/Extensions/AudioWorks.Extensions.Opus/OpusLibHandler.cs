/* Copyright © 2019 Jeremy Herbison

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
#if !WINDOWS
using System.Diagnostics;
#endif
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Opus
{
    [PrerequisiteHandlerExport]
    public sealed class OpusLibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<OpusLibHandler>();

#if WINDOWS
            var libPath = Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                Environment.Is64BitProcess ? "win-x64" : "win-x86");

#if NETSTANDARD2_0
            // On Full Framework, AssemblyLoadContext isn't available, so we add the directory to PATH
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.Ordinal))
                Environment.SetEnvironmentVariable("PATH",
                    $"{libPath}{Path.PathSeparator}{Environment.GetEnvironmentVariable("PATH")}");
            else
                AddUnmanagedLibraryPath(libPath);
#else
            AddUnmanagedLibraryPath(libPath);
#endif
#elif OSX
            var osVersion = GetOSVersion();

            AddUnmanagedLibraryPath(Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                osVersion switch
                {
                    "10.13" => "osx.10.13-x64",
                    "10.14" => "osx.10.14-x64",
                    _ => "osx.10.15-x64",
                }));
#else // LINUX
            var release = GetRelease();

            if (release.StartsWith("Ubuntu", StringComparison.OrdinalIgnoreCase))
            {
                var version = release.Replace("Ubuntu ", string.Empty, StringComparison.OrdinalIgnoreCase);
                AddUnmanagedLibraryPath(Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                    version switch
                    {
                        "16.04" => "ubuntu.16.04-x64",
                        "18.04" => "ubuntu.18.04-x64",
                        _ => "ubuntu.20.04-x64"
                    }));
            }

            if (!VerifyLibrary("libopus.so.0"))
            {
                logger.LogWarning(
                    release.StartsWith("Ubuntu", StringComparison.OrdinalIgnoreCase)
                        ? "Missing libopus.so.0. Run 'sudo apt-get install -y libopus0 && sudo updatedb' then restart AudioWorks."
                        : "Missing libopus.so.0.");
                return false;
            }

            if (!VerifyLibrary("libogg.so.0"))
            {
                logger.LogWarning(
                    release.StartsWith("Ubuntu", StringComparison.OrdinalIgnoreCase)
                        ? "Missing libogg.so.0. Run 'sudo apt-get install -y libogg0 && sudo updatedb' then restart AudioWorks."
                        : "Missing libogg.so.0.");
                return false;
            }
#endif

            logger.LogInformation("Using libopusenc version {0}.",
                // ReSharper disable once PossibleNullReferenceException
                Marshal.PtrToStringAnsi(SafeNativeMethods.OpusEncoderGetVersion())
#if NETSTANDARD2_0
                    .Replace("libopusenc ", string.Empty));
#else
                    .Replace("libopusenc ", string.Empty, StringComparison.Ordinal));
#endif

            logger.LogInformation("Using libopus version {0}.",
                // ReSharper disable once PossibleNullReferenceException
                Marshal.PtrToStringAnsi(SafeNativeMethods.OpusGetVersion())
#if NETSTANDARD2_0
                    .Replace("libopus ", string.Empty));
#else
                    .Replace("libopus ", string.Empty, StringComparison.Ordinal));
#endif

            return true;
        }

        static void AddUnmanagedLibraryPath(string libPath) =>
            ((ExtensionLoadContext) AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()))
            .AddUnmanagedLibraryPath(libPath);
#if LINUX

        static bool VerifyLibrary(string libraryName)
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo("locate", $"-r {libraryName}$")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
        }

        public static string GetRelease()
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo("lsb_release", "-d -s")
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    process.Start();
                    var result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    return result.Trim();
                }
            }
            catch (FileNotFoundException)
            {
                // If lsb_release isn't available, the distribution is unknown
                return string.Empty;
            }
        }
#elif OSX

        public static string GetOSVersion()
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
                return result.Trim();
            }
        }
#endif
    }
}
