using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// An extension that can decode an audio file stream.
    /// </summary>
    [PublicAPI]
    public interface IAudioDecoder
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IAudioDecoder"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        bool Finished { get; }

        /// <summary>
        /// Initializes the decoder.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        void Initialize([NotNull] FileStream fileStream);

        /// <summary>
        /// Decodes a collection of samples.
        /// </summary>
        /// <returns>The samples.</returns>
        [NotNull]
        SampleCollection DecodeSamples();
    }
}