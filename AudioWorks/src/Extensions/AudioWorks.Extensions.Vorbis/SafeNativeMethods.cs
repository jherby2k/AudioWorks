/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if LINUX
        const string _oggLibrary = "libogg.so.0";
        const string _vorbisLibrary = "libvorbis.so.0";
        const string _vorbisEncLibrary = "libvorbisenc.so.2";
#else
        const string _oggLibrary = "libogg";
        const string _vorbisLibrary = "libvorbis";
#endif
#if OSX
        const string _vorbisEncLibrary = "libvorbisenc";
#endif

        [Pure]
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_serialno",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggPageSerialNo(
            in OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_eos",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OggPageEos(
            in OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_page_checksum_set",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OggPageChecksumSet(
            ref OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncInit(
            IntPtr syncState);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_pageout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncPageOut(IntPtr syncState,
            out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_buffer",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OggSyncBuffer(
            IntPtr syncState,
#if WINDOWS
            int size);
#else
            long size);
#endif

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_wrote",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncWrote(
            IntPtr syncState,
#if WINDOWS
            int bytes);
#else
            long bytes);
#endif

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncClear(
            IntPtr syncState);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamInit(
            IntPtr streamState,
#if WINDOWS
            int serialNumber);
#else
            long serialNumber);
#endif

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pagein",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageIn(
            IntPtr streamState,
            in OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pageout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageOut(
            IntPtr streamState,
            out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_packetin",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPacketIn(
            IntPtr streamState,
            in OggPacket packet);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_packetout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPacketOut(
            IntPtr streamState,
            out OggPacket packet);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_flush",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamFlush(
            IntPtr streamState,
            out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OggStreamClear(
            IntPtr streamState);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentInit(
            out VorbisComment comment);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void VorbisCommentAddTag(
            in VorbisComment comment,
            ref byte tag,
            byte* contents);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentClear(
            ref VorbisComment comment);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisCommentHeaderOut(
            in VorbisComment comment,
            out OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoInit(
            IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoClear(
            IntPtr info);

#if WINDOWS
        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init",
#else
        [DllImport(_vorbisEncLibrary, EntryPoint = "vorbis_encode_init",
#endif
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeInit(
            IntPtr info,
#if WINDOWS
            int channels,
            int sampleRate,
            int maximumBitRate,
            int nominalBitRate,
            int minimumBitRate);
#else
            long channels,
            long sampleRate,
            long maximumBitRate,
            long nominalBitRate,
            long minimumBitRate);
#endif

#if WINDOWS
        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init_vbr",
#else
        [DllImport(_vorbisEncLibrary, EntryPoint = "vorbis_encode_init_vbr",
#endif
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeInitVbr(
            IntPtr info,
#if WINDOWS
            int channels,
            int rate,
#else
            long channels,
            long rate,
#endif
            float baseQuality);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisAnalysisInit(
            IntPtr dspState,
            IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_headerout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisAnalysisHeaderOut(
            IntPtr dspState,
            in VorbisComment comment,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_buffer",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VorbisAnalysisBuffer(
            IntPtr dspState,
            int samples);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_wrote",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisAnalysisWrote(
            IntPtr dspState,
            int samples);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis_blockout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisAnalysisBlockOut(
            IntPtr dspState,
            IntPtr block);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_analysis",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisAnalysis(
            IntPtr block,
            IntPtr packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_addblock",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisBitrateAddBlock(
            IntPtr block);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_bitrate_flushpacket",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisBitrateFlushPacket(
            IntPtr dspState,
            out OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_dsp_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisDspClear(
            IntPtr dspState);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_block_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisBlockInit(
            IntPtr dspState,
            IntPtr block);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_block_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisBlockClear(
            IntPtr block);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool VorbisSynthesisIdHeader(
            in OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisSynthesisHeaderIn(
            IntPtr info,
            in VorbisComment comment,
            in OggPacket packet);
    }
}
