using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about an analyzer.
    /// </summary>
    public sealed class AnalyzerInfo
    {
        /// <summary>
        /// Gets the name of the analyzer.
        /// </summary>
        /// <value>The name.</value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Gets a description of the analyzer.
        /// </summary>
        /// <value>The description.</value>
        [NotNull]
        public string Description { get; }

        internal AnalyzerInfo([NotNull] IDictionary<string, object> metadata)
        {
            Name = (string) metadata["Name"];
            Description = (string) metadata["Description"];
        }
    }
}