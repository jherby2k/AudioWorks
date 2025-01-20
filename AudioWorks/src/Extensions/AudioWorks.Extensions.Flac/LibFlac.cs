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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace AudioWorks.Extensions.Flac
{
#pragma warning disable CA1060
    static partial class LibFlac
#pragma warning restore CA1060
    {
#if LINUX
        const string _flacLibrary = "libFLAC.so.8";
#else
        const string _flacLibrary = "libFLAC";
#endif

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial StreamDecoderHandle StreamDecoderNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int StreamDecoderInitStream(
            StreamDecoderHandle handle,
            delegate* unmanaged<IntPtr, byte*, int*, IntPtr, DecoderReadStatus> readCallback,
            delegate* unmanaged<IntPtr, ulong, IntPtr, DecoderSeekStatus> seekCallback,
            delegate* unmanaged<IntPtr, ulong*, IntPtr, DecoderTellStatus> tellCallback,
            delegate* unmanaged<IntPtr, ulong*, IntPtr, DecoderLengthStatus> lengthCallback,
            delegate* unmanaged<IntPtr, IntPtr, int> eofCallback,
            StreamDecoderWriteCallback writeCallback,
            StreamDecoderMetadataCallback? metadataCallback,
            delegate* unmanaged<IntPtr, DecoderErrorStatus, IntPtr, void> errorCallback,
            IntPtr userData);

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
        internal static partial void StreamDecoderDelete(IntPtr handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial StreamEncoderHandle StreamEncoderNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_channels")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetChannels(StreamEncoderHandle handle, uint channels);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_channels")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial uint StreamEncoderGetChannels(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_bits_per_sample")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetBitsPerSample(StreamEncoderHandle handle, uint bitsPerSample);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_bits_per_sample")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial uint StreamEncoderGetBitsPerSample(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_sample_rate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetSampleRate(StreamEncoderHandle handle, uint sampleRate);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_total_samples_estimate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetTotalSamplesEstimate(
            StreamEncoderHandle handle, ulong totalSamples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_compression_level")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial void StreamEncoderSetCompressionLevel(
            StreamEncoderHandle handle, uint compressionLevel);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_metadata")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetMetadata(
            StreamEncoderHandle handle, [In] IntPtr[] metaData, uint blocks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_init_stream")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int StreamEncoderInitStream(
            StreamEncoderHandle handle,
            delegate* unmanaged<IntPtr, byte*, int, uint, uint, IntPtr, EncoderWriteStatus> writeCallback,
            delegate* unmanaged<IntPtr, ulong, IntPtr, EncoderSeekStatus> seekCallback,
            delegate* unmanaged<IntPtr, ulong*, IntPtr, EncoderTellStatus> tellCallback,
            delegate* unmanaged<IntPtr, IntPtr, IntPtr, void> metadataCallback,
            IntPtr userData);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_process")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderProcess(StreamEncoderHandle handle, in IntPtr buffer, uint samples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_process_interleaved")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderProcessInterleaved(
            StreamEncoderHandle handle, in int buffer, uint samples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_finish")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderFinish(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_state")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial EncoderState StreamEncoderGetState(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void StreamEncoderDelete(IntPtr handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial MetadataBlockHandle MetadataObjectNew(MetadataType blockType);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry entry, IntPtr key, IntPtr value);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectVorbisCommentAppendComment(
            MetadataBlockHandle handle, VorbisCommentEntry vorbisComment, [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_seektable_template_append_spaced_points")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectSeekTableTemplateAppendSpacedPoints(
            MetadataBlockHandle handle, uint num, ulong totalSamples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_picture_set_mime_type")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectPictureSetMimeType(
            MetadataBlockHandle handle,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string mimeType,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_picture_set_data")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectPictureSetData(
            MetadataBlockHandle handle, [In] byte[] data, uint length, [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataObjectDelete(IntPtr handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial MetadataChainHandle MetadataChainNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainReadWithCallbacks(
            MetadataChainHandle handle, IntPtr ioHandle, IoCallbacks callbacks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainCheckIfTempFileNeeded(
            MetadataChainHandle handle, [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainWriteWithCallbacks(
            MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainWriteWithCallbacksAndTempFile(
            MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks,
            IntPtr tempIoHandle,
            IoCallbacks tempCallbacks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataChainDelete(IntPtr handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial MetadataIteratorHandle MetadataIteratorNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataIteratorInit(
            MetadataIteratorHandle handle, MetadataChainHandle chainHandle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_next")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataIteratorNext(MetadataIteratorHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr MetadataIteratorGetBlock(MetadataIteratorHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataIteratorInsertBlockAfter(
            MetadataIteratorHandle handle, MetadataBlockHandle metadataHandle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataIteratorDeleteBlock(
            MetadataIteratorHandle handle, [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataIteratorDelete(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderWriteStatus StreamDecoderWriteCallback(
            IntPtr handle, ref Frame frame, IntPtr buffer, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamDecoderMetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData);

    }
}