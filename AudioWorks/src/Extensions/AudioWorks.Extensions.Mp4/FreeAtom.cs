using System.IO;

namespace AudioWorks.Extensions.Mp4
{
    sealed class FreeAtom : WritableAtom
    {
        readonly uint _size;

        internal FreeAtom(uint size)
        {
            _size = size;
        }

        internal override void Write(Stream output)
        {
            if (_size <= 8) return;

            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header
                writer.WriteBigEndian(_size);
                writer.WriteBigEndian(0x66726565);  // 'free'

                writer.WriteZeros((int) _size - 8);
            }
        }
    }
}