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
        readonly CoreAudioToolbox.AudioFileReadCallback _readCallback;
        readonly CoreAudioToolbox.AudioFileGetSizeCallback _getSizeCallback;
        readonly CoreAudioToolbox.AudioFileWriteCallback? _writeCallback;
        readonly CoreAudioToolbox.AudioFileSetSizeCallback? _setSizeCallback;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
            Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
        long _endOfData;

        protected AudioFileHandle Handle { get; }

        internal AudioFile(AudioFileType fileType, Stream stream)
        {
            // This constructor is for reading
            _readCallback = ReadCallback;
            _getSizeCallback = GetSizeCallback;

            _stream = stream;
            _endOfData = stream.Length;

            CoreAudioToolbox.AudioFileOpenWithCallbacks(nint.Zero,
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

            CoreAudioToolbox.AudioFileInitializeWithCallbacks(nint.Zero,
                _readCallback, _writeCallback, _getSizeCallback, _setSizeCallback,
                fileType, ref description, 0, out var handle);
            Handle = handle;
        }

        internal nint GetProperty(AudioFilePropertyId id, uint size)
        {
            // Callers must release this!
            var unmanagedValue = Marshal.AllocHGlobal((int) size);
            CoreAudioToolbox.AudioFileGetProperty(Handle, id, ref size, unmanagedValue);
            return unmanagedValue;
        }

        internal T GetProperty<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                        DynamicallyAccessedMemberTypes.NonPublicConstructors)]
            T>(AudioFilePropertyId id) where T : unmanaged
        {
            var size = (uint) Marshal.SizeOf<T>();
            var unmanagedValue = Marshal.AllocHGlobal((int) size);
            try
            {
                CoreAudioToolbox.AudioFileGetProperty(Handle, id, ref size, unmanagedValue);
                return Marshal.PtrToStructure<T>(unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }

        internal uint GetPropertyInfo(AudioFilePropertyId id)
        {
            CoreAudioToolbox.AudioFileGetPropertyInfo(Handle, id, out var dataSize, out _);
            return dataSize;
        }

        internal void ReadPackets(
            ref uint numBytes,
            AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            nint data) =>
            CoreAudioToolbox.AudioFileReadPacketData(Handle, false, ref numBytes, packetDescriptions,
                startingPacket, ref packets, data);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Handle.Dispose();
        }

        public void Dispose() => Dispose(true);

        AudioFileStatus ReadCallback(nint userData, long position, uint requestCount, byte[] buffer, out uint actualCount)
        {
            _stream.Position = position;
            actualCount = (uint) _stream.Read(buffer, 0, (int) requestCount);
            return actualCount == 0 ? AudioFileStatus.EndOfFileError : AudioFileStatus.Ok;
        }

        long GetSizeCallback(nint userData) => _endOfData;

        AudioFileStatus WriteCallback(nint userData, long position, uint requestCount, byte[] buffer, out uint actualCount)
        {
            _stream.Position = position;
            _stream.Write(buffer, 0, (int) requestCount);
            actualCount = requestCount;
            _endOfData = Math.Max(_endOfData, _stream.Position);
            return AudioFileStatus.Ok;
        }

        AudioFileStatus SetSizeCallback(nint userData, long size)
        {
            _endOfData = size;
            return AudioFileStatus.Ok;
        }
    }
}
