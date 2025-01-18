/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Opus
{
    sealed class Encoder : IDisposable
    {
#pragma warning disable CA2213 // Disposable fields should be disposed
        readonly Stream _outputStream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        readonly int _channels;
        readonly int _totalSeconds;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly OpusEncoderCallbacks _callbacks;
        GCHandle _outputStreamHandle;
        readonly OpusEncoderHandle _encoderHandle;
        int _requestedBitRate;
        bool _headersFlushed;

        internal Encoder(
            Stream stream,
            int sampleRate,
            int channels,
            int totalSeconds,
            OpusCommentsHandle comments)
        {
            _outputStream = stream;
            _channels = channels;
            _totalSeconds = totalSeconds;
            _callbacks = InitializeCallbacks();

            // The callbacks have to be static, so pass the output stream through as userData
            _outputStreamHandle = GCHandle.Alloc(_outputStream);

            _encoderHandle = LibOpusEnc.CreateCallbacks(
                ref _callbacks,
                GCHandle.ToIntPtr(_outputStreamHandle),
                comments,
                sampleRate,
                channels,
                0,
                out var error);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' during initialization.");
        }

        internal void SetSerialNumber(int serialNumber)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetSerialNumber, serialNumber);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the serial number.");
        }

        internal void SetHeaderGain(int gain)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetHeaderGain, gain);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the gain.");
        }

        internal void SetLsbDepth(int depth)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetLsbDepth, depth);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the LSB depth.");
        }

        internal void SetVbrConstraint(bool enabled)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetVbrConstraint, enabled ? 1 : 0);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting VBR constraint.");
        }

        internal void SetVbr(bool enabled)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetVbr, enabled ? 1 : 0);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting VBR.");
        }

        internal void SetBitRate(int bitRate)
        {
            // Cache this value for determining pre-allocation
            _requestedBitRate = bitRate * 1000;

            var error = OpusEncoderControlSet(EncoderControlRequest.SetBitRate, _requestedBitRate);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the bit rate.");
        }

        internal void SetSignal(SignalType signal)
        {
            var error = OpusEncoderControlSet(EncoderControlRequest.SetSignal, (int) signal);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the complexity.");
        }

        internal void Write(ReadOnlySpan<float> interleavedSamples)
        {
            if (!_headersFlushed) FlushHeaders();

            var error = LibOpusEnc.WriteFloat(
                _encoderHandle,
                MemoryMarshal.GetReference(interleavedSamples),
                interleavedSamples.Length / _channels);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' while encoding.");
        }

        internal void Drain()
        {
            var error = LibOpusEnc.Drain(_encoderHandle);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' draining the encoder.");

            // The pre-allocation was based on an estimated bitrate
            _outputStream.SetLength(_outputStream.Position);
        }

        public void Dispose()
        {
            _outputStreamHandle.Free();
            _encoderHandle.Dispose();
        }

        static unsafe OpusEncoderCallbacks InitializeCallbacks() => new()
        {
            Write = &WriteCallback,
            // Leave the stream open
            Close = &CloseCallback
        };

        [UnmanagedCallersOnly]
        static unsafe int WriteCallback(IntPtr userData, byte* buffer, int length)
        {
            var outputStream = (Stream) GCHandle.FromIntPtr(userData).Target!;
            outputStream.Write(new(buffer, length));
            return 0;
        }

        [UnmanagedCallersOnly]
        static int CloseCallback(IntPtr userData) => 0;

        void FlushHeaders()
        {
            var error = LibOpusEnc.FlushHeader(_encoderHandle);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' flushing the header.");

            if (_requestedBitRate == 0)
            {
                // If the bit rate isn't explicit, get the automatic value
                error = OpusEncoderControlGet(EncoderControlRequest.GetBitRate, out _requestedBitRate);
                if (error != 0)
                    throw new AudioEncodingException($"Opus encountered error '{error}' getting the bit rate.");
            }

            // Pre-allocate the whole stream (estimate worst case bit rate, plus the header)
            _outputStream.SetLength((_requestedBitRate + 25000) / 8 * _totalSeconds + _outputStream.Position);

            _headersFlushed = true;
        }

        int OpusEncoderControlGet(EncoderControlRequest request, out int value) =>
#if OSX
            // HACK ope_encoder_ctl needs the variadic argument pushed to the stack on ARM64
            RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                ? LibOpusEnc.ControlGetArm64(_handle, request,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, out value)
                : LibOpusEnc.ControlGet(_handle, request, out value);
#else
            LibOpusEnc.ControlGet(_encoderHandle, request, out value);
#endif

        int OpusEncoderControlSet(EncoderControlRequest request, int argument) =>
#if OSX
            // HACK ope_encoder_ctl needs the variadic argument pushed to the stack on ARM64
            RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                ? LibOpusEnc.ControlSetArm64(_handle, request,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, argument)
                : LibOpusEnc.ControlSet(_handle, request, argument);
#else
            LibOpusEnc.ControlSet(_encoderHandle, request, argument);
#endif
    }
}
