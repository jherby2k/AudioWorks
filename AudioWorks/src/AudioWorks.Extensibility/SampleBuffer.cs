/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
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

        [CanBeNull] readonly IMemoryOwner<float> _buffer;
        bool _isDisposed;

        /// <summary>
        /// Gets a value indicating whether the samples are interleaved internally.
        /// </summary>
        /// <remarks>
        /// Always returns <c>false</c> when <see cref="Channels"/> equals 1.
        /// </remarks>
        /// <value><c>true</c> if the samples are interleaved; otherwise, <c>false</c>.</value>
        public bool IsInterleaved { get; }

        /// <summary>
        /// Gets the # of channels.
        /// </summary>
        /// <value>The # of channels.</value>
        public int Channels { get; }

        /// <summary>
        /// Gets the frame count.
        /// </summary>
        /// <value>The frame count.</value>
        public int Frames { get; }

        SampleBuffer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class using interleaved floating-point
        /// samples.
        /// </summary>
        /// <param name="interleavedSamples">The interleaved samples.</param>
        /// <param name="channels">The channels.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="interleavedSamples"/> is not a multiple of
        /// channels.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="channels"/> is out of range.
        /// </exception>
        public SampleBuffer(ReadOnlySpan<float> interleavedSamples, int channels)
        {
            if (interleavedSamples.Length % channels != 0)
                throw new ArgumentException($"{nameof(interleavedSamples)} has an invalid length.",
                    nameof(interleavedSamples));
            if (channels < 1 || channels > 2)
                throw new ArgumentOutOfRangeException(nameof(channels),
                    $"{nameof(channels)} must be 1 or 2.");

            Channels = channels;
            Frames = interleavedSamples.Length / channels;
            _buffer = MemoryPool<float>.Shared.Rent(Frames * channels);
            if (channels > 1)
                IsInterleaved = true;

            // ReSharper disable once PossibleNullReferenceException
            interleavedSamples.CopyTo(_buffer.Memory.Span);
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

            Channels = 1;
            Frames = monoSamples.Length;
            _buffer = MemoryPool<float>.Shared.Rent(Frames);

            // ReSharper disable once PossibleNullReferenceException
            Convert(monoSamples, _buffer.Memory.Span, bitsPerSample);
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

            Channels = 2;
            Frames = leftSamples.Length;
            _buffer = MemoryPool<float>.Shared.Rent(Frames * 2);

            // ReSharper disable once PossibleNullReferenceException
            Convert(leftSamples, _buffer.Memory.Span.Slice(0, Frames), bitsPerSample);
            Convert(rightSamples, _buffer.Memory.Span.Slice(Frames), bitsPerSample);
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

            Channels = channels;
            Frames = interleavedSamples.Length / channels;
            _buffer = MemoryPool<float>.Shared.Rent(Frames * channels);
            if (channels > 1)
                IsInterleaved = true;

            // ReSharper disable once PossibleNullReferenceException
            Convert(interleavedSamples, _buffer.Memory.Span, bitsPerSample);
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

            Channels = channels;
            Frames = interleavedSamples.Length / channels / bytesPerSample;
            _buffer = MemoryPool<float>.Shared.Rent(Frames * channels);
            if (channels > 1)
                IsInterleaved = true;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (bytesPerSample)
            {
                case 1:
                    // ReSharper disable once PossibleNullReferenceException
                    Convert(interleavedSamples, _buffer.Memory.Span, bitsPerSample);
                    break;
                case 2:
                    var interleavedInt16Samples = MemoryMarshal.Cast<byte, short>(interleavedSamples);
                    // ReSharper disable once PossibleNullReferenceException
                    Convert(interleavedInt16Samples, _buffer.Memory.Span, bitsPerSample);
                    break;
                case 3:
                    var interleavedInt24Samples = MemoryMarshal.Cast<byte, Int24>(interleavedSamples);
                    // ReSharper disable once PossibleNullReferenceException
                    Convert(interleavedInt24Samples, _buffer.Memory.Span, bitsPerSample);
                    break;
                case 4:
                    var interleavedInt32Samples = MemoryMarshal.Cast<byte, int>(interleavedSamples);
                    // ReSharper disable once PossibleNullReferenceException
                    Convert(interleavedInt32Samples, _buffer.Memory.Span, bitsPerSample);
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
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the Channels property does not equal 1.</exception>
        public void CopyTo(Span<float> monoDestination)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            if (Channels != 1)
                throw new InvalidOperationException("Not a single-channel SampleBuffer.");

            _buffer.Memory.Span.Slice(0, Frames).CopyTo(monoDestination);
        }

        /// <summary>
        /// Copies both channels of audio samples in normalized floating-point format.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized within -1.0 and 1.0.
        /// </remarks>
        /// <param name="leftDestination">The destination for the left channel.</param>
        /// <param name="rightDestination">The destination for the right channel.</param>
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the Channels property does not equal 2.</exception>
        public void CopyTo(Span<float> leftDestination, Span<float> rightDestination)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            if (Channels != 2)
                throw new InvalidOperationException("Not a 2-channel SampleBuffer.");

            if (IsInterleaved)
                DeInterleave(_buffer.Memory.Span.Slice(0, Frames * 2), leftDestination, rightDestination);
            else
            {
                _buffer.Memory.Span.Slice(0, Frames).CopyTo(leftDestination);
                _buffer.Memory.Span.Slice(Frames, Frames).CopyTo(rightDestination);
            }
        }

        /// <summary>
        /// Copies both channels of audio samples in integer format.
        /// </summary>
        /// <remarks>
        /// The samples are floating-point values normalized within -1.0 and 1.0.
        /// </remarks>
        /// <param name="leftDestination">The destination for the left channel.</param>
        /// <param name="rightDestination">The destination for the right channel.</param>
        /// <param name="bitsPerSample">The # of bits per sample.</param>
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the Channels property does not equal 2.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyTo(Span<int> leftDestination, Span<int> rightDestination, int bitsPerSample)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            if (Channels != 2)
                throw new InvalidOperationException("Not a 2-channel SampleBuffer.");

            if (IsInterleaved)
            {
                Span<float> leftBuffer = stackalloc float[Frames];
                Span<float> rightBuffer = stackalloc float[Frames];
                DeInterleave(_buffer.Memory.Span.Slice(0, Frames * 2), leftBuffer, rightBuffer);
                Convert(leftBuffer, leftDestination, bitsPerSample);
                Convert(rightBuffer, rightDestination, bitsPerSample);
            }
            else
            {
                Convert(_buffer.Memory.Span.Slice(0, Frames), leftDestination, bitsPerSample);
                Convert(_buffer.Memory.Span.Slice(Frames, Frames), rightDestination, bitsPerSample);
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
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        public void CopyToInterleaved(Span<float> destination)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            if (destination.Length < Frames * Channels)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));

            if (Channels == 1 || IsInterleaved)
                _buffer.Memory.Span.Slice(0, Frames * Channels).CopyTo(destination);
            else
                Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), destination);
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
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyToInterleaved(Span<int> destination, int bitsPerSample)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            if (destination.Length < Frames * Channels)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            if (Channels == 1 || IsInterleaved)
                Convert(_buffer.Memory.Span.Slice(0, Frames * Channels), destination, bitsPerSample);
            else
            {
                Span<float> interleavedBuffer = stackalloc float[Frames * 2];
                Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), interleavedBuffer);
                Convert(interleavedBuffer, destination, bitsPerSample);
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
        /// <exception cref="ObjectDisposedException">This <see cref="SampleBuffer"/> has been disposed.</exception>
        /// <exception cref="ArgumentException"><paramref name="destination"/> is not long enough to store the samples.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitsPerSample"/> is out of range.</exception>
        public void CopyToInterleaved(Span<byte> destination, int bitsPerSample)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().ToString());
            if (_buffer == null) return;

            var bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);

            if (destination.Length < Frames * Channels * bytesPerSample)
                throw new ArgumentException("destination is not long enough to store the samples.",
                    nameof(destination));
            if (bitsPerSample < 1 || bitsPerSample > 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "bitsPerSample is out of range.");

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (bytesPerSample)
            {
                case 1:
                    if (Channels == 1 || IsInterleaved)
                        Convert(_buffer.Memory.Span.Slice(0, Frames * Channels), destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedBuffer = stackalloc float[Frames * 2];
                        Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), interleavedBuffer);
                        Convert(interleavedBuffer, destination, bitsPerSample);
                    }
                    break;
                case 2:
                    var int16Destination = MemoryMarshal.Cast<byte, short>(destination);
                    if (Channels == 1 || IsInterleaved)
                        Convert(_buffer.Memory.Span.Slice(0, Frames * Channels), int16Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedBuffer = stackalloc float[Frames * 2];
                        Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), interleavedBuffer);
                        Convert(interleavedBuffer, int16Destination, bitsPerSample);
                    }
                    break;
                case 3:
                    var int24Destination = MemoryMarshal.Cast<byte, Int24>(destination);
                    if (Channels == 1 || IsInterleaved)
                        Convert(_buffer.Memory.Span.Slice(0, Frames * Channels), int24Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedBuffer = stackalloc float[Frames * 2];
                        Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), interleavedBuffer);
                        Convert(interleavedBuffer, int24Destination, bitsPerSample);
                    }
                    break;
                case 4:
                    var int32Destination = MemoryMarshal.Cast<byte, int>(destination);
                    if (Channels == 1 || IsInterleaved)
                        Convert(_buffer.Memory.Span.Slice(0, Frames * Channels), int32Destination, bitsPerSample);
                    else
                    {
                        Span<float> interleavedBuffer = stackalloc float[Frames * 2];
                        Interleave(_buffer.Memory.Span.Slice(0, Frames), _buffer.Memory.Span.Slice(Frames, Frames), interleavedBuffer);
                        Convert(interleavedBuffer, int32Destination, bitsPerSample);
                    }
                    break;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_isDisposed) return;

            _buffer?.Dispose();
            _isDisposed = true;
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
                destination[sampleIndex] = new Int24(Math.Min(source[sampleIndex] * multiplier, max));
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
            fixed (float* destinationAddress = destination)
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
            fixed (float* sourceAddress = source)
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

            public Int24(float value)
            {
                _byte1 = (byte) value;
                _byte2 = (byte) (((uint) value >> 8) & 0xFF);
                _byte3 = (byte) (((uint) value >> 16) & 0xFF);
            }

            public static implicit operator int(Int24 value) =>
                value._byte1 | value._byte2 << 8 | ((sbyte) value._byte3 << 16);
        }
    }
}