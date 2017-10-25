using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    abstract class StreamDecoder : IDisposable
    {
        [NotNull] readonly NativeCallbacks.StreamDecoderReadCallback _readCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderSeekCallback _seekCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderTellCallback _tellCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderLengthCallback _lengthCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderEofCallback _eofCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderWriteCallback _writeCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderMetadataCallback _metadataCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderErrorCallback _errorCallback;
        [NotNull] readonly Stream _stream;

        [NotNull]
        protected StreamDecoderHandle Handle { get; } = SafeNativeMethods.StreamDecoderNew();

        internal StreamDecoder([NotNull] Stream stream)
        {
            // Need a reference to the callbacks for the lifetime of the decoder
            _readCallback = ReadCallback;
            _seekCallback = SeekCallback;
            _tellCallback = TellCallback;
            _lengthCallback = LengthCallback;
            _eofCallback = EofCallback;
            _writeCallback = WriteCallback;
            _metadataCallback = MetadataCallback;
            _errorCallback = ErrorCallback;

            _stream = stream;
        }

        internal void Initialize()
        {
            SafeNativeMethods.StreamDecoderInitializeStream(Handle,
                _readCallback,
                _seekCallback,
                _tellCallback,
                _lengthCallback,
                _eofCallback,
                _writeCallback,
                _metadataCallback,
                _errorCallback);
        }

        internal bool ProcessMetadata()
        {
            return SafeNativeMethods.StreamDecoderProcessUntilEndOfMetadata(Handle);
        }

        internal void Finish()
        {
            SafeNativeMethods.StreamDecoderFinish(Handle);
        }

        [Pure]
        internal DecoderState GetState()
        {
            return SafeNativeMethods.StreamDecoderGetState(Handle);
        }

        public void Dispose()
        {
            Handle.Dispose();
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderReadStatus ReadCallback(IntPtr handle, [NotNull] byte[] buffer, ref int bytes, IntPtr userData)
        {
            bytes = _stream.Read(buffer, 0, bytes);
            return bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Seek((long) absoluteOffset, SeekOrigin.Begin);
            return DecoderSeekStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return DecoderTellStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderLengthStatus LengthCallback(IntPtr handle, out ulong streamLength, IntPtr userData)
        {
            streamLength = (ulong) _stream.Length;
            return DecoderLengthStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        bool EofCallback(IntPtr handle, IntPtr userData)
        {
            return _stream.Position >= _stream.Length;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        static DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer, IntPtr userData)
        {
            return DecoderWriteStatus.Continue;
        }

        protected virtual void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        static void ErrorCallback(IntPtr handle, DecoderErrorStatus error, IntPtr userData)
        {
        }
    }
}