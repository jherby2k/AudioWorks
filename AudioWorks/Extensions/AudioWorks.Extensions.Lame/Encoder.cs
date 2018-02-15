using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    sealed class Encoder : IDisposable
    {
        [NotNull] readonly EncoderHandle _handle = SafeNativeMethods.Init();
        [NotNull] readonly Stream _stream;
        long _startPosition;
        [CanBeNull] byte[] _buffer;

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

        internal void Encode([NotNull] float[] leftSamples, [CanBeNull] float[] rightSamples)
        {
            if (_buffer == null)
                _buffer = new byte[(int) Math.Ceiling(1.25 * leftSamples.Length) + 7200];

            var bytesEncoded = SafeNativeMethods.EncodeBufferIeeeFloat(_handle, leftSamples, rightSamples, leftSamples.Length, _buffer, _buffer.Length);
            //TODO throw on negative values (errors)
            _stream.Write(_buffer, 0, bytesEncoded);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        internal void Flush()
        {
            var bytesFlushed = SafeNativeMethods.EncodeFlush(_handle, _buffer, _buffer.Length);
            if (bytesFlushed > 0)
                _stream.Write(_buffer, 0, bytesFlushed);
            //TODO really need to check for > 0?
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        internal void UpdateLameTag()
        {
            _stream.Position = _startPosition;
            _stream.Write(_buffer, 0,
                (int) SafeNativeMethods.GetLameTagFrame(_handle, _buffer, new UIntPtr((uint) _buffer.Length))
                    .ToUInt32());
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}