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
        const string _standardFlacLibrary = "libFLAC";
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
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxStreamDecoderNew()
                : StandardStreamDecoderNew();
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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
            else
                StandardStreamDecoderInitStream(
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
        }

        internal static void StreamDecoderSetMetadataRespond(
            [NotNull] StreamDecoderHandle handle,
            MetadataType metadataType)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxStreamDecoderSetMetadataRespond(handle, metadataType);
            else
                StandardStreamDecoderSetMetadataRespond(handle, metadataType);
        }

        internal static bool StreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxStreamDecoderProcessUntilEndOfMetadata(handle)
                : StandardStreamDecoderProcessUntilEndOfMetadata(handle);
        }

        internal static bool StreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxStreamDecoderProcessSingle(handle)
                : StandardStreamDecoderProcessSingle(handle);
        }

        internal static DecoderState StreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxStreamDecoderGetState(handle)
                : StandardStreamDecoderGetState(handle);
        }

        internal static void StreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxStreamDecoderFinish(handle);
            else
                StandardStreamDecoderFinish(handle);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void StreamDecoderDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxStreamDecoderDelete(handle);
            else
                StandardStreamDecoderDelete(handle);
        }

        [NotNull]
        internal static MetadataBlockHandle MetadataObjectNew(
            MetadataType metadataType)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataObjectNew(metadataType)
                : StandardMetadataObjectNew(metadataType);
        }

        internal static void MetadataObjectVorbisCommentEntryFromNameValuePair(
            out VorbisCommentEntry vorbisComment,
            [NotNull] byte[] key,
            [NotNull] byte[] value)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataObjectVorbisCommentEntryFromNameValuePair(out vorbisComment, key, value);
            else
                StandardMetadataObjectVorbisCommentEntryFromNameValuePair(out vorbisComment, key, value);
        }

        internal static void MetadataObjectVorbisCommentAppendComment(
            [NotNull] MetadataBlockHandle handle,
            VorbisCommentEntry vorbisComment,
            bool copy)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataObjectVorbisCommentAppendComment(handle, vorbisComment, copy);
            else
                StandardMetadataObjectVorbisCommentAppendComment(handle, vorbisComment, copy);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataObjectDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataObjectDelete(handle);
            else
                StandardMetadataObjectDelete(handle);
        }

        [NotNull]
        internal static MetadataChainHandle MetadataChainNew()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataChainNew()
                : StandardMetadataChainNew();
        }

        internal static void MetadataChainReadWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataChainReadWithCallbacks(handle, IntPtr.Zero, callbacks);
            else
                StandardMetadataChainReadWithCallbacks(handle, IntPtr.Zero, callbacks);
        }

        internal static bool MetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            bool usePadding)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataChainCheckIfTempFileNeeded(handle, usePadding)
                : StandardMetadataChainCheckIfTempFileNeeded(handle, usePadding);
        }

        internal static void MetadataChainWriteWithCallbacks(
            [NotNull] MetadataChainHandle handle,
            bool usePadding,
            IoCallbacks callbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataChainWriteWithCallbacks(handle, usePadding, IntPtr.Zero, callbacks);
            else
                StandardMetadataChainWriteWithCallbacks(handle, usePadding, IntPtr.Zero, callbacks);
        }

        internal static void MetadataChainWriteWithCallbacksAndTempFile(
            [NotNull] MetadataChainHandle handle,
            bool usePadding,
            IoCallbacks callbacks,
            IoCallbacks tempCallbacks)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataChainWriteWithCallbacksAndTempFile(
                    handle,
                    usePadding,
                    IntPtr.Zero,
                    callbacks,
                    IntPtr.Zero,
                    tempCallbacks);
            else
                StandardMetadataChainWriteWithCallbacksAndTempFile(
                    handle,
                    usePadding,
                    IntPtr.Zero,
                    callbacks,
                    IntPtr.Zero,
                    tempCallbacks);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataChainDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataChainDelete(handle);
            else
                StandardMetadataChainDelete(handle);
        }

        [NotNull]
        internal static MetadataIteratorHandle MetadataIteratorNew()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataIteratorNew()
                : StandardMetadataIteratorNew();
        }

        internal static void MetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataIteratorInit(handle, chainHandle);
            else
                StandardMetadataIteratorInit(handle, chainHandle);
        }

        internal static bool MetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataIteratorNext(handle)
                : StandardMetadataIteratorNext(handle);
        }

        internal static IntPtr MetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? LinuxMetadataIteratorGetBlock(handle)
                : StandardMetadataIteratorGetBlock(handle);
        }

        internal static void MetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataIteratorInsertBlockAfter(handle, metadataHandle);
            else
                StandardMetadataIteratorInsertBlockAfter(handle, metadataHandle);
        }

        internal static void MetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            bool replaceWithPadding)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataIteratorDeleteBlock(handle, replaceWithPadding);
            else
                StandardMetadataIteratorDeleteBlock(handle, replaceWithPadding);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void MetadataIteratorDelete(
            IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxMetadataIteratorDelete(handle);
            else
                StandardMetadataIteratorDelete(handle);
        }

        [NotNull]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StreamDecoderHandle StandardStreamDecoderNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StreamDecoderHandle LinuxStreamDecoderNew();

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_init_stream",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int StandardStreamDecoderInitStream(
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

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardStreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_set_metadata_respond",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderSetMetadataRespond(
            StreamDecoderHandle handle,
            MetadataType metadataType);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardStreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_until_end_of_metadata",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderProcessUntilEndOfMetadata(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_single",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardStreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_process_single",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderProcessSingle(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState StandardStreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_get_state",
            CallingConvention = CallingConvention.Cdecl)]
        static extern DecoderState LinuxStreamDecoderGetState(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardStreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_finish",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxStreamDecoderFinish(
            [NotNull] StreamDecoderHandle handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardStreamDecoderDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__stream_decoder_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxStreamDecoderDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_object_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataBlockHandle StandardMetadataObjectNew(
            MetadataType blockType);

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataBlockHandle LinuxMetadataObjectNew(
            MetadataType blockType);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_entry_from_name_value_pair",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataObjectVorbisCommentEntryFromNameValuePair(
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

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_object_vorbiscomment_append_comment",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataObjectVorbisCommentAppendComment(
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
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardMetadataObjectDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_object_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataObjectDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataChainHandle StandardMetadataChainNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataChainHandle LinuxMetadataChainNew();

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_read_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataChainReadWithCallbacks(
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

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_check_if_tempfile_needed",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataChainCheckIfTempFileNeeded(
            [NotNull] MetadataChainHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool usePadding);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataChainWriteWithCallbacks(
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

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_write_with_callbacks_and_tempfile",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataChainWriteWithCallbacksAndTempFile(
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
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_chain_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardMetadataChainDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_chain_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataChainDelete(
            IntPtr handle);

        [NotNull]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataIteratorHandle StandardMetadataIteratorNew();

        [NotNull]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_new",
            CallingConvention = CallingConvention.Cdecl)]
        static extern MetadataIteratorHandle LinuxMetadataIteratorNew();

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardMetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataIteratorInit(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataChainHandle chainHandle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_next",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorNext(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr StandardMetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_get_block",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr LinuxMetadataIteratorGetBlock(
            [NotNull] MetadataIteratorHandle handle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_insert_block_after",
            CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorInsertBlockAfter(
            [NotNull] MetadataIteratorHandle handle,
            [NotNull] MetadataBlockHandle metadataHandle);

        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StandardMetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete_block"
            , CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LinuxMetadataIteratorDeleteBlock(
            [NotNull] MetadataIteratorHandle handle,
            [MarshalAs(UnmanagedType.Bool)] bool replaceWithPadding);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_standardFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void StandardMetadataIteratorDelete(
            IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxFlacLibrary, EntryPoint = "FLAC__metadata_iterator_delete",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxMetadataIteratorDelete(
            IntPtr handle);
    }
}