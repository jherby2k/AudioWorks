using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class AudioInfo
    {
        [NotNull]
        public static AudioInfo CreateForLossless(
            [NotNull] string description,
            int channels,
            int bitsPerSample,
            int sampleRate,
            long sampleCount = 0)
        {
            if (bitsPerSample < 1)
                throw new AudioInvalidException($"{bitsPerSample} is not a valid # of bits per sample.");
            if (bitsPerSample > 32)
                throw new AudioUnsupportedException($"{bitsPerSample} is not a supported # of bits per sample.");

            return new AudioInfo(
                description,
                channels,
                bitsPerSample,
                sampleRate,
                sampleCount,
                channels * bitsPerSample * sampleRate);
        }

        [NotNull]
        public static AudioInfo CreateForLossy(
            [NotNull] string description,
            int channels,
            int sampleRate,
            long sampleCount = 0,
            int bitRate = 0)
        {
            if (bitRate < 0)
                throw new AudioInvalidException($"{bitRate} is not a valid bitrate.");

            return new AudioInfo(
                description,
                channels,
                0,
                sampleRate,
                sampleCount,
                bitRate);
        }

        [NotNull]
        public string Description { get; }

        public int Channels { get; }

        public int BitsPerSample { get; }

        public int SampleRate { get; }

        public long SampleCount { get; }

        public int BitRate { get; }

        public TimeSpan PlayLength { get; }

        AudioInfo(
            [NotNull] string description,
            int channels,
            int bitsPerSample,
            int sampleRate,
            long sampleCount,
            int bitRate)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description), "The description cannot be null or empty.");
            if (channels < 1)
                throw new AudioInvalidException($"{channels} is not a valid channel count.");
            if (channels > 2)
                throw new AudioUnsupportedException($"{channels} is not a supported channel count.");
            if (sampleRate < 1)
                throw new AudioInvalidException($"{sampleRate} is not a valid sample rate.");
            if (sampleCount < 0)
                throw new AudioInvalidException($"{sampleCount} is not a valid sample count.");

            Description = description;
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;
            SampleCount = sampleCount;
            BitRate = bitRate;
            PlayLength = SampleCount == 0
                ? TimeSpan.Zero
                : new TimeSpan(0, 0, (int) Math.Round(SampleCount / (double) SampleRate));
        }
    }
}
