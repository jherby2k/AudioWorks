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

namespace AudioWorks.Extensions.Flac.Metadata
{
#pragma warning disable CA1060
    static partial class LibFlac
#pragma warning restore CA1060
    {
        const string _flacLibrary = "FLAC";

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial MetadataObjectHandle MetadataObjectNew(MetadataType blockType);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry entry,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string key,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectVorbisCommentAppendComment(
            MetadataObjectHandle handle, VorbisCommentEntry vorbisComment, [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_seektable_template_append_spaced_points")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectSeekTableTemplateAppendSpacedPoints(
            MetadataObjectHandle handle, uint num, ulong totalSamples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_picture_set_mime_type")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectPictureSetMimeType(
            MetadataObjectHandle handle,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string mimeType,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_picture_set_data")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataObjectPictureSetData(
            MetadataObjectHandle handle, [In] byte[] data, uint length, [MarshalAs(UnmanagedType.Bool)] bool copy);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataObjectDelete(nint handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial MetadataChainHandle MetadataChainNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainReadWithCallbacks(
            MetadataChainHandle handle, nint ioHandle, IoCallbacks callbacks);

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
            nint ioHandle,
            IoCallbacks callbacks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataChainWriteWithCallbacksAndTempFile(
            MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            nint ioHandle,
            IoCallbacks callbacks,
            nint tempIoHandle,
            IoCallbacks tempCallbacks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataChainDelete(nint handle);

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
        internal static partial nint MetadataIteratorGetBlock(MetadataIteratorHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataIteratorInsertBlockAfter(
            MetadataIteratorHandle handle, MetadataObjectHandle metadataHandle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool MetadataIteratorDeleteBlock(
            MetadataIteratorHandle handle, [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void MetadataIteratorDelete(nint handle);
    }
}