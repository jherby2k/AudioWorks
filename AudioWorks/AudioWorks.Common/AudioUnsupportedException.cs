using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public sealed class AudioUnsupportedException : AudioException
    {
        [CanBeNull, NonSerialized] readonly FileInfo _fileInfo;

        [CanBeNull]
        public FileInfo FileInfo => _fileInfo;

        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                _fileInfo = new FileInfo(fileName);
        }

        public AudioUnsupportedException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            _fileInfo = fileInfo;
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

        AudioUnsupportedException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            var fileName = info.GetString("fileName");
            if (fileName != null)
                _fileInfo = new FileInfo(fileName);
        }

        public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("fileName", _fileInfo?.FullName);
        }
    }
}
