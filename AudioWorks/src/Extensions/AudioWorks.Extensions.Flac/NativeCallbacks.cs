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
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    static class NativeCallbacks
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderReadStatus StreamDecoderReadCallback(
            IntPtr handle,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            ref int bytes,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderSeekStatus StreamDecoderSeekCallback(
            IntPtr handle,
            ulong absoluteOffset,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderTellStatus StreamDecoderTellCallback(
            IntPtr handle,
            out ulong absoluteOffset,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderLengthStatus StreamDecoderLengthCallback(
            IntPtr handle,
            out ulong streamLength,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool StreamDecoderEofCallback(
            IntPtr handle,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate DecoderWriteStatus StreamDecoderWriteCallback(
            IntPtr handle,
            ref Frame frame,
            IntPtr buffer,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamDecoderMetadataCallback(
            IntPtr handle,
            IntPtr metadataBlock,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamDecoderErrorCallback(
            IntPtr handle,
            DecoderErrorStatus error,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate EncoderWriteStatus StreamEncoderWriteCallback(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            int bytes,
            uint samples,
            uint currentFrame,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate EncoderSeekStatus StreamEncoderSeekCallback(
            IntPtr handle,
            ulong absoluteOffset,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate EncoderTellStatus StreamEncoderTellCallback(
            IntPtr handle,
            out ulong absoluteOffset,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StreamEncoderMetadataCallback(
            IntPtr handle,
            IntPtr metaData,
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr IoCallbacksReadCallback(
            IntPtr readBuffer,
            IntPtr bufferSize,
            IntPtr numberOfRecords,
            IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr IoCallbacksWriteCallback(
            IntPtr writeBuffer,
            IntPtr bufferSize,
            IntPtr numberOfRecords,
            IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IoCallbacksSeekCallback(
            IntPtr handle,
            long offset,
            SeekOrigin whence);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate long IoCallbacksTellCallback(
            IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IoCallbacksEofCallback(
            IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IoCallbacksCloseCallback(
            IntPtr handle);
    }
}
