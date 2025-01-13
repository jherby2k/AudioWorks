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

#if NETSTANDARD2_0
using System;
#endif
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
#if !NETSTANDARD2_0
using System.Runtime.Loader;
#endif
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

#if NETSTANDARD2_0
            Assembly = Assembly.LoadFrom(path);
#else
            Assembly = LoadWithLoader(path);
#endif

            var assemblyFiles = Directory.GetFiles(Path.GetDirectoryName(path)!, "*.dll");

#if NETSTANDARD2_0
            ResolveFullFramework(assemblyFiles);
        }

        void ResolveFullFramework(IEnumerable<string> assemblyFiles) =>
            AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
            {
                _logger.LogTrace("Attempting to resolve a dependency on '{name}'.", args.Name);

                var matchingAssemblyName = assemblyFiles
                    .FirstOrDefault(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile), new(args.Name)));
                if (matchingAssemblyName == null)
                {
                    _logger.LogTrace("Did not locate dependency '{name}'.", args.Name);
                    return null;
                }

                var assembly = Assembly.LoadFrom(matchingAssemblyName);
                _logger.LogTrace("Located dependency '{assembly}'.", assembly.FullName);
                return assembly;
            };
#else
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
#endif
    }
}
