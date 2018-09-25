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
        public ILogger CreateLogger([CanBeNull] string categoryName) => _lazyLogger.Value;

        public void Dispose()
        {
        }

        [ContractAnnotation("=> false, result:null; => true, result:notnull")]
        internal bool TryDequeueMessage(out object result) => _lazyLogger.Value.MessageQueue.TryDequeue(out result);

        internal void Enable()
        {
            // Ensure this provider is only added once
            if (_enabled) return;
            LoggingManager.LoggerFactory.AddProvider(this);
            _enabled = true;
        }
    }
}