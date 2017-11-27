using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class GroupState : IDisposable
    {
        readonly object _syncRoot = new object();

        internal double GroupPeak { get; private set; }

        [NotNull]
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
