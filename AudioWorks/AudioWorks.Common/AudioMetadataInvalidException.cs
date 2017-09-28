using JetBrains.Annotations;
using System;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public sealed class AudioMetadataInvalidException : AudioException
    {
        public AudioMetadataInvalidException()
        {
        }

        public AudioMetadataInvalidException([CanBeNull] string message)
            : base(message)
        {
        }

        public AudioMetadataInvalidException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        AudioMetadataInvalidException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
