using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that can write metadata to an audio file stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioMetadataEncoder
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="WriteMetadata"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Writes the metadata to the file stream.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="settings">The settings.</param>
        void WriteMetadata(
            [NotNull] FileStream stream,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings);
    }
}
