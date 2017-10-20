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
        const string _winOggLibrary = "libogg.dll";
        const string _winVorbisLibrary = "libvorbis.dll";
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggPageSerialNo(ref page);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggPageSerialNo(ref page);

            throw new NotImplementedException();
        }

        [Pure]
        internal static bool OggPageEndOfStream(ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggPageEos(ref page);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggPageEos(ref page);

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal static void OggSyncInitialize(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncInit(syncState);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggSyncInit(syncState);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool OggSyncPageOut(IntPtr syncState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggSyncPageOut(syncState, out page) == 1;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggSyncPageOut(syncState, out page) == 1;

            throw new NotImplementedException();
        }

        internal static IntPtr OggSyncBuffer(IntPtr syncState, int size)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggSyncBuffer(syncState, size);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggSyncBuffer(syncState, size);

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggSyncWrote(IntPtr syncState, int bytes)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncWrote(syncState, bytes);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggSyncWrote(syncState, bytes);
                return;
            }

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal static void OggSyncClear(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncClear(syncState);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggSyncClear(syncState);
                return;
            }

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamInitialize(IntPtr streamState, int serialNumber)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamInit(streamState, serialNumber);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggStreamInit(streamState, serialNumber);
                return;
            }

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamPageIn(IntPtr streamState, ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamPageIn(streamState, ref page);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggStreamPageIn(streamState, ref page);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool OggStreamPageOut(IntPtr streamState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggStreamPageOut(streamState, out page) != 0;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamPageOut(streamState, out page) != 0;

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void OggStreamPacketIn(IntPtr streamState, ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamPacketIn(streamState, ref packet);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggStreamPacketIn(streamState, ref packet);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool OggStreamPacketOut(IntPtr streamState, out OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggStreamPacketOut(streamState, out packet) == 1;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamPacketOut(streamState, out packet) == 1;

            throw new NotImplementedException();
        }

        internal static bool OggStreamFlush(IntPtr streamState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggStreamFlush(streamState, out page) != 0;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOggStreamFlush(streamState, out page) != 0;

            throw new NotImplementedException();
        }

        internal static void OggStreamClear(IntPtr streamState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamClear(streamState);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxOggStreamClear(streamState);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void VorbisCommentInitialize(out VorbisComment comment)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisCommentInit(out comment);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisCommentInit(out comment);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void VorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisCommentAddTag(ref comment, tag, contents);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisCommentAddTag(ref comment, tag, contents);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void VorbisCommentClear(ref VorbisComment comment)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisCommentClear(ref comment);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisCommentClear(ref comment);
                return;
            }

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void VorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisCommentHeaderOut(ref comment, out packet);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisCommentHeaderOut(ref comment, out packet);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void VorbisInfoInitialize(IntPtr info)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisInfoInit(info);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisInfoInit(info);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void VorbisInfoClear(IntPtr info)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisInfoClear(info);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisInfoClear(info);
                return;
            }

            throw new NotImplementedException();
        }

        [Pure]
        internal static bool VorbisSynthesisIdHeader(ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinVorbisSynthesisIdHeader(ref packet);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxVorbisSynthesisIdHeader(ref packet);

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void VorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisSynthesisHeaderIn(info, ref comment, ref packet);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxVorbisSynthesisHeaderIn(info, ref comment, ref packet);
                return;
            }

            throw new NotImplementedException();
        }

        [Pure]
        [DllImport(_winOggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggPageSerialNo(ref OggPage page);

        [Pure]
        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggPageSerialNo(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_winOggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        static extern bool WinOggPageEos(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        static extern bool LinuxOggPageEos(ref OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncInit(IntPtr syncState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncInit(IntPtr syncState);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncPageOut(IntPtr syncState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncPageOut(IntPtr syncState, out OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr WinOggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr LinuxOggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncWrote(IntPtr syncState, int bytes);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncWrote(IntPtr syncState, int bytes);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncClear(IntPtr syncState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggSyncClear(IntPtr syncState);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamInit(IntPtr streamState, int serialNumber);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamInit(IntPtr streamState, int serialNumber);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPageOut(IntPtr streamState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPageOut(IntPtr streamState, out OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_packetin", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPacketIn(IntPtr streamState, ref OggPacket packet);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_packetin", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPacketIn(IntPtr streamState, ref OggPacket packet);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_flush", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamFlush(IntPtr streamState, out OggPage page);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_flush", CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxOggStreamFlush(IntPtr streamState, out OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinOggStreamClear(IntPtr streamState);

        [DllImport(_linuxOggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxOggStreamClear(IntPtr streamState);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_comment_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisCommentInit(out VorbisComment comment);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentInit(out VorbisComment comment);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_add_tag",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentAddTag(ref VorbisComment comment, byte[] tag, byte[] contents);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_comment_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisCommentClear(ref VorbisComment comment);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_comment_clear",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisCommentClear(ref VorbisComment comment);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int WinVorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_commentheader_out",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxVorbisCommentHeaderOut(ref VorbisComment comment, out OggPacket packet);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisInfoInit(IntPtr info);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisInfoInit(IntPtr info);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisInfoClear(IntPtr info);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxVorbisInfoClear(IntPtr info);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        static extern bool WinVorbisSynthesisIdHeader(ref OggPacket packet);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_synthesis_idheader",
            CallingConvention = CallingConvention.Cdecl)]
        static extern bool LinuxVorbisSynthesisIdHeader(ref OggPacket packet);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int WinVorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);

        [DllImport(_linuxVorbisLibrary, EntryPoint = "vorbis_synthesis_headerin",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxVorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);
    }
}
