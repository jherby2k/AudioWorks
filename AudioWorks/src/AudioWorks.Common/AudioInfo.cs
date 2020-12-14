/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;

namespace AudioWorks.Common
{
    /// <summary>
    /// Contains information about the audio itself, which cannot be modified without re-encoding.
    /// </summary>
    [Serializable]
    public sealed class AudioInfo
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AudioInfo"/> class describing a lossless audio file.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="channels">The # of channels.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <param name="sampleRate">The sample rate (in samples per second).</param>
        /// <param name="frameCount">The frame count, or 0 if unknown.</param>
        /// <returns>The instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="format"/> is null or empty.</exception>
        /// <exception cref="AudioInvalidException">Thrown if one or more parameters is out of valid range.</exception>
        /// <exception cref="AudioUnsupportedException">Thrown if one or more parameters is out of the range supported
        /// by AudioWorks.</exception>
        public static AudioInfo CreateForLossless(
            string format,
            int channels,
            int bitsPerSample,
            int sampleRate,
            long frameCount = 0)
        {
            if (bitsPerSample == 0)
                throw new AudioInvalidException($"{bitsPerSample} cannot be 0 for a lossless file.");

            return new(
                format,
                channels,
                bitsPerSample,
                sampleRate,
                channels * bitsPerSample * sampleRate,
                frameCount);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AudioInfo"/> class describing a lossy-compressed audio file.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="channels">The # of channels.</param>
        /// <param name="sampleRate">The sample rate (in samples per second).</param>
        /// <param name="frameCount">The frame count, or 0 if unknown.</param>
        /// <param name="bitRate">The bit rate (in bits per second).</param>
        /// <returns>The instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="format"/> is null or empty.</exception>
        /// <exception cref="AudioInvalidException">Thrown if one or more parameters is out of valid range.</exception>
        /// <exception cref="AudioUnsupportedException">Thrown if one or more parameters is out of the range supported
        /// by AudioWorks.</exception>
        public static AudioInfo CreateForLossy(
            string format,
            int channels,
            int sampleRate,
            long frameCount = 0,
            int bitRate = 0) =>
            new(
                format,
                channels,
                0,
                sampleRate,
                bitRate,
                frameCount);

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        public string Format { get; }

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>The # of channels.</value>
        public int Channels { get; }

        /// <summary>
        /// Gets the # of bits per sample, or 0 for lossy files.
        /// </summary>
        /// <value>The # of bits per sample.</value>
        public int BitsPerSample { get; }

        /// <summary>
        /// Gets the sample rate (in samples per second).
        /// </summary>
        /// <value>The sample rate.</value>
        public int SampleRate { get; }

        /// <summary>
        /// Gets the bit rate (in bits per second).
        /// </summary>
        /// <value>The bit rate.</value>
        public int BitRate { get; }

        /// <summary>
        /// Gets the frame count, or 0 if unknown.
        /// </summary>
        /// <value>The frame count.</value>
        public long FrameCount { get; }

        /// <summary>
        /// Gets the play length, which is 0 if the frame count is unknown.
        /// </summary>
        /// <value>The play length.</value>
        public TimeSpan PlayLength =>
            FrameCount == 0
                ? TimeSpan.Zero
                : new(0, 0, (int) Math.Round(FrameCount / (double) SampleRate));

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInfo"/> class. You should normally use the
        /// <see cref="CreateForLossy"/> and <see cref="CreateForLossless"/> static helper methods for creating new
        /// instances.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="channels"></param>
        /// <param name="bitsPerSample"></param>
        /// <param name="sampleRate"></param>
        /// <param name="bitRate"></param>
        /// <param name="frameCount"></param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="format"/> is null or empty.</exception>
        /// <exception cref="AudioInvalidException">Thrown if one or more parameters is out of valid range.</exception>
        /// <exception cref="AudioUnsupportedException">Thrown if one or more parameters is out of the range supported
        /// by AudioWorks.</exception>
        public AudioInfo(
            string format,
            int channels,
            int bitsPerSample,
            int sampleRate,
            int bitRate,
            long frameCount)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException(nameof(format), "The format cannot be null or empty.");
            switch (channels)
            {
                case < 1:
                    throw new AudioInvalidException($"{channels} is not a valid channel count.");
                case > 2:
                    throw new AudioUnsupportedException($"{channels} is not a supported channel count.");
            }
            switch (bitsPerSample)
            {
                case < 0:
                    throw new AudioInvalidException($"{bitsPerSample} is not a valid # of bits per sample.");
                case > 32:
                    throw new AudioUnsupportedException($"{bitsPerSample} is not a supported # of bits per sample.");
            }
            if (sampleRate < 1)
                throw new AudioInvalidException($"{sampleRate} is not a valid sample rate.");
            if (bitRate < 0)
                throw new AudioInvalidException($"{bitRate} is not a valid bitrate.");
            if (frameCount < 0)
                throw new AudioInvalidException($"{frameCount} is not a valid frame count.");

            Format = format;
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;
            BitRate = bitRate;
            FrameCount = frameCount;
        }
    }
}
