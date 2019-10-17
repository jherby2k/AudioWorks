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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages the retrieval of configuration settings from disk.
    /// </summary>
    public static class ConfigurationManager
    {
        const string _currentRepository = "https://www.myget.org/F/audioworks-extensions-v4/api/v3/index.json";

        static readonly List<string> _oldRepositories = new List<string>(new[]
        {
            "https://www.myget.org/F/audioworks-extensions/api/v3/index.json",
            "https://www.myget.org/F/audioworks-extensions-v1/api/v3/index.json",
            "https://www.myget.org/F/audioworks-extensions-v2/api/v3/index.json",
            "https://www.myget.org/F/audioworks-extensions-v3/api/v3/index.json"
        });

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
            const string settingsFileName = "settings.json";
            var settingsFile = Path.Combine(settingsPath, settingsFileName);

            MigrateToRoamingProfile(settingsFile);

            if (File.Exists(settingsFile))
                UpgradeRepositoryUrl(settingsFile);
            else
            {
                // Copy the settings template if the file doesn't already exist
                Directory.CreateDirectory(settingsPath);
                File.Copy(
                    Path.Combine(
                        Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                        settingsFileName),
                    settingsFile);
            }

            Configuration = new ConfigurationBuilder()
                .SetBasePath(settingsPath)
                .AddJsonFile(settingsFileName, true)
                .Build();
        }

        static void MigrateToRoamingProfile(string settingsFile)
        {
            var oldSettingsFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AudioWorks", Path.GetFileName(settingsFile));
            if (!File.Exists(oldSettingsFile)) return;

            if (!File.Exists(settingsFile))
                File.Copy(oldSettingsFile, settingsFile);
            File.Delete(oldSettingsFile);
        }

        static void UpgradeRepositoryUrl(string settingsFile)
        {
            var settings = File.ReadAllText(settingsFile);
            foreach (var oldRepository in _oldRepositories)
#if NETSTANDARD2_0
                settings = settings.Replace(oldRepository, _currentRepository);
#else
                settings = settings.Replace(oldRepository, _currentRepository, StringComparison.OrdinalIgnoreCase);
#endif
            File.WriteAllText(settingsFile, settings);
        }
    }
}
