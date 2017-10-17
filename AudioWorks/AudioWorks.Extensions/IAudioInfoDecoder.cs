using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// An extension that can read basic information about an audio file stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioInfoDecoder
    {
        /// <summary>
        /// Reads the audio information from a file stream.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <returns>The audio information.</returns>
        [NotNull]
        AudioInfo ReadAudioInfo([NotNull] FileStream stream);
    }
}