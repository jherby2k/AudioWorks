using System;
using System.Collections.Concurrent;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Commands
{
    sealed class CmdletLoggerProvider : ILoggerProvider
    {
        [NotNull] static readonly Lazy<CmdletLoggerProvider> _lazyInstance =
            new Lazy<CmdletLoggerProvider>(() => new CmdletLoggerProvider());

        [NotNull]
        internal static CmdletLoggerProvider Instance => _lazyInstance.Value;

        [NotNull] readonly Lazy<CmdletLogger> _lazyLogger = new Lazy<CmdletLogger>();
        bool _enabled;

        CmdletLoggerProvider()
        {
        }

        [NotNull]
        public ILogger CreateLogger([CanBeNull] string categoryName)
        {
            // Just use the same logger every time
            return _lazyLogger.Value;
        }

        public void Dispose()
        {
        }

        internal void SetMessageQueue([CanBeNull] BlockingCollection<object> messageQueue)
        {
            _lazyLogger.Value.MessageQueue = messageQueue;
        }

        public void Enable()
        {
            // Ensure this provider is only added once
            if (_enabled) return;
            LoggingManager.LoggerFactory.AddProvider(this);
            _enabled = true;
        }
    }
}