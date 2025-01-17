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

namespace AudioWorks.Extensions.Lame
{
    static partial class LibMp3Lame
    {
#if LINUX
        const string _lameLibrary = "libmp3lame.so.0";
#else
        const string _lameLibrary = "libmp3lame";
#endif

        [LibraryImport(_lameLibrary, EntryPoint = "get_lame_version")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial IntPtr GetVersion();

        [LibraryImport(_lameLibrary, EntryPoint = "lame_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial EncoderHandle Init();

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_num_channels")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetNumChannels(EncoderHandle handle, int channels);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_in_samplerate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetInSampleRate(EncoderHandle handle, int sampleRate);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_num_samples")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetNumSamples(EncoderHandle handle, uint samples);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_brate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetBRate(EncoderHandle handle, int bitRate);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_VBR")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetVbr(EncoderHandle handle, VbrMode vbrMode);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_VBR_mean_bitrate_kbps")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetVbrMeanBitRateKbps(EncoderHandle handle, int bitRate);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_set_VBR_quality")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void SetVbrQuality(EncoderHandle handle, float quality);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_init_params")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int InitParams(EncoderHandle handle);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_encode_buffer_ieee_float")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int EncodeBufferIeeeFloat(
            EncoderHandle handle, in float leftSamples, in float rightSamples, int sampleCount, ref byte buffer, int bufferSize);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_encode_buffer_interleaved_ieee_float")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int EncodeBufferInterleavedIeeeFloat(
            EncoderHandle handle, in float samples, int sampleCount, ref byte buffer, int bufferSize);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_encode_flush")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int EncodeFlush(EncoderHandle handle, ref byte buffer, int bufferSize);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_get_lametag_frame")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial UIntPtr GetLameTagFrame(EncoderHandle handle, ref byte buffer, UIntPtr bufferSize);

        [LibraryImport(_lameLibrary, EntryPoint = "lame_close")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void Close(IntPtr handle);
    }
}