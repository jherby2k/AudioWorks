using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
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
        readonly bool _isInterleaved;

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>The # of channels.</value>
        public int Channels => _isInterleaved ? 2 : _buffers.Length;

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

            Convert(monoSamples, _buffers[0].Memory.Span, bitsPerSample);
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

            Convert(leftSamples, _buffers[0].Memory.Span, bitsPerSample);
            Convert(rightSamples, _buffers[1].Memory.Span, bitsPerSample);
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
            _buffers = new IMemoryOwner<float>[1];
            _buffers[0] = MemoryPool<float>.Shared.Rent(Frames * channels);
            if (channels == 2)
                _isInterleaved = true;

            Convert(interleavedSamples, _buffers[0].Memory.Span, bitsPerSample);
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
            _buffers = new IMemoryOwner<float>[1];
            _buffers[0] = MemoryPool<float>.Shared.Rent(Frames * channels);
            if (channels == 2)
                _isInterleaved = true;

            switch (bytesPerSample)
            {
                case 1:
                    Convert(interleavedSamples, _buffers[0].Memory.Span, bitsPerSample);
                    break;
                case 2:
                    var interleavedInt16Samples = MemoryMarshal.Cast<byte, short>(interleavedSamples);
                    Convert(interleavedInt16Samples, _buffers[0].Memory.Span, bitsPerSample);
                    break;
                case 3:
                    var interleavedInt24Samples = MemoryMarshal.Cast<byte, Int24>(interleavedSamples);
                    Convert(interleavedInt24Samples, _buffers[0].Memory.Span, bitsPerSample);
                    break;
                case 4:
                    var interleavedInt32Samples = MemoryMarshal.Cast<byte, int>(interleavedSamples);
                    Convert(interleavedInt32Samples, _buffers[0].Memory.Span, bitsPerSample);
                    break;
            }
        }

        /// <summary>
        /// Copies the single channel of audio samples in normalized floating-point format.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized within -1.0 and 1.0.
        /// </remarks>
        /// <param name="monoDestination">The destination.</param>
        /// <exception cref="InvalidOperationException">Thrown if the Channels property does not equal 1.</exception>
        public void CopyTo(Span<float> monoDestination)
        {
            if (Channels != 1)
                throw new InvalidOperationException("Not a single-channel SampleBuffer.");

            _buffers[0].Memory.Span.Slice(0, Frames).CopyTo(monoDestination);
        }

        /// <summary>
        /// Copies both channels of audio samples in normalized floating-point format.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized within -1.0 and 1.0.
        /// </remarks>
        /// <param name="leftDestination">The destination for the left channel.</param>
        /// <param name="rightDestination">The destination for the right channel.</param>
        /// <exception cref="InvalidOperationException">Thrown if the Channels property does not equal 2.</exception>
        public void CopyTo(Span<float> leftDestination, Span<float> rightDestination)
        {
            if (Channels != 2)
                throw new InvalidOperationException("Not a 2-channel SampleBuffer.");

            if (_isInterleaved)
                DeInterleave(_buffers[0].Memory.Span.Slice(0, Frames * 2), leftDestination, rightDestination);
            else
            {
                _buffers[0].Memory.Span.Slice(0, Frames).CopyTo(leftDestination);
                _buffers[1].Memory.Span.Slice(0, Frames).CopyTo(rightDestination);
            }
        }

        /// <summary>
        /// Copies the interleaved channels of audio samples, in normalized floating-point format.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized within -1.0 and 1.0. Stereo samples are interleaved,
        /// beginning with the left channel.
        /// </remarks>
        /// <param name="destination">The destination.</param>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        public void CopyToInterleaved(Span<float> destination)
        {
            if (destination.Length < Frames * Channels)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));

            if (Channels == 1 || _isInterleaved)
                _buffers[0].Memory.Span.Slice(0, Frames * Channels).CopyTo(destination);
            else
                Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), destination);
        }

        /// <summary>
        /// Copies the interleaved channels of audio samples, in integer format.
        /// </summary>
        /// <remarks>
        /// The samples are signed and right-justified. Stereo samples are interleaved, beginning with the left
        /// channel.
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

            if (Channels == 1 || _isInterleaved)
                Convert(_buffers[0].Memory.Span.Slice(0, Frames * Channels), destination, bitsPerSample);
            else
            {
                Span<float> interleavedSamples = stackalloc float[Frames * 2];
                Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), interleavedSamples);
                Convert(interleavedSamples, destination, bitsPerSample);
            }
        }

        /// <summary>
        /// Copies the interleaved channels of audio samples, in packed integer format.
        /// </summary>
        /// <remarks>
        /// The samples are stored as little-endian integers, aligned at the byte boundary. If
        /// <paramref name="bitsPerSample"/> is 8 or less, they are unsigned. Otherwise, they are signed. Stereo
        /// samples are interleaved, beginning with the left channel.
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

            switch (bytesPerSample)
            {
                case 1:
                    if (Channels == 1 || _isInterleaved)
                        Convert(_buffers[0].Memory.Span.Slice(0, Frames * Channels), destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedSamples = stackalloc float[Frames * 2];
                        Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), interleavedSamples);
                        Convert(interleavedSamples, destination, bitsPerSample);
                    }
                    break;
                case 2:
                    var int16Destination = MemoryMarshal.Cast<byte, short>(destination);
                    if (Channels == 1 || _isInterleaved)
                        Convert(_buffers[0].Memory.Span.Slice(0, Frames * Channels), int16Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedSamples = stackalloc float[Frames * 2];
                        Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), interleavedSamples);
                        Convert(interleavedSamples, int16Destination, bitsPerSample);
                    }
                    break;
                case 3:
                    var int24Destination = MemoryMarshal.Cast<byte, Int24>(destination);
                    if (Channels == 1 || _isInterleaved)
                        Convert(_buffers[0].Memory.Span.Slice(0, Frames * Channels), int24Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedSamples = stackalloc float[Frames * 2];
                        Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), interleavedSamples);
                        Convert(interleavedSamples, int24Destination, bitsPerSample);
                    }
                    break;
                case 4:
                    var int32Destination = MemoryMarshal.Cast<byte, int>(destination);
                    if (Channels == 1 || _isInterleaved)
                        Convert(_buffers[0].Memory.Span.Slice(0, Frames * Channels), int32Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedSamples = stackalloc float[Frames * 2];
                        Interleave(_buffers[0].Memory.Span.Slice(0, Frames), _buffers[1].Memory.Span.Slice(0, Frames), interleavedSamples);
                        Convert(interleavedSamples, int32Destination, bitsPerSample);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<float> source, Span<byte> destination, int bitsPerSample)
        {
            var multiplier = (int) Math.Pow(2, bitsPerSample - 1);
            var max = multiplier - 1;

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var maxVector = new Vector<int>(max);
                var adjustmentVector = new Vector<int>(multiplier);
                var sourceVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                sourceVectors = sourceVectors.Slice(0, sourceVectors.Length - sourceVectors.Length % 4);
                var destinationVectors = MemoryMarshal.Cast<byte, Vector<byte>>(destination);

                for (int sourceIndex = 0, destinationIndex = 0; sourceIndex < sourceVectors.Length; destinationIndex++)
                {
                    var intVector1 = (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector) - adjustmentVector);
                    var intVector2 = (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector) - adjustmentVector);
                    var intVector3 = (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector) - adjustmentVector);
                    var intVector4 = (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector) - adjustmentVector);
                    var shortVector1 = Vector.Narrow(intVector1, intVector2);
                    var shortVector2 = Vector.Narrow(intVector3, intVector4);
                    destinationVectors[destinationIndex] = Vector.Narrow(shortVector1, shortVector2);
                }

                for (var sampleIndex = sourceVectors.Length * Vector<float>.Count; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (byte) Math.Min(source[sampleIndex] * multiplier - multiplier, max);
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (byte) Math.Min(source[sampleIndex] * multiplier - multiplier, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<float> source, Span<short> destination, int bitsPerSample)
        {
            var multiplier = (int) Math.Pow(2, bitsPerSample - 1);
            var max = multiplier - 1;

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var maxVector = new Vector<int>(max);
                var sourceVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                sourceVectors = sourceVectors.Slice(0, sourceVectors.Length - sourceVectors.Length % 2);
                var destinationVectors = MemoryMarshal.Cast<short, Vector<short>>(destination);

                for (int sourceIndex = 0, destinationIndex = 0; sourceIndex < sourceVectors.Length; destinationIndex++)
                {
                    var intVector1 = Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector);
                    var intVector2 = Vector.Min(Vector.ConvertToInt32(sourceVectors[sourceIndex++] * multiplier), maxVector);
                    destinationVectors[destinationIndex] = Vector.Narrow(intVector1, intVector2);
                }

                for (var sampleIndex = sourceVectors.Length * Vector<float>.Count; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (short) Math.Min(source[sampleIndex] * multiplier, max);
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (short) Math.Min(source[sampleIndex] * multiplier, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<float> source, Span<Int24> destination, int bitsPerSample)
        {
            var multiplier = (int) Math.Pow(2, bitsPerSample - 1);
            var max = multiplier - 1;

            for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                destination[sampleIndex] = new Int24((int) Math.Min(source[sampleIndex] * multiplier, max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<float> source, Span<int> destination, int bitsPerSample)
        {
            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated && bitsPerSample < 32)
            {
                var multiplier = (int) Math.Pow(2, bitsPerSample - 1);
                var max = multiplier - 1;
                var maxVector = new Vector<int>(max);
                var sourceVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                var destinationVectors = MemoryMarshal.Cast<int, Vector<int>>(destination);

                for (var vectorIndex = 0; vectorIndex < sourceVectors.Length; vectorIndex++)
                    destinationVectors[vectorIndex] = Vector.Min(Vector.ConvertToInt32(sourceVectors[vectorIndex] * multiplier), maxVector);

                for (var sampleIndex = sourceVectors.Length * Vector<float>.Count; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (short) Math.Min(source[sampleIndex] * multiplier, max);
            }
            else
            {
                // At 32 bits per sample, multiplier > int.MaxValue
                var multiplier = (uint) Math.Pow(2, bitsPerSample - 1);
                var max = (int) multiplier - 1;

                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    try
                    {
                        destination[sampleIndex] = Math.Min(checked((int) (source[sampleIndex] * multiplier)), max);
                    }
                    catch (OverflowException)
                    {
                        // Can occur at 32 bitsPerSample and +1.0
                        destination[sampleIndex] = max;
                    }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<byte> source, Span<float> destination, int bitsPerSample)
        {
            var adjustment = (float) Math.Pow(2, bitsPerSample - 1);
            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var adjustmentVector = new Vector<float>(adjustment);
                var sourceVectors = MemoryMarshal.Cast<byte, Vector<byte>>(source);
                var destinationVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (int sourceIndex = 0, destinationIndex = 0; sourceIndex < sourceVectors.Length; sourceIndex++)
                {
                    Vector.Widen(sourceVectors[sourceIndex], out var shortVector1, out var shortVector2);
                    Vector.Widen(shortVector1, out var intVector1, out var intVector2);
                    Vector.Widen(shortVector2, out var intVector3, out var intVector4);
                    destinationVectors[destinationIndex++] =
                        (Vector.ConvertToSingle(intVector1) - adjustmentVector) * multiplier;
                    destinationVectors[destinationIndex++] =
                        (Vector.ConvertToSingle(intVector2) - adjustmentVector) * multiplier;
                    destinationVectors[destinationIndex++] =
                        (Vector.ConvertToSingle(intVector3) - adjustmentVector) * multiplier;
                    destinationVectors[destinationIndex++] =
                        (Vector.ConvertToSingle(intVector4) - adjustmentVector) * multiplier;
                }

                for (var sampleIndex = sourceVectors.Length * Vector<byte>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = (source[sampleIndex] - adjustment) * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (source[sampleIndex] - adjustment) * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<short> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var sourceVectors = MemoryMarshal.Cast<short, Vector<short>>(source);
                var destinationVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (int sourceIndex = 0, destinationIndex = 0; sourceIndex < sourceVectors.Length; sourceIndex++)
                {
                    Vector.Widen(sourceVectors[sourceIndex], out var destinationVector1, out var destinationVector2);
                    destinationVectors[destinationIndex++] = Vector.ConvertToSingle(destinationVector1) * multiplier;
                    destinationVectors[destinationIndex++] = Vector.ConvertToSingle(destinationVector2) * multiplier;
                }

                for (var sampleIndex = sourceVectors.Length * Vector<short>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<Int24> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Convert(ReadOnlySpan<int> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) Math.Pow(2, bitsPerSample - 1);

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var sourceVectors = MemoryMarshal.Cast<int, Vector<int>>(source);
                var destinationVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (var vectorIndex = 0; vectorIndex < sourceVectors.Length; vectorIndex++)
                    destinationVectors[vectorIndex] = Vector.ConvertToSingle(sourceVectors[vectorIndex]) * multiplier;

                for (var sampleIndex = sourceVectors.Length * Vector<int>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void Interleave(ReadOnlySpan<float> leftSource, ReadOnlySpan<float> rightSource, Span<float> destination)
        {
            // Optimization - Unsafe implementation is faster
            fixed (float* destinationAddress = &MemoryMarshal.GetReference(destination))
            {
                var destinationPointer = destinationAddress;

                for (var frameIndex = 0; frameIndex < leftSource.Length; frameIndex++)
                {
                    *destinationPointer++ = leftSource[frameIndex];
                    *destinationPointer++ = rightSource[frameIndex];
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void DeInterleave(ReadOnlySpan<float> source, Span<float> leftDestination, Span<float> rightDestination)
        {
            // Optimization - Unsafe implementation is faster
            fixed (float* sourceAddress = &MemoryMarshal.GetReference(source))
            {
                var sourcePointer = sourceAddress;

                for (var frameIndex = 0; frameIndex < source.Length / 2; frameIndex++)
                {
                    leftDestination[frameIndex] = *sourcePointer++;
                    rightDestination[frameIndex] = *sourcePointer++;
                }
            }
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