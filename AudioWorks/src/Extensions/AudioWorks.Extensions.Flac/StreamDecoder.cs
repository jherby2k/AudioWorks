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
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    abstract class StreamDecoder : IDisposable
    {
        readonly LibFlac.StreamDecoderReadCallback _readCallback;
        readonly LibFlac.StreamDecoderSeekCallback _seekCallback;
        readonly LibFlac.StreamDecoderTellCallback _tellCallback;
        readonly LibFlac.StreamDecoderLengthCallback _lengthCallback;
        readonly LibFlac.StreamDecoderEofCallback _eofCallback;
        readonly LibFlac.StreamDecoderWriteCallback _writeCallback;
        readonly LibFlac.StreamDecoderMetadataCallback _metadataCallback;
        readonly LibFlac.StreamDecoderErrorCallback _errorCallback;
#pragma warning disable CA2213 // Disposable fields should be disposed
        readonly Stream _stream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        readonly long _streamLength;

        protected StreamDecoderHandle Handle { get; } = LibFlac.StreamDecoderNew();

        internal StreamDecoder(Stream stream)
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
            _streamLength = stream.Length;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Initialize() =>
            LibFlac.StreamDecoderInitStream(Handle,
                _readCallback,
                _seekCallback,
                _tellCallback,
                _lengthCallback,
                _eofCallback,
                _writeCallback,
                _metadataCallback,
                _errorCallback,
                IntPtr.Zero);

        internal bool ProcessMetadata() => LibFlac.StreamDecoderProcessUntilEndOfMetadata(Handle);

        internal void Finish() => LibFlac.StreamDecoderFinish(Handle);

        internal DecoderState GetState() => LibFlac.StreamDecoderGetState(Handle);

        public void Dispose() => Handle.Dispose();

        DecoderReadStatus ReadCallback(IntPtr handle, byte[] buffer, ref int bytes, IntPtr userData)
        {
            bytes = _stream.Read(buffer, 0, bytes);
            return bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        DecoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Position = (long) absoluteOffset;
            return DecoderSeekStatus.Ok;
        }

        DecoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return DecoderTellStatus.Ok;
        }

        DecoderLengthStatus LengthCallback(IntPtr handle, out ulong streamLength, IntPtr userData)
        {
            streamLength = (ulong) _streamLength;
            return DecoderLengthStatus.Ok;
        }

        bool EofCallback(IntPtr handle, IntPtr userData) => _stream.Position >= _streamLength;

        protected virtual DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer,
            IntPtr userData) =>
            DecoderWriteStatus.Continue;

        protected virtual void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
        }

        static void ErrorCallback(IntPtr handle, DecoderErrorStatus error, IntPtr userData)
        {
        }
    }
}