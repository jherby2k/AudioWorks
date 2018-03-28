using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents errors that occur during the reading of an <see cref="IAudioFile"/> that are the result of invalid
    /// data.
    /// </summary>
    /// <seealso cref="AudioException"/>
    [PublicAPI]
    [Serializable]
    public sealed class AudioInvalidException : AudioException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInvalidException"/> class.
        /// </summary>
        public AudioInvalidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AudioInvalidException([CanBeNull] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="path">The file path.</param>
        public AudioInvalidException([CanBeNull] string message, [CanBeNull] string path)
            : base(message, path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInvalidException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AudioInvalidException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInvalidException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the
        /// source or destination.</param>
        AudioInvalidException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
