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
using System.IO;

namespace AudioWorks.Common
{
    /// <summary>
    /// The primary base type for working with AudioWorks. Represents a single track of audio within the filesystem.
    /// </summary>
    public interface IAudioFile
    {
        /// <summary>
        /// Gets the fully-qualified file path.
        /// </summary>
        /// <value>The file path.</value>
        string Path { get; }

        /// <summary>
        /// Gets the audio information.
        /// </summary>
        /// <value>The audio information.</value>
        AudioInfo Info { get; }

        /// <summary>
        /// Renames the audio file.
        /// </summary>
        /// <param name="name">The new file name, not including the extension.</param>
        /// <param name="replace">if set to <c>true</c> and the new file name already exists, replace it.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name" /> is null or empty.</exception>
        /// <exception cref="IOException">Thrown if the new file already exists, and <paramref name="replace"/> is
        /// <c>false</c>.</exception>
        void Rename(string name, bool replace);
    }
}