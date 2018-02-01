using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class RenameValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "Testing 123",
                "Testing 123.wav"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title"
                },
                "{Title}",
                "Test Title.wav"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist"
                },
                "{Title} by {Artist}",
                "Test Title by Test Artist.wav"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "{Title} by {Artist}",
                "Unknown Title by Unknown Artist.wav"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            // Prepend an index to each row
            [UsedImplicitly] get => _data.Select((item, index) => item.Prepend(index).ToArray());
        }
    }
}
