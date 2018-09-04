using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

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
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
    }
}
