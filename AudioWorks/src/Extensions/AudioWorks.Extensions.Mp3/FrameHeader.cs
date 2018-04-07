using System;
using System.Diagnostics.CodeAnalysis;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp3
{
    sealed class FrameHeader
    {
        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space.")]
        static readonly int[,] _bitRateTable =
        {
            { 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 },
            { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160 }
        };

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space.")]
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

        internal FrameHeader(ReadOnlySpan<byte> data)
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

        [Pure]
        static MpegVersion ParseMpegVersion(ReadOnlySpan<byte> data)
        {
            var value = (data[1] >> 3) & 0b00000011;
            if (typeof(MpegVersion).IsEnumDefined(value))
                return (MpegVersion) value;
            throw new AudioInvalidException("Not a valid MPEG header.");
        }

        enum MpegVersion
        {
            TwoPointFive = 0b00000000,
            Two = 0b00000010,
            One = 0b00000011
        }

        static void VerifyLayer(ReadOnlySpan<byte> data)
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
        static int ParseBitRate(ReadOnlySpan<byte> data, MpegVersion mpegVersion)
        {
            var column = (data[2] >> 4) & 0b00001111;
            if (column == 0b00001111)
                throw new AudioInvalidException("Not a valid MPEG header.");

            return _bitRateTable[mpegVersion == MpegVersion.One ? 0 : 1, column];
        }

        [Pure]
        static int ParseSampleRate(ReadOnlySpan<byte> data, MpegVersion mpegVersion)
        {
            var column = (data[2] >> 2) & 0b00000011;
            if (column == 0b00000011)
                throw new AudioInvalidException("Not a valid MPEG header.");

            return _sampleRateTable[
                mpegVersion == MpegVersion.One ? 0
                : mpegVersion == MpegVersion.Two ? 1
                : 2,
                column];
        }

        [Pure]
        static int ParsePadding(ReadOnlySpan<byte> data)
        {
            return (data[2] >> 1) & 0b00000001;
        }

        [Pure]
        static int ParseChannels(ReadOnlySpan<byte> data)
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
        static int CalculateSamplesPerFrame(MpegVersion mpegVersion)
        {
            return mpegVersion == MpegVersion.One ? 1152 : 576;
        }

        [Pure]
        static int CalculateSideInfoLength(MpegVersion mpegVersion, int channels)
        {
            if (channels == 1)
                return mpegVersion == MpegVersion.One ? 17 : 9;
            return mpegVersion == MpegVersion.One ? 32 : 17;
        }
    }
}