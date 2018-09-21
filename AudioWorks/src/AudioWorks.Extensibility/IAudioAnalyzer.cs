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

using System.Diagnostics.CodeAnalysis;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that can analyze audio samples.
    /// </summary>
    [PublicAPI]
    public interface IAudioAnalyzer : ISampleProcessor
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="Initialize"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Initializes the analyzer.
        /// </summary>
        /// <param name="info">The audio information.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="groupToken">The group token.</param>
        void Initialize(
            [NotNull] AudioInfo info,
            [NotNull] SettingDictionary settings,
            [NotNull] GroupToken groupToken);

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>The result.</returns>
        [NotNull]
        [SuppressMessage("Design", "CA1024:Use properties where appropriate",
            Justification = "Order of execution is important")]
        AudioMetadata GetResult();

        /// <summary>
        /// Gets the result for a group.
        /// </summary>
        /// <returns>The result.</returns>
        [NotNull]
        [SuppressMessage("Design", "CA1024:Use properties where appropriate",
            Justification = "Order of execution is important")]
        AudioMetadata GetGroupResult();
    }
}