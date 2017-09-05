using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public class InvalidTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not RIFF Format.wav" },
            new object[] { "Unexpectedly Truncated.wav" },
            new object[] { "Not Wave Format.wav" },
            new object[] { "Missing 'fmt' Chunk.wav" },
            new object[] { "Not MPEG Audio.mp3"},
            new object[] { "Not Audio Layer III.mp3"},
            new object[] { "Not MPEG Audio.m4a" }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}