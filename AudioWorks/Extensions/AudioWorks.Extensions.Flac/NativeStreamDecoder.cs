using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    class NativeStreamDecoder : IDisposable
    {
        [NotNull] readonly NativeStreamDecoderHandle _handle = SafeNativeMethods.StreamDecoderNew();
        [NotNull] readonly Stream _stream;

        internal NativeStreamDecoder([NotNull] FileStream stream)
        {
            _stream = stream;
        }

        internal void Initialize()
        {
            SafeNativeMethods.StreamDecoderInitializeStream(_handle,

                // Read Callback
                (IntPtr handle, byte[] buffer, ref int bytes, IntPtr userData) =>
                {
                    bytes = _stream.Read(buffer, 0, bytes);
                    return bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
                },

                // Seek Callback
                (handle, absoluteOffset, userData) =>
                {
                    _stream.Seek((long) absoluteOffset, SeekOrigin.Begin);
                    return DecoderSeekStatus.Ok;
                },

                // Tell Callback
                (IntPtr handle, out ulong absoluteOffset, IntPtr userData) =>
                {
                    absoluteOffset = (ulong) _stream.Position;
                    return DecoderTellStatus.Ok;
                },

                // Length Callback
                (IntPtr handle, out ulong streamLength, IntPtr userData) =>
                {
                    streamLength = (ulong) _stream.Length;
                    return DecoderLengthStatus.Ok;
                },

                // EOF Callback
                (handle, userData) => _stream.Position >= _stream.Length,

                // Write Callback
                (IntPtr handle, ref Frame frame, IntPtr buffer, IntPtr userData) => DecoderWriteStatus.Continue,

                // Metadata Callback is virtual
                MetadataCallback,

                // Error Callback
                (handle, error, userData) => { });
        }

        internal void ProcessMetadata()
        {
            SafeNativeMethods.StreamDecoderProcessUntilEndOfMetadata(_handle);
        }

        internal void Finish()
        {
            SafeNativeMethods.StreamDecoderFinish(_handle);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        protected virtual void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
        }
    }
}