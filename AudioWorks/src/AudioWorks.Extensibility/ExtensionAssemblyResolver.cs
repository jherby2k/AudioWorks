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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AudioWorks.Common;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensibility
{
    sealed class ExtensionAssemblyResolver
    {
        readonly ILogger _logger = LoggerManager.LoggerFactory.CreateLogger<ExtensionAssemblyResolver>();

        internal Assembly Assembly { get; }

        internal ExtensionAssemblyResolver(string path)
        {
            _logger.LogDebug("Loading extension '{path}'.", path);

            Assembly = LoadWithLoader(path);

            var assemblyFiles = Directory.GetFiles(Path.GetDirectoryName(path)!, "*.dll");
            ResolveWithLoader(assemblyFiles);
        }

        static Assembly LoadWithLoader(string path) =>
            new ExtensionLoadContext().LoadFromAssemblyPath(path);

        void ResolveWithLoader(IEnumerable<string> assemblyFiles) =>
            AssemblyLoadContext.Default.Resolving += (_, assemblyName) =>
            {
                if (assemblyName.Name == null) return null;

                _logger.LogTrace("Attempting to resolve a dependency on '{assemblyName}'.", assemblyName.Name);

                var matchingAssemblyName = assemblyFiles
                    .FirstOrDefault(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile), new(assemblyName.Name)));
                if (matchingAssemblyName == null)
                {
                    _logger.LogTrace("Did not locate dependency '{assemblyName}'.", assemblyName.FullName);
                    return null;
                }

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(matchingAssemblyName);
                _logger.LogTrace("Located dependency '{assembly}'.", assembly.FullName);
                return assembly;
            };
    }
}
