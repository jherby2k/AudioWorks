using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 8,
                    SampleRate = 8000,
                    SampleCount = 22515,
                    BitRate = 128000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 135087,
                    BitRate = 1536000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 270174,
                    BitRate = 4608000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo (extensible).wav",
                new TestAudioInfo
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 24 8000Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 24192,
                    BitRate = 24571
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 64 44100Hz Mono.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 64582
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (no header).mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 128000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 48000Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 137088,
                    BitRate = 129076
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame VBR Standard 44100Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 213358
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Fraunhofer CBR 128 44100Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 128000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo.mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 160000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo (with header).mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126720,
                    BitRate = 143200
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Other",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01"
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 UTF16).mp3",
                new TestAudioInfo
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "QAAC TVBR 91 8000Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 25600,
                    BitRate = 50795
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Mono.m4a",
                new TestAudioInfo
                {
                    Format = "AAC",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 93207
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 183702
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 48000Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 137216,
                    BitRate = 197757
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo (Tagged).m4a",
                new TestAudioInfo
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 183702
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                new TestAudioInfo
                {
                    Format = "ALAC",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 705600
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 1411200
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 131072,
                    BitRate = 1536000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                new TestAudioInfo
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 266240,
                    BitRate = 4608000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new TestAudioInfo
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "Vorbis Quality 3 8000Hz Stereo.ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 8000,
                    BitRate = 31800
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Mono.ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 1,
                    SampleRate = 44100,
                    BitRate = 80000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 112000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 48000Hz Stereo.ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 48000,
                    BitRate = 112000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 96000Hz Stereo.ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 96000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged).ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 112000
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using extended DATE).ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 112000
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid DATE).ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 112000
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid TRACKNUMBER).ogg",
                new TestAudioInfo
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 112000
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017"
                }
            },
            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 8,
                    SampleRate = 8000,
                    SampleCount = 22515,
                    BitRate = 128000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 135087,
                    BitRate = 1536000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 270174,
                    BitRate = 4608000
                },
                new TestAudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using mixed-case fields).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using COMMENT).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TOTALTRACKS).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKCOUNT).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKTOTAL).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using YEAR).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using extended DATE).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid DATE).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid TRACKNUMBER).flac",
                new TestAudioInfo
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017"
                }
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNames
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndAudioInfo
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[1] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndMetadata
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[2] });
        }
    }
}
