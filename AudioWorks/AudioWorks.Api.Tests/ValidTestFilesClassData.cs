using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using AudioWorks.Common;

namespace AudioWorks.Api.Tests
{
    public class ValidTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "LPCM 8-bit 8000Hz Stereo.wav", new AudioInfo("LPCM") },
            new object[] { "LPCM 16-bit 44100Hz Mono.wav", new AudioInfo("LPCM") },
            new object[] { "LPCM 16-bit 44100Hz Stereo.wav", new AudioInfo("LPCM") },
            new object[] { "LPCM 16-bit 48000Hz Stereo.wav", new AudioInfo("LPCM") },
            new object[] { "LPCM 24-bit 96000Hz Stereo.wav", new AudioInfo("LPCM") }
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