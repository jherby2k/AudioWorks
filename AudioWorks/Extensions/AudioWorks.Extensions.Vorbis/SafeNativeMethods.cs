using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _standardOggLibrary = "libogg";
        const string _standardVorbisLibrary = "libvorbis";
        const string _linuxOggLibrary = "libogg.so.0";
        const string _linuxVorbisLibrary = "libvorbis.so.0";

        static SafeNativeMethods()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Select an architecture-appropriate directory by prefixing the PATH variable:
                var newPath = new StringBuilder(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
                newPath.Append(Path.DirectorySeparatorChar);
                newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
                newPath.Append(Path.PathSeparator);
                newPath.Append(Environment.GetEnvironmentVariable("PATH"));

                Environment.SetEnvironmentVariable("PATH", newPath.ToString());
            }
        }

        [Pure]
        internal static int OggPageGetSerialNumber(ref OggPage page)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxOggPageSerialNo(ref page)
                : StandardOggPageSerialNo(ref page);
        }

        [Pure]
        internal static bool OggPageEndOfStream(ref OggPage page)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxOggPageEos(ref page)
                : StandardOggPageEos(ref page);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal static void OggSyncInitialize(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggSyncInit(syncState);
            else
                StandardOggSyncInit(syncState);
        }

        internal static bool OggSyncPageOut(IntPtr syncState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggSyncPageOut(syncState, out page) == 1;
            return StandardOggSyncPageOut(syncState, out page) == 1;
        }

        internal static IntPtr OggSyncBuffer(IntPtr syncState, int size)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxOggSyncBuffer(syncState, size)
                : StandardOggSyncBuffer(syncState, size);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggSyncWrote(IntPtr syncState, int bytes)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggSyncWrote(syncState, bytes);
            else
                StandardOggSyncWrote(syncState, bytes);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal static void OggSyncClear(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggSyncClear(syncState);
            else
                StandardOggSyncClear(syncState);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamInitialize(IntPtr streamState, int serialNumber)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggStreamInit(streamState, serialNumber);
            else
                StandardOggStreamInit(streamState, serialNumber);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamPageIn(IntPtr streamState, ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggStreamPageIn(streamState, ref page);
            else
                StandardOggStreamPageIn(streamState, ref page);
        }

        internal static bool OggStreamPageOut(IntPtr streamState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamPageOut(streamState, out page) != 0;
            return StandardOggStreamPageOut(streamState, out page) != 0;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamPacketIn(IntPtr streamState, ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggStreamPacketIn(streamState, ref packet);
            else
                StandardOggStreamPacketIn(streamState, ref packet);
        }

        internal static bool OggStreamPacketOut(IntPtr streamState, out OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamPacketOut(streamState, out packet) == 1;
            return StandardOggStreamPacketOut(streamState, out packet) == 1;
        }

        internal static bool OggStreamFlush(IntPtr streamState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamFlush(streamState, out page) != 0;
            return StandardOggStreamFlush(streamState, out page) != 0;
        }

        internal static void OggStreamClear(IntPtr streamState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxOggStreamClear(streamState);
            else
                StandardOggStreamClear(streamState);
        }

        internal static void VorbisCommentInitialize(out VorbisComment comment)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisCommentInit(out comment);
            else
                StandardVorbisCommentInit(out comment);
        }

        internal static void VorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisCommentAddTag(ref comment, tag, contents);
            else
                StandardVorbisCommentAddTag(ref comment, tag, contents);
        }

        internal static void VorbisCommentClear(ref VorbisComment comment)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisCommentClear(ref comment);
            else
                StandardVorbisCommentClear(ref comment);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void VorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisCommentHeaderOut(ref comment, out packet);
            else
                StandardVorbisCommentHeaderOut(ref comment, out packet);
        }

        internal static void VorbisInfoInitialize(IntPtr info)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisInfoInit(info);
            else
                StandardVorbisInfoInit(info);
        }

        internal static void VorbisInfoClear(IntPtr info)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisInfoClear(info);
            else
                StandardVorbisInfoClear(info);
        }

        [Pure]
        internal static bool VorbisSynthesisIdHeader(ref OggPacket packet)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxVorbisSynthesisIdHeader(ref packet)
                : StandardVorbisSynthesisIdHeader(ref packet);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void VorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxVorbisSynthesisHeaderIn(info, ref comment, ref packet);
            else
                StandardVorbisSynthesisHeaderIn(info, ref comment, ref packet);
        }

        [Pure]
        [DllImport(_standardOggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggPageSerialNo(ref OggPage page);

        [Pure]
        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggPageSerialNo(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_standardOggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        static extern bool StandardOggPageEos(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        static extern bool LinuxOggPageEos(ref OggPage page);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggSyncInit(IntPtr syncState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncInit(IntPtr syncState);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggSyncPageOut(IntPtr syncState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncPageOut(IntPtr syncState, out OggPage page);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr StandardOggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr LinuxOggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggSyncWrote(IntPtr syncState, int bytes);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncWrote(IntPtr syncState, int bytes);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggSyncClear(IntPtr syncState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncClear(IntPtr syncState);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamInit(IntPtr streamState, int serialNumber);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamInit(IntPtr streamState, int serialNumber);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamPageOut(IntPtr streamState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPageOut(IntPtr streamState, out OggPage page);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_packetin", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamPacketIn(IntPtr streamState, ref OggPacket packet);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_packetin", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPacketIn(IntPtr streamState, ref OggPacket packet);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_flush", CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardOggStreamFlush(IntPtr streamState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_flush", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamFlush(IntPtr streamState, out OggPage page);

        [DllImport(_standardOggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardOggStreamClear(IntPtr streamState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxOggStreamClear(IntPtr streamState);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_comment_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardVorbisCommentInit(out VorbisComment comment);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentInit(out VorbisComment comment);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardVorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_comment_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardVorbisCommentClear(ref VorbisComment comment);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_clear",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentClear(ref VorbisComment comment);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardVorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxVorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardVorbisInfoInit(IntPtr info);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisInfoInit(IntPtr info);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardVorbisInfoClear(IntPtr info);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisInfoClear(IntPtr info);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        static extern bool StandardVorbisSynthesisIdHeader(ref OggPacket packet);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        static extern bool LinuxVorbisSynthesisIdHeader(ref OggPacket packet);

        [DllImport(_standardVorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardVorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxVorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);
    }
}
