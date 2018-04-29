using System;
using System.Composition;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioMetadataEncoder"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioMetadataEncoder))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioMetadataEncoderExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
        [NotNull]
        public string Extension { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataEncoderExportAttribute"/> class.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="extension"/> is not a valid file extension.
        /// </exception>
        public AudioMetadataEncoderExportAttribute([NotNull] string extension)
            : base(typeof(IAudioMetadataEncoder))
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));
            if (!extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                || extension.Any(char.IsWhiteSpace)
                || extension.Any(character => Path.GetInvalidFileNameChars().Contains(character)))
                throw new ArgumentException($"'{extension}' is not a valid file extension.", nameof(extension));

            Extension = extension;
        }
    }
}
