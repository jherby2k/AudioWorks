using JetBrains.Annotations;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace AudioWorks.Extensions.Apple
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _winCoreAudioLibrary = "CoreAudioToolbox";
        const string _macCoreAudioLibrary = "CoreAudio";

        static SafeNativeMethods()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prefix the PATH variable with the Apple Application Support installation directory
                var newPath = new StringBuilder(Registry.LocalMachine.OpenSubKey("SOFTWARE")?
                                                    .OpenSubKey("Apple Inc.")?
                                                    .OpenSubKey("Apple Application Support")?
                                                    .GetValue("InstallDir")?.ToString() ?? string.Empty);
                newPath.Append(Path.PathSeparator);
                newPath.Append(Environment.GetEnvironmentVariable("PATH"));

                Environment.SetEnvironmentVariable("PATH", newPath.ToString());
            }
        }

        internal static void AudioFileOpenWithCallbacks(
            IntPtr userData,
            [NotNull] NativeCallbacks.AudioFileReadCallback readCallback,
            [CanBeNull] NativeCallbacks.AudioFileWriteCallback writeCallback,
            [NotNull] NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            [CanBeNull] NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            [NotNull] out AudioFileHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioFileOpenWithCallbacks(userData, readCallback, writeCallback, getSizeCallback, setSizeCallback,
                    fileType, out handle);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioFileOpenWithCallbacks(userData, readCallback, writeCallback, getSizeCallback, setSizeCallback,
                    fileType, out handle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void AudioFileGetProperty(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioFileGetProperty(handle, id, ref size, data);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioFileGetProperty(handle, id, ref size, data);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void AudioFileGetPropertyInfo(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioFileGetPropertyInfo(handle, id, out dataSize, out isWritable);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioFileGetPropertyInfo(handle, id, out dataSize, out isWritable);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void AudioFileReadPackets(
            [NotNull] AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)]bool useCache,
            out uint numBytes,
            [NotNull] [In, Out]AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioFileReadPackets(handle, useCache, out numBytes, packetDescriptions, startingPacket, ref packets,
                    data);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioFileReadPackets(handle, useCache, out numBytes, packetDescriptions, startingPacket, ref packets,
                    data);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void AudioFileClose(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinAudioFileClose(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                MacAudioFileClose(handle);
        }

        internal static void AudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            [NotNull] out AudioConverterHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioConverterNew(ref sourceFormat, ref destinationFormat, out handle);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioConverterNew(ref sourceFormat, ref destinationFormat, out handle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void AudioConverterFillComplexBuffer(
            [NotNull] AudioConverterHandle handle,
            [NotNull] NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [CanBeNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioConverterFillComplexBuffer(handle, inputCallback, userData, ref packetSize, ref outputData,
                    packetDescriptions);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioConverterFillComplexBuffer(handle, inputCallback, userData, ref packetSize, ref outputData,
                    packetDescriptions);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void AudioConverterSetProperty(
            [NotNull] AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAudioConverterSetProperty(handle, id, size, data);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MacAudioConverterSetProperty(handle, id, size, data);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void AudioConverterDispose(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinAudioConverterDispose(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                MacAudioConverterDispose(handle);
        }

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioFileOpenWithCallbacks",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileOpenWithCallbacks(
            IntPtr userData,
            [NotNull] NativeCallbacks.AudioFileReadCallback readCallback,
            [CanBeNull] NativeCallbacks.AudioFileWriteCallback writeCallback,
            [NotNull] NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            [CanBeNull] NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            [NotNull] out AudioFileHandle handle);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioFileOpenWithCallbacks",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus MacAudioFileOpenWithCallbacks(
            IntPtr userData,
            [NotNull] NativeCallbacks.AudioFileReadCallback readCallback,
            [CanBeNull] NativeCallbacks.AudioFileWriteCallback writeCallback,
            [NotNull] NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            [CanBeNull] NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            [NotNull] out AudioFileHandle handle);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioFileGetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileGetProperty(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioFileGetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus MacAudioFileGetProperty(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioFileGetPropertyInfo",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileGetPropertyInfo(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioFileGetPropertyInfo",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus MacAudioFileGetPropertyInfo(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioFileReadPackets",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileReadPackets(
            [NotNull] AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)]bool useCache,
            out uint numBytes,
            [NotNull] [In, Out]AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioFileReadPackets",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus MacAudioFileReadPackets(
            [NotNull] AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)]bool useCache,
            out uint numBytes,
            [NotNull] [In, Out]AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioFileClose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileClose(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioFileClose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus MacAudioFileClose(
            IntPtr handle);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioConverterNew",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            [NotNull] out AudioConverterHandle handle);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioConverterNew",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus MacAudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            [NotNull] out AudioConverterHandle handle);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioConverterFillComplexBuffer",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterFillComplexBuffer(
            [NotNull] AudioConverterHandle handle,
            [NotNull] NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [CanBeNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioConverterFillComplexBuffer",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus MacAudioConverterFillComplexBuffer(
            [NotNull] AudioConverterHandle handle,
            [NotNull] NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [CanBeNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions);

        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioConverterSetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterSetProperty(
            [NotNull] AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioConverterSetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus MacAudioConverterSetProperty(
            [NotNull] AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winCoreAudioLibrary, EntryPoint = "AudioConverterDispose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterDispose(IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_macCoreAudioLibrary, EntryPoint = "AudioConverterDispose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus MacAudioConverterDispose(IntPtr handle);
    }
}