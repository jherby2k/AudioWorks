using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class AudioFile
    {
        [NotNull] Func<FileInfo, AudioMetadata> _getMetadataFunc;
        AudioMetadata _metadata;

        [NotNull]
        public FileInfo FileInfo { get; }

        [NotNull]
        public AudioInfo AudioInfo { get; }

        [NotNull]
        public AudioMetadata Metadata => _metadata ?? (_metadata = _getMetadataFunc(FileInfo));

        internal AudioFile(
            [NotNull] FileInfo fileInfo,
            [NotNull] AudioInfo audioInfo,
            [NotNull] Func<FileInfo, AudioMetadata> getMetadataFunc)
        {
            FileInfo = fileInfo;
            AudioInfo = audioInfo;
            _getMetadataFunc = getMetadataFunc;
        }
    }
}
