using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Extensions.Mp3
{
    sealed class FrameHeader
    {
        static readonly int[,] _bitRateTable =
        {
            { 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 },
            { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160 }
        };

        static readonly int[,] _sampleRateTable =
        {
            { 44100, 48000, 32000 },
            { 22050, 24000, 16000 },
            { 11025, 12000, 8000 }
        };

        internal int BitRate { get; }

        internal int SampleRate { get; }

        internal int Padding { get; }

        internal int Channels { get; }

        internal int SamplesPerFrame { get; }

        internal int SideInfoLength { get; }

        public FrameHeader([NotNull] IReadOnlyList<byte> data)
        {
            var mpegVersion = ParseMpegVersion(data);
            VerifyLayer(data);
            BitRate = ParseBitRate(data, mpegVersion);
            SampleRate = ParseSampleRate(data, mpegVersion);
            Padding = ParsePadding(data);
            Channels = ParseChannels(data);
            SamplesPerFrame = CalculateSamplesPerFrame(mpegVersion);
            SideInfoLength = CalculateSideInfoLength(mpegVersion, Channels);
        }

        [Pure, NotNull]
        static string ParseMpegVersion([NotNull] IReadOnlyList<byte> data)
        {
            switch ((data[1] >> 3) & 0b00000011)
            {
                case 0b00000000:
                    return "2.5";
                case 0b00000010:
                    return "2";
                case 0b00000011:
                    return "1";
                default:
                    throw new AudioInvalidException("Not a valid MPEG header.");
            }
        }

        static void VerifyLayer([NotNull] IReadOnlyList<byte> data)
        {
            switch ((data[1] >> 1) & 0b00000011)
            {
                case 0b00000001:
                    return;
                default:
                    throw new AudioUnsupportedException("Not an MPEG audio layer III header.");
            }
        }

        [Pure]
        static int ParseBitRate([NotNull] IReadOnlyList<byte> data, [NotNull] string mpegVersion)
        {
            var column = (data[2] >> 4) & 0b00001111;
            if (column == 0b00001111)
                throw new AudioInvalidException("Not a valid MPEG header.");

            return _bitRateTable[mpegVersion == "1" ? 0 : 1, column];
        }

        [Pure]
        static int ParseSampleRate([NotNull] IReadOnlyList<byte> data, [NotNull] string mpegVersion)
        {
            var column = (data[2] >> 2) & 0b00000011;
            if (column == 0b00000011)
                throw new AudioInvalidException("Not a valid MPEG header.");

            int row;
            switch (mpegVersion)
            {
                case "1":
                    row = 0;
                    break;
                case "2":
                    row = 1;
                    break;
                default:
                    row = 2;
                    break;
            }

            return _sampleRateTable[row, column];
        }

        [Pure]
        static int ParsePadding([NotNull] IReadOnlyList<byte> data)
        {
            return (data[2] >> 1) & 0b00000001;
        }

        [Pure]
        static int ParseChannels([NotNull] IReadOnlyList<byte> data)
        {
            switch ((data[3] >> 6) & 0b00000011)
            {
                case 0b00000011:
                    return 1;
                default:
                    return 2;
            }
        }

        [Pure]
        static int CalculateSamplesPerFrame([NotNull] string mpegVersion)
        {
            return mpegVersion == "1" ? 1152 : 576;
        }

        [Pure]
        static int CalculateSideInfoLength([NotNull] string mpegVersion, int channels)
        {
            if (channels == 1)
                return mpegVersion == "1" ? 17 : 9;
            return mpegVersion == "1" ? 32 : 17;
        }
    }
}