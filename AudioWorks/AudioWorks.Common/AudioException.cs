using JetBrains.Annotations;
using System;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public class AudioException : Exception
    {
        public AudioException()
        {
        }

        public AudioException([CanBeNull] string message)
            : base(message)
        {
        }

        public AudioException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        protected AudioException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
