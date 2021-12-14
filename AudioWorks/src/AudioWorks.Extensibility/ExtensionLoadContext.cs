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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using AudioWorks.Common;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An <see cref="AssemblyLoadContext"/> that can load unmanaged libraries from a list of paths added at runtime.
    /// Each AudioWorks extension is loaded into its own <see cref="ExtensionLoadContext"/> instance.
    /// </summary>
    /// <seealso cref="AssemblyLoadContext"/>
    public sealed class ExtensionLoadContext : AssemblyLoadContext
    {
        readonly List<string> _unmanagedLibraryPaths = new();

        /// <summary>
        /// Adds a path that contains unmanaged libraries. When an unmanaged method is called via
        /// <see cref="DllImportAttribute"/>, these paths are searched first before falling back on default resolution
        /// behavior.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null.</exception>
        public void AddUnmanagedLibraryPath(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            _unmanagedLibraryPaths.Add(path);
        }

        /// <inheritdoc/>
        protected override Assembly? Load(AssemblyName? assemblyName) => null;

        /// <inheritdoc/>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var fullPath = _unmanagedLibraryPaths
                .SelectMany(path =>
                {
                    // Look for a matching assembly with or without an extension
                    var directoryInfo = new DirectoryInfo(path);
                    return directoryInfo.GetFiles(unmanagedDllName)
                        .Concat(directoryInfo.GetFiles($"{unmanagedDllName}.*"));
                }).FirstOrDefault()?
                .FullName;
            if (fullPath == null) return base.LoadUnmanagedDll(unmanagedDllName);

            LoggerManager.LoggerFactory.CreateLogger<ExtensionLoadContext>()
                .LogDebug("Loading unmanaged assembly '{assembly}'.", fullPath);
            return LoadUnmanagedDllFromPath(fullPath);
        }
    }
}
