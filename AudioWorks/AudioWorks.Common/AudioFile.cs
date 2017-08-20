using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Common
{
    [PublicAPI]
    public class AudioFile
    {
        public FileInfo FileInfo { get; }

        internal AudioFile(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
    }
}
