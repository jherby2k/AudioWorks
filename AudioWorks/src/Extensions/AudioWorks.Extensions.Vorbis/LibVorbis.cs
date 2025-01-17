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
    static partial class LibVorbis
    {
        const string _vorbisLibrary = "libvorbis";
#if !WINDOWS
        const string _vorbisEncLibrary = "libvorbisenc";
#endif

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_version_string")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr VorbisVersion();

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisCommentInit(
            out VorbisComment comment);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_add_tag")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial void VorbisCommentAddTag(
            in VorbisComment comment,
            ref byte tag,
            byte* contents);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisCommentClear(
            ref VorbisComment comment);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_commentheader_out")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisCommentHeaderOut(
            in VorbisComment comment,
            out OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_info_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisInfoInit(
            IntPtr info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisInfoClear(
            IntPtr info);

#if WINDOWS
        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init")]
#else
        [LibraryImport(_vorbisEncLibrary, EntryPoint = "vorbis_encode_init")]
#endif
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisEncodeInit(
            IntPtr info,
            CLong channels,
            CLong sampleRate,
            CLong maximumBitRate,
            CLong nominalBitRate,
            CLong minimumBitRate);

#if WINDOWS
        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init_vbr")]
#else
        [LibraryImport(_vorbisEncLibrary, EntryPoint = "vorbis_encode_init_vbr")]
#endif
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisEncodeInitVbr(
            IntPtr info,
            CLong channels,
            CLong rate,
            float baseQuality);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisAnalysisInit(
            IntPtr dspState,
            IntPtr info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_headerout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisAnalysisHeaderOut(
            IntPtr dspState,
            in VorbisComment comment,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_buffer")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr VorbisAnalysisBuffer(
            IntPtr dspState,
            int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_wrote")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisAnalysisWrote(
            IntPtr dspState,
            int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_blockout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisAnalysisBlockOut(
            IntPtr dspState,
            IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisAnalysis(
            IntPtr block,
            IntPtr packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_addblock")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisBitrateAddBlock(
            IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_flushpacket")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisBitrateFlushPacket(
            IntPtr dspState,
            out OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_dsp_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisDspClear(
            IntPtr dspState);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisBlockInit(
            IntPtr dspState,
            IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void VorbisBlockClear(
            IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_idheader")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool VorbisSynthesisIdHeader(
            in OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int VorbisSynthesisHeaderIn(
            IntPtr info,
            in VorbisComment comment,
            in OggPacket packet);
    }
}
