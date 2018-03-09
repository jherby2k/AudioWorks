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
        static readonly ArrayPool<float> _pool = ArrayPool<float>.Create();

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
            if (channels <= 0 || channels > 2)
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
        public ReadOnlySpan<float> GetSamples(int channel) => _samples[channel].AsReadOnlySpan().Slice(0, Frames);

        /// <summary>
        /// Copies the samples to an interleaved array.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized to between -1.0 and 1.0.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        public void CopyToInterleaved(Span<float> destination)
        {
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
        public void CopyToInterleaved(Span<int> destination, int bitsPerSample)
        {
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
        public void CopyToInterleaved(Span<byte> destination, int bitsPerSample)
        {
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
                var bytesPerSample = (int)Math.Ceiling(bitsPerSample / 8.0);
                Span<byte> temp = stackalloc byte[4];

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                    {
                        BinaryPrimitives.WriteInt32LittleEndian(temp,
                            (int)Math.Round(channel[frameIndex] * multiplier));
                        temp.Slice(0, bytesPerSample).CopyTo(destination.Slice(index));
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
        public void CopyFrom(int channel, Span<int> source, int bitsPerSample)
        {
            var divisor = (float) Math.Pow(2, bitsPerSample - 1);
            var samples = _samples[channel];

            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                samples[frameIndex] = source[frameIndex] / divisor;
        }

        /// <summary>
        /// Populates this object with copies of the interleaved samples in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The interleaved source samples.</param>
        public void CopyFromInterleaved(Span<int> source)
        {
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
        public void CopyFromInterleaved(Span<byte> source, int bitsPerSample)
        {
            // 1-8 bit samples are unsigned
            if (bitsPerSample <= 8)
            {
                var divisor = (float) Math.Pow(2, bitsPerSample - 1);
                var index = 0;

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                        channel[frameIndex] = (source[index++] - 128) / divisor;
            }
            else
            {
                var bytesPerSample = (int) Math.Ceiling(bitsPerSample / (double) 8);
                Span<byte> temp = stackalloc byte[4];
                var index = 0;

                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                    {
                        source.Slice(index, bytesPerSample).CopyTo(temp.Slice(4 - bytesPerSample));
                        BinaryPrimitives.ReadInt32LittleEndian(temp);
                        channel[frameIndex] =
                            BinaryPrimitives.ReadInt32LittleEndian(temp) / (float) 0x8000_0000;
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