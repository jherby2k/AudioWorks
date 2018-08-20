using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull] static readonly Lazy<CompositionHost> _compositionHost = new Lazy<CompositionHost>(() =>
            new ContainerConfiguration()
                .WithAssemblies(
                    new DirectoryInfo(Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "AudioWorks",
                            "Extensions",
#if NETCOREAPP2_1
                            "netcoreapp2.1"))
#else
                        "netstandard2.0"))
#endif
                        .GetDirectories()
                        .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                        .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
                .CreateContainer());

        [NotNull]
        protected static CompositionHost CompositionHost => _compositionHost.Value;

        static ExtensionContainerBase()
        {
            ExtensionInstaller.Download();
        }
    }
}
