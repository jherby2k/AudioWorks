using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull]
        protected static CompositionHost CompositionHost { get; } = new ContainerConfiguration()
            .WithAssemblies(
                new DirectoryInfo(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AudioWorks",
                        "Extensions"))
                    .GetDirectories()
                    .SelectMany(extensionDir => extensionDir
#if NETCOREAPP2_1
                        .GetDirectories("netcoreapp2.1")
#else
                        .GetDirectories("netstandard2.0")
#endif
                        .First()
                        .GetFiles("AudioWorks.Extensions.*.dll"))
                    .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName)
                        .Assembly))
            .CreateContainer();
    }
}
