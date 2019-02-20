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

using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that may or may not contain a metadata "tag".
    /// </summary>
    /// <seealso cref="IAudioFile"/>
    [PublicAPI]
    public interface ITaggedAudioFile : IAudioFile
    {
        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [NotNull]
        AudioMetadata Metadata { get; }

        /// <summary>
        /// Loads the metadata from disk, replacing the current values.
        /// </summary>
        void LoadMetadata();

        /// <summary>
        /// Persists the current metadata to disk.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void SaveMetadata([CanBeNull] SettingDictionary settings = null);
    }
}