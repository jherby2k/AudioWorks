using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Commands.Tests
{
    public class TestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "LPCM 8-bit 8000Hz Stereo.wav" },
            new object[] { "LPCM 16-bit 44100Hz Mono.wav" },
            new object[] { "LPCM 16-bit 44100Hz Stereo.wav" },
            new object[] { "LPCM 16-bit 48000Hz Stereo.wav" },
            new object[] { "LPCM 24-bit 96000Hz Stereo.wav" }
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