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
        readonly Stream _realStream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        Stream _outputStream;
        readonly int _channels;
        readonly int _totalSeconds;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly OpusEncoderCallbacks _callbacks;
        readonly OpusEncoderHandle _handle;
        int _requestedBitRate;
        bool _headersFlushed;

        internal Encoder(
            Stream stream,
            int sampleRate,
            int channels,
            int totalSeconds,
            OpusCommentsHandle comments)
        {
            _realStream = stream;
            _outputStream = new MemoryStream();
            _channels = channels;
            _totalSeconds = totalSeconds;
            _callbacks = InitializeCallbacks();
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

        internal void SetHeaderGain(int gain)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle,
                EncoderControlRequest.SetHeaderGain, gain);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the gain.");
        }

        internal void SetLsbDepth(int depth)
        {
            var error = SafeNativeMethods.OpusEncoderControl(_handle,
                EncoderControlRequest.SetLsbDepth, depth);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' setting the LSB depth.");
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
            // Cache this value for determining pre-allocation
            _requestedBitRate = bitRate * 1000;

            var error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.SetBitRate, _requestedBitRate);
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
            if (!_headersFlushed) FlushHeaders();

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
                throw new AudioEncodingException($"Opus encountered error '{error}' draining the encoder.");

            // The pre-allocation was based on an estimated bitrate
            _outputStream.SetLength(_outputStream.Position);
        }

        public void Dispose()
        {
            if (_outputStream is MemoryStream)
                _outputStream.Dispose();
            _handle.Dispose();
        }

        OpusEncoderCallbacks InitializeCallbacks() => new()
        {
            // ReSharper disable once UnusedParameter.Local
            Write = (userData, buffer, length) =>
            {
                _outputStream.Write(buffer, 0, length);
                return 0;
            },

            // Leave the stream open
            // ReSharper disable once UnusedParameter.Local
            Close = userData => 0
        };

        void FlushHeaders()
        {
            var error = SafeNativeMethods.OpusEncoderFlushHeader(_handle);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error '{error}' flushing the header.");

            if (_requestedBitRate == 0)
            {
                // If the bit rate isn't explicit, get the automatic value
                error = SafeNativeMethods.OpusEncoderControl(_handle, EncoderControlRequest.GetBitRate, out _requestedBitRate);
                if (error != 0)
                    throw new AudioEncodingException($"Opus encountered error '{error}' getting the bit rate.");
            }

            // Pre-allocate the whole stream (estimate worst case bit rate, plus the header)
            _realStream.SetLength((_requestedBitRate + 25000) / 8 * _totalSeconds + _outputStream.Length);

            // Flush the headers to the real output stream
            _outputStream.Position = 0;
            _outputStream.CopyTo(_realStream);
            _outputStream.Close();
            _outputStream = _realStream;

            _headersFlushed = true;
        }
    }
}
