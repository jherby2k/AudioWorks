using System;
using System.Threading;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Identifies an audio file as a member of a group.
    /// </summary>
    public sealed class GroupToken : IDisposable
    {
        [NotNull] readonly ManualResetEventSlim _resetEvent = new ManualResetEventSlim();
        readonly object _syncRoot = new object();
        int _remainingMembers;
        object _groupState;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupToken"/> class.
        /// </summary>
        /// <param name="count">The member count.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> must be 1 or greater.</exception>
        public GroupToken(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} must be 1 or greater.");

            _remainingMembers = count;
        }

        /// <summary>
        /// Signals that one of the members has completed.
        /// </summary>
        public void CompleteMember()
        {
            if (Interlocked.Decrement(ref _remainingMembers) <= 0)
                _resetEvent.Set();
        }

        /// <summary>
        /// Blocks the current thread until the last member completes.
        /// </summary>
        public void WaitForMembers()
        {
            _resetEvent.Wait();
        }

        /// <summary>
        /// Sets a group state object, or returns the current one if it has already been set.
        /// </summary>
        /// <remarks>
        /// The group state object will automatically be disposed with the <see cref="GroupToken"/>, if it implements
        /// <see cref="IDisposable"/>.
        /// </remarks>
        /// <param name="groupState">A new group state object.</param>
        /// <returns>The current group state</returns>
        public object GetOrSetGroupState(object groupState)
        {
            lock (_syncRoot)
            {
                if (_groupState == null)
                    return _groupState = groupState;
                return _groupState;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _resetEvent.Dispose();
            if (_groupState is IDisposable disposableState)
                disposableState.Dispose();
        }
    }
}