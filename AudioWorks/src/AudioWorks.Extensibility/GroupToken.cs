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
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Identifies an audio file as a member of a group.
    /// </summary>
    public sealed class GroupToken : IDisposable
    {
        [NotNull] readonly object _syncRoot = new object();
        [CanBeNull] object _groupState;

        /// <summary>
        /// Sets a group state object, or returns the current one if it has already been set.
        /// </summary>
        /// <remarks>
        /// The group state object will automatically be disposed with the <see cref="GroupToken"/>, if it implements
        /// <see cref="IDisposable"/>.
        /// </remarks>
        /// <param name="groupState">A new group state object.</param>
        /// <returns>The current group state</returns>
        [CanBeNull]
        public object GetOrSetGroupState([CanBeNull] object groupState)
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