using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public interface IAudioMetadataDecoder
    {
        [NotNull]
        AudioMetadata ReadMetadata([NotNull] FileStream stream);
    }
}
