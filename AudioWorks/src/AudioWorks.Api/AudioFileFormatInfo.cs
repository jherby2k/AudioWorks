/* Copyright © 2019 Jeremy Herbison

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
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about an audio file format.
    /// </summary>
    [Serializable]
    public sealed class AudioFileFormatInfo
    {
        /// <summary>
        /// Gets the file extension that this format uses.
        /// </summary>
        /// <value>The file extension.</value>
        [NotNull]
        public string Extension { get; }

        /// <summary>
        /// Gets the audio format.
        /// </summary>
        /// <value>The format.</value>
        [NotNull]
        public string Format { get; }

        internal AudioFileFormatInfo([NotNull, ItemNotNull] IDictionary<string, object> metadata)
        {
            Extension = (string) metadata["Extension"];
            Format = (string) metadata["Format"];
        }
    }
}