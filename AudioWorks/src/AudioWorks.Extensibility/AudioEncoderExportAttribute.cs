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
using System.Composition;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioEncoder"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioEncoderExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the name of the encoder.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets a description of the encoder.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncoderExportAttribute"/> class.
        /// </summary>
        /// <param name="name">The encoder name.</param>
        /// <param name="description">The encoder description.</param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="name"/> or
        /// <paramref name="description"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if either <paramref name="name"/> or
        /// <paramref name="description"/> is empty.</exception>
        public AudioEncoderExportAttribute(string name, string description)
            : base(typeof(IAudioEncoder))
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);
            Name = name;
            Description = description;
        }
    }
}
