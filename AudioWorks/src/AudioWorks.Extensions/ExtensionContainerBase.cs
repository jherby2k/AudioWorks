using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull] static readonly string _projectRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AudioWorks",
            "Extensions");

        [NotNull]
        protected static CompositionHost CompositionHost { get; } = new ContainerConfiguration()
            .WithAssemblies(
                new DirectoryInfo(_projectRoot).GetDirectories()
                    .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                    .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
            .CreateContainer();
    }
}
