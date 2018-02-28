using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about an encoder.
    /// </summary>
    public sealed class EncoderInfo
    {
        /// <summary>
        /// Gets the name of the encoder.
        /// </summary>
        /// <value>The name.</value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Gets a description of the encoder.
        /// </summary>
        /// <value>The description.</value>
        [NotNull]
        public string Description { get; }

        internal EncoderInfo([NotNull] IDictionary<string, object> metadata)
        {
            Name = (string) metadata["Name"];
            Description = (string) metadata["Description"];
        }
    }
}