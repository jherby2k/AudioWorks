using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks when attempting to export an audio
    /// file to another format.
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
        /// Gets the name of the analyzer.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncoderExportAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the analyzer.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null or empty.</exception>
        public AudioEncoderExportAttribute([NotNull] string name)
            : base(typeof(IAudioEncoder))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
        }
    }
}
