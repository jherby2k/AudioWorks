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
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that can decode an audio stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioDecoder
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IAudioDecoder"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        bool Finished { get; }

        /// <summary>
        /// Initializes the decoder.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void Initialize([NotNull] Stream stream);

        /// <summary>
        /// Decodes a collection of samples.
        /// </summary>
        /// <returns>The samples.</returns>
        [NotNull]
        SampleBuffer DecodeSamples();
    }
}