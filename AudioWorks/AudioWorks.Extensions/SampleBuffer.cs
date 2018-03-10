using System;
using System.Buffers;
using System.Buffers.Binary;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a block of audio samples.
    /// </summary>
    public sealed class SampleBuffer
    {
        [NotNull] static readonly ArrayPool<float> _pool = ArrayPool<float>.Create();

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class.
        /// </summary>
        /// <param name="channels">The # of channels.</param>
        /// <param name="frames">The # of frames.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw in <paramref name="channels"/> or
        /// <paramref name="frames"/> is out of range.</exception>
        public SampleBuffer(int channels, int frames)
        {
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException(nameof(frames),
                    $"{nameof(frames)} must be 0 or greater.");

            Frames = frames;

            _samples = new float[channels][];
            for (var i = 0; i < _samples.Length; i++)
                _samples[i] = _pool.Rent(frames);
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

            var index = 0;

            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    destination[index++] = channel[frameIndex];
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

            var multiplier = (float) Math.Pow(2, bitsPerSample - 1);
            var index = 0;

            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    destination[index++] = (int) Math.Round(channel[frameIndex] * multiplier);
        }

        /// <summary>
        /// Copies the samples to an interleaved and packed array of integer samples.
        /// </summary>
        /// <remarks>
        /// The samples are stored as little-endian integers, aligned at the byte boundary. If
        /// <paramref name="bitsPerSample"/> is 8 or less, they are unsigned. Otherwise,
        /// they are signed.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyToInterleaved(Span<byte> destination, int bitsPerSample)
        {
            var bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);

            if (destination.Length < Frames * Channels * bytesPerSample)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            var multiplier = (float) Math.Pow(2, bitsPerSample - 1);
            var index = 0;

            // 1-8 bit samples are unsigned
            if (bitsPerSample <= 8)
            {
                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                        destination[index++] = (byte) Math.Round(channel[frameIndex] * multiplier + 128);
            }
            else
            {
                Span<byte> buffer = stackalloc byte[4];

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                    {
                        BinaryPrimitives.WriteInt32LittleEndian(buffer,
                            (int) Math.Round(channel[frameIndex] * multiplier));
                        buffer.Slice(0, bytesPerSample).CopyTo(destination.Slice(index));
                        index += bytesPerSample;
                    }
            }
        }

        /// <summary>
        /// Populates this object with copies of the samples in <paramref name="source"/>.
        /// </summary>
        /// <param name="channel">The channel index.</param>
        /// <param name="source">The source samples.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException"><paramref name="source"/> does not contain enough samples to fill the
        /// <see cref="SampleBuffer"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="channel"/> and/or
        /// <paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyFrom(int channel, ReadOnlySpan<int> source, int bitsPerSample)
        {
            if (channel < 0 || channel > 1)
                throw new ArgumentOutOfRangeException(nameof(channel), "channel is out of range.");
            if (source.Length < Frames)
                throw new ArgumentException("source does not contain enough samples.", nameof(source));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            var divisor = (float) Math.Pow(2, bitsPerSample - 1);

            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                _samples[channel][frameIndex] = source[frameIndex] / divisor;
        }

        /// <summary>
        /// Populates this object with copies of the interleaved samples in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The interleaved source samples.</param>
        /// <exception cref="ArgumentException"><paramref name="source"/> does not contain enough samples to fill the
        /// <see cref="SampleBuffer"/>.</exception>
        public void CopyFromInterleaved(ReadOnlySpan<int> source)
        {
            if (source.Length < Frames * Channels)
                throw new ArgumentException("source does not contain enough samples.", nameof(source));

            var index = 0;

            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    channel[frameIndex] = source[index++] / (float) 0x8000_0000;
        }

        /// <summary>
        /// Populates this object with copies of the interleaved samples in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The interleaved source samples.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ArgumentException"><paramref name="source"/> does not contain enough samples to fill the
        /// <see cref="SampleBuffer"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyFromInterleaved(ReadOnlySpan<byte> source, int bitsPerSample)
        {
            var bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);

            if (source.Length < Frames * Channels * bytesPerSample)
                throw new ArgumentException("source does not contain enough samples.", nameof(source));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            var index = 0;

            // 1-8 bit samples are unsigned
            if (bytesPerSample == 1)
            {
                var divisor = (float) Math.Pow(2, bitsPerSample - 1);

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                        channel[frameIndex] = (source[index++] - 128) / divisor;
            }
            else
            {
                Span<byte> buffer = stackalloc byte[4];

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                    {
                        source.Slice(index, bytesPerSample).CopyTo(buffer.Slice(4 - bytesPerSample));
                        BinaryPrimitives.ReadInt32LittleEndian(buffer);
                        channel[frameIndex] =
                            BinaryPrimitives.ReadInt32LittleEndian(buffer) / (float) 0x8000_0000;
                        index += bytesPerSample;
                    }
            }
        }

        /// <summary>
        /// Returns this <see cref="SampleBuffer"/>'s internal storage to the array pool.
        /// </summary>
        public void ReturnToPool()
        {
            foreach (var channel in _samples)
                _pool.Return(channel);
        }
    }
}