using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class AudioFile
    {
        [NotNull] Func<FileInfo, AudioMetadata> _getMetadataFunc;
        [NotNull] Action<AudioMetadata, FileInfo> _saveMetadataAction;
        AudioMetadata _metadata;

        [NotNull]
        public FileInfo FileInfo { get; }

        [NotNull]
        public AudioInfo AudioInfo { get; }

        [NotNull]
        public AudioMetadata Metadata
        {
            get => _metadata ?? (_metadata = _getMetadataFunc(FileInfo));
            set => _metadata = value;
        }

        internal AudioFile(
            [NotNull] FileInfo fileInfo,
            [NotNull] AudioInfo audioInfo,
            [NotNull] Func<FileInfo, AudioMetadata> getMetadataFunc,
            [NotNull] Action<AudioMetadata, FileInfo> saveMetadataAction)
        {
            FileInfo = fileInfo;
            AudioInfo = audioInfo;
            _getMetadataFunc = getMetadataFunc;
            _saveMetadataAction = saveMetadataAction;
        }

        public void SaveMetadata()
        {
            _saveMetadataAction(_metadata, FileInfo);
        }
    }
}
