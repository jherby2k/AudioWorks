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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    sealed class AudioConverter : IDisposable
    {
        readonly AudioConverterHandle _handle;
        readonly AudioFile _audioFile;
        long _packetIndex;
        IMemoryOwner<byte>? _buffer;
        MemoryHandle _bufferHandle;
        GCHandle _descriptionsHandle;

        internal AudioConverter(ref AudioStreamBasicDescription inputDescription,
            ref AudioStreamBasicDescription outputDescription,
            AudioFile audioFile)
        {
            CoreAudioToolbox.AudioConverterNew(ref inputDescription,
                ref outputDescription, out _handle);

            _audioFile = audioFile;
        }

        internal unsafe void FillBuffer(
            ref uint packetSize,
            ref AudioBufferListSingle outputBuffer,
            AudioStreamPacketDescription[]? packetDescriptions)
        {
            // The callbacks have to be static, so pass this instance through as userData
            var instanceHandle = GCHandle.Alloc(this);
            try
            {
                CoreAudioToolbox.AudioConverterFillComplexBuffer(
                    _handle,
                    &InputCallback,
                    GCHandle.ToIntPtr(instanceHandle),
                    ref packetSize,
                    ref outputBuffer,
                    packetDescriptions);
            }
            finally
            {
                instanceHandle.Free();
            }
        }

        internal void SetProperty(AudioConverterPropertyId propertyId, uint size, nint data) =>
            CoreAudioToolbox.AudioConverterSetProperty(_handle, propertyId, size, data);

        public void Dispose()
        {
            _handle.Dispose();
            _bufferHandle.Dispose();
            _buffer?.Dispose();
            if (_descriptionsHandle.IsAllocated)
                _descriptionsHandle.Free();
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static unsafe AudioConverterStatus InputCallback(
            nint handle, uint* numberPackets, AudioBufferListSingle* data, nint packetDescriptions, nint userData)
        {
            var instance = (AudioConverter) GCHandle.FromIntPtr(userData).Target!;

            if (instance._buffer == null)
            {
                // Rent a reusable buffer and keep it pinned between calls
                instance._buffer = MemoryPool<byte>.Shared.Rent((int)
                    (*numberPackets * instance._audioFile.GetProperty<uint>(AudioFilePropertyId.PacketSizeUpperBound)));
                instance._bufferHandle = instance._buffer.Memory.Pin();
            }

            // Free the GCHandle from a previous call
            if (instance._descriptionsHandle.IsAllocated)
                instance._descriptionsHandle.Free();

            var inputDescriptions = new AudioStreamPacketDescription[*numberPackets];

            var numBytes = (uint) instance._buffer.Memory.Length;

            instance._audioFile.ReadPackets(ref numBytes, inputDescriptions, instance._packetIndex, ref *numberPackets,
                new(instance._bufferHandle.Pointer));

            instance._packetIndex += *numberPackets;

            (*data).Buffer1.DataByteSize = numBytes;
            (*data).Buffer1.Data = new(instance._bufferHandle.Pointer);

            // If this conversion requires packet descriptions, provide them
            if (packetDescriptions != nint.Zero)
            {
                instance._descriptionsHandle = GCHandle.Alloc(inputDescriptions, GCHandleType.Pinned);
                Marshal.WriteIntPtr(packetDescriptions, instance._descriptionsHandle.AddrOfPinnedObject());
            }

            return AudioConverterStatus.Ok;
        }
    }
}