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

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class GroupState : IDisposable
    {
        readonly object _syncRoot = new object();

        internal double GroupPeak { get; private set; }

        internal ConcurrentQueue<StateHandle> Handles { get; } = new ConcurrentQueue<StateHandle>();

        internal void AddPeak(double peak)
        {
            lock (_syncRoot)
                GroupPeak = Math.Max(peak, GroupPeak);
        }

        public void Dispose()
        {
            while (Handles.TryDequeue(out var handle))
                handle.Dispose();
        }
    }
}
