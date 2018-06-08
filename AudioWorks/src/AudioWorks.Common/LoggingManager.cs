using System;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages logging to various destinations.
    /// </summary>
    public static class LoggingManager
    {
        [NotNull] static readonly ILoggerFactory _loggerFactory = new LoggerFactory().AddNLog();

        static LoggingManager()
        {
            NLog.Config.SimpleConfigurator.ConfigureForFileLogging(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "AudioWorks",
                    "log.txt"));
        }

        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance using the full name of the given type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The logger.</returns>
        [NotNull]
        [CLSCompliant(false)]
        public static ILogger CreateLogger<T>() => _loggerFactory.CreateLogger<T>();
    }
}
