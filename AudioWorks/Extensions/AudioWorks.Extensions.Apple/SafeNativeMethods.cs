using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _winCoreAudioToolboxLibrary = "CoreAudioToolbox.dll";

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

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void AudioFileClose(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinAudioFileClose(handle);
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

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void AudioConverterDispose(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinAudioConverterDispose(handle);
        }

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioFileOpenWithCallbacks",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileOpenWithCallbacks(
            IntPtr userData,
            [NotNull] NativeCallbacks.AudioFileReadCallback readCallback,
            [CanBeNull] NativeCallbacks.AudioFileWriteCallback writeCallback,
            [NotNull] NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            [CanBeNull] NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            [NotNull] out AudioFileHandle handle);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioFileGetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileGetProperty(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioFileGetPropertyInfo",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileGetPropertyInfo(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioFileReadPackets",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileReadPackets(
            [NotNull] AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)]bool useCache,
            out uint numBytes,
            [NotNull] [In, Out]AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioFileClose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioFileStatus WinAudioFileClose(
            IntPtr handle);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioConverterNew",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            [NotNull] out AudioConverterHandle handle);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioConverterFillComplexBuffer",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterFillComplexBuffer(
            [NotNull] AudioConverterHandle handle,
            [NotNull] NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [CanBeNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions);

        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioConverterSetProperty",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterSetProperty(
            [NotNull] AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winCoreAudioToolboxLibrary, EntryPoint = "AudioConverterDispose",
            CallingConvention = CallingConvention.Cdecl)]
        static extern AudioConverterStatus WinAudioConverterDispose(IntPtr handle);
    }
}