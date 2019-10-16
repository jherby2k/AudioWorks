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
    /// An extension that can read metadata from an audio stream.
    /// </summary>
    public interface IAudioMetadataDecoder
    {
        /// <summary>
        /// Gets the name of the metadata format decoded by this extension.
        /// </summary>
        /// <value>The metadata format.</value>
        string Format { get; }

        /// <summary>
        /// Reads the metadata.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The metadata.</returns>
        AudioMetadata ReadMetadata(Stream stream);
    }
}
