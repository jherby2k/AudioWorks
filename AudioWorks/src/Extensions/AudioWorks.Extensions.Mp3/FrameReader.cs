using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp3
{
    sealed class FrameReader : BinaryReader
    {
        internal long FrameStart { get; private set; }

        [NotNull] readonly byte[] _buffer = new byte[4];

        internal FrameReader([NotNull] Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void SeekToNextFrame()
        {
            // A frame begins with the first 11 bits set:
            while (true)
            {
                if (ReadByte() != 0xFF || ReadByte() < 0xE0) continue;
                FrameStart = BaseStream.Seek(-2, SeekOrigin.Current);
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
            return firstByte == 0xFF && secondByte >= 0xE0;
        }

        internal uint ReadUInt32BigEndian()
        {
            Read(_buffer, 0, 4); //TODO read into Span
            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
        }
    }
}