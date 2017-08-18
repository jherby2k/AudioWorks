using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public interface IAudioInfoDecoder
    {
        AudioInfo ReadAudioInfo([NotNull] FileStream stream);
    }
}