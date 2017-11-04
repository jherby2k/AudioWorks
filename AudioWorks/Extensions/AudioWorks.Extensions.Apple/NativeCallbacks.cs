using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    static class NativeCallbacks
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileReadCallback(
            IntPtr userData,
            long position,
            uint requestCount,
            [NotNull] [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out uint actualCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileWriteCallback(
            IntPtr userData,
            long position,
            uint requestCount,
            [NotNull] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out uint actualCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate long AudioFileGetSizeCallback(
            IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileSetSizeCallback(
            IntPtr userData,
            long size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioConverterStatus AudioConverterComplexInputCallback(
            IntPtr handle,
            ref uint numberPackets,
            ref AudioBufferList data,
            IntPtr packetDescriptions,
            IntPtr userData);
    }
}
