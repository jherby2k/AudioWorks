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

        public TimeSpan PlayLength =>
            SampleCount == 0
                ? TimeSpan.Zero
                : new TimeSpan(0, 0, (int) Math.Round(SampleCount / (double) SampleRate));

        public AudioInfo(
            [NotNull] string description, 
            int channels, 
            int bitsPerSample, 
            int sampleRate,
            long sampleCount)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description), "The description cannot be null or empty.");
            if (channels < 1)
                throw new AudioInvalidException($"{channels} is not a valid channel count.");
            if (channels > 2)
                throw new AudioUnsupportedException($"{channels} is not a supported channel count.");
            if (bitsPerSample < 0)
                throw new AudioInvalidException($"{bitsPerSample} is not a valid # of bits per sample.");
            if (bitsPerSample > 32)
                throw new AudioUnsupportedException($"{bitsPerSample} is not a supported # of bits per sample.");
            if (sampleRate < 1)
                throw new AudioInvalidException($"{sampleRate} is not a valid sample rate.");
            if (sampleCount < 0)
                throw new AudioInvalidException($"{sampleCount} is not a valid sample count.");

            Description = description;
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;
            SampleCount = sampleCount;
        }
    }
}
