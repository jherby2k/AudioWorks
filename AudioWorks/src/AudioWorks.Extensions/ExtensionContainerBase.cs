using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull]
        protected static CompositionHost CompositionHost { get; } = new ContainerConfiguration()
            .WithAssemblies(new DirectoryInfo(Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
                    string.Empty,
                    "Extensions")).GetDirectories()
                .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
            .CreateContainer();
    }
}
