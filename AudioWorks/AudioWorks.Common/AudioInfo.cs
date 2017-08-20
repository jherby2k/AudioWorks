using JetBrains.Annotations;
using System;

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

        public TimeSpan PlayLength { get; }

        public AudioInfo(
            [NotNull] string description, 
            int channels, 
            int bitsPerSample, 
            int sampleRate,
            long sampleCount)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description), "Value cannot be null or empty.");
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels), channels, "Value should be 1 or 2.");
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), bitsPerSample,
                    "Value should be between 1 and 32.");
            if (sampleRate < 1)
                throw new ArgumentOutOfRangeException(nameof(sampleRate), sampleRate, "Value should be 1 or greater.");
            if (sampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), sampleCount, "Value cannot be negative.");

            Description = description;
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;
            SampleCount = sampleCount;
            if (sampleCount == 0)
                PlayLength = new TimeSpan(0, 0, (int) Math.Round(sampleCount / (double) sampleRate));
        }
    }
}
