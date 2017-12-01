using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;
#if (WINDOWS)
using System.IO;
using System.Reflection;
using System.Text;
#endif

namespace AudioWorks.Extensions.Flac
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if (LINUX)
        const string _flacLibrary = "libFLAC.so.8";
#else
        const string _flacLibrary = "libFLAC";
#endif

#if (WINDOWS)
        static SafeNativeMethods()
        {
            // Select an architecture-appropriate directory by prefixing the PATH variable
            var newPath = new StringBuilder(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            newPath.Append(Path.DirectorySeparatorChar);
            newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
            newPath.Append(Path.PathSeparator);
            newPath.Append(Environment.GetEnvironmentVariable("PATH"));

            Environment.SetEnvironmentVariable("PATH", newPath.ToString());
        }
#endif

        [NotNull]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_new",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern StreamDecoderHandle StreamDecoderNew();

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StreamDecoderInitStream(
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

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_process_single",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern DecoderState StreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StreamDecoderDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_new", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MetadataBlockHandle MetadataObjectNew(
            MetadataType blockType);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataObjectVorbisCommentAppendComment(
            [NotNull] MetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            [MarshalAs(UnmanagedType.Bool)] bool copy);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MetadataObjectDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_new", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MetadataChainHandle MetadataChainNew();

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataChainReadWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataChainWriteWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding,
            IntPtr ioHandle,
            IoCallbacks callbacks,
            IntPtr tempIoHandle,
            IoCallbacks tempCallbacks);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_chain_delete",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MetadataChainDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern MetadataIteratorHandle MetadataIteratorNew();

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle);

        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_flacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MetadataIteratorDelete(
            IntPtr handle);
    }
}