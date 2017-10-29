using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Api
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that can be analyzed in order to set additional metadata.
    /// </summary>
    /// <seealso cref="TaggedAudioFile"/>
    /// <seealso cref="IAnalyzableAudioFile"/>
    [PublicAPI]
    [Serializable]
    public sealed class AnalyzableAudioFile : TaggedAudioFile, IAnalyzableAudioFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzableAudioFile"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        public AnalyzableAudioFile([NotNull] string path)
            : base(path)
        {
        }

        /// <inheritdoc />
        public void Analyze(string analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            // TODO
        }
    }
}