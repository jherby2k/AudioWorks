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

namespace AudioWorks.Extensions.Apple
{
    class AudioFile : IDisposable
    {
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        readonly NativeCallbacks.AudioFileReadCallback _readCallback;
        readonly NativeCallbacks.AudioFileGetSizeCallback _getSizeCallback;
        readonly NativeCallbacks.AudioFileWriteCallback? _writeCallback;
        readonly NativeCallbacks.AudioFileSetSizeCallback? _setSizeCallback;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable
#pragma warning disable CA2213 // Disposable fields should be disposed
        readonly Stream _stream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        long _endOfData;

        protected AudioFileHandle Handle { get; }

        internal AudioFile(AudioFileType fileType, Stream stream)
        {
            // This constructor is for reading
            _readCallback = ReadCallback;
            _getSizeCallback = GetSizeCallback;

            _stream = stream;
            _endOfData = stream.Length;

            SafeNativeMethods.AudioFileOpenWithCallbacks(IntPtr.Zero,
                _readCallback, null, _getSizeCallback, null,
                fileType, out var handle);
            Handle = handle;
        }

        internal AudioFile(AudioStreamBasicDescription description, AudioFileType fileType, Stream stream)
        {
            // This constructor is for writing
            _readCallback = ReadCallback;
            _getSizeCallback = GetSizeCallback;
            _writeCallback = WriteCallback;
            _setSizeCallback = SetSizeCallback;

            _stream = stream;

            SafeNativeMethods.AudioFileInitializeWithCallbacks(IntPtr.Zero,
                _readCallback, _writeCallback, _getSizeCallback, _setSizeCallback,
                fileType, ref description, 0, out var handle);
            Handle = handle;
        }

        internal IntPtr GetProperty(AudioFilePropertyId id, uint size)
        {
            // Callers must release this!
            var unmanagedValue = Marshal.AllocHGlobal((int) size);
            SafeNativeMethods.AudioFileGetProperty(Handle, id, ref size, unmanagedValue);
            return unmanagedValue;
        }

        internal T GetProperty<T>(AudioFilePropertyId id) where T : unmanaged
        {
            var size = (uint) Marshal.SizeOf(typeof(T));
            var unmanagedValue = Marshal.AllocHGlobal((int)size);
            try
            {
                SafeNativeMethods.AudioFileGetProperty(Handle, id, ref size, unmanagedValue);
                return Marshal.PtrToStructure<T>(unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }

        internal void GetPropertyInfo(AudioFilePropertyId id, out uint dataSize, out uint isWritable) =>
            SafeNativeMethods.AudioFileGetPropertyInfo(Handle, id, out dataSize, out isWritable);

        internal void ReadPackets(
            out uint numBytes,
            AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data) =>
            SafeNativeMethods.AudioFileReadPackets(Handle, false, out numBytes, packetDescriptions,
                startingPacket, ref packets, data);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Handle.Dispose();
        }

        public void Dispose() => Dispose(true);

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of native API")]
        AudioFileStatus ReadCallback(IntPtr userData, long position, uint requestCount, byte[] buffer, out uint actualCount)
        {
            _stream.Position = position;
            actualCount = (uint) _stream.Read(buffer, 0, (int) requestCount);
            return actualCount == 0 ? AudioFileStatus.EndOfFileError : AudioFileStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of native API")]
        long GetSizeCallback(IntPtr userData) => _endOfData;

        AudioFileStatus WriteCallback(IntPtr userData, long position, uint requestCount, byte[] buffer, out uint actualCount)
        {
            _stream.Position = position;
            _stream.Write(buffer, 0, (int) requestCount);
            actualCount = requestCount;
            _endOfData = Math.Max(_endOfData, _stream.Position);
            return AudioFileStatus.Ok;
        }

        AudioFileStatus SetSizeCallback(IntPtr userData, long size)
        {
            _endOfData = size;
            return AudioFileStatus.Ok;
        }
    }
}
