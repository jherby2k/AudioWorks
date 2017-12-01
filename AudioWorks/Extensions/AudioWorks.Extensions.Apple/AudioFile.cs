using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    sealed class AudioFile : IDisposable
    {
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        [NotNull] readonly NativeCallbacks.AudioFileReadCallback _readCallback;
        [NotNull] readonly NativeCallbacks.AudioFileGetSizeCallback _getSizeCallback;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable
        [NotNull] readonly Stream _stream;
        [NotNull] readonly AudioFileHandle _handle;

        internal AudioFile(AudioFileType fileType, [NotNull] Stream stream)
        {
            _readCallback = ReadCallback;
            _getSizeCallback = GetSizeCallback;

            _stream = stream;

            SafeNativeMethods.AudioFileOpenWithCallbacks(IntPtr.Zero, _readCallback, null,
                _getSizeCallback, null, fileType, out _handle);
        }

        internal IntPtr GetProperty(AudioFilePropertyId id, uint size)
        {
            // Callers must release this!
            var unmanagedValue = Marshal.AllocHGlobal((int) size);
            SafeNativeMethods.AudioFileGetProperty(_handle, id, ref size, unmanagedValue);
            return unmanagedValue;
        }

        internal T GetProperty<T>(AudioFilePropertyId id) where T : struct
        {
            var size = (uint) Marshal.SizeOf(typeof(T));
            var unmanagedValue = Marshal.AllocHGlobal((int)size);
            try
            {
                SafeNativeMethods.AudioFileGetProperty(_handle, id, ref size, unmanagedValue);
                return Marshal.PtrToStructure<T>(unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }

        internal void GetPropertyInfo(AudioFilePropertyId id, out uint dataSize, out uint isWritable)
        {
            SafeNativeMethods.AudioFileGetPropertyInfo(_handle, id, out dataSize, out isWritable);
        }

        internal void ReadPackets(
            out uint numBytes,
            [NotNull] AudioStreamPacketDescription[] packetDescriptions,
            long startingPacket,
            ref uint packets,
            IntPtr data)
        {
            SafeNativeMethods.AudioFileReadPackets(_handle, false, out numBytes, packetDescriptions,
                startingPacket, ref packets, data);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        AudioFileStatus ReadCallback(IntPtr userData, long position, uint requestCount, [NotNull] byte[] buffer, out uint actualCount)
        {
            _stream.Position = position;
            actualCount = (uint) _stream.Read(buffer, 0, (int) requestCount);

            return AudioFileStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        long GetSizeCallback(IntPtr userData)
        {
            return _stream.Length;
        }
    }
}
