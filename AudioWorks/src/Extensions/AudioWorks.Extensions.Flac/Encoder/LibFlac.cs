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

namespace AudioWorks.Extensions.Flac.Encoder
{
#pragma warning disable CA1060
    static partial class LibFlac
#pragma warning restore CA1060
    {
#if LINUX
        const string _flacLibrary = "libFLAC.so.8";
#else
        const string _flacLibrary = "libFLAC";
#endif

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_new")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial StreamEncoderHandle StreamEncoderNew();

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_channels")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetChannels(StreamEncoderHandle handle, uint channels);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_channels")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial uint StreamEncoderGetChannels(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_bits_per_sample")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetBitsPerSample(StreamEncoderHandle handle, uint bitsPerSample);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_bits_per_sample")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial uint StreamEncoderGetBitsPerSample(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_sample_rate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetSampleRate(StreamEncoderHandle handle, uint sampleRate);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_total_samples_estimate")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetTotalSamplesEstimate(
            StreamEncoderHandle handle, ulong totalSamples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_compression_level")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial void StreamEncoderSetCompressionLevel(
            StreamEncoderHandle handle, uint compressionLevel);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_set_metadata")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderSetMetadata(
            StreamEncoderHandle handle, [In] IntPtr[] metaData, uint blocks);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_init_stream")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int StreamEncoderInitStream(
            StreamEncoderHandle handle,
            delegate* unmanaged<IntPtr, byte*, int, uint, uint, IntPtr, EncoderWriteStatus> writeCallback,
            delegate* unmanaged<IntPtr, ulong, IntPtr, EncoderSeekStatus> seekCallback,
            delegate* unmanaged<IntPtr, ulong*, IntPtr, EncoderTellStatus> tellCallback,
            delegate* unmanaged<IntPtr, IntPtr, IntPtr, void> metadataCallback,
            IntPtr userData);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_process")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderProcess(StreamEncoderHandle handle, in IntPtr buffer, uint samples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_process_interleaved")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderProcessInterleaved(
            StreamEncoderHandle handle, in int buffer, uint samples);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_finish")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool StreamEncoderFinish(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_get_state")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial EncoderState StreamEncoderGetState(StreamEncoderHandle handle);

        [LibraryImport(_flacLibrary, EntryPoint = "FLAC__stream_encoder_delete")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void StreamEncoderDelete(IntPtr handle);
    }
}