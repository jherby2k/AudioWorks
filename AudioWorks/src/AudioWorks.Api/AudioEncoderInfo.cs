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
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about an audio encoder.
    /// </summary>
    [Serializable]
    public sealed class AudioEncoderInfo
    {
        /// <summary>
        /// Gets the name of the encoder.
        /// </summary>
        /// <value>The name.</value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Gets a description of the encoder.
        /// </summary>
        /// <value>The description.</value>
        [NotNull]
        public string Description { get; }

        internal AudioEncoderInfo([NotNull] IDictionary<string, object> metadata)
        {
            Name = (string) metadata["Name"];
            Description = (string) metadata["Description"];
        }
    }
}