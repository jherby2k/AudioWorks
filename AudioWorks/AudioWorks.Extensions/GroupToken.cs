using System;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Identifies an audio file as a member of a group.
    /// </summary>
    public sealed class GroupToken : IDisposable
    {
        readonly object _syncRoot = new object();
        object _groupState;

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
            if (_groupState is IDisposable disposableState)
                disposableState.Dispose();
        }
    }
}