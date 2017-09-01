using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions.Mp4
{
    class EsdsAtom
    {
        [NotNull] static readonly uint[] _sampleRates =
        {
            96000,
            88200,
            64000,
            48000,
            44100,
            32000,
            24000,
            22050,
            16000,
            12000,
            11025,
            8000,
            7350,
            0
        };

        internal uint SampleRate { get; }

        internal ushort Channels { get; }

        internal EsdsAtom([NotNull] byte[] data)
        {
            using (var reader = new Mp4Reader(new MemoryStream(data), false))
            {
                reader.BaseStream.Seek(12, SeekOrigin.Begin);

                // This appears to be 0 for Apple Lossless files: 
                if (reader.ReadByte() == 0) return;

                SkipDescriptorLength(reader);
                reader.BaseStream.Seek(4, SeekOrigin.Current);
                SkipDescriptorLength(reader);
                reader.BaseStream.Seek(14, SeekOrigin.Current);
                SkipDescriptorLength(reader);

                var dsiBytes = reader.ReadBytes(2);
                SampleRate = _sampleRates[(dsiBytes[0] << 1) & 0b00001110 | (dsiBytes[1] >> 7) & 0b00000001];
                Channels = (ushort) ((dsiBytes[1] >> 3) & 0b00001111);
            }
        }

        static void SkipDescriptorLength([NotNull] BinaryReader reader)
        {
            byte currentByte;
            do
            {
                currentByte = reader.ReadByte();
            } while ((currentByte & 0b10000000) == 0b10000000);
        }
    }
}