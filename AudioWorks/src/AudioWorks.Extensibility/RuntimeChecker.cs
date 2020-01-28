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

using Microsoft.Win32;
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
        public static string GetShortFolderName()
        {
#if NETSTANDARD2_0
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                return $"net{GetFrameworkVersion()}";

            // .NET Core 2.x didn't report the version correctly. Assume they are v2.0
            var version = Version.Parse(RuntimeInformation.FrameworkDescription.Substring(RuntimeInformation.FrameworkDescription.LastIndexOf(' ')));
            return version.Major == 4 ? "netcoreapp2.0" : $"netcoreapp{version.ToString(2)}";
#else
            var version = Version.Parse(RuntimeInformation.FrameworkDescription.Substring(RuntimeInformation.FrameworkDescription.LastIndexOf(' ')));
            return $"netcoreapp{version.ToString(2)}";
#endif
        }
#if NETSTANDARD2_0

        static string GetFrameworkVersion()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using (var ndpKey = baseKey.OpenSubKey(subkey))
            {
                var release = (int) ndpKey.GetValue("Release", 0);

                if (release >= 528040)
                    return "48";
                if (release >= 461808)
                    return "472";
                if (release >= 461308)
                    return "471";
                if (release >= 460798)
                    return "47";
                return "462";
            }
        }
#endif
    }
}