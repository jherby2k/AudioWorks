/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

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
        [NotNull]
        [CLSCompliant(false)]
        public static IConfigurationRoot Configuration { get; }

        static ConfigurationManager()
        {
            var settingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AudioWorks");
            const string settingsFileName = "settings.json";

            // Copy the settings template if the file doesn't already exist
            var settingsFile = Path.Combine(settingsPath, settingsFileName);
            if (!File.Exists(settingsFile))
            {
                Directory.CreateDirectory(settingsPath);
                File.Copy(
                    Path.Combine(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                        settingsFileName),
                    settingsFile);
            }

            Configuration = new ConfigurationBuilder()
                .SetBasePath(settingsPath)
                .AddJsonFile(settingsFileName, true)
                .Build();
        }
    }
}
