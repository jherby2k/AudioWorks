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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    [SuppressMessage("Design", "CA1060:Move pinvokes to native methods class",
        Justification = "Following latest native interop best practices")]
    static partial class CoreAudioToolbox
    {
        const string _coreAudioLibrary = "CoreAudioToolbox";

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial AudioFileStatus AudioFileOpenWithCallbacks(
            nint userData,
            delegate* unmanaged[Cdecl]<nint, long, uint, byte*, uint*, AudioFileStatus> readCallback,
            delegate* unmanaged[Cdecl]<nint, long, uint, byte*, uint*, AudioFileStatus> writeCallback,
            delegate* unmanaged[Cdecl]<nint, long> getSizeCallback,
            delegate* unmanaged[Cdecl]<nint, long, AudioFileStatus> setSizeCallback,
            AudioFileType fileType,
            out AudioFileHandle handle);

        [LibraryImport(_coreAudioLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static unsafe partial AudioFileStatus AudioFileInitializeWithCallbacks(
            nint userData,
            delegate* unmanaged[Cdecl]<nint, long, uint, byte*, uint*, AudioFileStatus> readCallback,
            delegate* unmanaged[Cdecl]<nint, long, uint, byte*, uint*, AudioFileStatus> writeCallback,
            delegate* unmanaged[Cdecl]<nint, long> getSizeCallback,
            delegate* unmanaged[Cdecl]<nint, long, AudioFileStatus> setSizeCallback,
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
        internal static unsafe partial AudioConverterStatus AudioConverterFillComplexBuffer(
            AudioConverterHandle handle,
            delegate* unmanaged[Cdecl]<nint, uint*, AudioBufferListSingle*, nint, nint, AudioConverterStatus> inputCallback,
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
    }
}