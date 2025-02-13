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

using System.Runtime.InteropServices;
using AudioWorks.Extensions.Flac.Metadata;

namespace AudioWorks.Extensions.Flac.Decoder
{
#pragma warning disable CA1060
    static partial class LibFlac
#pragma warning restore CA1060
    {
        const string _flacLibrary = "FLAC";

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial StreamDecoderHandle StreamDecoderNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int StreamDecoderInitStream(
            StreamDecoderHandle handle,
            delegate* unmanaged<nint, byte*, int*, nint, DecoderReadStatus> readCallback,
            delegate* unmanaged<nint, ulong, nint, DecoderSeekStatus> seekCallback,
            delegate* unmanaged<nint, ulong*, nint, DecoderTellStatus> tellCallback,
            delegate* unmanaged<nint, ulong*, nint, DecoderLengthStatus> lengthCallback,
            delegate* unmanaged<nint, nint, int> eofCallback,
            StreamDecoderWriteCallback writeCallback,
            StreamDecoderMetadataCallback? metadataCallback,
            delegate* unmanaged<nint, DecoderErrorStatus, nint, void> errorCallback,
            nint userData);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle, MetadataType metadataType);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamDecoderProcessUntilEndOfMetadata(StreamDecoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_process_single")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamDecoderProcessSingle(StreamDecoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_get_state")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial DecoderState StreamDecoderGetState(StreamDecoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_finish")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamDecoderFinish(StreamDecoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void StreamDecoderDelete(nint handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderWriteStatus StreamDecoderWriteCallback(
            nint handle, ref Frame frame, nint buffer, nint userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamDecoderMetadataCallback(
            nint handle, ref MetadataBlock metadataBlock, nint userData);
    }
}