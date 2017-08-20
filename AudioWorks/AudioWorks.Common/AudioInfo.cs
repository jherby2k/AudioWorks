using JetBrains.Annotations;

namespace AudioWorks.Common
{
    [PublicAPI]
    public class AudioInfo
    {
        [NotNull]
        public string Description { get; }

        public int Channels { get; }

        public int BitsPerSample { get; }

        public AudioInfo([NotNull] string description, int channels, int bitsPerSample)
        {
            Description = description;
            Channels = channels;
            BitsPerSample = bitsPerSample;
        }
    }
}
