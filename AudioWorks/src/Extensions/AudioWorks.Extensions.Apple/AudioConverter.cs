﻿/* Copyright © 2018 Jeremy Herbison

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
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    sealed class AudioConverter : IDisposable
    {
        readonly CoreAudioToolbox.AudioConverterComplexInputCallback _inputCallback;
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
            _inputCallback = InputCallback;

            CoreAudioToolbox.AudioConverterNew(ref inputDescription,
                ref outputDescription, out _handle);

            _audioFile = audioFile;
        }

        internal void FillBuffer(
            ref uint packetSize,
            ref AudioBufferListSingle outputBuffer,
            AudioStreamPacketDescription[]? packetDescriptions) =>
            CoreAudioToolbox.AudioConverterFillComplexBuffer(_handle, _inputCallback, nint.Zero,
                ref packetSize, ref outputBuffer, packetDescriptions);

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

        unsafe AudioConverterStatus InputCallback(
            nint handle,
            ref uint numberPackets,
            ref AudioBufferListSingle data,
            nint packetDescriptions,
            nint userData)
        {
            if (_buffer == null)
            {
                _buffer = MemoryPool<byte>.Shared.Rent((int)
                    (numberPackets * _audioFile.GetProperty<uint>(AudioFilePropertyId.PacketSizeUpperBound)));
                _bufferHandle = _buffer.Memory.Pin();
            }

            if (_descriptionsHandle.IsAllocated)
                _descriptionsHandle.Free();

            var inputDescriptions = new AudioStreamPacketDescription[numberPackets];
            var numBytes = (uint) _buffer.Memory.Length;
            _audioFile.ReadPackets(ref numBytes, inputDescriptions, _packetIndex, ref numberPackets,
                new(_bufferHandle.Pointer));

            _packetIndex += numberPackets;

            data.Buffer1.DataByteSize = numBytes;
            data.Buffer1.Data = new(_bufferHandle.Pointer);

            // If this conversion requires packet descriptions, provide them
            // ReSharper disable once InvertIf
            if (packetDescriptions != nint.Zero)
            {
                _descriptionsHandle = GCHandle.Alloc(inputDescriptions, GCHandleType.Pinned);
                Marshal.WriteIntPtr(packetDescriptions, _descriptionsHandle.AddrOfPinnedObject());
            }

            return AudioConverterStatus.Ok;
        }
    }
}