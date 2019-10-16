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
using System.Diagnostics.CodeAnalysis;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Mp3
{
    sealed class FrameHeader
    {
        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional",
            Justification = "Does not waste space.")]
        static readonly int[,] _bitRateTable =
        {
            { 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 },
            { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160 }
        };

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional",
            Justification = "Does not waste space.")]
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

        static MpegVersion ParseMpegVersion(ReadOnlySpan<byte> data)
        {
            var value = (data[1] >> 3) & 0b00000011;
            if (typeof(MpegVersion).IsEnumDefined(value))
                return (MpegVersion) value;
            throw new AudioInvalidException("Not a valid MPEG header.");
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

        static int ParseBitRate(ReadOnlySpan<byte> data, MpegVersion mpegVersion)
        {
            var column = (data[2] >> 4) & 0b00001111;
            if (column == 0b00001111)
                throw new AudioInvalidException("Not a valid MPEG header.");

            return _bitRateTable[mpegVersion == MpegVersion.One ? 0 : 1, column];
        }

        static int ParseSampleRate(ReadOnlySpan<byte> data, MpegVersion mpegVersion)
        {
            var column = (data[2] >> 2) & 0b00000011;
            if (column == 0b00000011)
                throw new AudioInvalidException("Not a valid MPEG header.");

            return _sampleRateTable[
                mpegVersion switch
                {
                    MpegVersion.One => 0,
                    MpegVersion.Two => 1,
                    _ => 2
                },
                column];
        }

        static int ParsePadding(ReadOnlySpan<byte> data) => (data[2] >> 1) & 0b00000001;

        static int ParseChannels(ReadOnlySpan<byte> data) => ((data[3] >> 6) & 0b00000011) == 0b00000011 ? 1 : 2;

        static int CalculateSamplesPerFrame(MpegVersion mpegVersion) => mpegVersion == MpegVersion.One ? 1152 : 576;

        static int CalculateSideInfoLength(MpegVersion mpegVersion, int channels)
        {
            if (channels == 1)
                return mpegVersion == MpegVersion.One ? 17 : 9;
            return mpegVersion == MpegVersion.One ? 32 : 17;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        enum MpegVersion
        {
            TwoPointFive = 0b00000000,
            Two = 0b00000010,
            One = 0b00000011
        }
    }
}