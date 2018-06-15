using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    sealed class Encoder : IDisposable
    {
        [NotNull] readonly EncoderHandle _handle = SafeNativeMethods.Init();
        [NotNull] readonly Stream _stream;
        long _startPosition;
#if !NETCOREAPP2_1
        [CanBeNull] byte[] _buffer;
#endif

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

        internal unsafe void Encode(ReadOnlySpan<float> leftSamples, ReadOnlySpan<float> rightSamples)
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[(int) Math.Ceiling(1.25 * leftSamples.Length) + 7200];

            var bytesEncoded = SafeNativeMethods.EncodeBufferIeeeFloat(
                _handle,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(leftSamples))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(rightSamples))),
                leftSamples.Length,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer))),
                buffer.Length);

            //TODO throw on negative values (errors)
            _stream.Write(buffer.Slice(0, bytesEncoded));
#else
            if (_buffer == null)
                _buffer = ArrayPool<byte>.Shared.Rent((int) Math.Ceiling(1.25 * leftSamples.Length) + 7200);

            var bytesEncoded = SafeNativeMethods.EncodeBufferIeeeFloat(
                _handle,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(leftSamples))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(rightSamples))),
                leftSamples.Length,
                // ReSharper disable once AssignNullToNotNullAttribute
                _buffer,
                // ReSharper disable once PossibleNullReferenceException
                _buffer.Length);

            //TODO throw on negative values (errors)
            _stream.Write(_buffer, 0, bytesEncoded);
#endif
        }

#if NETCOREAPP2_1
        internal unsafe void Flush()
        {
            Span<byte> buffer = stackalloc byte[7200];
            var bytesFlushed = SafeNativeMethods.EncodeFlush(
                _handle,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer))),
                buffer.Length);
            _stream.Write(buffer.Slice(0, bytesFlushed));
        }
#else
        internal void Flush()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once PossibleNullReferenceException
            var bytesFlushed = SafeNativeMethods.EncodeFlush(_handle, _buffer, _buffer.Length);
            _stream.Write(_buffer, 0, bytesFlushed);
        }
#endif

#if NETCOREAPP2_1
        internal unsafe void UpdateLameTag()
        {
            _stream.Position = _startPosition;

            var bufferSize = SafeNativeMethods.GetLameTagFrame(_handle, IntPtr.Zero, UIntPtr.Zero);
            Span<byte> buffer = stackalloc byte[(int) bufferSize.ToUInt32()];
            _stream.Write(buffer.Slice(0, (int) SafeNativeMethods.GetLameTagFrame(
                _handle,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer))),
                bufferSize).ToUInt32()));
        }
#else
        internal void UpdateLameTag()
        {
            _stream.Position = _startPosition;

            // ReSharper disable once AssignNullToNotNullAttribute
            _stream.Write(_buffer, 0,
                // ReSharper disable once AssignNullToNotNullAttribute
                // ReSharper disable once PossibleNullReferenceException
                (int) SafeNativeMethods.GetLameTagFrame(_handle, _buffer, new UIntPtr((uint) _buffer.Length))
                    .ToUInt32());
        }
#endif


        public void Dispose()
        {
            _handle.Dispose();
#if !NETCOREAPP2_1
            if (_buffer != null)
                ArrayPool<byte>.Shared.Return(_buffer);
#endif
        }
    }
}