using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Manages the creation and pooling of <see cref="SampleCollection"/> objects.
    /// </summary>
    public sealed class SampleCollectionPool
    {
        [NotNull] static readonly Lazy<SampleCollectionPool> _lazyInstance =
            new Lazy<SampleCollectionPool>(() => new SampleCollectionPool());

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        [NotNull]
        public static SampleCollectionPool Instance => _lazyInstance.Value;

        readonly ConcurrentDictionary<int, ConcurrentBag<WeakReference<float[]>>> _pooledArrayDictionary =
            new ConcurrentDictionary<int, ConcurrentBag<WeakReference<float[]>>>();

        SampleCollectionPool()
        {
        }

        /// <summary>
        /// Creates a new <see cref="SampleCollection"/>.
        /// </summary>
        /// <param name="channels">The # of channels.</param>
        /// <param name="frames">The frame count.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="channels"/> or
        /// <paramref name="frames"/> is out of range.</exception>
        [NotNull]
        public SampleCollection Create(int channels, int frames)
        {
            if (channels <= 0 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (frames < 0)
                throw new ArgumentOutOfRangeException(nameof(frames),
                    $"{nameof(frames)} must be 0 or greater.");

            var samples = new float[channels][];
            for (var i = 0; i < samples.Length; i++)
                samples[i] = CreateOrGetCachedArray(frames);

            return new SampleCollection(samples);
        }

        /// <summary>
        /// Returns the memory used by the <see cref="SampleCollection"/> to the pool, so they can be reallocated.
        /// </summary>
        /// <remarks>
        /// The <see cref="SampleCollection"/> should not be used again after calling this method, as its internal
        /// arrays may be owned by another <see cref="SampleCollection"/>, possibly on another thread.
        /// </remarks>
        /// <param name="samples">The <see cref="SampleCollection"/> to release.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="samples"/> is null.</exception>
        public void Release([NotNull] SampleCollection samples)
        {
            if (samples == null) throw new ArgumentNullException(nameof(samples));

            if (samples.Frames == 0)
                return;

            foreach (var channel in samples)
                _pooledArrayDictionary.AddOrUpdate(channel.Length, new ConcurrentBag<WeakReference<float[]>>(),
                    (i, bag) =>
                    {
                        bag.Add(new WeakReference<float[]>(channel));
                        return bag;
                    });
        }

        [NotNull]
        float[] CreateOrGetCachedArray(int sampleCount)
        {
            if (sampleCount == 0)
                return Array.Empty<float>();

            // Check the pool for arrays of the same length, discarding any that have been disposed already
            if (_pooledArrayDictionary.TryGetValue(sampleCount, out var cachedArrays))
                while (cachedArrays.TryTake(out var weakReference))
                    if (weakReference.TryGetTarget(out var target))
                        return target;

            // If no cached arrays are available, create a new one
            return new float[sampleCount];
        }
    }
}
