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
using System.Collections.Concurrent;
using System.Management.Automation;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Commands
{
    sealed class CmdletLogger : ILogger
    {
        readonly ConcurrentQueue<object> _messageQueue;

        internal CmdletLogger(ConcurrentQueue<object> messageQueue) => _messageQueue = messageQueue;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (logLevel)
            {
                case LogLevel.Debug:
                    _messageQueue.Enqueue(new DebugRecord(message));
                    break;
                case LogLevel.Information:
                    _messageQueue.Enqueue(new InformationRecord(message, null));
                    break;
                case LogLevel.Warning:
                    _messageQueue.Enqueue(new WarningRecord(message));
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel) =>
            logLevel switch
            {
                LogLevel.Debug => true,
                LogLevel.Information => true,
                LogLevel.Warning => true,
                _ => false
            };

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => new NullScope();
    }
}