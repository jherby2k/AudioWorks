using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    class NativeStreamDecoder : IDisposable
    {
        [NotNull] readonly NativeStreamDecoderHandle _handle = SafeNativeMethods.StreamDecoderNew();
        [NotNull] readonly Stream _stream;
        [NotNull] readonly NativeCallbacks.StreamDecoderReadCallback _readCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderSeekCallback _seekCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderTellCallback _tellCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderLengthCallback _lengthCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderEofCallback _eofCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderWriteCallback _writeCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderMetadataCallback _metadataCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderErrorCallback _errorCallback;

        internal NativeStreamDecoder([NotNull] FileStream stream)
        {
            _stream = stream;
            _readCallback = ReadCallback;
            _seekCallback = SeekCallback;
            _tellCallback = TellCallback;
            _lengthCallback = LengthCallback;
            _eofCallback = EofCallback;
            _writeCallback = WriteCallback;
            _metadataCallback = MetadataCallback;
            _errorCallback = ErrorCallback;
        }

        internal void Initialize()
        {
            SafeNativeMethods.StreamDecoderInitialize(_handle,
                _readCallback,
                _seekCallback,
                _tellCallback,
                _lengthCallback,
                _eofCallback,
                _writeCallback,
                _metadataCallback,
                _errorCallback);
        }

        internal void ProcessMetadata()
        {
            SafeNativeMethods.StreamDecoderProcessMetadata(_handle);
        }

        internal void Finish()
        {
            SafeNativeMethods.StreamDecoderFinish(_handle);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        DecoderReadStatus ReadCallback(IntPtr handle, [NotNull] byte[] buffer, ref int bytes, IntPtr userData)
        {
            bytes = _stream.Read(buffer, 0, bytes);
            return bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        DecoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Seek((long) absoluteOffset, SeekOrigin.Begin);
            return DecoderSeekStatus.Ok;
        }

        DecoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return DecoderTellStatus.Ok;
        }

        DecoderLengthStatus LengthCallback(IntPtr handle, out ulong streamLength, IntPtr userData)
        {
            streamLength = (ulong) _stream.Length;
            return DecoderLengthStatus.Ok;
        }

        bool EofCallback(IntPtr handle, IntPtr userData)
        {
            return _stream.Position >= _stream.Length;
        }

        static DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer, IntPtr userData)
        {
            return DecoderWriteStatus.Continue;
        }

        protected virtual void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
        }

        static void ErrorCallback(IntPtr handle, int error, IntPtr userData)
        {
        }
    }
}