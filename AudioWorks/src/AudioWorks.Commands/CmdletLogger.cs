/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace AudioWorks.Commands
{
    [UsedImplicitly]
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes",
        Justification = "Instantiated via Lazy<T>")]
    sealed class CmdletLogger : ILogger
    {
        [NotNull]
        internal ConcurrentQueue<object> MessageQueue { get; } = new ConcurrentQueue<object>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, [CanBeNull] TState state, [CanBeNull] Exception exception, [NotNull] Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (logLevel)
            {
                case LogLevel.Debug:
                    MessageQueue.Enqueue(new DebugRecord(message));
                    break;
                case LogLevel.Information:
                    MessageQueue.Enqueue(new InformationRecord(message, null));
                    break;
                case LogLevel.Warning:
                    MessageQueue.Enqueue(new WarningRecord(message));
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (logLevel)
            {
                case LogLevel.Debug:
                case LogLevel.Information:
                case LogLevel.Warning:
                    return true;
                default:
                    return false;
            }
        }

        [NotNull]
        public IDisposable BeginScope<TState>([CanBeNull] TState state) => NullScope.Instance;
    }
}