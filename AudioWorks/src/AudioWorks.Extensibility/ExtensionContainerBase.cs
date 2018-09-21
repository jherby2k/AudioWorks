using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensibility
{
    abstract class ExtensionContainerBase
    {
        [NotNull]
        protected static CompositionHost CompositionHost { get; }

        static ExtensionContainerBase()
        {
            var assemblies = new DirectoryInfo(Path.Combine(
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
                .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly)
                .ToList();

            var logger = LoggingManager.LoggerFactory.CreateLogger<ExtensionContainerBase>();
            logger.LogDebug("Discovered {0} extension assemblies.", assemblies.Count);

            using (var unvalidatedContainer = new ContainerConfiguration().WithAssemblies(assemblies).CreateContainer())
            {
                // Remove any extension assemblies that can't have prerequisites handled automatically
                foreach (var handler in unvalidatedContainer.GetExports<IPrerequisiteHandler>())
                    if (!handler.Handle())
                    {
                        var validatorAssembly = handler.GetType().Assembly;
                        logger.LogDebug("Extension assembly {0} failed prerequisite check. Removing.",
                            validatorAssembly.FullName);

                        assemblies.RemoveAll(assembly =>
                            assembly.CodeBase.Equals(validatorAssembly.CodeBase, StringComparison.OrdinalIgnoreCase));
                    }
            }

            CompositionHost = new ContainerConfiguration()
                .WithAssemblies(assemblies).CreateContainer();
        }
    }
}
