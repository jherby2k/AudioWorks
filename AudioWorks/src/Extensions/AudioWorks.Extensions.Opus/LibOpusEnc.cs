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
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Opus
{
#pragma warning disable CA1060
    static partial class LibOpusEnc
#pragma warning restore CA1060
    {
#if WINDOWS
        const string _opusEncLibrary = "opusenc";
#else
        const string _opusEncLibrary = "libopusenc";
#endif

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_create")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial OpusCommentsHandle CommentsCreate();

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_add")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int CommentsAdd(OpusCommentsHandle handle, ref byte tag, byte* value);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_add_picture_from_memory")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int CommentsAddPictureFromMemory(
            OpusCommentsHandle handle, [In] byte[] data, IntPtr size, int pictureType, IntPtr description);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_destroy")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void CommentsDestroy(IntPtr handle);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_create_callbacks")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial OpusEncoderHandle CreateCallbacks(
            ref OpusEncoderCallbacks callbacks,
            IntPtr userData,
            OpusCommentsHandle comments,
            int rate,
            int channels,
            int family,
            out int error);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_flush_header")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int FlushHeader(OpusEncoderHandle handle);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_write_float")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int WriteFloat(OpusEncoderHandle handle, in float pcm, int samplesPerChannel);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_drain")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int Drain(OpusEncoderHandle handle);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int ControlGet(OpusEncoderHandle handle, EncoderControlRequest request, out int value);
#if OSX

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        internal static partial int ControlGetArm64(
            OpusEncoderHandle handle,
            EncoderControlRequest request,
            IntPtr register2, IntPtr register3, IntPtr register4, IntPtr register5, IntPtr register6, IntPtr register7,
            out int value);
#endif

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int ControlSet(OpusEncoderHandle handle, EncoderControlRequest request, int argument);
#if OSX

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        internal static partial int ControlSetArm64(
            OpusEncoderHandle handle,
            EncoderControlRequest request,
            IntPtr register2, IntPtr register3, IntPtr register4, IntPtr register5, IntPtr register6, IntPtr register7,
            int argument);
#endif

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_destroy")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void Destroy(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WriteCallback(
            IntPtr userData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CloseCallback(IntPtr userData);
    }
}
