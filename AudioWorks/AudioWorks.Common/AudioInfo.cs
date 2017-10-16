using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    /// <summary>
    /// Contains information about the audio itself. Cannot be modified without re-encoding.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public sealed class AudioInfo
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AudioInfo"/> class describing a lossless audio file.
        /// </summary>
        /// <param name="description">The format description.</param>
        /// <param name="channels">The # of channels.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <param name="sampleRate">The sample rate (in samples per second).</param>
        /// <param name="sampleCount">The sample count, or 0 if unknown.</param>
        /// <returns>The instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="description"/> is null or empty.</exception>
        /// <exception cref="AudioInvalidException">Thrown if one or more parameters is out of valid range.</exception>
        /// <exception cref="AudioUnsupportedException">Thrown if one or more parameters is out of the range supported
        /// by AudioWorks.</exception>
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

        /// <summary>
        /// Creates a new instance of the <see cref="AudioInfo"/> class describing a lossy-compressed audio file.
        /// </summary>
        /// <param name="description">The format description.</param>
        /// <param name="channels">The # of channels.</param>
        /// <param name="sampleRate">The sample rate (in samples per second).</param>
        /// <param name="sampleCount">The sample count, or 0 if unknown.</param>
        /// <param name="bitRate">The bit rate (in bits per second).</param>
        /// <returns>The instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="description"/> is null or empty.</exception>
        /// <exception cref="AudioInvalidException">Thrown if one or more parameters is out of valid range.</exception>
        /// <exception cref="AudioUnsupportedException">Thrown if one or more parameters is out of the range supported
        /// by AudioWorks.</exception>
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

        /// <summary>
        /// Gets the format description.
        /// </summary>
        /// <value>
        /// The format description.
        /// </value>
        [NotNull]
        public string Description { get; }

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>
        /// The # of channels.
        /// </value>
        public int Channels { get; }

        /// <summary>
        /// Gets the # of bits per sample, or 0 for lossy files.
        /// </summary>
        /// <value>
        /// The # of bits per sample.
        /// </value>
        public int BitsPerSample { get; }

        /// <summary>
        /// Gets the sample rate (in samples per second).
        /// </summary>
        /// <value>
        /// The sample rate.
        /// </value>
        public int SampleRate { get; }

        /// <summary>
        /// Gets the sample count, or 0 if unknown.
        /// </summary>
        /// <value>
        /// The sample count.
        /// </value>
        public long SampleCount { get; }

        /// <summary>
        /// Gets the bit rate (in bits per second).
        /// </summary>
        /// <value>
        /// The bit rate.
        /// </value>
        public int BitRate { get; }

        /// <summary>
        /// Gets the play length, which is 0 if the sample count is unknown.
        /// </summary>
        /// <value>
        /// The play length.
        /// </value>
        public TimeSpan PlayLength =>
            SampleCount == 0
                ? TimeSpan.Zero
                : new TimeSpan(0, 0, (int) Math.Round(SampleCount / (double) SampleRate));

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
        }
    }
}
