using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Common
{
    [PublicAPI]
    public interface IAudioFile
    {
        [NotNull]
        AudioInfo AudioInfo { get; }

        [NotNull]
        FileInfo FileInfo { get; }
    }
}