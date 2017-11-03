using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
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
        const string _linuxFlacLibrary = "libFLAC.so.8";

        static SafeNativeMethods()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Select an architecture-appropriate directory by prefixing the PATH variable:
                var newPath = new StringBuilder(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
                newPath.Append(Path.DirectorySeparatorChar);
                newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
                newPath.Append(Path.PathSeparator);
                newPath.Append(Environment.GetEnvironmentVariable("PATH"));

                Environment.SetEnvironmentVariable("PATH", newPath.ToString());
            }
        }

        [NotNull]
        internal static StreamDecoderHandle StreamDecoderNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderNew();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxStreamDecoderNew();

            throw new NotImplementedException();
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal static void StreamDecoderInitializeStream(
            [NotNull] StreamDecoderHandle handle,
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
                WinStreamDecoderInitStream(
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxStreamDecoderInitStream(
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

        internal static void StreamDecoderSetMetadataRespond(
            [NotNull] StreamDecoderHandle handle,
            MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderSetMetadataRespond(handle, metadataType);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxStreamDecoderSetMetadataRespond(handle, metadataType);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool StreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderProcessUntilEndOfMetadata(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxStreamDecoderProcessUntilEndOfMetadata(handle);

            throw new NotImplementedException();
        }

        internal static bool StreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderProcessSingle(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxStreamDecoderProcessSingle(handle);

            throw new NotImplementedException();
        }

        internal static DecoderState StreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderGetState(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxStreamDecoderGetState(handle);

            throw new NotImplementedException();
        }


        internal static void StreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderFinish(handle);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxStreamDecoderFinish(handle);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void StreamDecoderDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinStreamDecoderDelete(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxStreamDecoderDelete(handle);
        }

        [NotNull]
        internal static MetadataBlockHandle MetadataObjectNew(
            MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataObjectNew(metadataType);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataObjectNew(metadataType);

            throw new NotImplementedException();
        }

        internal static void MetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataObjectVorbisCommentEntryFromNameValuePair(out vorbisComment, key, value);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataObjectVorbisCommentEntryFromNameValuePair(out vorbisComment, key, value);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void MetadataObjectVorbisCommentAppendComment(
            [NotNull] MetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            bool copy)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataObjectVorbisCommentAppendComment(handle, vorbisComment, copy);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataObjectVorbisCommentAppendComment(handle, vorbisComment, copy);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataObjectDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinMetadataObjectDelete(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataObjectDelete(handle);
        }

        [NotNull]
        internal static MetadataChainHandle MetadataChainNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataChainNew();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataChainNew();

            throw new NotImplementedException();
        }

        internal static void MetadataChainReadWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataChainReadWithCallbacks(handle, IntPtr.Zero, callbacks);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataChainReadWithCallbacks(handle, IntPtr.Zero, callbacks);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool MetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            bool usePadding)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataChainCheckIfTempFileNeeded(handle, usePadding);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataChainCheckIfTempFileNeeded(handle, usePadding);

            throw new NotImplementedException();
        }

        internal static void MetadataChainWriteWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            bool usePadding,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataChainWriteWithCallbacks(handle, usePadding, IntPtr.Zero, callbacks);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataChainWriteWithCallbacks(handle, usePadding, IntPtr.Zero, callbacks);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void MetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] MetadataChainHandle handle,
            bool usePadding,
            IoCallbacks callbacks,
            IoCallbacks tempCallbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataChainWriteWithCallbacksAndTempFile(
                    handle,
                    usePadding,
                    IntPtr.Zero,
                    callbacks,
                    IntPtr.Zero,
                    tempCallbacks);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataChainWriteWithCallbacksAndTempFile(
                    handle,
                    usePadding,
                    IntPtr.Zero,
                    callbacks,
                    IntPtr.Zero,
                    tempCallbacks);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataChainDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinMetadataChainDelete(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataChainDelete(handle);
        }

        [NotNull]
        internal static MetadataIteratorHandle MetadataIteratorNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorNew();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataIteratorNew();

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorInit(handle, chainHandle);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataIteratorInit(handle, chainHandle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool MetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorNext(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataIteratorNext(handle);

            throw new NotImplementedException();
        }

        internal static IntPtr MetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorGetBlock(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxMetadataIteratorGetBlock(handle);

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorInsertBlockAfter(handle, metadataHandle);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataIteratorInsertBlockAfter(handle, metadataHandle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            bool replaceWithPadding)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorDeleteBlock(handle, replaceWithPadding);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMetadataIteratorDeleteBlock(handle, replaceWithPadding);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataIteratorDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinMetadataIteratorDelete(handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataIteratorDelete(handle);
        }

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StreamDecoderHandle WinStreamDecoderNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StreamDecoderHandle LinuxStreamDecoderNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int WinStreamDecoderInitStream(
            [NotNull] StreamDecoderHandle handle,
            [NotNull] NativeCallbacks.StreamDecoderReadCallback readCallback,
            [NotNull] NativeCallbacks.StreamDecoderSeekCallback seekCallback,
            [NotNull] NativeCallbacks.StreamDecoderTellCallback tellCallback,
            [NotNull] NativeCallbacks.StreamDecoderLengthCallback lengthCallback,
            [NotNull] NativeCallbacks.StreamDecoderEofCallback eofCallback,
            [NotNull] NativeCallbacks.StreamDecoderWriteCallback writeCallback,
            [NotNull] NativeCallbacks.StreamDecoderMetadataCallback metadataCallback,
            [NotNull] NativeCallbacks.StreamDecoderErrorCallback errorCallback,
            IntPtr userData);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int LinuxStreamDecoderInitStream(
            [NotNull] StreamDecoderHandle handle,
            [NotNull] NativeCallbacks.StreamDecoderReadCallback readCallback,
            [NotNull] NativeCallbacks.StreamDecoderSeekCallback seekCallback,
            [NotNull] NativeCallbacks.StreamDecoderTellCallback tellCallback,
            [NotNull] NativeCallbacks.StreamDecoderLengthCallback lengthCallback,
            [NotNull] NativeCallbacks.StreamDecoderEofCallback eofCallback,
            [NotNull] NativeCallbacks.StreamDecoderWriteCallback writeCallback,
            [NotNull] NativeCallbacks.StreamDecoderMetadataCallback metadataCallback,
            [NotNull] NativeCallbacks.StreamDecoderErrorCallback errorCallback,
            IntPtr userData);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_single",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_single",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState WinStreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState LinuxStreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinStreamDecoderDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxStreamDecoderDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataBlockHandle WinMetadataObjectNew(
            MetadataType blockType);

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataBlockHandle LinuxMetadataObjectNew(
            MetadataType blockType);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataObjectVorbisCommentAppendComment(
            [NotNull] MetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataObjectVorbisCommentAppendComment(
            [NotNull] MetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataObjectDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataObjectDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataChainHandle WinMetadataChainNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataChainHandle LinuxMetadataChainNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainReadWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataChainReadWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainWriteWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataChainWriteWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks,
            IntPtr tempIoHandle,
            IoCallbacks tempCallbacks);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks,
            IntPtr tempIoHandle,
            IoCallbacks tempCallbacks);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataChainDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataChainDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataIteratorHandle WinMetadataIteratorNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataIteratorHandle LinuxMetadataIteratorNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr WinMetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr LinuxMetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataIteratorDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataIteratorDelete(
            IntPtr handle);
    }
}