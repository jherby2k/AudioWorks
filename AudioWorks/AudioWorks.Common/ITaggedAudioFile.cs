using System;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that may or may not contain a metadata "tag".
    /// </summary>
    /// <seealso cref="IAudioFile" />
    [PublicAPI]
    public interface ITaggedAudioFile : IAudioFile
    {
        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        AudioMetadata Metadata { get; set; }

        /// <summary>
        /// Loads the metadata from disk, replacing the current values.
        /// </summary>
        void LoadMetadata();

        /// <summary>
        /// Persists the current metadata to disk.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void SaveMetadata([CanBeNull] SettingDictionary settings = null);
    }
}