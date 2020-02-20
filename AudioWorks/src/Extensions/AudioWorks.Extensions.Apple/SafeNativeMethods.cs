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
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace AudioWorks.Extensions.Apple
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if OSX
        const string _coreAudioLibrary = "/System/Library/Frameworks/CoreAudio.framework/Versions/A/CoreAudio";
#else
        const string _coreAudioLibrary = "CoreAudioToolbox";
#endif

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileOpenWithCallbacks(
            IntPtr userData,
            NativeCallbacks.AudioFileReadCallback readCallback,
            NativeCallbacks.AudioFileWriteCallback? writeCallback,
            NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            NativeCallbacks.AudioFileSetSizeCallback? setSizeCallback,
            AudioFileType fileType,
            out AudioFileHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileInitializeWithCallbacks(
            IntPtr userData,
            NativeCallbacks.AudioFileReadCallback readCallback,
            NativeCallbacks.AudioFileWriteCallback writeCallback,
            NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            ref AudioStreamBasicDescription description,
            uint flags,
            out AudioFileHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileGetProperty(
            AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileGetPropertyInfo(
            AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileReadPackets(
            AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool useCache,
            out uint numBytes,
            [In, Out] AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileClose(
            IntPtr handle);

        [DllImport(_coreAudioLibrary, EntryPoint = "ExtAudioFileWrapAudioFileID",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern ExtendedAudioFileStatus ExtAudioFileWrapAudioFile(
            AudioFileHandle audioFileHandle,
            [MarshalAs(UnmanagedType.Bool)] bool forWriting,
            out ExtendedAudioFileHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ExtendedAudioFileStatus ExtAudioFileGetProperty(
            ExtendedAudioFileHandle handle,
            ExtendedAudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ExtendedAudioFileStatus ExtAudioFileSetProperty(
            ExtendedAudioFileHandle handle,
            ExtendedAudioFilePropertyId id,
            uint size,
            IntPtr data);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ExtendedAudioFileStatus ExtAudioFileWrite(
            ExtendedAudioFileHandle handle,
            uint frames,
            ref AudioBufferList data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ExtendedAudioFileStatus ExtAudioFileDispose(
            IntPtr handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            out AudioConverterHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterFillComplexBuffer(
            AudioConverterHandle handle,
            NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [In, Out] AudioStreamPacketDescription[]? packetDescriptions);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterSetProperty(
            AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterSetProperty(
            IntPtr handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterDispose(IntPtr handle);
    }
}