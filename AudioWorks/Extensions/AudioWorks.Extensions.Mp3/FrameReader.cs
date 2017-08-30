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

        internal XingHeader ReadXingHeader()
        {
            var result = new XingHeader();

            var headerId = new string(ReadChars(4));
            if (headerId != "Xing" && headerId != "Info")
                return result;

            // The flags DWORD indicates whether the frame and byte counts are present:
            var flags = ReadUInt32BigEndian();

            if ((flags & 0x1) == 1)
                result.FrameCount = ReadUInt32BigEndian();

            if ((flags >> 1 & 0x1) == 1)
                result.ByteCount = ReadUInt32BigEndian();

            return result;
        }

        uint ReadUInt32BigEndian()
        {
            Read(_buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(_buffer);
            return BitConverter.ToUInt32(_buffer, 0);
        }
    }
}