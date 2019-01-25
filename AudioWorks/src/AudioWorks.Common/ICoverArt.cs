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

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a cover art image.
    /// </summary>
    [PublicAPI]
    public interface ICoverArt
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        int Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        int Height { get; }

        /// <summary>
        /// Gets the color depth.
        /// </summary>
        /// <value>The color depth.</value>
        int ColorDepth { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="CoverArt"/> is lossless (stored in PNG format).
        /// </summary>
        /// <value><c>true</c> if the image is lossless; otherwise, <c>false</c>.</value>
        bool Lossless { get; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        /// <value>The MIME type.</value>
        [NotNull]
        string MimeType { get; }

        /// <summary>
        /// Gets the default file extension (.png for lossless, or .jpg for lossy files).
        /// </summary>
        /// <value>The file extension.</value>
        [NotNull]
        string FileExtension { get; set; }

        /// <summary>
        /// Gets the raw image data.
        /// </summary>
        /// <value>The data.</value>
        ReadOnlySpan<byte> Data { get; }
    }
}