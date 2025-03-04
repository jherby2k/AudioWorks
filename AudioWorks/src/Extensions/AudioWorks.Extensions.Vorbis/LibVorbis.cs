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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressMessage("Design", "CA1060:Move pinvokes to native methods class",
            Justification = "Following latest native interop best practices")]
    static partial class LibVorbis
    {
        const string _vorbisLibrary = "vorbis";

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_version_string")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial nint GetVersion();

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
        internal static partial void InfoInit(nint info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void InfoClear(nint info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisInit(nint dspState, nint info);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_headerout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisHeaderOut(
            nint dspState,
            in VorbisComment comment,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_buffer")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial nint AnalysisBuffer(nint dspState, int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_wrote")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisWrote(nint dspState, int samples);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_blockout")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int AnalysisBlockOut(nint dspState, nint block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_analysis")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int Analysis(nint block, nint packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_addblock")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BitrateAddBlock(nint block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_flushpacket")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BitrateFlushPacket(nint dspState, out OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_dsp_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void DspClear(nint dspState);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int BlockInit(nint dspState, nint block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_block_clear")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void BlockClear(nint block);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_idheader")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SynthesisIdHeader(in OggPacket packet);

        [LibraryImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int SynthesisHeaderIn(
            nint info, in VorbisComment comment, in OggPacket packet);
    }
}
