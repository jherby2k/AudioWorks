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
using System.Reflection;
using AudioWorks.Common;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensibility
{
    abstract class ExtensionContainerBase
    {
        protected static CompositionHost CompositionHost { get; }

        static ExtensionContainerBase()
        {
            var assemblies =
                new DirectoryInfo(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!)
                    .GetFiles("AudioWorks.Extensions.*.dll")
                    .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly)
                    .ToList();

            var logger = LoggerManager.LoggerFactory.CreateLogger<ExtensionContainerBase>();
            logger.LogDebug("Discovered {count} extension assemblies.", assemblies.Count);

            // Remove any extension assemblies that can't have prerequisites handled automatically
            using (var unvalidatedContainer = new ContainerConfiguration().WithAssemblies(assemblies).CreateContainer())
                foreach (var handler in unvalidatedContainer.GetExports<IPrerequisiteHandler>())
                    if (!handler.Handle())
                    {
                        var validatorAssembly = handler.GetType().Assembly;
                        logger.LogDebug("Extension assembly {assembly} failed prerequisite check. Removing.",
                            validatorAssembly.FullName);

                        assemblies.RemoveAll(assembly =>
                            assembly.Location.Equals(validatorAssembly.Location, StringComparison.OrdinalIgnoreCase));
                    }

            CompositionHost = new ContainerConfiguration()
                .WithAssemblies(assemblies).CreateContainer();
        }
    }
}
