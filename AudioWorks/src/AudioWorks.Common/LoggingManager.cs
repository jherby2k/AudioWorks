using System;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages logging to various destinations.
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// Gets the singleton logger factory.
        /// </summary>
        /// <value>The logger factory.</value>
        [NotNull]
        [CLSCompliant(false)]
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory().AddNLog();

        static LoggingManager()
        {
            if (!ConfigurationManager.Configuration.GetValue("FileLogging", true)) return;

            var logFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AudioWorks",
                "Log Files",
                $"{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)}.log");

            // Log at "Info" level by default
            switch (ConfigurationManager.Configuration.GetValue("LogLevel", 2))
            {
                case 0:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Trace);
                    break;
                case 1:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Debug);
                    break;
                case 2:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Info);
                    break;
                case 3:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Warn);
                    break;
                case 4:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Error);
                    break;
                case 5:
                    SimpleConfigurator.ConfigureForFileLogging(logFile, NLog.LogLevel.Fatal);
                    break;
                // Don't log anything at 6+
            }
        }
    }
}
