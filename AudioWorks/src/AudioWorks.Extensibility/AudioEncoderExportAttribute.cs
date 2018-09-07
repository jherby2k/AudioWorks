using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioEncoder"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioEncoder))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioEncoderExportAttribute : ExportAttribute
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncoderExportAttribute"/> class.
        /// </summary>
        /// <param name="name">The encoder name.</param>
        /// <param name="description">The encoder description.</param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="name"/> or
        /// <paramref name="description"/> is null or empty.</exception>
        public AudioEncoderExportAttribute([NotNull] string name, [NotNull] string description)
            : base(typeof(IAudioEncoder))
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
