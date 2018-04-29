using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioFilter"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioFilter))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioFilterExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the name of the encoder.
        /// </summary>
        /// <value>The name.</value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilterExportAttribute"/> class.
        /// </summary>
        /// <param name="name">The filter name.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null or empty.</exception>
        public AudioFilterExportAttribute([NotNull] string name)
            : base(typeof(IAudioFilter))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
        }
    }
}
