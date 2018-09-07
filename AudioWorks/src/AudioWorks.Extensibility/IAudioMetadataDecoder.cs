using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that can read metadata from an audio file stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioMetadataDecoder
    {
        /// <summary>
        /// Reads the metadata.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <returns>The metadata.</returns>
        [NotNull]
        AudioMetadata ReadMetadata([NotNull] FileStream stream);
    }
}
