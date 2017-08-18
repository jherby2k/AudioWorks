using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public class UnsupportedFileException : Exception
    {
        [CanBeNull]
        public FileInfo FileInfo { get; }

        public UnsupportedFileException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                FileInfo = new FileInfo(fileName);
        }

        public UnsupportedFileException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            FileInfo = fileInfo;
        }

        public UnsupportedFileException()
        {
        }

        public UnsupportedFileException([CanBeNull] string message)
            : base(message)
        {
        }

        public UnsupportedFileException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnsupportedFileException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
