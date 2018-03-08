using System;
using System.Buffers;
using System.Buffers.Binary;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a collection of audio samples.
    /// </summary>
    public sealed class SampleCollection
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

        public SampleCollection(int channels, int frames)
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

        public Span<float> GetChannel(int channel) => _samples[channel].AsSpan().Slice(0, Frames);

        public void Return()
        {
            foreach (var channel in _samples)
                _pool.Return(channel);
        }

        public void CopyToInterleaved(Span<float> destination)
        {
            var index = 0;
            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    destination[index++] = channel[frameIndex];
        }

        public void CopyToInterleaved(Span<int> destination, int bitsPerSample)
        {
            var multiplier = (float) Math.Pow(2, bitsPerSample - 1);

            var index = 0;
            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    destination[index++] = (int) Math.Round(channel[frameIndex] * multiplier);
        }

        public void CopyToPcm(Span<byte> destination, int bitsPerSample)
        {
            var multiplier = (float)Math.Pow(2, bitsPerSample - 1);

            if (bitsPerSample <= 0)
            {
                var index = 0;
                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                        destination[index++] = (byte) Math.Round(channel[frameIndex] * multiplier + 128);
            }
            else
            {
                var bytesPerSample = (int) Math.Ceiling(bitsPerSample / (double) 8);

                var index = 0;
                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                    foreach (var channel in _samples)
                    {
                        BinaryPrimitives.WriteInt32LittleEndian(destination.Slice(index, 4),
                            (int) Math.Round(channel[frameIndex] * multiplier));
                        index += bytesPerSample;
                    }
            }
        }

        public void CopyFromInterlaced(Span<int> source)
        {
            var index = 0;
            for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
            foreach (var channel in _samples)
                channel[frameIndex] = source[index++] / (float) 0x8000_0000;
        }

        public void CopyFromPcm(Span<byte> source, int bitsPerSample)
        {
            // 1-8 bit samples are unsigned:
            if (bitsPerSample <= 8)
            {
                var index = 0;
                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                    channel[frameIndex] = (source[index++] - 128) / (float) 128;
            }
            else
            {
                var bytesPerSample = (int) Math.Ceiling(bitsPerSample / (double) 8);
                Span<byte> temp = stackalloc byte[4];

                var index = 0;
                for (var frameIndex = 0; frameIndex < Frames; frameIndex++)
                foreach (var channel in _samples)
                {
                    source.Slice(index + 4 - bytesPerSample, bytesPerSample).CopyTo(temp);
                    BinaryPrimitives.ReadInt32LittleEndian(temp);
                    channel[frameIndex] =
                        BinaryPrimitives.ReadInt32LittleEndian(temp) / (float) 0x8000_0000;
                }
            }
        }
    }
}