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
#pragma warning disable CA1060
    static partial class CoreAudioToolbox
#pragma warning restore CA1060
    {
        const string _coreAudioLibrary = "CoreAudioToolbox";

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileOpenWithCallbacks(
            nint userData,
            AudioFileReadCallback readCallback,
            AudioFileWriteCallback? writeCallback,
            AudioFileGetSizeCallback getSizeCallback,
            AudioFileSetSizeCallback? setSizeCallback,
            AudioFileType fileType,
            out AudioFileHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileInitializeWithCallbacks(
            nint userData,
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
            nint data);

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
            nint data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioFileStatus AudioFileClose(
            nint handle);

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
            nint data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileSetProperty(
            ExtendedAudioFileHandle handle,
            ExtendedAudioFilePropertyId id,
            uint size,
            nint data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileWrite(
            ExtendedAudioFileHandle handle,
            uint frames,
            ref AudioBufferListSingle data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial ExtendedAudioFileStatus ExtAudioFileDispose(
            nint handle);

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
            nint userData,
            ref uint packetSize,
            ref AudioBufferListSingle outputData,
            [In, Out] AudioStreamPacketDescription[]? packetDescriptions);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterSetProperty(
            AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            nint data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterSetProperty(
            nint handle,
            AudioConverterPropertyId id,
            uint size,
            nint data);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial AudioConverterStatus AudioConverterDispose(nint handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileReadCallback(
            nint userData,
            long position,
            uint requestCount,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out uint actualCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileWriteCallback(
            nint userData,
            long position,
            uint requestCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out uint actualCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate long AudioFileGetSizeCallback(
            nint userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioFileStatus AudioFileSetSizeCallback(
            nint userData,
            long size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate AudioConverterStatus AudioConverterComplexInputCallback(
            nint handle,
            ref uint numberPackets,
            ref AudioBufferListSingle data,
            nint packetDescriptions,
            nint userData);
    }
}