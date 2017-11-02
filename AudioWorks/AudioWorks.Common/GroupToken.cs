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
        int _remainingMembers;

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

        /// <inheritdoc/>
        public void Dispose()
        {
            _resetEvent.Dispose();
        }
    }
}