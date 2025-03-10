﻿/* Copyright © 2018 Jeremy Herbison

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Lame
{
    sealed class Encoder : IDisposable
    {
        readonly EncoderHandle _handle = LibMp3Lame.Init();
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
                    Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
        long _startPosition;

        internal Encoder(Stream stream) => _stream = stream;

        internal void SetChannels(int channels) => LibMp3Lame.SetNumChannels(_handle, channels);

        internal void SetSampleRate(int sampleRate) => LibMp3Lame.SetInSampleRate(_handle, sampleRate);

        internal void SetSampleCount(uint sampleCount) => LibMp3Lame.SetNumSamples(_handle, sampleCount);

        internal void SetBitRate(int bitRate) => LibMp3Lame.SetBRate(_handle, bitRate);

        internal void SetVbrMode(VbrMode mode) => LibMp3Lame.SetVbr(_handle, mode);

        internal void SetVbrMeanBitRate(int bitRate) => LibMp3Lame.SetVbrMeanBitRateKbps(_handle, bitRate);

        internal void SetVbrQuality(float quality) => LibMp3Lame.SetVbrQuality(_handle, quality);

        internal void InitializeParameters()
        {
            _startPosition = _stream.Position;
            _ = LibMp3Lame.InitParams(_handle);
        }

        internal void Encode(ReadOnlySpan<float> leftSamples, ReadOnlySpan<float> rightSamples)
        {
            Span<byte> buffer = stackalloc byte[(int) Math.Ceiling(1.25 * leftSamples.Length) + 7200];

            var bytesEncoded = LibMp3Lame.EncodeBufferIeeeFloat(
                _handle,
                MemoryMarshal.GetReference(leftSamples),
                MemoryMarshal.GetReference(rightSamples),
                leftSamples.Length,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);

            if (bytesEncoded < 0)
                throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

            _stream.Write(buffer[..bytesEncoded]);
        }

        internal void EncodeInterleaved(ReadOnlySpan<float> samples, int frameCount)
        {
            Span<byte> buffer = stackalloc byte[(int) Math.Ceiling(1.25 * frameCount) + 7200];

            var bytesEncoded = LibMp3Lame.EncodeBufferInterleavedIeeeFloat(
                _handle,
                MemoryMarshal.GetReference(samples),
                frameCount,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);

            if (bytesEncoded < 0)
                throw new AudioEncodingException($"Lame encountered error '{bytesEncoded}' while encoding.");

            _stream.Write(buffer[..bytesEncoded]);
        }

        internal void Flush()
        {
            Span<byte> buffer = stackalloc byte[7200];
            var bytesFlushed = LibMp3Lame.EncodeFlush(
                _handle,
                ref MemoryMarshal.GetReference(buffer),
                buffer.Length);
            _stream.Write(buffer[..bytesFlushed]);
        }

        internal void UpdateLameTag()
        {
            var endOfData = _stream.Position;
            _stream.Position = _startPosition;

            byte empty = 0;
            var bufferSize = LibMp3Lame.GetLameTagFrame(_handle, ref empty, nuint.Zero);
            Span<byte> buffer = stackalloc byte[(int) bufferSize.ToUInt32()];
            _stream.Write(buffer[..(int) LibMp3Lame.GetLameTagFrame(
                _handle,
                ref MemoryMarshal.GetReference(buffer),
                bufferSize).ToUInt32()]);

            _stream.Position = endOfData;
        }

        public void Dispose() => _handle.Dispose();
    }
}