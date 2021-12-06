/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if NETSTANDARD2_0
using Microsoft.Win32;
#endif
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// A utility class for dealing with NuGet dependencies at runtime.
    /// </summary>
    public static class RuntimeChecker
    {
        /// <summary>
        /// Returns the NuGet framework short folder name for the current runtime.
        /// </summary>
        /// <returns>The NuGet short folder name.</returns>
#if NETSTANDARD2_0
        public static string GetShortFolderName() =>
            RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.Ordinal)
                ? $"net{GetFrameworkVersion()}"
                : $"netcoreapp{Version.Parse(RuntimeInformation.FrameworkDescription.Substring(RuntimeInformation.FrameworkDescription.LastIndexOf(' '))).ToString(2)}";

        static string GetFrameworkVersion()
        {
            using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            using (var ndpKey = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
                return (int?) ndpKey?.GetValue("Release", 0) switch
                {
                    >= 528040 => "48",
                    >= 461808 => "472",
                    >= 461308 => "471",
                    >= 460798 => "47",
                    _ => "462"
                };
        }
#else
        public static string GetShortFolderName()
        {
            var version = Version.Parse(
                RuntimeInformation.FrameworkDescription[RuntimeInformation.FrameworkDescription.LastIndexOf(' ')..]
                    .Split('-')[0]);

            return version.Major >= 5 ? $"net{version.ToString(2)}" : $"netcoreapp{version.ToString(2)}";
        }
#endif
    }
}