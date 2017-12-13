using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// An extension that can encode audio samples into a specific format.
    /// </summary>
    [PublicAPI]
    public interface IAudioEncoder
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="Initialize"/> method.
        /// </summary>
        /// <value>
        /// The setting information.
        /// </value>
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Initializes the encoder.
        /// </summary>
        /// <param name="info">The audio information.</param>
        /// <param name="metadata">The audio metadata.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="groupToken">The group token.</param>
        void Initialize(
            [NotNull] AudioInfo info,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings,
            [NotNull] GroupToken groupToken);

        /// <summary>
        /// Submits samples for processing.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Submit([NotNull] SampleCollection samples);
    }
}