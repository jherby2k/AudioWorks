using System;
using System.Composition;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks when attempting to read an audio file from the
    /// filesystem, if the file extension matches.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute should implement <see cref="IAudioInfoDecoder"/> and also be marked with the
    /// <see cref="SharedAttribute"/> attribute.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioInfoDecoder))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioInfoDecoderExportAttribute : ExportAttribute
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
        /// Initializes a new instance of the <see cref="AudioInfoDecoderExportAttribute"/> class.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="extension"/> is not a valid file extension.
        /// </exception>
        public AudioInfoDecoderExportAttribute([NotNull] string extension)
            : base(typeof(IAudioInfoDecoder))
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
