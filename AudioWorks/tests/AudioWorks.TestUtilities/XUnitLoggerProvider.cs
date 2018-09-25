using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AudioWorks.TestUtilities
{
    public sealed class XUnitLoggerProvider : ILoggerProvider
    {
        [NotNull] readonly ITestOutputHelper _outputHelper;
        readonly LogLevel _logLevel;

        public XUnitLoggerProvider([NotNull] ITestOutputHelper outputHelper, LogLevel logLevel = LogLevel.Information)
        {
            _outputHelper = outputHelper;
            _logLevel = logLevel;
        }

        [NotNull]
        public ILogger CreateLogger([CanBeNull] string categoryName)
        {
            return new XUnitLogger(_outputHelper, categoryName, _logLevel);
        }

        public void Dispose()
        {
        }
    }
}
