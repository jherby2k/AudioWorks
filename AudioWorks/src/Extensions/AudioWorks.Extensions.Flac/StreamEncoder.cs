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
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class StreamEncoder : IDisposable
    {
        [NotNull] readonly StreamEncoderHandle _handle = SafeNativeMethods.StreamEncoderNew();
        [NotNull] readonly NativeCallbacks.StreamEncoderWriteCallback _writeCallback;
        [NotNull] readonly NativeCallbacks.StreamEncoderSeekCallback _seekCallback;
        [NotNull] readonly NativeCallbacks.StreamEncoderTellCallback _tellCallback;
        [NotNull] Stream _stream;
        long _endOfData;

        internal StreamEncoder([NotNull] Stream stream)
        {
            // Need a reference to the callbacks for the lifetime of the encoder
            _writeCallback = WriteCallback;
            _seekCallback = SeekCallback;
            _tellCallback = TellCallback;

            _stream = stream;
        }

        internal void SetChannels(uint channels)
        {
            SafeNativeMethods.StreamEncoderSetChannels(_handle, channels);
        }

        internal void SetBitsPerSample(uint bitsPerSample)
        {
            SafeNativeMethods.StreamEncoderSetBitsPerSample(_handle, bitsPerSample);
        }

        internal void SetSampleRate(uint sampleRate)
        {
            SafeNativeMethods.StreamEncoderSetSampleRate(_handle, sampleRate);
        }

        internal void SetTotalSamplesEstimate(ulong sampleCount)
        {
            SafeNativeMethods.StreamEncoderSetTotalSamplesEstimate(_handle, sampleCount);
        }

        internal void SetCompressionLevel(uint compressionLevel)
        {
            SafeNativeMethods.StreamEncoderSetCompressionLevel(_handle, compressionLevel);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods",
            Justification = "Can't pass an array of SafeHandles")]
        internal void SetMetadata([NotNull, ItemNotNull] IEnumerable<MetadataBlock> metadataBlocks)
        {
            var blockPointers = metadataBlocks.Select(block => block.Handle.DangerousGetHandle()).ToArray();
            SafeNativeMethods.StreamEncoderSetMetadata(_handle, blockPointers, (uint) blockPointers.Length);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Initialize()
        {
            // Buffer the metadata writes in memory by swapping the stream
            using (var tempStream = new MemoryStream())
            {
                var fileStream = _stream;
                _stream = tempStream;
                SafeNativeMethods.StreamEncoderInitStream(_handle, _writeCallback, _seekCallback, _tellCallback, null,
                    IntPtr.Zero);

                // Pre-allocate the whole stream (estimate worst case compression, plus metadata)
                fileStream.SetLength(
                    SafeNativeMethods.StreamEncoderGetChannels(_handle) *
                    (uint) Math.Ceiling(SafeNativeMethods.StreamEncoderGetBitsPerSample(_handle) / 8.0) *
                    (long) SafeNativeMethods.StreamEncoderGetTotalSamplesEstimate(_handle) +
                    tempStream.Length);

                // Flush the metadata to the output stream, and swap the streams back
                _stream = fileStream;
                tempStream.WriteTo(fileStream);
            }
        }

        internal unsafe void Process(ReadOnlySpan<int> leftBuffer, ReadOnlySpan<int> rightBuffer)
        {
            Span<IntPtr> buffers = stackalloc IntPtr[]
            {
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(leftBuffer))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(rightBuffer)))
            };

            if (!SafeNativeMethods.StreamEncoderProcess(
                _handle,
                MemoryMarshal.GetReference(buffers),
                (uint) leftBuffer.Length))
                throw new AudioEncodingException($"FLAC encountered error {GetState()} while processing samples.");

        }

        internal void ProcessInterleaved(ReadOnlySpan<int> buffer, uint frames)
        {
            if (!SafeNativeMethods.StreamEncoderProcessInterleaved(
                _handle,
                MemoryMarshal.GetReference(buffer),
                frames))
                throw new AudioEncodingException($"FLAC encountered error '{GetState()}' while processing samples.");
        }

        internal void Finish()
        {
            if (!SafeNativeMethods.StreamEncoderFinish(_handle))
                throw new AudioEncodingException($"FLAC encountered error '{GetState()}' while finishing encoding.");

            // The pre-allocation may have been based on an estimated frame count
            _stream.SetLength(_endOfData);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        EncoderWriteStatus WriteCallback(IntPtr handle, [NotNull] byte[] buffer, int bytes, uint samples, uint currentFrame, IntPtr userData)
        {
            _stream.Write(buffer, 0, bytes);
            _endOfData = Math.Max(_endOfData, _stream.Position);
            return EncoderWriteStatus.Ok;
        }

        EncoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Position = (long) absoluteOffset;
            return EncoderSeekStatus.Ok;
        }

        EncoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return EncoderTellStatus.Ok;
        }

        [Pure]
        EncoderState GetState()
        {
            return SafeNativeMethods.StreamEncoderGetState(_handle);
        }
    }
}