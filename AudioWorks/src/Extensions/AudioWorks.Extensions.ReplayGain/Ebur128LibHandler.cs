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
#if OSX
using System.Diagnostics;
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
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                Environment.Is64BitProcess ? "win-x64" : "win-x86");

            AddUnmanagedLibraryPath(libPath);
#elif OSX
            AddUnmanagedLibraryPath(Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                $"macos.{GetOSVersion()}-{GetArch()}"));
#endif

            try
            {
                foreach (var methodInfo in typeof(LibEbur128).GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Static))
                    Marshal.Prelink(methodInfo);
            }
            catch (DllNotFoundException e)
            {
                logger.LogWarning(e, "The ebur128 library could not be found.");
                return false;
            }
            catch (EntryPointNotFoundException e)
            {
                logger.LogWarning(e, "An expected entry point in the ebur128 library was not found.");
                return false;
            }

            LibEbur128.GetVersion(out var major, out var minor, out var patch);
            // ReSharper disable once StringLiteralTypo
            logger.LogInformation("Using libebur128 version {major}.{minor}.{patch}.", major, minor, patch);

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
