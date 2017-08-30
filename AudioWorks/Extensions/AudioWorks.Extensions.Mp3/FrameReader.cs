using JetBrains.Annotations;
using System;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Mp3
{
    class FrameReader : BinaryReader
    {
        readonly byte[] _buffer = new byte[4];

        public FrameReader([NotNull] Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void SeekToNextFrame()
        {
            // A frame begins with the first 11 bits set:
            while (true)
            {
                if (ReadByte() != 0xff || ReadByte() < 0xe0) continue;
                BaseStream.Seek(-2, SeekOrigin.Current);
                return;
            }
        }

        internal bool VerifyFrameSync([NotNull] FrameHeader header)
        {
            var frameLength = header.SamplesPerFrame / 8 * header.BitRate * 1000 /
                                header.SampleRate + header.Padding;

            // Seek to where the next frame should start
            var initialPosition = BaseStream.Position;
            BaseStream.Seek(frameLength - 4, SeekOrigin.Current);
            var firstByte = ReadByte();
            var secondByte = ReadByte();
            BaseStream.Position = initialPosition;

            // If another sync is detected, return success
            return firstByte == 0xff && secondByte >= 0xe0;
        }

        internal uint ReadUInt32BigEndian()
        {
            Read(_buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(_buffer);
            return BitConverter.ToUInt32(_buffer, 0);
        }
    }
}