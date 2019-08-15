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

using System.Collections.Generic;
using System.Linq;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about available audio file formats, and helper methods for loading them.
    /// </summary>
    [PublicAPI]
    public static class AudioFileManager
    {
        /// <summary>
        /// Gets information about the available audio file formats.
        /// </summary>
        /// <returns>The format info.</returns>
        [NotNull]
        public static IEnumerable<AudioFileFormatInfo> GetFormatInfo() =>
            ExtensionProviderWrapper.GetFactories<IAudioInfoDecoder>()
                .Select(factory => new AudioFileFormatInfo(factory.Metadata));
    }
}
