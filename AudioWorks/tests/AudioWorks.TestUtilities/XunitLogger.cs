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
using Microsoft.Extensions.Logging;

namespace AudioWorks.TestUtilities
{
    public sealed class XunitLogger(XunitLoggerProvider provider, string? categoryName) : ILogger
    {
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            try
            {
                if (categoryName != null)
                    provider.OutputHelper?.WriteLine("{0}: {1}: {2}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        categoryName,
                        formatter(state, exception));
                else
                    provider.OutputHelper?.WriteLine("{0}: {1}",
                        Enum.GetName(typeof(LogLevel), logLevel),
                        formatter(state, exception));
            }
            catch (InvalidOperationException)
            {
                // Test already completed on another thread
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= provider.MinLogLevel;

        public IDisposable BeginScope<TState>(TState state) => new NullScope();
    }
}