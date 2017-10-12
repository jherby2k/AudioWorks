using JetBrains.Annotations;

namespace AudioWorks.Common
{
    [PublicAPI]
    public interface ITaggedAudioFile : IAudioFile
    {
        [NotNull]
        AudioMetadata Metadata { get; set; }

        void SaveMetadata([CanBeNull] SettingDictionary settings = null);
    }
}