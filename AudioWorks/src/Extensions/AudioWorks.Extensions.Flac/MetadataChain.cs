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
using System.Buffers;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataChain : IDisposable
    {
        static readonly IoCallbacks _callbacks = InitializeCallbacks();
#pragma warning disable CA2213 // Disposable fields should be disposed
        readonly Stream _stream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        readonly MetadataChainHandle _handle = LibFlac.MetadataChainNew();

        internal MetadataChain(Stream stream) => _stream = stream;

        internal void Read()
        {
            var streamHandle = GCHandle.Alloc(_stream);
            try
            {
                LibFlac.MetadataChainReadWithCallbacks(_handle, GCHandle.ToIntPtr(streamHandle), _callbacks);
            }
            finally
            {
                streamHandle.Free();
            }
        }

        internal bool CheckIfTempFileNeeded(bool usePadding) =>
            LibFlac.MetadataChainCheckIfTempFileNeeded(_handle, usePadding);

        internal void Write(bool usePadding)
        {
            var streamHandle = GCHandle.Alloc(_stream);
            try
            {
                LibFlac.MetadataChainWriteWithCallbacks(
                    _handle, usePadding, GCHandle.ToIntPtr(streamHandle), _callbacks);
            }
            finally
            {
                streamHandle.Free();
            }
        }

        internal void WriteWithTempFile(bool usePadding, Stream tempStream)
        {
            var streamHandle = GCHandle.Alloc(_stream);
            var tempStreamHandle = GCHandle.Alloc(tempStream);
            try
            {
                LibFlac.MetadataChainWriteWithCallbacksAndTempFile(
                    _handle,
                    usePadding,
                    GCHandle.ToIntPtr(streamHandle),
                    _callbacks,
                    GCHandle.ToIntPtr(tempStreamHandle),
                    _callbacks);
            }
            finally
            {
                streamHandle.Free();
                tempStreamHandle.Free();
            }
        }

        internal MetadataIterator GetIterator() => new(_handle);

        public void Dispose() => _handle.Dispose();

        static unsafe IoCallbacks InitializeCallbacks() => new()
        {
            Read = &ReadCallback,
            Write = &WriteCallback,
            Seek = &SeekCallback,
            Tell = &TellCallback,
            Eof = &EofCallback
        };

        [UnmanagedCallersOnly]
        static IntPtr ReadCallback(IntPtr readBuffer, IntPtr bufferSize, IntPtr numberOfRecords, IntPtr handle)
        {
            var totalBufferSize = bufferSize.ToInt32() * numberOfRecords.ToInt32();
            var buffer = ArrayPool<byte>.Shared.Rent(totalBufferSize);
            try
            {
                var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
                var bytesRead = stream.Read(buffer, 0, totalBufferSize);
                Marshal.Copy(buffer, 0, readBuffer, totalBufferSize);
                return new(bytesRead);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        [UnmanagedCallersOnly]
        static IntPtr WriteCallback(IntPtr writeBuffer, IntPtr bufferSize, IntPtr numberOfRecords, IntPtr handle)
        {
            var castNumberOfRecords = numberOfRecords.ToInt32();
            var totalBufferSize = bufferSize.ToInt32() * castNumberOfRecords;
            var buffer = ArrayPool<byte>.Shared.Rent(totalBufferSize);
            try
            {
                var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
                Marshal.Copy(writeBuffer, buffer, 0, totalBufferSize);
                stream.Write(buffer, 0, totalBufferSize);
                return new(castNumberOfRecords);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        [UnmanagedCallersOnly]
        static int SeekCallback(IntPtr handle, long offset, SeekOrigin whence)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            stream.Seek(offset, whence);
            return 0;
        }

        [UnmanagedCallersOnly]
        static long TellCallback(IntPtr handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            return stream.Position;
        }

        [UnmanagedCallersOnly]
        static int EofCallback(IntPtr handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            return stream.Position < stream.Length ? 0 : 1;
        }
    }
}