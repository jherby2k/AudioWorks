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

namespace AudioWorks.Extensions.Vorbis
{
#pragma warning disable CA1060
    static partial class LibVorbis
#pragma warning restore CA1060
    {
        const string _vorbisLibrary = "libvorbis";
#if !WINDOWS
        const string _vorbisEncLibrary = "libvorbisenc";
#endif

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_version_string")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr GetVersion();

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void CommentInit(out VorbisComment comment);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_add_tag")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial void CommentAddTag(
            in VorbisComment comment,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string tag,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string contents);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_add_tag")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial void CommentAddTag(
            in VorbisComment comment, [MarshalUsing(typeof(AnsiStringMarshaller))] string tag, byte* contents);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_comment_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void CommentClear(ref VorbisComment comment);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_commentheader_out")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int CommentHeaderOut(in VorbisComment comment, out OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_info_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void InfoInit(IntPtr info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void InfoClear(IntPtr info);

#if WINDOWS
        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init")]
#else
        [LibraryImport(_vorbisEncLibrary, EntryPoint = "vorbis_encode_init")]
#endif
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int EncodeInit(
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
        internal static partial int EncodeInitVbr(
            IntPtr info, CLong channels, CLong rate, float baseQuality);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisInit(IntPtr dspState, IntPtr info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_headerout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisHeaderOut(
            IntPtr dspState,
            in VorbisComment comment,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_buffer")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr AnalysisBuffer(IntPtr dspState, int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_wrote")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisWrote(IntPtr dspState, int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_blockout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisBlockOut(IntPtr dspState, IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int Analysis(IntPtr block, IntPtr packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_addblock")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BitrateAddBlock(IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_flushpacket")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BitrateFlushPacket(IntPtr dspState, out OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_dsp_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void DspClear(IntPtr dspState);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BlockInit(IntPtr dspState, IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void BlockClear(IntPtr block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_idheader")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SynthesisIdHeader(in OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SynthesisHeaderIn(
            IntPtr info, in VorbisComment comment, in OggPacket packet);
    }
}
