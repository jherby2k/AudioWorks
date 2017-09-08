﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public sealed class InvalidTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
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

        [NotNull]
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        [NotNull]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}