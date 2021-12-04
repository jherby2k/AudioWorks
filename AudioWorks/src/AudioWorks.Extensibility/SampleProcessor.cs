/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Numerics;
using System.Runtime.InteropServices;
#if !NETSTANDARD2_0
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace AudioWorks.Extensibility
{
    static class SampleProcessor
    {
        internal static void Convert(ReadOnlySpan<float> source, Span<byte> destination, int bitsPerSample)
        {
            var adjustment = (int) GetAbsoluteQuantizationLevels(bitsPerSample);
            var max = adjustment - 1;

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var adjVector = new Vector<int>(adjustment);
                var maxVector = new Vector<int>(max);
                var srcVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                srcVectors = srcVectors.Slice(0, srcVectors.Length - srcVectors.Length % 4);
                var destVectors = MemoryMarshal.Cast<byte, Vector<byte>>(destination);

                for (int srcIndex = 0, destIndex = 0; srcIndex < srcVectors.Length; destIndex++)
                    destVectors[destIndex] = Vector.Narrow(
                        Vector.Narrow(
                            (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * adjustment),
                                                maxVector) - adjVector),
                            (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * adjustment),
                                                maxVector) - adjVector)),
                        Vector.Narrow(
                            (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * adjustment),
                                                maxVector) - adjVector),
                            (Vector<uint>) (Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * adjustment),
                                                maxVector) - adjVector)));

                for (var sampleIndex = srcVectors.Length * Vector<float>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = (byte) Math.Min(source[sampleIndex] * adjustment - adjustment, max);
            }
            else
            {
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (byte) Math.Min(source[sampleIndex] * adjustment - adjustment, max);
            }
        }

        internal static void Convert(ReadOnlySpan<float> source, Span<short> destination, int bitsPerSample)
        {
            var multiplier = (int) GetAbsoluteQuantizationLevels(bitsPerSample);
            var max = multiplier - 1;

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var maxVector = new Vector<int>(max);
                var srcVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                srcVectors = srcVectors.Slice(0, srcVectors.Length - srcVectors.Length % 2);
                var destVectors = MemoryMarshal.Cast<short, Vector<short>>(destination);

                for (int srcIndex = 0, destIndex = 0; srcIndex < srcVectors.Length; destIndex++)
                    destVectors[destIndex] = Vector.Narrow(
                        Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * multiplier), maxVector),
                        Vector.Min(Vector.ConvertToInt32(srcVectors[srcIndex++] * multiplier), maxVector));

                for (var sampleIndex = srcVectors.Length * Vector<float>.Count; sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = (short) Math.Min(source[sampleIndex] * multiplier, max);
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (short) Math.Min(source[sampleIndex] * multiplier, max);
        }

        internal static void Convert(ReadOnlySpan<float> source, Span<Int24> destination, int bitsPerSample)
        {
            var multiplier = GetAbsoluteQuantizationLevels(bitsPerSample);
            var max = multiplier - 1;

            for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                destination[sampleIndex] = new((int) Math.Min(source[sampleIndex] * multiplier, max));
        }

        internal static void Convert(ReadOnlySpan<float> source, Span<int> destination, int bitsPerSample)
        {
            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated && bitsPerSample < 32)
            {
                var multiplier = (int) GetAbsoluteQuantizationLevels(bitsPerSample);
                var max = multiplier - 1;
                var maxVector = new Vector<int>(max);
                var srcVectors = MemoryMarshal.Cast<float, Vector<float>>(source);
                var destVectors = MemoryMarshal.Cast<int, Vector<int>>(destination);

                for (var vectorIndex = 0; vectorIndex < srcVectors.Length; vectorIndex++)
                    destVectors[vectorIndex] =
                        Vector.Min(Vector.ConvertToInt32(srcVectors[vectorIndex] * multiplier), maxVector);

                for (var sampleIndex = srcVectors.Length * Vector<float>.Count; sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = (int) Math.Min(source[sampleIndex] * multiplier, max);
            }
            else
            {
                var multiplier = GetAbsoluteQuantizationLevels(bitsPerSample);
                var max = (int) multiplier - 1;

                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    try
                    {
                        destination[sampleIndex] = Math.Min(checked((int) (source[sampleIndex] * multiplier)), max);
                    }
                    catch (OverflowException)
                    {
                        // Clamp at 32 bitsPerSample and +1.0
                        destination[sampleIndex] = max;
                    }
            }
        }

        internal static void Convert(ReadOnlySpan<byte> source, Span<float> destination, int bitsPerSample)
        {
            var adjustment = (float) GetAbsoluteQuantizationLevels(bitsPerSample);
            var multiplier = 1 / adjustment;

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var adjustmentVector = new Vector<float>(adjustment);
                var srcVectors = MemoryMarshal.Cast<byte, Vector<byte>>(source);
                var destVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (int srcIndex = 0, destIndex = 0; srcIndex < srcVectors.Length; srcIndex++)
                {
                    Vector.Widen(srcVectors[srcIndex], out var shortVector1, out var shortVector2);
                    Vector.Widen(shortVector1, out var intVector1, out var intVector2);
                    Vector.Widen(shortVector2, out var intVector3, out var intVector4);

                    destVectors[destIndex++] = (Vector.ConvertToSingle(intVector1) - adjustmentVector) * multiplier;
                    destVectors[destIndex++] = (Vector.ConvertToSingle(intVector2) - adjustmentVector) * multiplier;
                    destVectors[destIndex++] = (Vector.ConvertToSingle(intVector3) - adjustmentVector) * multiplier;
                    destVectors[destIndex++] = (Vector.ConvertToSingle(intVector4) - adjustmentVector) * multiplier;
                }

                for (var sampleIndex = srcVectors.Length * Vector<byte>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = (source[sampleIndex] - adjustment) * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = (source[sampleIndex] - adjustment) * multiplier;
        }

        internal static void Convert(ReadOnlySpan<short> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) GetAbsoluteQuantizationLevels(bitsPerSample);

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var srcVectors = MemoryMarshal.Cast<short, Vector<short>>(source);
                var destVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (int srcIndex = 0, destIndex = 0; srcIndex < srcVectors.Length; srcIndex++)
                {
                    Vector.Widen(srcVectors[srcIndex], out var destVector1, out var destVector2);

                    destVectors[destIndex++] = Vector.ConvertToSingle(destVector1) * multiplier;
                    destVectors[destIndex++] = Vector.ConvertToSingle(destVector2) * multiplier;
                }

                for (var sampleIndex = srcVectors.Length * Vector<short>.Count;
                    sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        internal static void Convert(ReadOnlySpan<Int24> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) GetAbsoluteQuantizationLevels(bitsPerSample);

            for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        internal static void Convert(ReadOnlySpan<int> source, Span<float> destination, int bitsPerSample)
        {
            var multiplier = 1 / (float) GetAbsoluteQuantizationLevels(bitsPerSample);

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var srcVectors = MemoryMarshal.Cast<int, Vector<int>>(source);
                var destVectors = MemoryMarshal.Cast<float, Vector<float>>(destination);

                for (var vectorIndex = 0; vectorIndex < srcVectors.Length; vectorIndex++)
                    destVectors[vectorIndex] = Vector.ConvertToSingle(srcVectors[vectorIndex]) * multiplier;

                for (var sampleIndex = srcVectors.Length * Vector<int>.Count; sampleIndex < source.Length;
                    sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
            }
            else
                for (var sampleIndex = 0; sampleIndex < source.Length; sampleIndex++)
                    destination[sampleIndex] = source[sampleIndex] * multiplier;
        }

        internal static void Interleave(
            ReadOnlySpan<float> leftSource,
            ReadOnlySpan<float> rightSource,
            Span<float> destination)
        {
#if NETSTANDARD2_0
            for (int frameIndex = 0, destIndex = 0; frameIndex < leftSource.Length; frameIndex++)
            {
                destination[destIndex++] = leftSource[frameIndex];
                destination[destIndex++] = rightSource[frameIndex];
            }
#else
            // Optimization - Vectorized implementation is about 3x faster with AVX2
            if (Avx2.IsSupported)
            {
                var controlVector = Vector256.Create(0, 2, 1, 3, 4, 6, 5, 7);
                var leftSrcVectors = MemoryMarshal.Cast<float, Vector256<float>>(leftSource);
                var rightSrcVectors = MemoryMarshal.Cast<float, Vector256<float>>(rightSource);
                var destVectors = MemoryMarshal.Cast<float, Vector256<float>>(destination);

                var destVectorIndex = 0;
                for (var srcVectorIndex = 0; srcVectorIndex < leftSrcVectors.Length; srcVectorIndex++)
                {
                    // This is more complicated than it should be because AVX uses 128-bit lanes for shuffles
                    var shuffled1 = Avx.Shuffle(leftSrcVectors[srcVectorIndex], rightSrcVectors[srcVectorIndex],
                        0b01000100);
                    var shuffled2 = Avx.Shuffle(leftSrcVectors[srcVectorIndex], rightSrcVectors[srcVectorIndex],
                        0b11101110);
                    destVectors[destVectorIndex++] = Avx2.PermuteVar8x32(
                        Vector256.Create(shuffled1.GetLower(), shuffled2.GetLower()), controlVector);
                    destVectors[destVectorIndex++] = Avx2.PermuteVar8x32(
                        Vector256.Create(shuffled1.GetUpper(), shuffled2.GetUpper()), controlVector);
                }

                for (int frameIndex = leftSrcVectors.Length * Vector256<float>.Count,
                    destIndex = destVectorIndex * Vector256<float>.Count;
                    frameIndex < leftSource.Length;
                    frameIndex++)
                {
                    destination[destIndex++] = leftSource[frameIndex];
                    destination[destIndex++] = rightSource[frameIndex];
                }
            }
            else
            {
                for (int frameIndex = 0, destIndex = 0; frameIndex < leftSource.Length; frameIndex++)
                {
                    destination[destIndex++] = leftSource[frameIndex];
                    destination[destIndex++] = rightSource[frameIndex];
                }
            }
#endif
        }

        internal static void DeInterleave(
            ReadOnlySpan<float> source,
            Span<float> leftDestination,
            Span<float> rightDestination)
        {
#if NETSTANDARD2_0
            for (int destIndex = 0, srcIndex = 0; destIndex < leftDestination.Length; destIndex++)
            {
                leftDestination[destIndex] = source[srcIndex++];
                rightDestination[destIndex] = source[srcIndex++];
            }
#else
            // Optimization - Vectorized implementation is about 3x faster with AVX2
            if (Avx2.IsSupported)
            {
                var controlVector = Vector256.Create(0, 2, 4, 6, 1, 3, 5, 7);
                var srcVectors = MemoryMarshal.Cast<float, Vector256<float>>(source);
                var leftDestVectors = MemoryMarshal.Cast<float, Vector256<float>>(leftDestination);
                var rightDestVectors = MemoryMarshal.Cast<float, Vector256<float>>(rightDestination);

                var srcVectorIndex = 0;
                for (var destVectorIndex = 0; destVectorIndex < leftDestVectors.Length; destVectorIndex++)
                {
                    var permuted1 = Avx2.PermuteVar8x32(srcVectors[srcVectorIndex++], controlVector);
                    var permuted2 = Avx2.PermuteVar8x32(srcVectors[srcVectorIndex++], controlVector);
                    leftDestVectors[destVectorIndex] = Vector256.Create(permuted1.GetLower(), permuted2.GetLower());
                    rightDestVectors[destVectorIndex] = Vector256.Create(permuted1.GetUpper(), permuted2.GetUpper());
                }

                for (int destIndex = leftDestVectors.Length * Vector256<float>.Count,
                    srcIndex = srcVectorIndex * Vector256<float>.Count;
                    destIndex < leftDestination.Length;
                    destIndex++)
                {
                    leftDestination[destIndex] = source[srcIndex++];
                    rightDestination[destIndex] = source[srcIndex++];
                }
            }
            else
            {
                for (int destIndex = 0, sourceIndex = 0; destIndex < leftDestination.Length; destIndex++)
                {
                    leftDestination[destIndex] = source[sourceIndex++];
                    rightDestination[destIndex] = source[sourceIndex++];
                }
            }
#endif
        }

        static uint GetAbsoluteQuantizationLevels(int bitsPerSample) => 1u << (bitsPerSample - 1);
    }
}