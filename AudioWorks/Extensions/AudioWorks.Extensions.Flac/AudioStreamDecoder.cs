using System;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class AudioStreamDecoder : AudioInfoStreamDecoder
    {
        float _divisor;

        [CanBeNull]
        internal SampleCollection Samples { get; set; }

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
            // Initialize the divisor
            if (_divisor < 1)
                _divisor = (float) Math.Pow(2, frame.Header.BitsPerSample - 1);

            Samples = new SampleCollection((int) frame.Header.Channels, (int) frame.Header.BlockSize);

            for (var channelIndex = 0; channelIndex < frame.Header.Channels; channelIndex++)
            {
                // buffer is an array of pointers to each channel
                var channelBuffer = new Span<int>(
                    Marshal.ReadIntPtr(buffer, channelIndex * Marshal.SizeOf(buffer)).ToPointer(),
                    (int)frame.Header.BlockSize);

                // Convert to floating point values
                var channel = Samples.GetChannel(channelIndex);
                for (var frameIndex = 0; frameIndex < (int)frame.Header.BlockSize; frameIndex++)
                    channel[frameIndex] = channelBuffer[frameIndex] / _divisor;
            }

            return DecoderWriteStatus.Continue;
        }
    }
}