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
#if !WINDOWS
using System.Diagnostics;
#endif
using System.IO;
#if !LINUX
using System.Reflection;
#endif
#if WINDOWS && NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif
#if !LINUX
using System.Runtime.Loader;
#endif
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.ReplayGain
{
    [PrerequisiteHandlerExport]
    public sealed class Ebur128LibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<Ebur128LibHandler>();

#if WINDOWS
            var libPath = Path.Combine(
                // ReSharper disable once AssignNullToNotNullAttribute
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
                // ReSharper disable once AssignNullToNotNullAttribute
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                osVersion.StartsWith("10.12", StringComparison.Ordinal) ? "osx.10.12-x64" :
                osVersion.StartsWith("10.13", StringComparison.Ordinal) ? "osx.10.13-x64" :
                "osx.10.14-x64"));
#else // LINUX
            if (!VerifyLibrary("libebur128.so.1"))
            {
                logger.LogWarning(
                    GetDistribution().Equals("Ubuntu", StringComparison.OrdinalIgnoreCase)
                        ? "Missing libebur128.so.1. Run 'sudo apt-get install -y libebur128-1 && sudo updatedb' then restart AudioWorks."
                        : "Missing libebur128.so.1.");
                return false;
            }
#endif

            SafeNativeMethods.GetVersion(out var major, out var minor, out var patch);
            // ReSharper disable once StringLiteralTypo
            logger.LogInformation("Using libebur128 version {0}.{1}.{2}.", major, minor, patch);

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
#else
        static void AddUnmanagedLibraryPath([NotNull] string libPath)
        {
            ((ExtensionLoadContext) AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()))
                .AddUnmanagedLibraryPath(libPath);
        }
#endif
#if OSX

        [NotNull]
        public static string GetOSVersion()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("sw_vers", "-productVersion")
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
#endif
    }
}
