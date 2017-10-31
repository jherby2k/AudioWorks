using System.Threading;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that can be analyzed in order to set additional metadata.
    /// </summary>
    /// <seealso cref="ITaggedAudioFile"/>
    [PublicAPI]
    public interface IAnalyzableAudioFile : ITaggedAudioFile
    {
        /// <summary>
        /// Analyzes the audio file.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="groupToken">The group token.</param>
        void Analyze([NotNull] string analyzer, CancellationToken cancellationToken, [CanBeNull] GroupToken groupToken = null);
    }
}