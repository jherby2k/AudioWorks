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
#if NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif
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
            _logger.LogDebug("Loading extension '{0}'.", path);

#if NETSTANDARD2_0
            Assembly = RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal)
                ? Assembly.LoadFrom(path)
                : LoadWithLoader(path);
#else
            Assembly = LoadWithLoader(path);
#endif

            var extensionDir = Path.GetDirectoryName(path)!;

            // Resolve dependencies from both the main and extension directories
            var assemblyFiles = Directory.GetFiles(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!, "*.dll")
                .Concat(Directory.GetFiles(extensionDir, "*.dll"));

#if NETSTANDARD2_0
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                ResolveFullFramework(assemblyFiles);
            else
                ResolveWithLoader(assemblyFiles);
        }

        void ResolveFullFramework(IEnumerable<string> assemblyFiles) =>
            AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
            {
                _logger.LogTrace("Attempting to resolve a dependency on '{1}'.", args.Name);

                var matchingAssemblyName = assemblyFiles
                    .FirstOrDefault(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile), new(args.Name)));
                if (matchingAssemblyName == null)
                {
                    _logger.LogTrace("Did not locate dependency '{0}'.", args.Name);
                    return null;
                }

                var assembly = Assembly.LoadFrom(matchingAssemblyName);
                _logger.LogTrace("Located dependency '{0}'.", assembly.FullName);
                return assembly;
            };
#else
            ResolveWithLoader(assemblyFiles);
        }
#endif

        static Assembly LoadWithLoader(string path) =>
            new ExtensionLoadContext().LoadFromAssemblyPath(path);

        void ResolveWithLoader(IEnumerable<string> assemblyFiles) =>
            AssemblyLoadContext.Default.Resolving += (_, assemblyName) =>
            {
                if (assemblyName.Name == null) return null;

                _logger.LogTrace("Attempting to resolve a dependency on '{1}'.", assemblyName.Name);

                var matchingAssemblyName = assemblyFiles
                    .FirstOrDefault(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile), new(assemblyName.Name)));
                if (matchingAssemblyName == null)
                {
                    _logger.LogTrace("Did not locate dependency '{0}'.", assemblyName.FullName);
                    return null;
                }

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(matchingAssemblyName);
                _logger.LogTrace("Located dependency '{0}'.", assembly.FullName);
                return assembly;
            };
    }
}
