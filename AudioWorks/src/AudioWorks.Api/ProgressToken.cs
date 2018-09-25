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

using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Api
{
    /// <summary>
    /// Represents progress for an asynchronous activity.
    /// </summary>
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types",
        Justification = "Instances will not be compared.")]
    public struct ProgressToken
    {
        /// <summary>
        /// Gets the total # of audio files completed since the activity started.
        /// </summary>
        /// <value>The # of audio files completed.</value>
        public int AudioFilesCompleted { get; internal set; }

        /// <summary>
        /// Gets the total # of frames completed since the activity started.
        /// </summary>
        /// <value>The # of frames completed.</value>
        public long FramesCompleted { get; internal set; }
    }
}