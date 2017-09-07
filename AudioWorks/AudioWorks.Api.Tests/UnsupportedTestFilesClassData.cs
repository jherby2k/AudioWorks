using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public class UnsupportedTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not Audio.txt" },
            new object[] { "MS ADPCM.wav" },
            new object[] { "Speex.ogg" },
            new object[] { "Lame MP3.m4a" }
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