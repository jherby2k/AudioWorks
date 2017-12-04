namespace AudioWorks.Api
{
    /// <summary>
    /// An immutable message that indicates progress.
    /// </summary>
    public sealed class ProgressToken
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }

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

        internal ProgressToken(string description, int completed, int total)
        {
            Description = description;
            Completed = completed;
            Total = total;
        }
    }
}