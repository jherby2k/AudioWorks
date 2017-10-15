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

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
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

            throw new NotImplementedException();
        }

        internal static void StreamDecoderSetMetadataRespond(
            [NotNull] NativeStreamDecoderHandle handle,
            MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderSetMetadataRespond(handle, metadataType);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool StreamDecoderProcessUntilEndOfMetadata(
            [NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderProcessUntilEndOfMetadata(handle);

            throw new NotImplementedException();
        }

        internal static DecoderState StreamDecoderGetState(
            [NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinStreamDecoderGetState(handle);

            throw new NotImplementedException();
        }


        internal static void StreamDecoderFinish(
            [NotNull] NativeStreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinStreamDecoderFinish(handle);
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
        }

        [NotNull]
        internal static NativeMetadataBlockHandle MetadataObjectNew(
            MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataObjectNew(metadataType);

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

            throw new NotImplementedException();
        }

        internal static void MetadataObjectVorbisCommentAppendComment(
            [NotNull] NativeMetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            bool copy)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataObjectVorbisCommentAppendComment(handle, vorbisComment, copy);
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
        }

        [NotNull]
        internal static NativeMetadataChainHandle MetadataChainNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataChainNew();

            throw new NotImplementedException();
        }

        internal static void MetadataChainReadWithCallbacks(
            [NotNull] NativeMetadataChainHandle handle,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataChainReadWithCallbacks(handle, IntPtr.Zero, callbacks);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool MetadataChainCheckIfTempFileNeeded(
            [NotNull] NativeMetadataChainHandle handle,
            bool usePadding)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataChainCheckIfTempFileNeeded(handle, usePadding);

            throw new NotImplementedException();
        }

        internal static void MetadataChainWriteWithCallbacks(
            [NotNull] NativeMetadataChainHandle handle,
            bool usePadding,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataChainWriteWithCallbacks(handle, usePadding, IntPtr.Zero, callbacks);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void MetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] NativeMetadataChainHandle handle,
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

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataChainDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinMetadataChainDelete(handle);
        }

        [NotNull]
        internal static NativeMetadataIteratorHandle MetadataIteratorNew()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorNew();

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorInit(
            [NotNull] NativeMetadataIteratorHandle handle,
            [NotNull] NativeMetadataChainHandle chainHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorInit(handle, chainHandle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static bool MetadataIteratorNext(
            [NotNull] NativeMetadataIteratorHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorNext(handle);

            throw new NotImplementedException();
        }

        internal static IntPtr MetadataIteratorGetBlock(
            [NotNull] NativeMetadataIteratorHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinMetadataIteratorGetBlock(handle);

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorInsertBlockAfter(
            [NotNull] NativeMetadataIteratorHandle handle,
            [NotNull] NativeMetadataBlockHandle metadataHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorInsertBlockAfter(handle, metadataHandle);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void MetadataIteratorDeleteBlock(
            [NotNull] NativeMetadataIteratorHandle handle,
            bool replaceWithPadding)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinMetadataIteratorDeleteBlock(handle, replaceWithPadding);
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
        }

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_new", CallingConvention = CallingConvention.Cdecl)]
        static extern NativeStreamDecoderHandle WinStreamDecoderNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream", CallingConvention = CallingConvention.Cdecl)]
        static extern int WinStreamDecoderInitStream(
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

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderSetMetadataRespond(
            NativeStreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderProcessUntilEndOfMetadata(
            [NotNull] NativeStreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState WinStreamDecoderGetState(
            [NotNull] NativeStreamDecoderHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinStreamDecoderFinish(
            [NotNull] NativeStreamDecoderHandle handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinStreamDecoderDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern NativeMetadataBlockHandle WinMetadataObjectNew(
            MetadataType blockType);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataObjectVorbisCommentAppendComment(
            [NotNull] NativeMetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataObjectDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern NativeMetadataChainHandle WinMetadataChainNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainReadWithCallbacks(
            [NotNull] NativeMetadataChainHandle handle,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainCheckIfTempFileNeeded(
            [NotNull] NativeMetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainWriteWithCallbacks(
            [NotNull] NativeMetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] NativeMetadataChainHandle handle,
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

        [NotNull]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern NativeMetadataIteratorHandle WinMetadataIteratorNew();

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataIteratorInit(
            [NotNull] NativeMetadataIteratorHandle handle,
            [NotNull] NativeMetadataChainHandle chainHandle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorNext(
            [NotNull] NativeMetadataIteratorHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr WinMetadataIteratorGetBlock(
            [NotNull] NativeMetadataIteratorHandle handle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorInsertBlockAfter(
            [NotNull] NativeMetadataIteratorHandle handle,
            [NotNull] NativeMetadataBlockHandle metadataHandle);

        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WinMetadataIteratorDeleteBlock(
            [NotNull] NativeMetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinMetadataIteratorDelete(
            IntPtr handle);
    }
}