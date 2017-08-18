using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public class InvalidFileException : Exception
    {
        [CanBeNull]
        public FileInfo FileInfo { get; }

        public InvalidFileException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                FileInfo = new FileInfo(fileName);
        }

        public InvalidFileException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            FileInfo = fileInfo;
        }

        public InvalidFileException()
        {
        }

        public InvalidFileException([CanBeNull] string message)
            : base(message)
        {
        }

        public InvalidFileException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidFileException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
