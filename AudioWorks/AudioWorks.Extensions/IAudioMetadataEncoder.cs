using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public interface IAudioMetadataEncoder
    {
        [NotNull]
        SettingInfoDictionary SettingInfo { get; }

        void WriteMetadata([NotNull] FileStream stream, [NotNull] AudioMetadata metadata, [NotNull] SettingDictionary settings);
    }
}
