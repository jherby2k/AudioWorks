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

        public int SampleRate { get; }

        public long SampleCount { get; }

        public AudioInfo([NotNull] string description, int channels, int bitsPerSample, int sampleRate, long sampleCount)
        {
            Description = description;
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;
            SampleCount = sampleCount;
        }
    }
}
