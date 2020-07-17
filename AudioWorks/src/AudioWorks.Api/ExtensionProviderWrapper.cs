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

using System.Collections.Generic;
using System.Composition;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Api
{
    static class ExtensionProviderWrapper
    {
        static ExtensionProviderWrapper()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger(typeof(ExtensionInstaller).FullName);

            if (!ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloads", true))
                logger.LogInformation("Automatic extension downloads are disabled.");
            else
            {
                logger.LogInformation("Beginning automatic extension updates.");

                ExtensionInstaller.InstallAsync().Wait();
            }

            ExtensionInstaller.SetLoadComplete();
        }

        internal static IEnumerable<ExportFactory<T, IDictionary<string, object>>> GetFactories<T>()
            where T : class =>
            ExtensionProvider.GetFactories<T>();

        internal static IEnumerable<ExportFactory<T>> GetFactories<T>(string key, string value)
            where T : class =>
            ExtensionProvider.GetFactories<T>(key, value);
    }
}