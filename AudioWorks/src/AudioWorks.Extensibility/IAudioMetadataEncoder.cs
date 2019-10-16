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

using System.IO;
using AudioWorks.Common;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that can write metadata to an audio stream.
    /// </summary>
    public interface IAudioMetadataEncoder
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="WriteMetadata"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Writes the metadata to the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="settings">The settings.</param>
        void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings);
    }
}
