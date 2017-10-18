using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// The primary base type for working with AudioWorks. Represents a single track of audio within the filesystem.
    /// </summary>
    [PublicAPI]
    public interface IAudioFile
    {
        /// <summary>
        /// Gets the fully-qualified file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        [NotNull]
        string Path { get; }

        /// <summary>
        /// Gets the audio information.
        /// </summary>
        /// <value>
        /// The audio information.
        /// </value>
        [NotNull]
        AudioInfo Info { get; }
    }
}