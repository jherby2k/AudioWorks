using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class StreamEncoder : IDisposable
    {
        [NotNull] readonly StreamEncoderHandle _handle = SafeNativeMethods.StreamEncoderNew();
        [NotNull] readonly NativeCallbacks.StreamEncoderWriteCallback _writeCallback;
        [NotNull] readonly NativeCallbacks.StreamEncoderSeekCallback _seekCallback;
        [NotNull] readonly NativeCallbacks.StreamEncoderTellCallback _tellCallback;
        [NotNull] readonly Stream _stream;

        internal StreamEncoder([NotNull] Stream stream)
        {
            // Need a reference to the callbacks for the lifetime of the encoder
            _writeCallback = WriteCallback;
            _seekCallback = SeekCallback;
            _tellCallback = TellCallback;

            _stream = stream;
        }

        internal void SetChannels(uint channels)
        {
            SafeNativeMethods.StreamEncoderSetChannels(_handle, channels);
        }

        internal void SetBitsPerSample(uint bitsPerSample)
        {
            SafeNativeMethods.StreamEncoderSetBitsPerSample(_handle, bitsPerSample);
        }

        internal void SetSampleRate(uint sampleRate)
        {
            SafeNativeMethods.StreamEncoderSetSampleRate(_handle, sampleRate);
        }

        internal void SetTotalSamplesEstimate(ulong sampleCount)
        {
            SafeNativeMethods.StreamEncoderSetTotalSamplesEstimate(_handle, sampleCount);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Initialize()
        {
            SafeNativeMethods.StreamEncoderInitialize(_handle, _writeCallback, _seekCallback, _tellCallback, null,
                IntPtr.Zero);
        }

        internal bool ProcessInterleaved([NotNull] int[] buffer, uint sampleCount)
        {
            return SafeNativeMethods.StreamEncoderProcessInterleaved(_handle, buffer, sampleCount);
        }

        internal bool Finish()
        {
            return SafeNativeMethods.StreamEncoderFinish(_handle);
        }

        [Pure]
        internal EncoderState GetState()
        {
            return SafeNativeMethods.StreamEncoderGetState(_handle);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        EncoderWriteStatus WriteCallback(IntPtr handle, [NotNull] byte[] buffer, int bytes, uint samples, uint currentFrame, IntPtr userData)
        {
            _stream.Write(buffer, 0, bytes);
            return EncoderWriteStatus.Ok;
        }

        EncoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Position = (long) absoluteOffset;
            return EncoderSeekStatus.Ok;
        }

        EncoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return EncoderTellStatus.Ok;
        }
    }
}