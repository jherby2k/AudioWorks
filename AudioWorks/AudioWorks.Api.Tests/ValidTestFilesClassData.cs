using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public sealed class ValidTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                new AudioInfo("LPCM", 2, 8, 8000, 22515)
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                new AudioInfo("LPCM", 1, 16, 44100, 124112)
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new AudioInfo("LPCM", 2, 16, 44100, 124112)
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                new AudioInfo("LPCM", 2, 16, 48000, 135087)
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                new AudioInfo("LPCM", 2, 24, 96000, 270174)
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo (extensible).wav",
                new AudioInfo("LPCM", 2, 16, 44100, 124112)
            },
            new object[]
            {
                "Lame CBR 24 8000Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 8000, 24192)
            },
            new object[]
            {
                "Lame CBR 64 44100Hz Mono.mp3",
                new AudioInfo("MP3", 1, 0, 44100, 125568)
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 44100, 125568)
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (no header).mp3",
                new AudioInfo("MP3", 2, 0, 44100, 0)
            },
            new object[]
            {
                "Lame CBR 128 48000Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 48000, 137088)
            },
            new object[]
            {
                "Lame VBR Standard 44100Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 44100, 125568)
            },
            new object[]
            {
                "Fraunhofer CBR 128 44100Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 44100, 0)
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo.mp3",
                new AudioInfo("MP3", 2, 0, 44100, 0)
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo (with header).mp3",
                new AudioInfo("MP3", 2, 0, 44100, 126720)
            },
            new object[]
            {
                "QAAC TVBR 91 8000Hz Stereo.m4a",
                new AudioInfo("AAC", 2, 0, 8000, 25600)
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Mono.m4a",
                new AudioInfo("AAC", 1, 0, 44100, 126976)
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new AudioInfo("AAC", 2, 0, 44100, 126976)
            },
            new object[]
            {
                "QAAC TVBR 91 48000Hz Stereo.m4a",
                new AudioInfo("AAC", 2, 0, 48000, 137216)
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                new AudioInfo("ALAC", 1, 16, 44100, 122880)
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new AudioInfo("ALAC", 2, 16, 44100, 122880)
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                new AudioInfo("ALAC", 2, 16, 48000, 131072)
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                new AudioInfo("ALAC", 2, 24, 96000, 266240)
            },
            new object[]
            {
                "Vorbis Quality 3 8000Hz Stereo.ogg",
                new AudioInfo("Vorbis", 2, 0, 8000, 0)
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Mono.ogg",
                new AudioInfo("Vorbis", 1, 0, 44100, 0)
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new AudioInfo("Vorbis", 2, 0, 44100, 0)
            },
            new object[]
            {
                "Vorbis Quality 3 48000Hz Stereo.ogg",
                new AudioInfo("Vorbis", 2, 0, 48000, 0)
            },
            new object[]
            {
                "Vorbis Quality 3 96000Hz Stereo.ogg",
                new AudioInfo("Vorbis", 2, 0, 96000, 0)
            }
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