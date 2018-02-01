using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents errors that occur during the reading of an <see cref="IAudioFile"/>.
    /// </summary>
    /// <seealso cref="Exception"/>
    [PublicAPI]
    [Serializable]
    public abstract class AudioException : Exception
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        [CanBeNull]
        public string Path { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        protected AudioException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected AudioException([CanBeNull] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="path">The file path.</param>
        protected AudioException([CanBeNull] string message, [CanBeNull] string path)
            : base(message)
        {
            Path = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        protected AudioException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the
        /// source or destination.</param>
        protected AudioException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Path = info.GetString("Path");
        }

        /// <inheritdoc/>
        public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Path", Path);
        }
    }
}
