using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that processes samples.
    /// </summary>
    [PublicAPI]
    public interface IAudioFilter
    {
        /// <summary>
        /// Gets information about the settings that can be passed to the <see cref="Process"/> method.
        /// </summary>
        /// <value>The setting information.</value>
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        /// <summary>
        /// Initializes the filter.
        /// </summary>
        /// <param name="info">The audio information.</param>
        /// <param name="metadata">The audio metadata.</param>
        /// <param name="settings">The settings.</param>
        void Initialize(
            [NotNull] AudioInfo info,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings);

        /// <summary>
        /// Processes the specified samples.
        /// </summary>
        /// <param name="samples">The samples.</param>
        /// <returns>The modified samples.</returns>
        [NotNull] SampleBuffer Process([NotNull] SampleBuffer samples);
    }
}