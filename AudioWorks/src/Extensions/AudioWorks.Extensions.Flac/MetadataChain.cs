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
        readonly MetadataChainHandle _handle = LibFlac.MetadataChainNew();
        readonly IoCallbacks _callbacks;

        internal MetadataChain(Stream stream) => _callbacks = InitializeCallbacks(stream);

        internal void Read() => LibFlac.MetadataChainReadWithCallbacks(_handle, IntPtr.Zero, _callbacks);

        internal bool CheckIfTempFileNeeded(bool usePadding) =>
            LibFlac.MetadataChainCheckIfTempFileNeeded(_handle, usePadding);

        internal void Write(bool usePadding) =>
            LibFlac.MetadataChainWriteWithCallbacks(_handle, usePadding, IntPtr.Zero, _callbacks);

        internal void WriteWithTempFile(bool usePadding, Stream tempStream) =>
            LibFlac.MetadataChainWriteWithCallbacksAndTempFile(
                _handle,
                usePadding,
                IntPtr.Zero,
                _callbacks,
                IntPtr.Zero,
                InitializeCallbacks(tempStream));

        internal MetadataIterator GetIterator() => new(_handle);

        public void Dispose() => _handle.Dispose();

        static IoCallbacks InitializeCallbacks(Stream stream) => new()
        {
            // ReSharper disable once UnusedParameter.Local
            Read = Marshal.GetFunctionPointerForDelegate<LibFlac.IoCallbacksReadCallback>(
                (readBuffer, bufferSize, numberOfRecords, handle) =>
                {
                    var totalBufferSize = bufferSize.ToInt32() * numberOfRecords.ToInt32();
                    var buffer = ArrayPool<byte>.Shared.Rent(totalBufferSize);
                    try
                    {
                        var bytesRead = stream.Read(buffer, 0, totalBufferSize);
                        Marshal.Copy(buffer, 0, readBuffer, totalBufferSize);
                        return new(bytesRead);
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                    }
                }),

            // ReSharper disable once UnusedParameter.Local
            Write = Marshal.GetFunctionPointerForDelegate<LibFlac.IoCallbacksWriteCallback>(
                (writeBuffer, bufferSize, numberOfRecords, handle) =>
                {
                    var castNumberOfRecords = numberOfRecords.ToInt32();
                    var totalBufferSize = bufferSize.ToInt32() * castNumberOfRecords;
                    var buffer = ArrayPool<byte>.Shared.Rent(totalBufferSize);
                    try
                    {
                        Marshal.Copy(writeBuffer, buffer, 0, totalBufferSize);
                        stream.Write(buffer, 0, totalBufferSize);
                        return new(castNumberOfRecords);
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                    }
                }),

            // ReSharper disable once UnusedParameter.Local
            Seek = Marshal.GetFunctionPointerForDelegate<LibFlac.IoCallbacksSeekCallback>(
                (handle, offset, whence) =>
                {
                    stream.Seek(offset, whence);
                    return 0;
                }),

            // ReSharper disable once UnusedParameter.Local
            Tell = Marshal.GetFunctionPointerForDelegate<LibFlac.IoCallbacksTellCallback>(
                handle => stream.Position),

            // ReSharper disable once UnusedParameter.Local
            Eof = Marshal.GetFunctionPointerForDelegate<LibFlac.IoCallbacksEofCallback>(
                handle => stream.Position < stream.Length ? 0 : 1)
        };
    }
}