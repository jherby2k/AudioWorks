using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                ResolveFullFramework();
            else
                ResolveWithLoader();
        }

        void ResolveFullFramework()
        {
            // .NET Framework should look for dependencies in the extensions's directory
            var extensionDir = Path.GetDirectoryName(Assembly.Location);
            AppDomain.CurrentDomain.AssemblyResolve += (context, name) =>
                // ReSharper disable once AssignNullToNotNullAttribute
                Assembly.LoadFrom(Path.Combine(extensionDir, $"{name.Name}.dll"));
        }

        void ResolveWithLoader()
        {
            // .NET Core can additionally resolve dependencies from each extension's deps.json file
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
                    var assemblies = new List<string>(1);
                    assemblyResolver.TryResolveAssemblyPaths(new CompilationLibrary(
                        runtimeLibrary.Type,
                        runtimeLibrary.Name,
                        runtimeLibrary.Version,
                        runtimeLibrary.Hash,
                        runtimeLibrary.RuntimeAssemblyGroups.SelectMany(group => group.AssetPaths),
                        runtimeLibrary.Dependencies,
                        runtimeLibrary.Serviceable), assemblies);
                    if (assemblies.Count > 0)
                        return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblies[0]);
                }

                return null;
            };
        }
    }
}
