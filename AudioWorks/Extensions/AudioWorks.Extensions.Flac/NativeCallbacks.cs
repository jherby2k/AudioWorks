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
