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
            // Basic rename
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "Testing 123",
                "Testing 123.wav"
            },

            // Metadata substitution
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

            // Composite of multiple metadata fields
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

            // Requested metadata not present
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "{Title} by {Artist}",
                "Unknown Title by Unknown Artist.wav"
            },

            // Metadata with invalid characters
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title <> with |invalid \"characters\""
                },
                "{Title}",
                "Test Title with invalid characters.wav"
            },

            // New name matches old
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "LPCM 16-bit 44100Hz Stereo",
                "LPCM 16-bit 44100Hz Stereo.wav"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesMetadataAndNames
        {
            // Prepend an index to each row
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[1], item[2] });
        }
    }
}
