using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// An extension that can analyze an audio file stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioAnalyzer
    {
        /// <summary>
        /// Initializes the analyzer.
        /// </summary>
        /// <param name="audioInfo">The audio information.</param>
        /// <param name="groupToken">The group token.</param>
        void Initialize([NotNull] AudioInfo audioInfo, [NotNull] GroupToken groupToken);

        /// <summary>
        /// Submits samples for processing.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Submit([NotNull] SampleCollection samples);

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>The result.</returns>
        [NotNull]
        AudioMetadata GetResult();
    }
}