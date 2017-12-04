namespace AudioWorks.Api
{
    /// <summary>
    /// An immutable message that indicates progress.
    /// </summary>
    public sealed class ProgressToken
    {
        /// <summary>
        /// Gets the # completed.
        /// </summary>
        /// <value>The # completed.</value>
        public int Completed { get; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>The total.</value>
        public int Total { get; }

        internal ProgressToken(int completed, int total)
        {
            Completed = completed;
            Total = total;
        }
    }
}