/* Copyright © 2018 Jeremy Herbison

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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensions.Flac.Metadata;

namespace AudioWorks.Extensions.Flac.Encoder
{
    sealed class StreamEncoder : IDisposable
    {
        readonly StreamEncoderHandle _handle = LibFlac.StreamEncoderNew();
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
                    Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
        GCHandle _streamHandle;
        long _endOfData;
        int _channels;
        int _bytesPerSample;
        long _sampleCount;

        internal StreamEncoder(Stream stream) => _stream = stream;

        internal void SetChannels(int channels)
        {
            _channels = channels;
            LibFlac.StreamEncoderSetChannels(_handle, (uint) channels);
        }

        internal void SetBitsPerSample(int bitsPerSample)
        {
            _bytesPerSample = (int) Math.Ceiling(bitsPerSample / 8.0);
            LibFlac.StreamEncoderSetBitsPerSample(_handle, (uint) bitsPerSample);
        }

        internal void SetSampleRate(int sampleRate) =>
            LibFlac.StreamEncoderSetSampleRate(_handle, (uint) sampleRate);

        internal void SetTotalSamplesEstimate(long sampleCount)
        {
            _sampleCount = sampleCount;
            LibFlac.StreamEncoderSetTotalSamplesEstimate(_handle, (ulong) sampleCount);
        }

        internal void SetCompressionLevel(int compressionLevel) =>
            LibFlac.StreamEncoderSetCompressionLevel(_handle, (uint) compressionLevel);

        internal void SetMetadata(IEnumerable<MetadataObject> metadataObjects)
        {
            var handles = metadataObjects.Select(o => o.Handle.DangerousGetHandle()).ToArray();
            LibFlac.StreamEncoderSetMetadata(_handle, handles, (uint) handles.Length);
        }

        internal unsafe void Initialize()
        {
            // The callbacks have to be static, so pass this instance through as userData
            _streamHandle = GCHandle.Alloc(this);

            _ = LibFlac.StreamEncoderInitStream(
                _handle, &WriteCallback, &SeekCallback, &TellCallback, null, GCHandle.ToIntPtr(_streamHandle));

            // Pre-allocate the whole stream (estimate worst case compression, plus metadata)
            _stream.SetLength(_channels * _bytesPerSample * _sampleCount + _stream.Length);
        }

        internal unsafe void Process(ReadOnlySpan<int> leftBuffer, ReadOnlySpan<int> rightBuffer)
        {
            Span<nint> buffers =
            [
                new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(leftBuffer))),
                new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(rightBuffer)))
            ];

            if (!LibFlac.StreamEncoderProcess(
                _handle,
                MemoryMarshal.GetReference(buffers),
                (uint) leftBuffer.Length))
                throw new AudioEncodingException($"FLAC encountered error {GetState()} while processing samples.");

        }

        internal void ProcessInterleaved(ReadOnlySpan<int> buffer, uint frames)
        {
            if (!LibFlac.StreamEncoderProcessInterleaved(
                _handle,
                MemoryMarshal.GetReference(buffer),
                frames))
                throw new AudioEncodingException($"FLAC encountered error '{GetState()}' while processing samples.");
        }

        internal void Finish()
        {
            if (!LibFlac.StreamEncoderFinish(_handle))
                throw new AudioEncodingException($"FLAC encountered error '{GetState()}' while finishing encoding.");

            // The pre-allocation may have been based on an estimated frame count
            _stream.SetLength(_endOfData);
        }

        public void Dispose()
        {
            _handle.Dispose();
            if (_streamHandle.IsAllocated)
                _streamHandle.Free();
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe EncoderWriteStatus WriteCallback(
            nint handle, byte* buffer, int bytes, uint samples, uint currentFrame, nint userData)
        {
            var instance = (StreamEncoder) GCHandle.FromIntPtr(userData).Target!;

            instance._stream.Write(new(buffer, bytes));
            instance._endOfData = Math.Max(instance._endOfData, instance._stream.Position);
            return EncoderWriteStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static EncoderSeekStatus SeekCallback(nint handle, ulong absoluteOffset, nint userData)
        {
            var instance = (StreamEncoder) GCHandle.FromIntPtr(userData).Target!;

            instance._stream.Position = (long) absoluteOffset;
            return EncoderSeekStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe EncoderTellStatus TellCallback(nint handle, ulong* absoluteOffset, nint userData)
        {
            var instance = (StreamEncoder) GCHandle.FromIntPtr(userData).Target!;

            *absoluteOffset = (ulong) instance._stream.Position;
            return EncoderTellStatus.Ok;
        }

        EncoderState GetState() => LibFlac.StreamEncoderGetState(_handle);
    }
}