using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks when attempting to analyze an audio file, if
    /// the name matches.
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
