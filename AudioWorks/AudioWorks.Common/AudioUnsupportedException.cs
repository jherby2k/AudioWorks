using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents errors that occur during the reading of an <see cref="IAudioFile"/> that are the result of
    /// encountered unsupported data.
    /// </summary>
    /// <seealso cref="AudioException"/>
    [PublicAPI]
    [Serializable]
    public sealed class AudioUnsupportedException : AudioException
    {
        [CanBeNull, NonSerialized] readonly FileInfo _fileInfo;

        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <value>
        /// The file information.
        /// </value>
        [CanBeNull]
        public FileInfo FileInfo => _fileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fileName">Name of the file.</param>
        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                _fileInfo = new FileInfo(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fileInfo">The file information.</param>
        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            _fileInfo = fileInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        public AudioUnsupportedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AudioUnsupportedException([CanBeNull] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUnsupportedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the
        /// source or destination.</param>
        AudioUnsupportedException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            var fileName = info.GetString("fileName");
            if (fileName != null)
                _fileInfo = new FileInfo(fileName);
        }

        /// <inheritdoc />
        public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("fileName", _fileInfo?.FullName);
        }
    }
}
