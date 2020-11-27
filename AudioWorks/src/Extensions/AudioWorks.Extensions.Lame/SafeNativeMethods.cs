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
using System.Security;

namespace AudioWorks.Extensions.Lame
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if WINDOWS
        const string _lameLibrary = "lame";
#elif LINUX
        const string _lameLibrary = "libmp3lame.so.0";
#else
        const string _lameLibrary = "libmp3lame";
#endif

        [DllImport(_lameLibrary, EntryPoint = "get_lame_version",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern IntPtr GetVersion();

        [DllImport(_lameLibrary, EntryPoint = "lame_init",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern EncoderHandle Init();

        [DllImport(_lameLibrary, EntryPoint = "lame_set_num_channels",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetNumChannels(
            EncoderHandle handle,
            int channels);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_in_samplerate",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetInSampleRate(
            EncoderHandle handle,
            int sampleRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_num_samples",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetNumSamples(
            EncoderHandle handle,
            uint samples);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_brate",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetBRate(
            EncoderHandle handle,
            int bitRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetVbr(
            EncoderHandle handle,
            VbrMode vbrMode);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR_mean_bitrate_kbps",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetVbrMeanBitRateKbps(
            EncoderHandle handle,
            int bitRate);

        [DllImport(_lameLibrary, EntryPoint = "lame_set_VBR_quality",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void SetVbrQuality(
            EncoderHandle handle,
            float quality);

        [DllImport(_lameLibrary, EntryPoint = "lame_init_params",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern int InitParams(
            EncoderHandle handle);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_buffer_ieee_float",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern int EncodeBufferIeeeFloat(
            EncoderHandle handle,
            in float leftSamples,
            in float rightSamples,
            int sampleCount,
#if NETSTANDARD2_0
            [In, Out] byte[] buffer,
#else
            ref byte buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_buffer_interleaved_ieee_float",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern int EncodeBufferInterleavedIeeeFloat(
            EncoderHandle handle,
            in float samples,
            int sampleCount,
#if NETSTANDARD2_0
            [In, Out] byte[] buffer,
#else
            ref byte buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_encode_flush",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern int EncodeFlush(
            EncoderHandle handle,
#if NETSTANDARD2_0
            [In, Out] byte[] buffer,
#else
            ref byte buffer,
#endif
            int bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_get_lametag_frame",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern UIntPtr GetLameTagFrame(
            EncoderHandle handle,
#if NETSTANDARD2_0
            [In, Out] byte[] buffer,
#else
            ref byte buffer,
#endif
            UIntPtr bufferSize);

        [DllImport(_lameLibrary, EntryPoint = "lame_close",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
#if NETSTANDARD2_0
        [DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)]
#else
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
#endif
        internal static extern void Close(
            IntPtr handle);
    }
}