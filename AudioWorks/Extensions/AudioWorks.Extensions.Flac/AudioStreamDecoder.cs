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
            Samples = new SampleBuffer((int) frame.Header.Channels, (int) frame.Header.BlockSize);

            for (var channelIndex = 0; channelIndex < frame.Header.Channels; channelIndex++)
            {
                // buffer is an array of pointers to each channel
                var channelBuffer = new Span<int>(
                    Marshal.ReadIntPtr(buffer, channelIndex * Marshal.SizeOf(buffer)).ToPointer(),
                    (int)frame.Header.BlockSize);

                Samples.CopyFrom(channelIndex, channelBuffer, (int) frame.Header.BitsPerSample);
            }

            return DecoderWriteStatus.Continue;
        }
    }
}