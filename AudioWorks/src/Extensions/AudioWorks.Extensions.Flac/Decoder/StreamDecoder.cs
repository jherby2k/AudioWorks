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
using System.Runtime.InteropServices;
using AudioWorks.Extensions.Flac.Metadata;

namespace AudioWorks.Extensions.Flac.Decoder
{
    abstract unsafe class StreamDecoder : IDisposable
    {
        readonly LibFlac.StreamDecoderWriteCallback _writeCallback;
        readonly LibFlac.StreamDecoderMetadataCallback _metadataCallback;
#pragma warning disable CA2213 // Disposable fields should be disposed
        readonly Stream _stream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        GCHandle _streamHandle;

        protected StreamDecoderHandle Handle { get; } = LibFlac.StreamDecoderNew();

        internal StreamDecoder(Stream stream)
        {
            // Need a reference to the callbacks for the lifetime of the decoder
            _writeCallback = WriteCallback;
            _metadataCallback = MetadataCallback;
            _stream = stream;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Initialize()
        {
            // The callbacks have to be static, so pass the output stream through as userData
            _streamHandle = GCHandle.Alloc(_stream);

            LibFlac.StreamDecoderInitStream(Handle,
                &ReadCallback,
                &SeekCallback,
                &TellCallback,
                &LengthCallback,
                &EofCallback,
                _writeCallback,
                _metadataCallback,
                &ErrorCallback,
                GCHandle.ToIntPtr(_streamHandle));
        }

        internal bool ProcessMetadata() => LibFlac.StreamDecoderProcessUntilEndOfMetadata(Handle);

        internal void Finish() => LibFlac.StreamDecoderFinish(Handle);

        internal DecoderState GetState() => LibFlac.StreamDecoderGetState(Handle);

        public void Dispose()
        {
            _streamHandle.Free();
            Handle.Dispose();
        }

        [UnmanagedCallersOnly]
        static DecoderReadStatus ReadCallback(nint handle, byte* buffer, int* bytes, nint userData)
        {
            var stream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            *bytes = stream.Read(new(buffer, *bytes));
            return *bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        [UnmanagedCallersOnly]
        static DecoderSeekStatus SeekCallback(nint handle, ulong absoluteOffset, nint userData)
        {
            var stream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            stream.Position = (long) absoluteOffset;
            return DecoderSeekStatus.Ok;
        }

        [UnmanagedCallersOnly]
        static DecoderTellStatus TellCallback(nint handle, ulong* absoluteOffset, nint userData)
        {
            var stream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            *absoluteOffset = (ulong) stream.Position;
            return DecoderTellStatus.Ok;
        }

        [UnmanagedCallersOnly]
        static DecoderLengthStatus LengthCallback(nint handle, ulong* streamLength, nint userData)
        {
            var stream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            *streamLength = (ulong) stream.Length;
            return DecoderLengthStatus.Ok;
        }

        [UnmanagedCallersOnly]
        static int EofCallback(nint handle, nint userData)
        {
            var stream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            return stream.Position >= stream.Length ? 1 : 0;
        }

        protected virtual DecoderWriteStatus WriteCallback(nint handle, ref Frame frame, nint buffer,
            nint userData) =>
            DecoderWriteStatus.Continue;

        protected virtual void MetadataCallback(nint handle, ref MetadataBlock metadataBlock, nint userData)
        {
        }

        [UnmanagedCallersOnly]
        static void ErrorCallback(nint handle, DecoderErrorStatus error, nint userData)
        {
        }
    }
}