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
        [CanBeNull, NonSerialized] readonly FileInfo _fileInfo;

        [CanBeNull]
        public FileInfo FileInfo => _fileInfo;

        public AudioInvalidException([CanBeNull] string message, [CanBeNull] string fileName)
            : base(message)
        {
            if (fileName != null)
                _fileInfo = new FileInfo(fileName);
        }

        public AudioInvalidException([CanBeNull] string message, [CanBeNull] FileInfo fileInfo)
            : base(message)
        {
            _fileInfo = fileInfo;
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
