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

using System.Collections.Concurrent;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Commands
{
    sealed class CmdletLoggerProvider : ILoggerProvider
    {
        [NotNull] readonly ConcurrentQueue<object> _messageQueue = new ConcurrentQueue<object>();

        [NotNull]
        public ILogger CreateLogger([CanBeNull] string categoryName)
        {
            return new CmdletLogger(_messageQueue);
        }

        public void Dispose()
        {
        }

        [ContractAnnotation("=> false, result:null; => true, result:notnull")]
        internal bool TryDequeueMessage(out object result) => _messageQueue.TryDequeue(out result);
    }
}