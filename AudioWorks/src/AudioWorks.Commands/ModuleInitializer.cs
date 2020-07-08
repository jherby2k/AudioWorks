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

#if NET462
using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AudioWorks.Commands
{
    public class ModuleInitializer : IModuleAssemblyInitializer
    {
        public void OnImport()
        {
            // Workaround for binding issue under Windows PowerShell
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
        }

        static Assembly? AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Load the available version of Newtonsoft.Json, regardless of the requested version
            if (!new AssemblyName(args.Name).Name.Equals("Newtonsoft.Json", StringComparison.Ordinal)) return null;

            var jsonAssembly = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Newtonsoft.Json.dll");

            return !File.Exists(jsonAssembly) ? null : Assembly.LoadFrom(jsonAssembly);
        }
    }
}

#endif