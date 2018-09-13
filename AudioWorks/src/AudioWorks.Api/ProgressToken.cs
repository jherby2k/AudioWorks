using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Api
{
    /// <summary>
    /// Represents progress for an asynchronous activity.
    /// </summary>
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types",
        Justification = "Instances will not be compared.")]
    public struct ProgressToken
    {
        /// <summary>
        /// Gets the total # of audio files completed since the activity started.
        /// </summary>
        /// <value>The # of audio files completed.</value>
        public int AudioFilesCompleted { get; internal set; }

        /// <summary>
        /// Gets the total # of frames completed since the activity started.
        /// </summary>
        /// <value>The # of frames completed.</value>
        public long FramesCompleted { get; internal set; }
    }
}