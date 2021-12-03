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

namespace AudioWorks.Extensions.Vorbis
{
    [PrerequisiteHandlerExport]
    public sealed class VorbisLibHandler : IPrerequisiteHandler
    {
        public bool Handle()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<VorbisLibHandler>();

#if WINDOWS
            var libPath = Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
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
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                osVersion.StartsWith("10.15", StringComparison.Ordinal)
                    ? "macos.10.15-x64" :
                osVersion.StartsWith("11", StringComparison.Ordinal) ? RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                    ? "macos.11-arm64"
                    : "macos.11-x64" :
                RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                    ? "macos.11-arm64"
                    : "macos.12-x64"));
#else // LINUX
            var release = GetRelease();
            if (release.StartsWith("Ubuntu", StringComparison.OrdinalIgnoreCase))
                AddUnmanagedLibraryPath(Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                    release.StartsWith("Ubuntu 18.04", StringComparison.OrdinalIgnoreCase) ? "ubuntu.18.04-x64" :
                    "ubuntu.20.04-x64"));
#endif

            try
            {
                foreach (var methodInfo in typeof(SafeNativeMethods).GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Static))
                    Marshal.Prelink(methodInfo);
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e.Message);
                return false;
            }
            catch (EntryPointNotFoundException e)
            {
                logger.LogWarning(e.Message);
                return false;
            }

            logger.LogInformation("Using Vorbis version {0}.",
                Marshal.PtrToStringAnsi(SafeNativeMethods.VorbisVersion()));

            return true;
        }

        static void AddUnmanagedLibraryPath(string libPath) =>
            (AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()) as ExtensionLoadContext)?
            .AddUnmanagedLibraryPath(libPath);
#if LINUX

        static string GetRelease()
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
                return result.Trim();
            }
        }
#endif
    }
}
