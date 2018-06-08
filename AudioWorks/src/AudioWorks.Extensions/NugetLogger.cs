using System.Threading.Tasks;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AudioWorks.Extensions
{
    sealed class NugetLogger : LoggerBase
    {
        [NotNull] readonly ILogger _logger = LoggingManager.CreateLogger<NugetLogger>();

        public override void Log([NotNull] ILogMessage message)
        {
            switch (message.Level)
            {
                case NuGet.Common.LogLevel.Debug:
                    _logger.LogDebug(message.FormatWithCode());
                    break;
                case NuGet.Common.LogLevel.Verbose:
                    _logger.LogTrace(message.FormatWithCode());
                    break;
                case NuGet.Common.LogLevel.Information:
                case NuGet.Common.LogLevel.Minimal:
                    _logger.LogInformation(message.FormatWithCode());
                    break;
                case NuGet.Common.LogLevel.Warning:
                    _logger.LogWarning(message.FormatWithCode());
                    break;
                case NuGet.Common.LogLevel.Error:
                    _logger.LogError(message.FormatWithCode());
                    break;
            }
        }

        public override async Task LogAsync([NotNull] ILogMessage message)
        {
            await Task.Run(() => Log(message)).ConfigureAwait(false);
        }
    }
}