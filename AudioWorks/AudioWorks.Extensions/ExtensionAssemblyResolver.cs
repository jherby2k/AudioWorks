using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    sealed class ExtensionAssemblyResolver
    {
        [NotNull]
        internal Assembly Assembly { get; }

        internal ExtensionAssemblyResolver([NotNull] string path)
        {
            Assembly = Assembly.LoadFrom(path);
            var dependencyContext = DependencyContext.Load(Assembly);
            var assemblyResolver = new PackageCompilationAssemblyResolver();

            AssemblyLoadContext.Default.Resolving += (context, name) =>
            {
                var runtimeLibrary = dependencyContext.RuntimeLibraries.FirstOrDefault(library =>
                    library.RuntimeAssemblyGroups.SelectMany(group => group.AssetPaths)
                        .Select(Path.GetFileNameWithoutExtension)
                        .Contains(name.Name, StringComparer.OrdinalIgnoreCase));

                if (runtimeLibrary != null)
                {
                    var compiliationLibrary = new CompilationLibrary(
                        runtimeLibrary.Type,
                        runtimeLibrary.Name,
                        runtimeLibrary.Version,
                        runtimeLibrary.Hash,
                        runtimeLibrary.RuntimeAssemblyGroups.SelectMany(group => group.AssetPaths),
                        runtimeLibrary.Dependencies,
                        runtimeLibrary.Serviceable);

                    var assemblies = new List<string>(1);
                    assemblyResolver.TryResolveAssemblyPaths(compiliationLibrary, assemblies);
                    if (assemblies.Count > 0)
                        return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblies[0]);
                }

                return null;
            };
        }
    }
}
