using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a collection of audio samples.
    /// </summary>
    public sealed class SampleCollection : IEnumerable<float[]>
    {
        /// <summary>
        /// Gets the maximum # of frames that can be stored in a single <see cref="SampleCollection"/>.
        /// </summary>
        /// <value>
        /// The maximum # of frames.
        /// </value>
        public static int MaxFrames { get; } = 4096;

        [NotNull, ItemNotNull] readonly float[][] _samples;

        /// <summary>
        /// Gets the samples for the specified channel #.
        /// </summary>
        /// <param name="channel">The channel #.</param>
        /// <returns>The samples.</returns>
        [NotNull, CollectionAccess(CollectionAccessType.Read)]
        public float[] this[int channel] => _samples[channel];

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>
        /// The # of channels.
        /// </value>
        [CollectionAccess(CollectionAccessType.None)]
        public int Channels => _samples.Length;

        /// <summary>
        /// Gets the frame count.
        /// </summary>
        /// <value>
        /// The frame count.
        /// </value>
        [CollectionAccess(CollectionAccessType.None)]
        public int Frames => _samples[0].Length;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleCollection"/> class.
        /// </summary>
        /// <param name="channels">The # of channels.</param>
        /// <param name="frames">The frame count.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="channels"/> or
        /// <paramref name="frames"/> is out of range.</exception>
        public SampleCollection(int channels, int frames)
        {
            if (channels <= 0 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (frames <= 0 || frames > MaxFrames)
                throw new ArgumentOutOfRangeException(nameof(frames),
                    $"{nameof(frames)} must be between 1 and {MaxFrames}.");

            _samples = new float[channels][];
            for (var i = 0; i < _samples.Length; i++)
                _samples[i] = new float[frames];
        }

        /// <inheritdoc/>
        [NotNull, CollectionAccess(CollectionAccessType.Read)]
        public IEnumerator<float[]> GetEnumerator()
        {
            return ((IEnumerable<float[]>)_samples).GetEnumerator();
        }

        /// <inheritdoc/>
        [NotNull, CollectionAccess(CollectionAccessType.Read)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _samples.GetEnumerator();
        }
    }
}