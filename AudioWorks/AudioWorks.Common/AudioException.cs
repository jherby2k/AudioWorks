using JetBrains.Annotations;
using System;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    [PublicAPI]
    [Serializable]
    public abstract class AudioException : Exception
    {
        protected AudioException()
        {
        }

        protected AudioException([CanBeNull] string message)
            : base(message)
        {
        }

        protected AudioException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }

        protected AudioException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
