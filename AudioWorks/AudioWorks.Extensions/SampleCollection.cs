using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a collection of audio samples.
    /// </summary>
    /// <remarks>
    /// New <see cref="SampleCollection"/> instances should be created using <see cref="SampleCollectionPool.Create"/>.
    /// </remarks>
    public sealed class SampleCollection : IEnumerable<float[]>
    {
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

        internal SampleCollection([NotNull] float[][] samples)
        {
            _samples = samples;
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