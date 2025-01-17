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

namespace AudioWorks.Extensions.Vorbis
{
    static partial class LibOgg
    {
#if LINUX
        const string _oggLibrary = "libogg.so.0";
#else
        const string _oggLibrary = "libogg";
#endif

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_serialno")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggPageSerialNo(
            in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_eos")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool OggPageEos(
            in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_checksum_set")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void OggPageChecksumSet(
            ref OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggSyncInit(
            IntPtr syncState);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_pageout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggSyncPageOut(IntPtr syncState,
            out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_buffer")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr OggSyncBuffer(
            IntPtr syncState,
            CLong size);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_wrote")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggSyncWrote(
            IntPtr syncState,
            CLong bytes);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggSyncClear(
            IntPtr syncState);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamInit(
            IntPtr streamState,
            int serialNumber);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_pagein")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamPageIn(
            IntPtr streamState,
            in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_pageout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamPageOut(
            IntPtr streamState,
            out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_packetin")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamPacketIn(
            IntPtr streamState,
            in OggPacket packet);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_packetout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamPacketOut(
            IntPtr streamState,
            out OggPacket packet);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_flush")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int OggStreamFlush(
            IntPtr streamState,
            out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void OggStreamClear(
            IntPtr streamState);
    }
}
