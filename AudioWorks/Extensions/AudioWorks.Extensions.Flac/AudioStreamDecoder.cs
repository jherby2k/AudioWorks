using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    sealed class AudioStreamDecoder : AudioInfoStreamDecoder
    {
        float _divisor;
        [CanBeNull] int[] _buffer;

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
            // Initialize the divisor
            if (_divisor < 1)
                _divisor = (float) Math.Pow(2, frame.Header.BitsPerSample - 1);

            // Initialize the output buffer
            if (_buffer == null)
                _buffer = new int[frame.Header.BlockSize];

            Samples = new SampleCollection((int)frame.Header.Channels, (int)frame.Header.BlockSize);

            for (var channelIndex = 0; channelIndex < frame.Header.Channels; channelIndex++)
            {
                // Copy the samples for each channel from unmanaged memory into the output buffer
                var channelPtr = Marshal.ReadIntPtr(buffer, channelIndex * Marshal.SizeOf(buffer));
                Marshal.Copy(channelPtr, _buffer, 0, (int) frame.Header.BlockSize);

                // Convert to floating point values
                for (var frameIndex = 0; frameIndex < (int)frame.Header.BlockSize; frameIndex++)
                    Samples[channelIndex][frameIndex] = _buffer[frameIndex] / _divisor;
            }

            return DecoderWriteStatus.Continue;
        }
    }
}