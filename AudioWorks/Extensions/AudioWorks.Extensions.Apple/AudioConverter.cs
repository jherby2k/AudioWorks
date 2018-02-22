using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    sealed class AudioConverter : IDisposable
    {
        [NotNull] readonly NativeCallbacks.AudioConverterComplexInputCallback _inputCallback;
        [NotNull] readonly AudioConverterHandle _handle;
        [NotNull] readonly AudioFile _audioFile;
        long _packetIndex;
        [CanBeNull] byte[] _buffer;
        GCHandle _bufferHandle;
        GCHandle _descriptionsHandle;

        internal AudioConverter(ref AudioStreamBasicDescription inputDescription,
            ref AudioStreamBasicDescription outputDescription,
            [NotNull] AudioFile audioFile)
        {
            _inputCallback = InputCallback;

            SafeNativeMethods.AudioConverterNew(ref inputDescription,
                ref outputDescription, out _handle);

            _audioFile = audioFile;
        }

        internal void FillBuffer(
            ref uint packetSize,
            ref AudioBufferList outputBuffer,
            [CanBeNull] AudioStreamPacketDescription[] packetDescriptions)
        {
            SafeNativeMethods.AudioConverterFillComplexBuffer(_handle, _inputCallback, IntPtr.Zero,
                ref packetSize, ref outputBuffer, packetDescriptions);
        }

        internal void SetProperty(AudioConverterPropertyId propertyId, uint size, IntPtr data)
        {
            SafeNativeMethods.AudioConverterSetProperty(_handle, propertyId, size, data);
        }

        public void Dispose()
        {
            _handle.Dispose();
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            if (_bufferHandle.IsAllocated)
                _bufferHandle.Free();
            if (_descriptionsHandle.IsAllocated)
                _descriptionsHandle.Free();
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of native API")]
        AudioConverterStatus InputCallback(IntPtr handle, ref uint numberPackets, ref AudioBufferList data, IntPtr packetDescriptions, IntPtr userData)
        {
            if (_buffer == null)
            {
                _buffer = new byte[numberPackets *
                                   _audioFile.GetProperty<uint>(AudioFilePropertyId.PacketSizeUpperBound)];
                _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            }

            if (_descriptionsHandle.IsAllocated)
                _descriptionsHandle.Free();

            var inputDescriptions = new AudioStreamPacketDescription[numberPackets];
            _audioFile.ReadPackets(out var numBytes, inputDescriptions, _packetIndex, ref numberPackets,
                _bufferHandle.AddrOfPinnedObject());

            _packetIndex += numberPackets;

            data.Buffers[0].DataByteSize = numBytes;
            data.Buffers[0].Data = _bufferHandle.AddrOfPinnedObject();

            // If this conversion requires packet descriptions, provide them
            if (packetDescriptions != IntPtr.Zero)
            {
                _descriptionsHandle = GCHandle.Alloc(inputDescriptions, GCHandleType.Pinned);
                Marshal.WriteIntPtr(packetDescriptions, _descriptionsHandle.AddrOfPinnedObject());
            }

            return AudioConverterStatus.Ok;
        }

        ~AudioConverter()
        {
            FreeUnmanaged();
        }
    }
}