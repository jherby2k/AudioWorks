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
            var extensionDir = Path.GetDirectoryName(path);

            // Resolve dependencies from both the main and extension directories
            var assemblyFiles = Directory.GetFiles(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")
                .Concat(Directory.GetFiles(extensionDir, "*.dll"));

            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                ResolveFullFramework(assemblyFiles);
            else
                ResolveWithLoader(assemblyFiles);
        }

        static void ResolveFullFramework([NotNull, ItemNotNull] IEnumerable<string> assemblyFiles)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (context, args) => assemblyFiles
                .Where(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                    AssemblyName.GetAssemblyName(assemblyFile),
                    new AssemblyName(args.Name)))
                .Select(Assembly.LoadFrom).FirstOrDefault();
        }

        static void ResolveWithLoader([NotNull, ItemNotNull] IEnumerable<string> assemblyFiles)
        {
            AssemblyLoadContext.Default.Resolving += (context, name) => assemblyFiles
                .Where(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                    AssemblyName.GetAssemblyName(assemblyFile),
                    new AssemblyName(name.Name)))
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).FirstOrDefault();
        }
    }
}
