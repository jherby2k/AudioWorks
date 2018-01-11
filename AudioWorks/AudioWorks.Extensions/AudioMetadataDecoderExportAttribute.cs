using System;
using System.Composition;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks when attempting to read metadata from an audio
    /// file, if the file extension matches.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IAudioMetadataDecoder"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioMetadataDecoder))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioMetadataDecoderExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        [NotNull]
        public string Extension { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataDecoderExportAttribute"/> class.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="extension"/> is not a valid file extension.
        /// </exception>
        public AudioMetadataDecoderExportAttribute([NotNull] string extension)
            : base(typeof(IAudioMetadataDecoder))
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
