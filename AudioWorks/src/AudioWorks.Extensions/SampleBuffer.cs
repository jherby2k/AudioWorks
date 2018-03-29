﻿using System;
using System.Buffers;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a block of audio samples.
    /// </summary>
    public sealed class SampleBuffer
    {
        /// <summary>
        /// Gets a <see cref="SampleBuffer"/> with 0 frames.
        /// </summary>
        /// <value>An empty <see cref="SampleBuffer"/>.</value>
        public static SampleBuffer Empty { get; } = new SampleBuffer();

        [NotNull, ItemNotNull] readonly float[][] _samples;

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>The # of channels.</value>
        public int Channels => _samples.Length;

        /// <summary>
        /// Gets the frame count.
        /// </summary>
        /// <value>The frame count.</value>
        public int Frames { get; }

        SampleBuffer()
        {
            _samples = Array.Empty<float[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class for a single channel, using integer
        /// samples.
        /// </summary>
        /// <param name="monoSamples">The samples.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="bitsPerSample"/> is out of range.
        /// </exception>
        public SampleBuffer(ReadOnlySpan<int> monoSamples, int bitsPerSample)
        {
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample),
                    $"{nameof(bitsPerSample)} is out of range.");

            _samples = new float[1][];
            Frames = monoSamples.Length;

            var multiplier = 1 / Math.Pow(2, bitsPerSample - 1);

            _samples[0] = ArrayPool<float>.Shared.Rent(Frames);
            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                _samples[0][frameIndex] = (float) (monoSamples[frameIndex] * multiplier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class for 2 channels, using integer samples.
        /// </summary>
        /// <param name="leftSamples">The left channel samples.</param>
        /// <param name="rightSamples">The right channel samples.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="rightSamples"/> has a different length than
        /// <paramref name="leftSamples"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="bitsPerSample"/> is out of range.
        /// </exception>
        public SampleBuffer(ReadOnlySpan<int> leftSamples, ReadOnlySpan<int> rightSamples, int bitsPerSample)
        {
            if (leftSamples.Length != rightSamples.Length)
                throw new ArgumentException(
                    $"{nameof(rightSamples)} does not match the length of {nameof(leftSamples)}", nameof(rightSamples));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample),
                    $"{nameof(bitsPerSample)} is out of range.");

            _samples = new float[2][];
            Frames = leftSamples.Length;

            var multiplier = 1 / Math.Pow(2, bitsPerSample - 1);

            _samples[0] = ArrayPool<float>.Shared.Rent(Frames);
            _samples[1] = ArrayPool<float>.Shared.Rent(Frames);
            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
            {
                _samples[0][frameIndex] = (float) (leftSamples[frameIndex] * multiplier);
                _samples[1][frameIndex] = (float) (rightSamples[frameIndex] * multiplier);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class using interleaved integer samples.
        /// </summary>
        /// <param name="interleavedSamples">The interleaved samples.</param>
        /// <param name="channels">The channels.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="interleavedSamples"/> is not a multiple of
        /// channels.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="channels"/> or
        /// <paramref name="bitsPerSample"/> is out of range.</exception>
        public SampleBuffer(ReadOnlySpan<int> interleavedSamples, int channels, int bitsPerSample)
        {
            if (interleavedSamples.Length % channels != 0)
                throw new ArgumentException($"{nameof(interleavedSamples)} has an invalid length.",
                    nameof(interleavedSamples));
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample),
                    $"{nameof(bitsPerSample)} is out of range.");

            _samples = new float[channels][];
            Frames = interleavedSamples.Length / channels;

            var multiplier = 1 / Math.Pow(2, bitsPerSample - 1);

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channel = _samples[channelIndex] = ArrayPool<float>.Shared.Rent(Frames);
                for (int frameIndex = 0, offset = channelIndex; frameIndex < Frames; frameIndex++, offset += Channels)
                    channel[frameIndex] = (float) (interleavedSamples[offset] * multiplier);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class using interleaved integer samples, which
        /// are packed on the byte boundary according to <paramref name="bitsPerSample"/>.
        /// </summary>
        /// <param name="interleavedSamples">The interleaved samples.</param>
        /// <param name="channels">The # of channels.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="interleavedSamples"/> is not a valid length.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="channels"/> or
        /// <paramref name="bitsPerSample"/> is out of range.</exception>
        public SampleBuffer(ReadOnlySpan<byte> interleavedSamples, int channels, int bitsPerSample)
        {
            var bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);
            var multiplier = 1 / Math.Pow(2, bitsPerSample - 1);

            if (interleavedSamples.Length % (channels * bytesPerSample) != 0)
                throw new ArgumentException($"{nameof(interleavedSamples)} has an invalid length.",
                    nameof(interleavedSamples));
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample),
                    $"{nameof(bitsPerSample)} is out of range.");

            _samples = new float[channels][];
            Frames = interleavedSamples.Length / channels / bytesPerSample;

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channel = _samples[channelIndex] = ArrayPool<float>.Shared.Rent(Frames);
                for (int frameIndex = 0, offset = channelIndex * bytesPerSample;
                    frameIndex < Frames;
                    frameIndex++, offset += Channels * bytesPerSample)
                    channel[frameIndex] =
                        (float) (ReadPackedInt(interleavedSamples.Slice(offset, bytesPerSample)) * multiplier);
            }
        }

        /// <summary>
        /// Gets the samples for a single channel of audio
        /// </summary>
        /// <param name="channel">The channel index.</param>
        /// <returns>The samples.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="channel"/> is out of range.</exception>
        public ReadOnlySpan<float> GetSamples(int channel)
        {
            if (channel < 0 || channel > 1)
                throw new ArgumentOutOfRangeException(nameof(channel), "channel is out of range.");

            return _samples[channel].AsReadOnlySpan().Slice(0, Frames);
        }

        /// <summary>
        /// Copies the samples to an interleaved array.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized to between -1.0 and 1.0.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        public void CopyToInterleaved(Span<float> destination)
        {
            if (destination.Length < Frames * Channels)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channel = _samples[channelIndex];
                for (int frameIndex = 0, offset = channelIndex; frameIndex < Frames; frameIndex++, offset += Channels)
                    destination[offset] = channel[frameIndex];
            }
        }

        /// <summary>
        /// Copies the samples to an interleaved array of integer samples.
        /// </summary>
        /// <remarks>
        /// The samples are signed and right-justified.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyToInterleaved(Span<int> destination, int bitsPerSample)
        {
            if (destination.Length < Frames * Channels)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            var multiplier = (long) Math.Pow(2, bitsPerSample - 1);
            var max = multiplier - 1;

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channel = _samples[channelIndex];
                for (int frameIndex = 0, offset = channelIndex; frameIndex < Frames; frameIndex++, offset += Channels)
                    destination[offset] = (int) Math.Min(channel[frameIndex] * multiplier, max);
            }
        }

        /// <summary>
        /// Copies the samples to an interleaved and packed array of integer samples.
        /// </summary>
        /// <remarks>
        /// The samples are stored as little-endian integers, aligned at the byte boundary. If
        /// <paramref name="bitsPerSample"/> is 8 or less, they are unsigned. Otherwise, they are signed.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyToInterleaved(Span<byte> destination, int bitsPerSample)
        {
            var bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);
            var multiplier = (long) Math.Pow(2, bitsPerSample - 1);
            var max = multiplier - 1;

            if (destination.Length < Frames * Channels * bytesPerSample)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channel = _samples[channelIndex];
                for (int frameIndex = 0, offset = channelIndex * bytesPerSample;
                    frameIndex < Frames;
                    frameIndex++, offset += Channels * bytesPerSample)
                    WritePackedInt(destination.Slice(offset, bytesPerSample), (int) Math.Min(channel[frameIndex] * multiplier, max));
            }
        }

        /// <summary>
        /// Returns this <see cref="SampleBuffer"/>'s internal storage to the array pool.
        /// </summary>
        public void ReturnToPool()
        {
            foreach (var channel in _samples)
                ArrayPool<float>.Shared.Return(channel);
        }

        [Pure]
        static int ReadPackedInt(ReadOnlySpan<byte> buffer)
        {
            switch (buffer.Length)
            {
                case 1:
                    return buffer[0] - 128;
                case 2:
                    return buffer[0] | ((sbyte) buffer[1] << 8);
                case 3:
                    return buffer[0] | buffer[1] << 8 | ((sbyte) buffer[2] << 16);
                default:
                    return buffer[0] | buffer[1] << 8 | buffer[2] << 16 | ((sbyte) buffer[3] << 24);
            }
        }

        static void WritePackedInt(Span<byte> buffer, int value)
        {
            switch (buffer.Length)
            {
                default:
                    buffer[3] = (byte) ((uint) value >> 24);
                    goto case 3;
                case 3:
                    buffer[2] = (byte) (((uint) value >> 16) & 0xFF);
                    goto case 2;
                case 2:
                    buffer[1] = (byte) (((uint) value >> 8) & 0xFF);
                    buffer[0] = (byte) value;
                    return;
                case 1:
                    buffer[0] = (byte) (value + 128);
                    return;
            }
        }
    }
}