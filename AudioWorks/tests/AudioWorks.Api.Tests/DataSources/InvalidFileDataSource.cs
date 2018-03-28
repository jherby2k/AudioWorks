using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class InvalidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not RIFF Format.wav" },
            new object[] { "Unexpectedly Truncated.wav" },
            new object[] { "Not Wave Format.wav" },
            new object[] { "Missing 'fmt' Chunk.wav" },
            new object[] { "Not MPEG Audio.mp3"},
            new object[] { "Not Audio Layer III.mp3"},
            new object[] { "Not MPEG Audio.m4a" },
            new object[] { "Not Ogg Format.ogg"},
            new object[] { "Not FLAC Format.flac"}
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}