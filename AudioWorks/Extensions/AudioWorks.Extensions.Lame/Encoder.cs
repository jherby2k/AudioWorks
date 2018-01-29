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
            SafeNativeMethods.SetChannels(_handle, channels);
        }

        internal void SetSampleRate(int sampleRate)
        {
            SafeNativeMethods.SetSampleRate(_handle, sampleRate);
        }

        internal void SetSampleCount(uint sampleCount)
        {
            SafeNativeMethods.SetSampleCount(_handle, sampleCount);
        }

        internal void InitializeParameters()
        {
            _startPosition = _stream.Position;

            SafeNativeMethods.InitParams(_handle);
            //TODO catch errors here?
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
            _stream.Seek(_startPosition, SeekOrigin.Begin);
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