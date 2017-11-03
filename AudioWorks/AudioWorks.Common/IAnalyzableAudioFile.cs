using System.Threading;
using System.Threading.Tasks;
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
        /// Analyzes the audio file asynchronously.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="groupToken">The group token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An awaitable task.</returns>
        Task AnalyzeAsync([NotNull] string analyzer, [NotNull] GroupToken groupToken, CancellationToken cancellationToken);
    }
}