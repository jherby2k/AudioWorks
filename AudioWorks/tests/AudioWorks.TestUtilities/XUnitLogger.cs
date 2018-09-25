using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using Xunit.Abstractions;

namespace AudioWorks.TestUtilities
{
    public sealed class XUnitLogger : ILogger
    {
        [NotNull] readonly ITestOutputHelper _outputHelper;
        [CanBeNull] readonly string _categoryName;
        readonly LogLevel _logLevel;

        public XUnitLogger([NotNull] ITestOutputHelper outputHelper, [CanBeNull] string categoryName, LogLevel logLevel)
        {
            _outputHelper = outputHelper;
            _categoryName = categoryName;
            _logLevel = logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, [CanBeNull] TState state,
            [CanBeNull] Exception exception, [NotNull] Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            try
            {
                if (_categoryName != null)
                    _outputHelper.WriteLine("{0}: {1}: {2}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        _categoryName,
                        formatter(state, exception));
                else
                    _outputHelper.WriteLine("{0}: {2}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        formatter(state, exception));
            }
            catch (InvalidOperationException)
            {
                // Test already completed on another thread
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;

        [NotNull]
        public IDisposable BeginScope<TState>([CanBeNull] TState state) => NullScope.Instance;
    }
}