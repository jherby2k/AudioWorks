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

            AddUnmanagedLibraryPath(libPath);
#elif OSX
            AddUnmanagedLibraryPath(Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                $"macos.{GetOSVersion()}-{GetArch()}"));
#else // LINUX
            var release = GetRelease();
            if (release.StartsWith("Ubuntu", StringComparison.OrdinalIgnoreCase))
                AddUnmanagedLibraryPath(Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                    release.StartsWith("Ubuntu 20.04", StringComparison.OrdinalIgnoreCase) ? "ubuntu.20.04-x64" :
                    "ubuntu.22.04-x64"));
#endif

            try
            {
                foreach (var methodInfo in typeof(SafeNativeMethods).GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Static))
                    Marshal.Prelink(methodInfo);
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The Vorbis library could not be found.");
                return false;
            }
            catch (EntryPointNotFoundException e)
            {
                logger.LogWarning(e, "An expected entry point in the Vorbis library was not found.");
                return false;
            }

            logger.LogInformation("Using Vorbis version {version}.",
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
                return result.Trim()[..2];
            }
        }

        static string GetArch() => RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "x64";
#endif
    }
}
