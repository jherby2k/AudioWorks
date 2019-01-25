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
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace AudioWorks.TestUtilities
{
    public sealed class XunitLogger : ILogger
    {
        [NotNull] readonly XunitLoggerProvider _provider;
        [CanBeNull] readonly string _categoryName;

        public XunitLogger([NotNull] XunitLoggerProvider provider, [CanBeNull] string categoryName)
        {
            _provider = provider;
            _categoryName = categoryName;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, [CanBeNull] TState state,
            [CanBeNull] Exception exception, [NotNull] Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            try
            {
                if (_categoryName != null)
                    _provider.OutputHelper?.WriteLine("{0}: {1}: {2}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        _categoryName,
                        formatter(state, exception));
                else
                    _provider.OutputHelper?.WriteLine("{0}: {2}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        formatter(state, exception));
            }
            catch (InvalidOperationException)
            {
                // Test already completed on another thread
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _provider.MinLogLevel;

        [NotNull]
        public IDisposable BeginScope<TState>([CanBeNull] TState state) => NullScope.Instance;
    }
}