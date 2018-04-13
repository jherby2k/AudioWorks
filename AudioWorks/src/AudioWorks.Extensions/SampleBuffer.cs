using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Represents a block of audio samples.
    /// </summary>
    public sealed class SampleBuffer : IDisposable
    {
        /// <summary>
        /// Gets a <see cref="SampleBuffer"/> with 0 frames.
        /// </summary>
        /// <value>An empty <see cref="SampleBuffer"/>.</value>
        public static SampleBuffer Empty { get; } = new SampleBuffer();

        [NotNull, ItemNotNull] readonly IMemoryOwner<float>[] _buffers;

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>The # of channels.</value>
        public int Channels => _buffers.Length;

        /// <summary>
        /// Gets the frame count.
        /// </summary>
        /// <value>The frame count.</value>
        public int Frames { get; }

        SampleBuffer()
        {
            _buffers = Array.Empty<IMemoryOwner<float>>();
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

            Frames = monoSamples.Length;
            _buffers = new IMemoryOwner<float>[1];
            _buffers[0] = MemoryPool<float>.Shared.Rent(Frames);

            ConvertToFloat(monoSamples, _buffers[0].Memory.Span.Slice(0, Frames), bitsPerSample);
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

            Frames = leftSamples.Length;
            _buffers = new IMemoryOwner<float>[2];
            _buffers[0] = MemoryPool<float>.Shared.Rent(Frames);
            _buffers[1] = MemoryPool<float>.Shared.Rent(Frames);

            ConvertToFloat(leftSamples, _buffers[0].Memory.Span.Slice(0, Frames), bitsPerSample);
            ConvertToFloat(rightSamples, _buffers[1].Memory.Span.Slice(0, Frames), bitsPerSample);
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

            Frames = interleavedSamples.Length / channels;
            _buffers = new IMemoryOwner<float>[channels];

            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                _buffers[channelIndex] = MemoryPool<float>.Shared.Rent(Frames);
                var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                for (int frameIndex = 0, offset = channelIndex;
                    frameIndex < channelBuffer.Length;
                    frameIndex++, offset += Channels)
                    channelBuffer[frameIndex] = interleavedSamples[offset] * multiplier;
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
            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            if (interleavedSamples.Length % (channels * bytesPerSample) != 0)
                throw new ArgumentException($"{nameof(interleavedSamples)} has an invalid length.",
                    nameof(interleavedSamples));
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample),
                    $"{nameof(bitsPerSample)} is out of range.");

            Frames = interleavedSamples.Length / channels / bytesPerSample;
            _buffers = new IMemoryOwner<float>[channels];
            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                _buffers[channelIndex] = MemoryPool<float>.Shared.Rent(Frames);

            switch (bytesPerSample)
            {
                case 1:
                    var adjustment = (float) Math.Pow(2, bitsPerSample - 1);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            channelBuffer[frameIndex] = (interleavedSamples[offset] - adjustment) * multiplier;
                    }
                    break;

                // Doing a non-portable cast is much faster than parsing the bytes manually
                case 2:
                    var shortSamples = MemoryMarshal.Cast<byte, short>(interleavedSamples);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            channelBuffer[frameIndex] = shortSamples[offset] * multiplier;
                    }
                    break;
                case 3:
                    var int24Samples = MemoryMarshal.Cast<byte, Int24>(interleavedSamples);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            channelBuffer[frameIndex] = int24Samples[offset] * multiplier;
                    }
                    break;
                case 4:
                    var intSamples = MemoryMarshal.Cast<byte, int>(interleavedSamples);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            channelBuffer[frameIndex] = intSamples[offset] * multiplier;
                    }
                    break;
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

            return _buffers[channel].Memory.Span.Slice(0, Frames);
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
                var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                for (int frameIndex = 0, offset = channelIndex;
                    frameIndex < channelBuffer.Length;
                    frameIndex++, offset += Channels)
                    destination[offset] = channelBuffer[frameIndex];
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

            var multiplier = (uint) Math.Pow(2, bitsPerSample - 1);
            var max = (int) (multiplier - 1);

            for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
            {
                var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                for (int frameIndex = 0, offset = channelIndex;
                    frameIndex < channelBuffer.Length;
                    frameIndex++, offset += Channels)
                    try
                    {
                        destination[offset] = Math.Min(checked((int) (channelBuffer[frameIndex] * multiplier)), max);
                    }
                    catch (OverflowException)
                    {
                        // Can occur at 32 bitsPerSample and +1.0
                        destination[offset] = max;
                    }
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
            var multiplier = (uint) Math.Pow(2, bitsPerSample - 1);
            var max = (int) (multiplier - 1);

            if (destination.Length < Frames * Channels * bytesPerSample)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            switch (bytesPerSample)
            {
                case 1:
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            destination[offset] = (byte) (Math.Min(channelBuffer[frameIndex] * multiplier, max) - multiplier);
                    }
                    break;

                // Doing a non-portable cast is much faster than parsing the bytes manually
                case 2:
                    var shortDestination = MemoryMarshal.Cast<byte, short>(destination);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            shortDestination[offset] = (short) Math.Min(channelBuffer[frameIndex] * multiplier, max);
                    }
                    break;
                case 3:
                    var int24Destination = MemoryMarshal.Cast<byte, Int24>(destination);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            int24Destination[offset] = new Int24(Math.Min((int) (channelBuffer[frameIndex] * multiplier), max));
                    }
                    break;
                case 4:
                    var intDestination = MemoryMarshal.Cast<byte, int>(destination);
                    for (var channelIndex = 0; channelIndex < Channels; channelIndex++)
                    {
                        var channelBuffer = _buffers[channelIndex].Memory.Span.Slice(0, Frames);
                        for (int frameIndex = 0, offset = channelIndex;
                            frameIndex < channelBuffer.Length;
                            frameIndex++, offset += Channels)
                            try
                            {
                                intDestination[offset] = Math.Min(checked((int) (channelBuffer[frameIndex] * multiplier)), max);
                            }
                            catch (OverflowException)
                            {
                                // Can occur at 32 bitsPerSample and +1.0
                                intDestination[offset] = max;
                            }
                    }
                    break;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var buffer in _buffers)
                buffer.Dispose();
        }

        static void ConvertToFloat(ReadOnlySpan<int> source, Span<float> destination, int bitDepth)
        {
            var multiplier = 1 / (float)Math.Pow(2, bitDepth - 1);

            // Vectorized code is 40-50% faster with AVX2
            if (Vector.IsHardwareAccelerated)
            {
                var sourceVectors = MemoryMarshal.Cast<int, Vector<int>>(source);
                var destinationVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (var vectorIndex = 0; vectorIndex < sourceVectors.Length; vectorIndex++)
                    destinationVectors[vectorIndex] = Vector.ConvertToSingle(sourceVectors[vectorIndex]) * multiplier;

                for (var frameIndex = sourceVectors.Length * Vector<int>.Count; frameIndex < source.Length; frameIndex++)
                    destination[frameIndex] = source[frameIndex] * multiplier;
            }
            else
                for (var frameIndex = 0; frameIndex < source.Length; frameIndex++)
                    destination[frameIndex] = source[frameIndex] * multiplier;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Int24
        {
            readonly byte _byte1;
            readonly byte _byte2;
            readonly byte _byte3;

            internal Int24(int value)
            {
                _byte1 = (byte) value;
                _byte2 = (byte) (((uint) value >> 8) & 0xFF);
                _byte3 = (byte) (((uint) value >> 16) & 0xFF);
            }

            public static implicit operator int(Int24 int24) =>
                int24._byte1 | int24._byte2 << 8 | ((sbyte) int24._byte3 << 16);
        }
    }
}