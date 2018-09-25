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

namespace AudioWorks.Extensions.Lame
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if LINUX
        const string _lameLibrary = "libmp3lame.so.0";
#else
        const string _lameLibrary = "libmp3lame";
#endif

        [DllImport(_lameLibrary, EntryPoint = "get_lame_version",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetVersion();

        [NotNull]
        [DllImport(_lameLibrary, EntryPoint = "lame_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern EncoderHandle Init();

        [DllImport(_lameLibrary, EntryPoint = "lame_set_num_channels",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetNumChannels(
            [NotNull] EncoderHandle handle,
            int channels);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_in_samplerate",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetInSampleRate(
            [NotNull] EncoderHandle handle,
            int sampleRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_num_samples",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetNumSamples(
            [NotNull] EncoderHandle handle,
            uint samples);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_brate",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetBRate(
            [NotNull] EncoderHandle handle,
            int bitRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetVbr(
            [NotNull] EncoderHandle handle,
            VbrMode vbrMode);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR_mean_bitrate_kbps",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetVbrMeanBitRateKbps(
            [NotNull] EncoderHandle handle,
            int bitRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR_quality",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetVbrQuality(
            [NotNull] EncoderHandle handle,
            float quality);

        [DllImport(_lameLibrary, EntryPoint = "lame_init_params",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int InitParams(
            [NotNull] EncoderHandle handle);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_buffer_ieee_float",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int EncodeBufferIeeeFloat(
            [NotNull] EncoderHandle handle,
            in float leftSamples,
            in float rightSamples,
            int sampleCount,
#if NETCOREAPP2_1
            ref byte buffer,
#else
            [NotNull] [In, Out] byte[] buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_buffer_interleaved_ieee_float",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int EncodeBufferInterleavedIeeeFloat(
            [NotNull] EncoderHandle handle,
            in float samples,
            int sampleCount,
#if NETCOREAPP2_1
            ref byte buffer,
#else
            [NotNull] [In, Out] byte[] buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_flush",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int EncodeFlush(
            [NotNull] EncoderHandle handle,
#if NETCOREAPP2_1
            ref byte buffer,
#else
            [NotNull] [In, Out] byte[] buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_get_lametag_frame",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr GetLameTagFrame(
            [NotNull] EncoderHandle handle,
#if NETCOREAPP2_1
            ref byte buffer,
#else
            [NotNull] [In, Out] byte[] buffer,
#endif
            UIntPtr bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_close",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Close(
            IntPtr handle);
    }
}