﻿/* Copyright © 2018 Jeremy Herbison

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
using System.IO;
using System.Linq;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioMetadataDecoder"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioMetadataDecoderExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
        public string Extension { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataDecoderExportAttribute"/> class.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="extension"/> is not a valid file extension.
        /// </exception>
        public AudioMetadataDecoderExportAttribute(string extension)
            : base(typeof(IAudioMetadataDecoder))
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(extension));
            if (!extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                || extension.Any(char.IsWhiteSpace)
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                || extension.Any(character => Path.GetInvalidFileNameChars().Contains(character)))
                throw new ArgumentException($"'{extension}' is not a valid file extension.", nameof(extension));

            Extension = extension;
        }
    }
}
