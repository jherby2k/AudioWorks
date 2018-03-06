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
        [NotNull] readonly ICompilationAssemblyResolver _assemblyResolver;
        [NotNull] readonly DependencyContext _dependencyContext;
        [NotNull] readonly AssemblyLoadContext _loadContext;

        [NotNull]
        internal Assembly Assembly { get; }

        internal ExtensionAssemblyResolver([NotNull] string path)
        {
            Assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            _dependencyContext = DependencyContext.Load(Assembly);

            _assemblyResolver = new CompositeCompilationAssemblyResolver
            (new ICompilationAssemblyResolver[]
            {
                new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(path)),
                new ReferenceAssemblyPathResolver(),
                new PackageCompilationAssemblyResolver()
            });

            _loadContext = AssemblyLoadContext.GetLoadContext(Assembly);
            _loadContext.Resolving += OnResolving;
        }

        [CanBeNull]
        Assembly OnResolving([NotNull] AssemblyLoadContext context, [NotNull] AssemblyName name)
        {
            var runtimeLibrary = _dependencyContext.RuntimeLibraries.FirstOrDefault(library =>
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
                _assemblyResolver.TryResolveAssemblyPaths(compiliationLibrary, assemblies);
                if (assemblies.Count > 0)
                    return _loadContext.LoadFromAssemblyPath(assemblies[0]);
            }

            return null;
        }
    }
}
