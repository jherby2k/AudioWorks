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

namespace AudioWorks.Extensions.Vorbis
{
#pragma warning disable CA1060
    static partial class LibOgg
#pragma warning restore CA1060
    {
        const string _oggLibrary = "ogg";

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_serialno")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int PageSerialNo(in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_eos")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool PageEos(in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_page_checksum_set")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void PageChecksumSet(ref OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SyncInit(nint syncState);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_pageout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SyncPageOut(nint syncState, out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_buffer")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial void* SyncBuffer(nint syncState, CLong size);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_wrote")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SyncWrote(nint syncState, CLong bytes);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_sync_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SyncClear(nint syncState);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamInit(nint streamState, int serialNumber);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_pagein")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamPageIn(nint streamState, in OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_pageout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamPageOut(nint streamState, out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_packetin")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamPacketIn(nint streamState, in OggPacket packet);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_packetout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamPacketOut(nint streamState, out OggPacket packet);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_flush")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int StreamFlush(nint streamState, out OggPage page);

        [LibraryImport(_oggLibrary, EntryPoint = "ogg_stream_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void StreamClear(nint streamState);
    }
}
