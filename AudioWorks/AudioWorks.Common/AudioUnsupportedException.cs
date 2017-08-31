using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public class AudioUnsupportedException : AudioException
    {
        [CanBeNull]
        public FileInfo FileInfo { get; }

        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                FileInfo = new FileInfo(fileName);
        }

        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            FileInfo = fileInfo;
        }

        public AudioUnsupportedException()
        {
        }

        public AudioUnsupportedException([CanBeNull] string message)
            : base(message)
        {
        }

        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        protected AudioUnsupportedException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
