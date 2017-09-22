using JetBrains.Annotations;
using System;
using System.Composition;
using System.IO;
using System.Linq;

namespace AudioWorks.Extensions
{
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IAudioMetadataEncoder))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AudioMetadataEncoderExportAttribute : ExportAttribute
    {
        [NotNull]
        public string Extension { get; }

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
