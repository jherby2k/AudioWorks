using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;
#if (WINDOWS)
using System.IO;
using System.Reflection;
using System.Text;
#endif

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if (LINUX)
        const string _oggLibrary = "libogg.so.0";
        const string _vorbisLibrary = "libvorbis.so.0";
#else
        const string _oggLibrary = "libogg";
        const string _vorbisLibrary = "libvorbis";
#endif

#if (WINDOWS)
        static SafeNativeMethods()
        {
            // Select an architecture-appropriate directory by prefixing the PATH variable
            var newPath = new StringBuilder(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            newPath.Append(Path.DirectorySeparatorChar);
            newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
            newPath.Append(Path.PathSeparator);
            newPath.Append(Environment.GetEnvironmentVariable("PATH"));

            Environment.SetEnvironmentVariable("PATH", newPath.ToString());
        }
#endif

        [Pure]
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_serialno",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggPageSerialNo(
            ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_eos",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OggPageEos(
            ref OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncInit(
            IntPtr syncState);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_pageout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncPageOut(IntPtr syncState,
            out OggPage page);

#if (WINDOWS)
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_buffer",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OggSyncBuffer(
            IntPtr syncState,
            int size);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_wrote",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncWrote(
            IntPtr syncState,
            int bytes);
#else
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_buffer",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OggSyncBuffer(
            IntPtr syncState,
            long size);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_wrote",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncWrote(
            IntPtr syncState,
            long bytes);
#endif
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncClear(
            IntPtr syncState);

#if (WINDOWS)
        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamInit(
            IntPtr streamState,
            int serialNumber);
#else
        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamInit(
            IntPtr streamState,
            long serialNumber);
#endif

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pagein",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageIn(
            IntPtr streamState,
            ref OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pageout",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageOut(
            IntPtr streamState,
            out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_packetin",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPacketIn(
            IntPtr streamState,
            ref OggPacket packet);

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
        internal static extern void VorbisCommentAddTag(
            ref VorbisComment comment,
            [NotNull] byte[] tag,
            [NotNull] byte[] contents);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentClear(
            ref VorbisComment comment);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisCommentHeaderOut(
            ref VorbisComment comment,
            out OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoInit(
            IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoClear(
            IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_setup_managed",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeSetupManaged(
            IntPtr info,
            int channels,
            int sampleRate,
            int maximumBitRate,
            int nominalBitRate,
            int minimumBitRate);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_ctl",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeCtl(IntPtr info,
            int request,
            IntPtr argument);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_setup_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeSetupInit(
            IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_encode_init_vbr",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisEncodeInitVbr(
            IntPtr info,
            int channels,
            int rate,
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
            ref VorbisComment comment,
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
            ref OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisSynthesisHeaderIn(
            IntPtr info,
            ref VorbisComment comment,
            ref OggPacket packet);
    }
}
