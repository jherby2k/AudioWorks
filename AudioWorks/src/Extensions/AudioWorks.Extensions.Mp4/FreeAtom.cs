using System;
using System.Buffers.Binary;

namespace AudioWorks.Extensions.Mp4
{
    sealed class FreeAtom : WritableAtom
    {
        readonly uint _size;

        internal FreeAtom(uint size)
        {
            _size = size;
        }

        internal override byte[] GetBytes()
        {
            if (_size <= 8) return Array.Empty<byte>();

            var result = new byte[_size];

            // Write the atom header
            BinaryPrimitives.WriteUInt32BigEndian(result, _size);
            BinaryPrimitives.WriteInt32BigEndian(result.AsSpan().Slice(4), 0x66726565);  // 'free'

            return result;
        }
    }
}