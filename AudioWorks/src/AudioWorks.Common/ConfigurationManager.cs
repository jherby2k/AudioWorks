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
