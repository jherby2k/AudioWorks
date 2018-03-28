using System;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class AudioStreamDecoder : AudioInfoStreamDecoder
    {
        internal SampleBuffer Samples { get; set; }

        internal AudioStreamDecoder([NotNull] Stream stream)
            : base(stream)
        {
        }

        internal bool ProcessSingle()
        {
            return SafeNativeMethods.StreamDecoderProcessSingle(Handle);
        }

        protected override unsafe DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer,
            IntPtr userData)
        {
            if (frame.Header.Channels == 1)
                Samples = new SampleBuffer(
                    new Span<int>(
                        Marshal.ReadIntPtr(buffer).ToPointer(),
                        (int) frame.Header.BlockSize),
                    (int) frame.Header.BitsPerSample);
            else
                Samples = new SampleBuffer(
                    new Span<int>(
                        Marshal.ReadIntPtr(buffer).ToPointer(),
                        (int) frame.Header.BlockSize),
                    new Span<int>(
                        Marshal.ReadIntPtr(buffer, Marshal.SizeOf<IntPtr>()).ToPointer(),
                        (int) frame.Header.BlockSize),
                    (int) frame.Header.BitsPerSample);

            return DecoderWriteStatus.Continue;
        }
    }
}