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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using AudioWorks.Extensions.Flac.Metadata;

namespace AudioWorks.Extensions.Flac.Decoder
{
    sealed class StreamDecoder : IDisposable
    {
        readonly StreamDecoderHandle _handle = LibFlac.StreamDecoderNew();
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
            Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
        GCHandle _instanceHandle;

        internal AudioInfo? AudioInfo { get; private set; }

        internal VorbisCommentToMetadataAdapter AudioMetadata { get; } = new();

        internal SampleBuffer? Samples { get; set; }

        internal StreamDecoder(Stream stream) => _stream = stream;

        internal unsafe void Initialize()
        {
            // The callbacks have to be static, so pass this instance through as userData
            _instanceHandle = GCHandle.Alloc(this);

            _ = LibFlac.StreamDecoderInitStream(_handle,
                &ReadCallback,
                &SeekCallback,
                &TellCallback,
                &LengthCallback,
                &EofCallback,
                &WriteCallback,
                &MetadataCallback,
                &ErrorCallback,
                GCHandle.ToIntPtr(_instanceHandle));
        }

        internal void SetMetadataRespond(MetadataType type) =>
            LibFlac.StreamDecoderSetMetadataRespond(_handle, type);

        internal bool ProcessMetadata() => LibFlac.StreamDecoderProcessUntilEndOfMetadata(_handle);

        internal bool ProcessSingle() => LibFlac.StreamDecoderProcessSingle(_handle);

        internal void Finish() => LibFlac.StreamDecoderFinish(_handle);

        internal DecoderState GetState() => LibFlac.StreamDecoderGetState(_handle);

        public void Dispose()
        {
            _handle.Dispose();
             _instanceHandle.Free();
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe DecoderReadStatus ReadCallback(nint handle, byte* buffer, int* bytes, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;
            *bytes = instance._stream.Read(new(buffer, *bytes));
            return *bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static DecoderSeekStatus SeekCallback(nint handle, ulong absoluteOffset, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;
            instance._stream.Position = (long) absoluteOffset;
            return DecoderSeekStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe DecoderTellStatus TellCallback(nint handle, ulong* absoluteOffset, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;
            *absoluteOffset = (ulong) instance._stream.Position;
            return DecoderTellStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe DecoderLengthStatus LengthCallback(nint handle, ulong* streamLength, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;
            *streamLength = (ulong) instance._stream.Length;
            return DecoderLengthStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static int EofCallback(nint handle, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;
            return instance._stream.Position >= instance._stream.Length ? 1 : 0;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe DecoderWriteStatus WriteCallback(nint handle, Frame* frame, nint buffer, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;

            if ((*frame).Header.Channels == 1)
                instance.Samples = new(
                    new Span<int>(Marshal.ReadIntPtr(buffer).ToPointer(), (int) (*frame).Header.BlockSize),
                    (int) (*frame).Header.BitsPerSample);
            else
                instance.Samples = new(
                    new Span<int>(Marshal.ReadIntPtr(buffer).ToPointer(), (int) (*frame).Header.BlockSize),
                    new Span<int>(Marshal.ReadIntPtr(buffer, nint.Size).ToPointer(), (int) (*frame).Header.BlockSize),
                    (int) (*frame).Header.BitsPerSample);

            return DecoderWriteStatus.Continue;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe void MetadataCallback(nint handle, MetadataBlock* metadataBlock, nint userData)
        {
            var instance = (StreamDecoder) GCHandle.FromIntPtr(userData).Target!;

            switch ((*metadataBlock).Type)
            {
                case MetadataType.StreamInfo:
                    instance.AudioInfo = AudioInfo.CreateForLossless(
                        "FLAC",
                        (int) (*metadataBlock).StreamInfo.Channels,
                        (int) (*metadataBlock).StreamInfo.BitsPerSample,
                        (int) (*metadataBlock).StreamInfo.SampleRate,
                        (long) (*metadataBlock).StreamInfo.TotalSamples);
                    break;

                case MetadataType.VorbisComment:
                    foreach (var entry in new Span<VorbisCommentEntry>(
                                 (*metadataBlock).VorbisComment.Comments,
                                 (int) (*metadataBlock).VorbisComment.Count))
                    {
                        var entryString = Utf8StringMarshaller.ConvertToManaged(entry.Entry) ?? string.Empty;
                        var delimiter = entryString.IndexOf('=', StringComparison.OrdinalIgnoreCase);
                        instance.AudioMetadata.Set(entryString[..delimiter], entryString[(delimiter + 1)..]);
                    }
                    break;

                case MetadataType.Picture:
                    if ((*metadataBlock).Picture.Type is PictureType.CoverFront or PictureType.Other)
                        instance.AudioMetadata.CoverArt = CoverArtFactory.GetOrCreate(new Span<byte>(
                            (*metadataBlock).Picture.Data,
                            (int) (*metadataBlock).Picture.DataLength));
                    break;
            }
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static void ErrorCallback(nint handle, DecoderErrorStatus error, nint userData)
        {
        }
    }
}