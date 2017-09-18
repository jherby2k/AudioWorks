using JetBrains.Annotations;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace AudioWorks.Extensions.Flac
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _winFlacLibrary = "libFLAC.dll";

        static SafeNativeMethods()
        {
            // Select an architecture-appropriate directory by prefixing the PATH variable:
            var newPath = new StringBuilder(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            newPath.Append(Path.DirectorySeparatorChar);
            newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
            newPath.Append(Path.PathSeparator);
            newPath.Append(Environment.GetEnvironmentVariable("PATH"));

            Environment.SetEnvironmentVariable("PATH", newPath.ToString());
        }

        [NotNull]
        internal static NativeStreamDecoderHandle StreamDecoderNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderNew();

            throw new NotImplementedException();
        }

        internal static void StreamDecoderInitializeStream(
            [NotNull] NativeStreamDecoderHandle handle,
            [NotNull] NativeCallbacks.StreamDecoderReadCallback readCallback,
            [NotNull] NativeCallbacks.StreamDecoderSeekCallback seekCallback,
            [NotNull] NativeCallbacks.StreamDecoderTellCallback tellCallback,
            [NotNull] NativeCallbacks.StreamDecoderLengthCallback lengthCallback,
            [NotNull] NativeCallbacks.StreamDecoderEofCallback eofCallback,
            [NotNull] NativeCallbacks.StreamDecoderWriteCallback writeCallback,
            [NotNull] NativeCallbacks.StreamDecoderMetadataCallback metadataCallback,
            [NotNull] NativeCallbacks.StreamDecoderErrorCallback errorCallback)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderInitializeStream(
                    handle,
                    readCallback,
                    seekCallback,
                    tellCallback,
                    lengthCallback,
                    eofCallback,
                    writeCallback,
                    metadataCallback,
                    errorCallback,
                    IntPtr.Zero);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void StreamDecoderSetMetadataRespond([NotNull] NativeStreamDecoderHandle handle, MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderSetMetadataRespond(handle, metadataType);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool StreamDecoderProcessUntilEndOfMetadata([NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderProcessUntilEndOfMetadata(handle);

            throw new NotImplementedException();
        }

        internal static DecoderState StreamDecoderGetState([NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderGetState(handle);

            throw new NotImplementedException();
        }


        internal static void StreamDecoderFinish([NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderFinish(handle);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void StreamDecoderDelete(IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinStreamDecoderDelete(handle);
        }

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_new", CallingConvention = CallingConvention.Cdecl)]
        static extern NativeStreamDecoderHandle WinStreamDecoderNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinStreamDecoderInitializeStream(
            [NotNull] NativeStreamDecoderHandle handle,
            [NotNull] NativeCallbacks.StreamDecoderReadCallback readCallback,
            [NotNull] NativeCallbacks.StreamDecoderSeekCallback seekCallback,
            [NotNull] NativeCallbacks.StreamDecoderTellCallback tellCallback,
            [NotNull] NativeCallbacks.StreamDecoderLengthCallback lengthCallback,
            [NotNull] NativeCallbacks.StreamDecoderEofCallback eofCallback,
            [NotNull] NativeCallbacks.StreamDecoderWriteCallback writeCallback,
            [NotNull] NativeCallbacks.StreamDecoderMetadataCallback metadataCallback,
            [NotNull] NativeCallbacks.StreamDecoderErrorCallback errorCallback,
            IntPtr userData);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderSetMetadataRespond(NativeStreamDecoderHandle handle, MetadataType metadataType);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderProcessUntilEndOfMetadata([NotNull] NativeStreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state", CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState WinStreamDecoderGetState([NotNull] NativeStreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderFinish([NotNull] NativeStreamDecoderHandle handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete", CallingConvention = CallingConvention.Cdecl)]
        static extern void WinStreamDecoderDelete(IntPtr handle);
    }
}