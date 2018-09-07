using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Accepts samples for processing.
    /// </summary>
    public interface ISampleProcessor
    {
        /// <summary>
        /// Submits samples for processing.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Submit([NotNull] SampleBuffer samples);
    }
}