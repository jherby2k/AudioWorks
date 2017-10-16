using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Common
{
    /// <summary>
    /// The primary base type for working with AudioWorks. Represents a single track of audio within the filesystem.
    /// </summary>
    [PublicAPI]
    public interface IAudioFile
    {
        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <value>
        /// The file information.
        /// </value>
        [NotNull]
        FileInfo FileInfo { get; }

        /// <summary>
        /// Gets the audio information.
        /// </summary>
        /// <value>
        /// The audio information.
        /// </value>
        [NotNull]
        AudioInfo AudioInfo { get; }
    }
}