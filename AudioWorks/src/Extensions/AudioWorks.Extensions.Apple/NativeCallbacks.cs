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
using JetBrains.Annotations;

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
