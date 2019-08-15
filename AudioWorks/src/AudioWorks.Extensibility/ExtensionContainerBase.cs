/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

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

            var logger = LoggerManager.LoggerFactory.CreateLogger<ExtensionContainerBase>();
            logger.LogDebug("Discovered {0} extension assemblies.", assemblies.Count);

            // Remove any extension assemblies that can't have prerequisites handled automatically
            using (var unvalidatedContainer = new ContainerConfiguration().WithAssemblies(assemblies).CreateContainer())
                foreach (var handler in unvalidatedContainer.GetExports<IPrerequisiteHandler>())
                    if (!TryHandle(handler, logger))
                    {
                        var validatorAssembly = handler.GetType().Assembly;
                        logger.LogDebug("Extension assembly {0} failed prerequisite check. Removing.",
                            validatorAssembly.FullName);

                        assemblies.RemoveAll(assembly =>
                            assembly.CodeBase.Equals(validatorAssembly.CodeBase, StringComparison.OrdinalIgnoreCase));
                    }

            CompositionHost = new ContainerConfiguration()
                .WithAssemblies(assemblies).CreateContainer();
        }

        static bool TryHandle([NotNull] IPrerequisiteHandler handler, [NotNull] ILogger logger)
        {
            try
            {
                return handler.Handle();
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "The Prerequisite handler threw an unexpected exception.");
                return false;
            }
        }
    }
}
