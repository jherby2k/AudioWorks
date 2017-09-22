using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public interface IAudioMetadataEncoder
    {
        void WriteMetadata([NotNull] FileStream stream, [NotNull] AudioMetadata metadata);
    }
}
