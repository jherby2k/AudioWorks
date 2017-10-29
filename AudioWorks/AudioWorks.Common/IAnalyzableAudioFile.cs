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
        void Analyze([NotNull] string analyzer);
    }
}