using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;
using System.Security;
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
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggPageSerialNo(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_oggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OggPageEos(ref OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncInit(IntPtr syncState);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncPageOut(IntPtr syncState, out OggPage page);

#if (WINDOWS)
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncWrote(IntPtr syncState, int bytes);
#else
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OggSyncBuffer(IntPtr syncState, long size);

        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncWrote(IntPtr syncState, long bytes);
#endif
        [DllImport(_oggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggSyncClear(IntPtr syncState);

#if (WINDOWS)
        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamInit(IntPtr streamState, int serialNumber);
#else
        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamInit(IntPtr streamState, long serialNumber);
#endif

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_pageout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPageOut(IntPtr streamState, out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_packetin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPacketIn(IntPtr streamState, ref OggPacket packet);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_flush", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OggStreamFlush(IntPtr streamState, out OggPage page);

        [DllImport(_oggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OggStreamClear(IntPtr streamState);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentInit(out VorbisComment comment);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_comment_clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisCommentClear(ref VorbisComment comment);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoInit(IntPtr info);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VorbisInfoClear(IntPtr info);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool VorbisSynthesisIdHeader(ref OggPacket packet);

        [DllImport(_vorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int VorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);
    }
}
