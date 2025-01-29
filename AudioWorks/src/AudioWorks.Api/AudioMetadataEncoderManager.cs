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
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available metadata encoders, which are used by an
    /// <see cref="ITaggedAudioFile"/>'s SaveMetadata method.
    /// </summary>
    public static class AudioMetadataEncoderManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="ITaggedAudioFile"/>'s
        /// SaveMetadata method, for a given file extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        public static SettingInfoDictionary GetSettingInfoByExtension(string extension)
        {
            ArgumentNullException.ThrowIfNull(nameof(extension));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioMetadataEncoder>(
                "Extension", extension))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new();
        }

        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="ITaggedAudioFile"/>'s
        /// SaveMetadata method, for a given metadata format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="format"/> is null.</exception>
        public static SettingInfoDictionary GetSettingInfoByFormat(string format)
        {
            ArgumentNullException.ThrowIfNull(nameof(format));

            // Try each encoder that supports this format:
            foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioMetadataEncoder>(
                "Format", format))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new();
        }

        /// <summary>
        /// Gets information about the available metadata encoders.
        /// </summary>
        /// <returns>The encoder info.</returns>
        public static IEnumerable<AudioMetadataEncoderInfo> GetEncoderInfo() =>
            ExtensionProviderWrapper.GetFactories<IAudioMetadataEncoder>()
                .Select(factory => new AudioMetadataEncoderInfo(factory.Metadata));
    }
}
