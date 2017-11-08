using JetBrains.Annotations;
using System.Collections.Concurrent;
using System.Threading;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class GroupState
    {
        int _handlesDisposed;

        [NotNull]
        internal ConcurrentBag<StateHandle> Handles { get; } = new ConcurrentBag<StateHandle>();

        internal int SignalHandleDisposing()
        {
            return Interlocked.Increment(ref _handlesDisposed);
        }
    }
}
