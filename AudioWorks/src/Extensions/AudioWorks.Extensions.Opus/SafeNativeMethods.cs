/* Copyright © 2019 Jeremy Herbison

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
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Opus
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if LINUX
        const string _oggLibrary = "libogg.so.0";
#else
        const string _oggLibrary = "libogg";
#endif
#if WINDOWS
        const string _opusLibrary = "opus";
        const string _opusEncLibrary = "opusenc";
#elif LINUX
        const string _opusLibrary = "libopus.so.0";
#else // MacOS
        const string _opusLibrary = "libopus";
#endif
#if !WINDOWS
        const string _opusEncLibrary = "libopusenc";
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

        [DllImport(_opusLibrary, EntryPoint = "opus_get_version_string",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OpusGetVersion();

        [DllImport(_opusEncLibrary, EntryPoint = "ope_get_version_string",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OpusEncoderGetVersion();

        [DllImport(_opusEncLibrary, EntryPoint = "ope_comments_create",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern OpusCommentsHandle OpusEncoderCommentsCreate();

        [DllImport(_opusEncLibrary, EntryPoint = "ope_comments_add",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int OpusEncoderCommentsAdd(
            [NotNull] OpusCommentsHandle handle,
            ref byte tag,
            byte* value);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_comments_add_picture_from_memory",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int OpusEncoderCommentsAddPictureFromMemory(
            [NotNull] OpusCommentsHandle handle,
            byte* data,
            IntPtr size,
            int pictureType,
            IntPtr description);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_opusEncLibrary, EntryPoint = "ope_comments_destroy",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OpusEncoderCommentsDestroy(
            IntPtr handle);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_create_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern OpusEncoderHandle OpusEncoderCreateCallbacks(
            ref OpusEncoderCallbacks callbacks,
            IntPtr userData,
            [NotNull] OpusCommentsHandle comments,
            int rate,
            int channels,
            int family,
            out int error);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_flush_header",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OpusEncoderFlushHeader(
            [NotNull] OpusEncoderHandle handle);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_write_float",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OpusEncoderWriteFloat(
            [NotNull] OpusEncoderHandle handle,
            in float pcm,
            int samplesPerChannel);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_drain",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OpusEncoderDrain(
            [NotNull] OpusEncoderHandle handle);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OpusEncoderControl(
            [NotNull] OpusEncoderHandle handle,
            EncoderControlRequest request,
            out int value);

        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int OpusEncoderControl(
            [NotNull] OpusEncoderHandle handle,
            EncoderControlRequest request,
            int argument);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_opusEncLibrary, EntryPoint = "ope_encoder_destroy",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OpusEncoderDestroy(
            IntPtr handle);
    }
}
