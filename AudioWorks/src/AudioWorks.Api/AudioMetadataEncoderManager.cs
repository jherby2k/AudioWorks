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

using System;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available metadata encoders, which are used by an
    /// <see cref="ITaggedAudioFile"/>'s SaveMetadata method.
    /// </summary>
    [PublicAPI]
    public static class AudioMetadataEncoderManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="ITaggedAudioFile"/>'s
        /// SaveMetadata method, for a given file extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioMetadataEncoder>(
                "Extension", extension))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new SettingInfoDictionary();
        }
    }
}
