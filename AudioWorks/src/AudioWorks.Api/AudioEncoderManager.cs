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
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available encoders, which are used by an <see cref="AudioFileEncoder"/>'s
    /// Encode method.
    /// </summary>
    [PublicAPI]
    public static class AudioEncoderManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="AudioFileEncoder"/>'s
        /// Encode method, for a given encoder.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioEncoder>(
                "Name", name))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new SettingInfoDictionary();
        }

        /// <summary>
        /// Gets information about the available encoders.
        /// </summary>
        /// <returns>The encoder info.</returns>
        [NotNull]
        public static IEnumerable<AudioEncoderInfo> GetEncoderInfo()
        {
            return ExtensionProviderWrapper.GetFactories<IAudioEncoder>()
                .Select(factory => new AudioEncoderInfo(factory.Metadata));
        }
    }
}
