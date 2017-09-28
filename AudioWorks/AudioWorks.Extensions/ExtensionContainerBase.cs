using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        static readonly DirectoryInfo _extensionRoot = new DirectoryInfo(Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ?? string.Empty,
            "Extensions"));

        protected static CompositionHost CompositionHost { get; } = new ContainerConfiguration()
            .WithAssemblies(_extensionRoot
                .EnumerateFiles("AudioWorks.Extensions.*.dll", SearchOption.AllDirectories)
                .Select(f => f.FullName)
                .Select(Assembly.LoadFrom))
            .CreateContainer();

        static ExtensionContainerBase()
        {
            // Search all extension directories for unresolved references

            // .NET Core
            AssemblyLoadContext.Default.Resolving += (context, name) =>
                AssemblyLoadContext.Default.LoadFromAssemblyPath(_extensionRoot
                    .EnumerateFiles($"{name.Name}.dll", SearchOption.AllDirectories)
                    .FirstOrDefault()?
                    .FullName);

            // Full Framework
            AppDomain.CurrentDomain.AssemblyResolve += (context, name) =>
                Assembly.LoadFrom(_extensionRoot
                    .EnumerateFiles($"{name.Name}.dll", SearchOption.AllDirectories)
                    .FirstOrDefault()?
                    .FullName);
        }
    }
}
