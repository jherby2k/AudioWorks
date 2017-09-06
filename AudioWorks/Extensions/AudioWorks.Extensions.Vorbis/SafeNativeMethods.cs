using JetBrains.Annotations;
using System;
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

        static SafeNativeMethods()
        {
            // Select an architecture-appropriate directory by prefixing the PATH variable:
            var newPath = new StringBuilder(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            newPath.Append(Path.DirectorySeparatorChar);
            newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
            newPath.Append(Path.PathSeparator);
            newPath.Append(Environment.GetEnvironmentVariable("PATH"));

            Environment.SetEnvironmentVariable("PATH", newPath.ToString());
        }

        [Pure]
        internal static int OggPageGetSerialNumber(ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggPageSerialNo(ref page);

            throw new NotImplementedException();
        }

        [Pure]
        internal static bool OggPageEndOfStream(ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggPageEos(ref page);

            throw new NotImplementedException();
        }

        internal static void OggSyncInitialize(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncInit(syncState);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool OggSyncPageOut(IntPtr syncState, out OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggSyncPageOut(syncState, out page) == 1;

            throw new NotImplementedException();
        }

        internal static IntPtr OggSyncBuffer(IntPtr syncState, int size)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggSyncBuffer(syncState, size);

            throw new NotImplementedException();
        }

        internal static void OggSyncWrote(IntPtr syncState, int bytes)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncWrote(syncState, bytes);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void OggSyncClear(IntPtr syncState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggSyncClear(syncState);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void OggStreamInitialize(IntPtr streamState, int serialNumber)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamInit(streamState, serialNumber);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void OggStreamPageIn(IntPtr streamState, ref OggPage page)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamPageIn(streamState, ref page);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool OggStreamPacketOut(IntPtr streamState, out OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinOggStreamPacketOut(streamState, out packet) == 1;

            throw new NotImplementedException();
        }

        internal static void OggStreamClear(IntPtr streamState)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinOggStreamClear(streamState);
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

            throw new NotImplementedException();
        }

        internal static void VorbisCommentClear(ref VorbisComment comment)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisCommentClear(ref comment);
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

            throw new NotImplementedException();
        }

        internal static void VorbisInfoClear(IntPtr info)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisInfoClear(info);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool VorbisSynthesisIdHeader(ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinVorbisSynthesisIdHeader(ref packet);

            throw new NotImplementedException();
        }

        internal static void VorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinVorbisSynthesisHeaderIn(info, ref comment, ref packet);
                return;
            }

            throw new NotImplementedException();
        }

        [Pure]
        [DllImport(_winOggLibrary, EntryPoint = "ogg_page_serialno", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggPageSerialNo(ref OggPage page);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_winOggLibrary, EntryPoint = "ogg_page_eos", CallingConvention = CallingConvention.Cdecl)]
        static extern bool WinOggPageEos(ref OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncInit(IntPtr syncState);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_pageout", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncPageOut(IntPtr syncState, out OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_buffer", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr WinOggSyncBuffer(IntPtr syncState, int size);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_wrote", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncWrote(IntPtr syncState, int bytes);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_sync_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggSyncClear(IntPtr syncState);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_init", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamInit(IntPtr streamState, int serialNumber);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_pagein", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPageIn(IntPtr streamState, ref OggPage page);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_packetout", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinOggStreamPacketOut(IntPtr streamState, out OggPacket packet);

        [DllImport(_winOggLibrary, EntryPoint = "ogg_stream_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinOggStreamClear(IntPtr streamState);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_comment_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisCommentInit(out VorbisComment comment);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_comment_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisCommentClear(ref VorbisComment comment);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_info_init", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisInfoInit(IntPtr info);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_info_clear", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinVorbisInfoClear(IntPtr info);

        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_synthesis_headerin", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinVorbisSynthesisHeaderIn(IntPtr info, ref VorbisComment comment, ref OggPacket packet);

        [Pure]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(_winVorbisLibrary, EntryPoint = "vorbis_synthesis_idheader", CallingConvention = CallingConvention.Cdecl)]
        static extern bool WinVorbisSynthesisIdHeader(ref OggPacket packet);
    }
}
