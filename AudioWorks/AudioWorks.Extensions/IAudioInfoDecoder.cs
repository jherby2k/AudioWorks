using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public interface IAudioInfoDecoder
    {
        [NotNull]
        AudioInfo ReadAudioInfo([NotNull] FileStream stream);
    }
}