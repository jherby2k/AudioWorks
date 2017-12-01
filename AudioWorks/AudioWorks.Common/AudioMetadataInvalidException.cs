using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents errors that occur during the reading of an <see cref="ITaggedAudioFile"/> that are the result of
    /// invalid metadata.
    /// </summary>
    /// <seealso cref="AudioException"/>
    [PublicAPI]
    [Serializable]
    public sealed class AudioMetadataInvalidException : AudioException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataInvalidException"/> class.
        /// </summary>
        public AudioMetadataInvalidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AudioMetadataInvalidException([CanBeNull] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataInvalidException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AudioMetadataInvalidException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadataInvalidException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the
        /// source or destination.</param>
        AudioMetadataInvalidException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
