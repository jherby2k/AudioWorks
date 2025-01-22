/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Linq;
using AudioWorks.TestUtilities.DataTypes;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class ValidFileDataSource
    {
        static readonly TheoryData<string, TestAudioInfo, TestAudioMetadata> _data = new()
        {
            #region Wave

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 8,
                    SampleRate = 8000,
                    SampleCount = 22515,
                    BitRate = 128000
                },
                new()
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new()
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 135087,
                    BitRate = 1536000
                },
                new()
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 270174,
                    BitRate = 4608000
                },
                new()
            },
            {
                "LPCM 16-bit 44100Hz Stereo (extensible).wav",
                new()
                {
                    Format = "LPCM",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
            },
            {
                "A-law 44100Hz Stereo.wav",
                new()
                {
                    Format = "A-law",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new()
            },
            {
                "µ-law 44100Hz Stereo.wav",
                new()
                {
                    Format = "µ-law",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new()
            },

            #endregion

            #region FLAC

            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 8,
                    SampleRate = 8000,
                    SampleCount = 22515,
                    BitRate = 128000
                },
                new()
            },
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 705600
                },
                new()
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
            },
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 135087,
                    BitRate = 1536000
                },
                new()
            },
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 270174,
                    BitRate = 4608000
                },
                new()
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using mixed-case fields).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using COMMENT).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TOTALTRACKS).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKCOUNT).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKTOTAL).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using YEAR).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using extended DATE).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid DATE).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid TRACKNUMBER).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using ReplayGain with '+' sign).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using ReplayGain with missing 'dB').flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid ReplayGain fields).flac",
                new()
                {
                    Format = "FLAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },

            #endregion

            #region ALAC

            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                new()
                {
                    Format = "ALAC",
                    Channels = 1,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 705600
                },
                new()
            },
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 1411200
                },
                new()
            },
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                new()
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 48000,
                    SampleCount = 131072,
                    BitRate = 1536000
                },
                new()
            },
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                new()
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 24,
                    SampleRate = 96000,
                    SampleCount = 266240,
                    BitRate = 4608000
                },
                new()
            },
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new()
                {
                    Format = "ALAC",
                    Channels = 2,
                    BitsPerSample = 16,
                    SampleRate = 44100,
                    SampleCount = 122880,
                    BitRate = 1411200
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },

            #endregion

            #region AAC

            {
                "QAAC TVBR 91 8000Hz Stereo.m4a",
                new()
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 25600,
                    BitRate = 50795
                },
                new()
            },
            {
                "QAAC TVBR 91 44100Hz Mono.m4a",
                new()
                {
                    Format = "AAC",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 93207
                },
                new()
            },
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new()
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 183702
                },
                new()
            },
            {
                "QAAC TVBR 91 48000Hz Stereo.m4a",
                new()
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 137216,
                    BitRate = 197757
                },
                new()
            },
            {
                "QAAC TVBR 91 44100Hz Stereo (Tagged).m4a",
                new()
                {
                    Format = "AAC",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126976,
                    BitRate = 183702
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },

            #endregion

            #region MP3

            {
                "Lame CBR 24 8000Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 24192,
                    BitRate = 24571
                },
                new()
            },
            {
                "Lame CBR 64 44100Hz Mono.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 64582
                },
                new()
            },
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
            },
            {
                "Lame CBR 128 44100Hz Stereo (no header).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 128000
                },
                new()
            },
            {
                "Lame CBR 128 48000Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 137088,
                    BitRate = 129076
                },
                new()
            },
            {
                "Lame VBR Standard 44100Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 213358
                },
                new()
            },
            {
                "Fraunhofer CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 128000
                },
                new()
            },
            {
                "Fraunhofer VBR 44100Hz Stereo.mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    BitRate = 160000
                },
                new()
            },
            {
                "Fraunhofer VBR 44100Hz Stereo (with header).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 126720,
                    BitRate = 143200
                },
                new()
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
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
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1 missing values).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Comment = "Test Comment"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1 Extended Header).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 UTF16).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 UTF16 Unsynchronised).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF16 Unsynchronised Frames).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF16 Big Endian).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF8).mp3",
                new()
                {
                    Format = "MP3",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 125568,
                    BitRate = 129170
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },

            #endregion

            #region Ogg Vorbis

            {
                "Vorbis Quality 3 8000Hz Stereo.ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 22515,
                    BitRate = 31800
                },
                new()
            },
            {
                "Vorbis Quality 3 44100Hz Mono.ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 80000
                },
                new()
            },
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
            },
            {
                "Vorbis Quality 3 48000Hz Stereo.ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 135087,
                    BitRate = 112000
                },
                new()
            },
            {
                "Vorbis Quality 3 96000Hz Stereo.ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 96000,
                    SampleCount = 270174
                },
                new()
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using mixed-case fields).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using COMMENT).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TOTALTRACKS).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TRACKCOUNT).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TRACKTOTAL).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using YEAR).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using extended DATE).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid DATE).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid TRACKNUMBER).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using ReplayGain with '+' sign).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using ReplayGain with missing 'dB').ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid ReplayGain fields).ogg",
                new()
                {
                    Format = "Vorbis",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112,
                    BitRate = 112000
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },

            #endregion

            #region Opus

            {
                "Opus VBR 8000Hz Stereo.opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 8000,
                    SampleCount = 22515
                },
                new()
            },
            {
                "Opus VBR 44100Hz Mono.opus",
                new()
                {
                    Format = "Opus",
                    Channels = 1,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
            },
            {
                "Opus VBR 44100Hz Stereo.opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
            },
            {
                "Opus VBR 48000Hz Stereo.opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 48000,
                    SampleCount = 135087
                },
                new()
            },
            {
                "Opus VBR 96000Hz Stereo.opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 96000,
                    SampleCount = 270174
                },
                new()
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using mixed-case fields).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using COMMENT).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using TOTALTRACKS).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using TRACKCOUNT).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using TRACKTOTAL).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using YEAR).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using extended DATE).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using invalid DATE).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    TrackNumber = "01",
                    TrackCount = "12"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using invalid TRACKNUMBER).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN with '+' sign).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN with missing 'dB').opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.500000",
                    AlbumPeak = "0.600000",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            },
            {
                "Opus VBR 44100Hz Stereo (Tagged using R128).opus",
                new()
                {
                    Format = "Opus",
                    Channels = 2,
                    SampleRate = 44100,
                    SampleCount = 124112
                },
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackGain = "0.70",
                    AlbumGain = "0.80"
                }
            }

            #endregion
        };

        public static TheoryData<string> FileNames =>
            new(_data.Select(item => item.Data.Item1));

        public static TheoryData<string, TestAudioInfo> FileNamesAndAudioInfo =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item2)));

        public static TheoryData<string, TestAudioMetadata> FileNamesAndMetadata =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item3)));
    }
}
