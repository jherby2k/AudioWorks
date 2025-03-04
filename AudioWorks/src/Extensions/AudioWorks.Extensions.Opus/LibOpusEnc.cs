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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace AudioWorks.Extensions.Opus
{
    [SuppressMessage("Design", "CA1060:Move pinvokes to native methods class",
            Justification = "Following latest native interop best practices")]
    static partial class LibOpusEnc
    {
        const string _opusEncLibrary = "opusenc";

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_create")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial OpusCommentsHandle CommentsCreate();

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_add")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial int CommentsAdd(
            OpusCommentsHandle handle,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string tag,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_add_picture_from_memory")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int CommentsAddPictureFromMemory(
            OpusCommentsHandle handle, [In] byte[] data, nint size, int pictureType, nint description);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_comments_destroy")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void CommentsDestroy(nint handle);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_create_callbacks")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial OpusEncoderHandle CreateCallbacks(
            ref OpusEncoderCallbacks callbacks,
            nint userData,
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

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int ControlGetArm64(
            OpusEncoderHandle handle,
            EncoderControlRequest request,
            nint register2, nint register3, nint register4, nint register5, nint register6, nint register7,
            out int value);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int ControlSet(OpusEncoderHandle handle, EncoderControlRequest request, int argument);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_ctl")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial int ControlSetArm64(
            OpusEncoderHandle handle,
            EncoderControlRequest request,
            nint register2, nint register3, nint register4, nint register5, nint register6, nint register7,
            int argument);

        [LibraryImport(_opusEncLibrary, EntryPoint = "ope_encoder_destroy")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void Destroy(nint handle);
    }
}
