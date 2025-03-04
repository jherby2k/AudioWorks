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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac.Metadata
{
    sealed class MetadataChain : IDisposable
    {
        static readonly IoCallbacks _callbacks = InitializeCallbacks();
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
                    Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
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
        static unsafe nint ReadCallback(void* readBuffer, nint bufferSize, nint numberOfRecords, nint handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            var bytesRead = stream.Read(new(readBuffer, bufferSize.ToInt32() * numberOfRecords.ToInt32()));
            return new(bytesRead);
        }

        [UnmanagedCallersOnly]
        static unsafe nint WriteCallback(void* writeBuffer, nint bufferSize, nint numberOfRecords, nint handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            stream.Write(new(writeBuffer, bufferSize.ToInt32() * numberOfRecords.ToInt32()));
            return numberOfRecords;
        }

        [UnmanagedCallersOnly]
        static int SeekCallback(nint handle, long offset, SeekOrigin whence)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            stream.Seek(offset, whence);
            return 0;
        }

        [UnmanagedCallersOnly]
        static long TellCallback(nint handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            return stream.Position;
        }

        [UnmanagedCallersOnly]
        static int EofCallback(nint handle)
        {
            var stream = (Stream) GCHandle.FromIntPtr(handle).Target!;
            return stream.Position < stream.Length ? 0 : 1;
        }
    }
}