using JetBrains.Annotations;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
#if (WINDOWS)
using Microsoft.Win32;
using System.IO;
using System.Text;
#endif

namespace AudioWorks.Extensions.Apple
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if (OSX)
        const string _coreAudioLibrary = "CoreAudio";
#else
        const string _coreAudioLibrary = "CoreAudioToolbox";

        static SafeNativeMethods()
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
#endif

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileOpenWithCallbacks(
            IntPtr userData,
            [NotNull] NativeCallbacks.AudioFileReadCallback readCallback,
            [CanBeNull] NativeCallbacks.AudioFileWriteCallback writeCallback,
            [NotNull] NativeCallbacks.AudioFileGetSizeCallback getSizeCallback,
            [CanBeNull] NativeCallbacks.AudioFileSetSizeCallback setSizeCallback,
            AudioFileType fileType,
            [NotNull] out AudioFileHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileGetProperty(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            ref uint size,
            [Out] IntPtr data);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileGetPropertyInfo(
            [NotNull] AudioFileHandle handle,
            AudioFilePropertyId id,
            out uint dataSize,
            out uint isWritable);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileReadPackets(
            [NotNull] AudioFileHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool useCache,
            out uint numBytes,
            [NotNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioFileStatus AudioFileClose(
            IntPtr handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterNew(
            ref AudioStreamBasicDescription sourceFormat,
            ref AudioStreamBasicDescription destinationFormat,
            [NotNull] out AudioConverterHandle handle);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterFillComplexBuffer(
            [NotNull] AudioConverterHandle handle,
            [NotNull] NativeCallbacks.AudioConverterComplexInputCallback inputCallback,
            IntPtr userData,
            ref uint packetSize,
            ref AudioBufferList outputData,
            [CanBeNull] [In, Out] AudioStreamPacketDescription[] packetDescriptions);

        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterSetProperty(
            [NotNull] AudioConverterHandle handle,
            AudioConverterPropertyId id,
            uint size,
            IntPtr data);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_coreAudioLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AudioConverterStatus AudioConverterDispose(IntPtr handle);
    }
}