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
#if !NETCOREAPP2_1
using System.Buffers;
#endif
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    sealed class Encoder : IDisposable
    {
        [NotNull] readonly EncoderHandle _handle = SafeNativeMethods.Init();
        [NotNull] readonly Stream _stream;
        long _startPosition;

        internal Encoder([NotNull] Stream stream)
        {
            _stream = stream;
        }

        internal void SetChannels(int channels)
        {
            SafeNativeMethods.SetNumChannels(_handle, channels);
        }

        internal void SetSampleRate(int sampleRate)
        {
            SafeNativeMethods.SetInSampleRate(_handle, sampleRate);
        }

        internal void SetSampleCount(uint sampleCount)
        {
            SafeNativeMethods.SetNumSamples(_handle, sampleCount);
        }

        internal void SetBitRate(int bitRate)
        {
            SafeNativeMethods.SetBRate(_handle, bitRate);
        }

        internal void SetVbrMode(VbrMode mode)
        {
            SafeNativeMethods.SetVbr(_handle, mode);
        }

        internal void SetVbrMeanBitRate(int bitRate)
        {
            SafeNativeMethods.SetVbrMeanBitRateKbps(_handle, bitRate);
        }

        internal void SetVbrQuality(float quality)
        {
            SafeNativeMethods.SetVbrQuality(_handle, quality);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void InitializeParameters()
        {
            _startPosition = _stream.Position;
            SafeNativeMethods.InitParams(_handle);
        }

        internal void Encode(ReadOnlySpan<float> leftSamples, ReadOnlySpan<float> rightSamples)
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[(int) Math.Ceiling(1.25 * leftSamples.Length) + 7200];

            var bytesEncoded = SafeNativeMethods.EncodeBufferIeeeFloat(
                _handle,
                MemoryMarshal.GetReference(leftSamples),
                MemoryMarshal.GetReference(rightSamples),
                leftSamples.Length,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);

            if (bytesEncoded < 0)
                throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

            _stream.Write(buffer.Slice(0, bytesEncoded));
#else
            var buffer = ArrayPool<byte>.Shared.Rent((int) Math.Ceiling(1.25 * leftSamples.Length) + 7200);
            try
            {
                var bytesEncoded = SafeNativeMethods.EncodeBufferIeeeFloat(
                    _handle,
                    MemoryMarshal.GetReference(leftSamples),
                    MemoryMarshal.GetReference(rightSamples),
                    leftSamples.Length,
                    buffer,
                    buffer.Length);

                if (bytesEncoded < 0)
                    throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

                _stream.Write(buffer, 0, bytesEncoded);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        internal void EncodeInterleaved(ReadOnlySpan<float> samples, int frameCount)
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[(int) Math.Ceiling(1.25 * frameCount) + 7200];

            var bytesEncoded = SafeNativeMethods.EncodeBufferInterleavedIeeeFloat(
                _handle,
                MemoryMarshal.GetReference(samples),
                frameCount,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);

            if (bytesEncoded < 0)
                throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

            _stream.Write(buffer.Slice(0, bytesEncoded));
#else
            var buffer = ArrayPool<byte>.Shared.Rent((int) Math.Ceiling(1.25 * frameCount) + 7200);
            try
            {
                var bytesEncoded = SafeNativeMethods.EncodeBufferInterleavedIeeeFloat(
                    _handle,
                    MemoryMarshal.GetReference(samples),
                    frameCount,
                    buffer,
                    buffer.Length);

                if (bytesEncoded < 0)
                    throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

                _stream.Write(buffer, 0, bytesEncoded);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        internal void Flush()
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[7200];
            var bytesFlushed = SafeNativeMethods.EncodeFlush(
                _handle,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);
            _stream.Write(buffer.Slice(0, bytesFlushed));
#else
            var buffer = ArrayPool<byte>.Shared.Rent(7200);
            try
            {
                var bytesFlushed = SafeNativeMethods.EncodeFlush(_handle, buffer, buffer.Length);
                _stream.Write(buffer, 0, bytesFlushed);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        internal void UpdateLameTag()
        {
            var endOfData = _stream.Position;
            _stream.Position = _startPosition;

#if NETCOREAPP2_1
            byte empty = 0;
            var bufferSize = SafeNativeMethods.GetLameTagFrame(_handle, ref empty, UIntPtr.Zero);
            Span<byte> buffer = stackalloc byte[(int) bufferSize.ToUInt32()];
            _stream.Write(buffer.Slice(0, (int) SafeNativeMethods.GetLameTagFrame(
                _handle,
                ref MemoryMarshal.GetReference(buffer),
                bufferSize).ToUInt32()));
#else
            var bufferSize = SafeNativeMethods.GetLameTagFrame(_handle, Array.Empty<byte>(), UIntPtr.Zero);
            var buffer = ArrayPool<byte>.Shared.Rent((int) bufferSize.ToUInt32());
            try
            {
                _stream.Write(buffer, 0, (int) SafeNativeMethods.GetLameTagFrame(_handle, buffer, bufferSize));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif

            _stream.Position = endOfData;
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}