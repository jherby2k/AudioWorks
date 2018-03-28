using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// An extension that can encode audio samples into a specific format.
    /// </summary>
    [PublicAPI]
    public interface IAudioEncoder : ISampleProcessor
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="Initialize"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Gets the file extension used by the encoder.
        /// </summary>
        /// <value>The file extension.</value>
        [NotNull]
        string FileExtension { get; }

        /// <summary>
        /// Initializes the encoder.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="info">The audio information.</param>
        /// <param name="metadata">The audio metadata.</param>
        /// <param name="settings">The settings.</param>
        void Initialize(
            [NotNull] FileStream fileStream,
            [NotNull] AudioInfo info,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings);

        /// <summary>
        /// Finishes encoding.
        /// </summary>
        void Finish();
    }
}