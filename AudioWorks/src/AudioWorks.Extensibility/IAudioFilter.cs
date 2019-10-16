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

using AudioWorks.Common;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that processes samples.
    /// </summary>
    public interface IAudioFilter
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="Process"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Initializes the filter.
        /// </summary>
        /// <param name="info">The audio information.</param>
        /// <param name="metadata">The audio metadata.</param>
        /// <param name="settings">The settings.</param>
        void Initialize(AudioInfo info, AudioMetadata metadata, SettingDictionary settings);

        /// <summary>
        /// Processes the specified samples.
        /// </summary>
        /// <param name="samples">The samples.</param>
        /// <returns>The modified samples.</returns>
        SampleBuffer Process(SampleBuffer samples);
    }
}