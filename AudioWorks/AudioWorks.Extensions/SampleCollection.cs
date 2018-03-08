using System;
using System.Buffers;
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
    }
}