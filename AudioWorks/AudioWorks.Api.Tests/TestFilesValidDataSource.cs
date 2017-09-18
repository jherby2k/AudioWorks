using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Api.Tests
{
    public static class TestFilesValidDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 8, 8000, 22515),
                new AudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                AudioInfo.CreateForLossless("LPCM", 1, 16, 44100, 124112),
                new AudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 44100, 124112),
                new AudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 48000, 135087),
                new AudioMetadata()
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 24, 96000, 270174),
                new AudioMetadata()
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo (extensible).wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 44100, 124112),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 24 8000Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 8000, 24192, 24571),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 64 44100Hz Mono.mp3",
                AudioInfo.CreateForLossy("MP3", 1, 44100, 125568, 64582),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (no header).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 128000),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 48000Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 48000, 137088, 129076),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame VBR Standard 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 213358),
                new AudioMetadata()
            },
            new object[]
            {
                "Fraunhofer CBR 128 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 128000),
                new AudioMetadata()
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 160000),
                new AudioMetadata()
            },
            new object[]
            {
                "Fraunhofer VBR 44100Hz Stereo (with header).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 126720, 143200),
                new AudioMetadata()
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new AudioMetadata
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
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new AudioMetadata
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
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new AudioMetadata
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
                AudioInfo.CreateForLossy("AAC", 2, 8000, 25600, 50795),
                new AudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Mono.m4a",
                AudioInfo.CreateForLossy("AAC", 1, 44100, 126976, 93207),
                new AudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                AudioInfo.CreateForLossy("AAC", 2, 44100, 126976, 183702),
                new AudioMetadata()
            },
            new object[]
            {
                "QAAC TVBR 91 48000Hz Stereo.m4a",
                AudioInfo.CreateForLossy("AAC", 2, 48000, 137216, 197757),
                new AudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                AudioInfo.CreateForLossless("ALAC", 1, 16, 44100, 122880),
                new AudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 16, 44100, 122880),
                new AudioMetadata()
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 16, 48000, 131072),
                new AudioMetadata()
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 24, 96000, 266240),
                new AudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 8000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 8000, 0, 31800),
                new AudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Mono.ogg",
                AudioInfo.CreateForLossy("Vorbis", 1, 44100, 0, 80000),
                new AudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 0, 112000),
                new AudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 48000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 48000, 0, 112000),
                new AudioMetadata()
            },
            new object[]
            {
                "Vorbis Quality 3 96000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 96000),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 8, 8000, 22515),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                AudioInfo.CreateForLossless("FLAC", 1, 16, 44100, 124112),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 48000, 135087),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 24, 96000, 270174),
                new AudioMetadata()
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new AudioMetadata
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
