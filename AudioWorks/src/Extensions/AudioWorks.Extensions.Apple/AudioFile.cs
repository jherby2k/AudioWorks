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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    class AudioFile : IDisposable
    {
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
            Justification = "Type does not have dispose ownership")]
        readonly Stream _stream;
        GCHandle _instanceHandle;

        protected AudioFileHandle Handle { get; }

        internal unsafe AudioFile(AudioFileType fileType, Stream stream)
        {
            _stream = stream;

            // The callbacks have to be static, so pass this instance through as userData
            _instanceHandle = GCHandle.Alloc(this);

            // Open for reading
            CoreAudioToolbox.AudioFileOpenWithCallbacks(
                GCHandle.ToIntPtr(_instanceHandle),
                &ReadCallback,
                null,
                &GetSizeCallback,
                null,
                fileType,
                out var handle);

            Handle = handle;
        }

        internal unsafe AudioFile(AudioStreamBasicDescription description, AudioFileType fileType, Stream stream)
        {
            _stream = stream;

            // The callbacks have to be static, so pass this instance through as userData
            _instanceHandle = GCHandle.Alloc(this);

            // Open for writing
            CoreAudioToolbox.AudioFileInitializeWithCallbacks(
                GCHandle.ToIntPtr(_instanceHandle),
                &ReadCallback,
                &WriteCallback,
                &GetSizeCallback,
                &SetSizeCallback,
                fileType,
                ref description,
                0,
                out var handle);

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
            {
                Handle.Dispose();
                _instanceHandle.Free();
            }
        }

        public void Dispose() => Dispose(true);

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe AudioFileStatus ReadCallback(
            nint userData, long position, uint requestCount, byte* buffer, uint* actualCount)
        {
            var instance = (AudioFile) GCHandle.FromIntPtr(userData).Target!;

            instance._stream.Position = position;
            *actualCount = (uint) instance._stream.Read(new Span<byte>(buffer, (int) requestCount));
            return *actualCount == 0 ? AudioFileStatus.EndOfFileError : AudioFileStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe AudioFileStatus WriteCallback(
            nint userData, long position, uint requestCount, byte* buffer, uint* actualCount)
        {
            var instance = (AudioFile) GCHandle.FromIntPtr(userData).Target!;

            instance._stream.Position = position;
            instance._stream.Write(new Span<byte>(buffer, (int) requestCount));
            *actualCount = requestCount;
            return AudioFileStatus.Ok;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static long GetSizeCallback(nint userData)
        {
            var instance = (AudioFile) GCHandle.FromIntPtr(userData).Target!;

            return instance._stream.Length;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static AudioFileStatus SetSizeCallback(nint userData, long size)
        {
            var instance = (AudioFile) GCHandle.FromIntPtr(userData).Target!;

            instance._stream.SetLength(size);
            return AudioFileStatus.Ok;
        }
    }
}
