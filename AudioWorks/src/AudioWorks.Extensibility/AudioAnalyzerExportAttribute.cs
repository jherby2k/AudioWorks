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
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioAnalyzer"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioAnalyzer))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioAnalyzerExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the name of the analyzer.
        /// </summary>
        /// <value>The name.</value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Gets a description of the analyzer.
        /// </summary>
        /// <value>The description.</value>
        [NotNull]
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioAnalyzerExportAttribute"/> class.
        /// </summary>
        /// <param name="name">The analyzer name.</param>
        /// <param name="description">The analyzer description.</param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="name"/> or
        /// <paramref name="description"/> is null or empty.</exception>
        public AudioAnalyzerExportAttribute([NotNull] string name, [NotNull] string description)
            : base(typeof(IAudioAnalyzer))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Value cannot be null or empty.", nameof(description));

            Name = name;
            Description = description;
        }
    }
}
