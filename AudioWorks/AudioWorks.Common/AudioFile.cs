using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Common
{
    [PublicAPI]
    public class AudioFile
    {
        [NotNull]
        public FileInfo FileInfo { get; }

        [NotNull]
        public AudioInfo AudioInfo { get; }

        internal AudioFile([NotNull] FileInfo fileInfo, [NotNull] AudioInfo audioInfo)
        {
            FileInfo = fileInfo;
            AudioInfo = audioInfo;
        }
    }
}
