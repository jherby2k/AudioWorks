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

namespace AudioWorks.Extensions.Apple
{
    static partial class CoreAudioToolbox
    {
#if OSX
        const string _coreAudioLibrary = "/System/Library/Frameworks/AudioToolbox.framework/AudioToolbox";
#else
        const string _coreAudioLibrary = "CoreAudioToolbox";
#endif

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileOpenWithCallbacks(
            IntPtr userData,
            AudioFileReadCallback readCallback,
            AudioFileWriteCallback? writeCallback,
            AudioFileGetSizeCallback getSizeCallback,
            AudioFileSetSizeCallback? setSizeCallback,
            AudioFileType fileType,
            out AudioFileHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileInitializeWithCallbacks(
            IntPtr userData,
            AudioFileReadCallback readCallback,
            AudioFileWriteCallback writeCallback,
            AudioFileGetSizeCallback getSizeCallback,
            AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            ref AudioStreamBasicDescription description,
            uint flags,
            out AudioFileHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileGetProperty(
            AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileGetPropertyInfo(
            AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileReadPacketData(
            AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool useCache,
            ref uint numBytes,
            [In, Out] AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileClose(
            IntPtr handle);

        [LibraryImport(_coreAudioLibrary, EntryPoint = "ExtAudioFileWrapAudioFileID")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileWrapAudioFile(
            AudioFileHandle audioFileHandle,
            [MarshalAs(UnmanagedType.Bool)] bool forWriting,
            out ExtendedAudioFileHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileGetProperty(
            ExtendedAudioFileHandle handle,
            ExtendedAudioFilePropertyId id,
            ref uint size,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileSetProperty(
            ExtendedAudioFileHandle handle,
            ExtendedAudioFilePropertyId id,
            uint size,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileWrite(
            ExtendedAudioFileHandle handle,
            uint frames,
            ref AudioBufferListSingle data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileDispose(
            IntPtr handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            out AudioConverterHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterFillComplexBuffer(
            AudioConverterHandle handle,
            AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferListSingle outputData,
            [In, Out] AudioStreamPacketDescription[]? packetDescriptions);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterSetProperty(
            AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterSetProperty(
            IntPtr handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterDispose(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileReadCallback(
            IntPtr userData,
            long position,
            uint requestCount,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out uint actualCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileWriteCallback(
            IntPtr userData,
            long position,
            uint requestCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
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
            ref AudioBufferListSingle data,
            IntPtr packetDescriptions,
            IntPtr userData);
    }
}