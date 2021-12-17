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
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages the retrieval of configuration settings from disk.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        [CLSCompliant(false)]
        public static IConfigurationRoot Configuration { get; }

        static ConfigurationManager()
        {
            var settingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "AudioWorks");
            var settingsFile = Path.Combine(settingsPath, "settings.json");

            // Settings read from disk override the defaults
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(LibraryConfiguration.GetDefaults().AsDictionary())
                .AddJsonFile(settingsFile, true)
                .Build();

            // Write the configuration out to disk
            var updatedConfig = new LibraryConfiguration();
            Configuration.Bind(updatedConfig);
            Directory.CreateDirectory(settingsPath);
            File.WriteAllText(settingsFile,
                JsonConvert.SerializeObject(updatedConfig,
                    new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
