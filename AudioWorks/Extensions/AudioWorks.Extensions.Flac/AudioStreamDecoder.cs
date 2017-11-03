using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    sealed class AudioStreamDecoder : AudioInfoStreamDecoder
    {
        float _divisor;
        int[][] _managedBuffer;

        [CanBeNull]
        public SampleCollection Samples { get; set; }

        internal AudioStreamDecoder([NotNull] Stream stream)
            : base(stream)
        {
        }

        public bool ProcessSingle()
        {
            return SafeNativeMethods.StreamDecoderProcessSingle(Handle);
        }

        protected override DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer,
            IntPtr userData)
        {
            // Initialize the divisor:
            if (_divisor < 1)
                _divisor = (float) Math.Pow(2, frame.Header.BitsPerSample - 1);

            // Initialize the output buffer:
            if (_managedBuffer == null)
            {
                _managedBuffer = new int[frame.Header.Channels][];
                for (var channelIndex = 0; channelIndex < frame.Header.Channels; channelIndex++)
                    _managedBuffer[channelIndex] = new int[frame.Header.BlockSize];
            }

            // Copy the samples from unmanaged memory into the output buffer:
            for (var channelIndex = 0; channelIndex < frame.Header.Channels; channelIndex++)
            {
                var channelPtr = Marshal.ReadIntPtr(buffer, channelIndex * Marshal.SizeOf(buffer));
                Marshal.Copy(channelPtr, _managedBuffer[channelIndex], 0, (int) frame.Header.BlockSize);
            }

            Samples = new SampleCollection((int) frame.Header.Channels, (int) frame.Header.BlockSize);

            // Copy the output buffer into a new sample block, converting to floating point values:
            for (var channelIndex = 0; channelIndex < (int) frame.Header.Channels; channelIndex++)
            for (var frameIndex = 0; frameIndex < (int) frame.Header.BlockSize; frameIndex++)
                Samples[channelIndex][frameIndex] = _managedBuffer[channelIndex][frameIndex] / _divisor;

            return DecoderWriteStatus.Continue;
        }
    }
}