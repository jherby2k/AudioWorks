/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Opus
{
    sealed class Encoder : IDisposable
    {
        readonly int _channels;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly OpusEncoderCallbacks _callbacks;
        [NotNull] readonly OpusEncoderHandle _handle;

        internal Encoder([NotNull] Stream stream, int sampleRate, int channels, [NotNull] OpusCommentsHandle comments)
        {
            _channels = channels;
            _callbacks = InitializeCallbacks(stream);
            _handle = SafeNativeMethods.OpusEncoderCreateCallbacks(
                ref _callbacks,
                IntPtr.Zero,
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
            var error = SafeNativeMethods.OpusEncoderControl(_handle,
                EncoderControlRequest.SetSerialNumber, serialNumber);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the serial number.");
        }

        internal void SetVbrConstraint(bool enabled)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.SetVbrConstraint,
                enabled ? 1 : 0);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting VBR constraint.");
        }

        internal void SetVbr(bool enabled)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.SetVbr, enabled ? 1 : 0);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting VBR.");
        }

        internal void SetBitRate(int bitRate)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.SetBitRate, bitRate * 1000);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the bit rate.");
        }

        internal void SetSignal(SignalType signal)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.SetSignal, (int) signal);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the complexity.");
        }

        internal void Write(ReadOnlySpan<float> interleavedSamples)
        {
            var error = SafeNativeMethods.OpusEncoderWriteFloat(
                _handle,
                MemoryMarshal.GetReference(interleavedSamples),
                interleavedSamples.Length / _channels);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' while encoding.");
        }

        internal void Drain()
        {
            var error = SafeNativeMethods.OpusEncoderDrain(_handle);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' while finishing encoding.");
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        static OpusEncoderCallbacks InitializeCallbacks([NotNull] Stream stream)
        {
            return new OpusEncoderCallbacks
            {
                Write = (userData, buffer, length) =>
                {
                    stream.Write(buffer, 0, length);
                    return 0;
                },

                // Leave the stream open
                Close = userData => 0
            };
        }
    }
}
