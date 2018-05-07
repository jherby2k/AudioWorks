using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidFileWithCoverArtDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                1280,
                935,
                24,
                true,
                "image/png",
                "6895D34A230827FE1F7C3141AC0454FA"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "9790278B4ADC6F23407AA11BD065F229"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - PNG).m4a",
                1280,
                935,
                24,
                true,
                "image/png",
                "6895D34A230827FE1F7C3141AC0454FA"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - JPEG).m4a",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "9790278B4ADC6F23407AA11BD065F229"
            },

            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - PNG).mp3",
                1280,
                935,
                24,
                true,
                "image/png",
                "6895D34A230827FE1F7C3141AC0454FA"
            },

            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - JPEG).mp3",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "9790278B4ADC6F23407AA11BD065F229"
            },

            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - PNG).ogg",
                1280,
                935,
                24,
                true,
                "image/png",
                "6895D34A230827FE1F7C3141AC0454FA"
            },

            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - JPEG).ogg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "9790278B4ADC6F23407AA11BD065F229"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndWidth
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[1] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndHeight
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[2] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndColorDepth
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[3] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndLossless
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[4] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndMimeType
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[5] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndDataHash
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[6] });
        }
    }
}
