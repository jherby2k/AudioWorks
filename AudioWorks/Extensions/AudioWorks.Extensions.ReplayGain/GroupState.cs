using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class GroupState
    {
        readonly object _syncRoot = new object();
        int _handlesDisposed;

        internal double GroupPeak { get; private set; }

        [NotNull]
        internal ConcurrentBag<StateHandle> Handles { get; } = new ConcurrentBag<StateHandle>();

        internal int SignalHandleDisposing()
        {
            return Interlocked.Increment(ref _handlesDisposed);
        }

        internal void AddPeak(double peak)
        {
            lock (_syncRoot)
            {
                GroupPeak = Math.Max(peak, GroupPeak);
            }
        }
    }
}
