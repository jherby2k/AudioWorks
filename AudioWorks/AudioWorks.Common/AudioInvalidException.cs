using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public sealed class AudioInvalidException : AudioException
    {
        [CanBeNull]
        public FileInfo FileInfo { get; }

        public AudioInvalidException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                FileInfo = new FileInfo(fileName);
        }

        public AudioInvalidException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            FileInfo = fileInfo;
        }

        public AudioInvalidException()
        {
        }

        public AudioInvalidException([CanBeNull] string message)
            : base(message)
        {
        }

        public AudioInvalidException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        AudioInvalidException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
