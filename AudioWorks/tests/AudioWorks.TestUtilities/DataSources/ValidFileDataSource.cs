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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class ValidFileDataSource
    {
        static readonly IEnumerable<TheoryDataRow<string, AudioInfo, AudioMetadata>> _data =
        [
            #region Wave

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 8, 8000, 22515),
                new()
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                AudioInfo.CreateForLossless("LPCM", 1, 16, 44100, 124112),
                new()
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 44100, 124112),
                new()
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 48000, 135087),
                new()
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                AudioInfo.CreateForLossless("LPCM", 2, 24, 96000, 270174),
                new()
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo (extensible).wav",
                AudioInfo.CreateForLossless("LPCM", 2, 16, 44100, 124112),
                new()
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                AudioInfo.CreateForLossy("A-law", 2, 44100, 124112, 705600),
                new()
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                AudioInfo.CreateForLossy("µ-law", 2, 44100, 124112, 705600),
                new()
            ),

            #endregion

            #region FLAC

            new(
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 8, 8000, 22515),
                new()
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                AudioInfo.CreateForLossless("FLAC", 1, 16, 44100, 124112),
                new()
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
                new()
            ),
            new(
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 48000, 135087),
                new()
            ),
            new(
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                AudioInfo.CreateForLossless("FLAC", 2, 24, 96000, 270174),
                new()
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using mixed-case fields).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using COMMENT).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TOTALTRACKS).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKCOUNT).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using TRACKTOTAL).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using YEAR).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using extended DATE).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid DATE).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid TRACKNUMBER).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using ReplayGain with '+' sign).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using ReplayGain with missing 'dB').flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using invalid ReplayGain fields).flac",
                AudioInfo.CreateForLossless("FLAC", 2, 16, 44100, 124112),
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
            ),

            #endregion

            #region ALAC

            new(
                "ALAC 16-bit 44100Hz Mono.m4a",
                AudioInfo.CreateForLossless("ALAC", 1, 16, 44100, 122880),
                new()
            ),
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 16, 44100, 122880),
                new()
            ),
            new(
                "ALAC 16-bit 48000Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 16, 48000, 131072),
                new()
            ),
            new(
                "ALAC 24-bit 96000Hz Stereo.m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 24, 96000, 266240),
                new()
            ),
            new(
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                AudioInfo.CreateForLossless("ALAC", 2, 16, 44100, 122880),
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
            ),

            #endregion

            #region AAC

            new(
                "QAAC TVBR 91 8000Hz Stereo.m4a",
                AudioInfo.CreateForLossy("AAC", 2, 8000, 25600, 50795),
                new()
            ),
            new(
                "QAAC TVBR 91 44100Hz Mono.m4a",
                AudioInfo.CreateForLossy("AAC", 1, 44100, 126976, 93207),
                new()
            ),
            new(
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                AudioInfo.CreateForLossy("AAC", 2, 44100, 126976, 183702),
                new()
            ),
            new(
                "QAAC TVBR 91 48000Hz Stereo.m4a",
                AudioInfo.CreateForLossy("AAC", 2, 48000, 137216, 197757),
                new()
            ),
            new(
                "QAAC TVBR 91 44100Hz Stereo (Tagged).m4a",
                AudioInfo.CreateForLossy("AAC", 2, 44100, 126976, 183702),
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
            ),

            #endregion

            #region MP3

            new(
                "Lame CBR 24 8000Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 8000, 24192, 24571),
                new()
            ),
            new(
                "Lame CBR 64 44100Hz Mono.mp3",
                AudioInfo.CreateForLossy("MP3", 1, 44100, 125568, 64582),
                new()
            ),
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new()
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (no header).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 128000),
                new()
            ),
            new(
                "Lame CBR 128 48000Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 48000, 137088, 129076),
                new()
            ),
            new(
                "Lame VBR Standard 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 213358),
                new()
            ),
            new(
                "Fraunhofer CBR 128 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 128000),
                new()
            ),
            new(
                "Fraunhofer VBR 44100Hz Stereo.mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 0, 160000),
                new()
            ),
            new(
                "Fraunhofer VBR 44100Hz Stereo (with header).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 126720, 143200),
                new()
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v1 missing values).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Comment = "Test Comment"
                }
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1 Extended Header).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 UTF16).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 UTF16 Unsynchronised).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF16 Unsynchronised Frames).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 Latin1).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF16 Big Endian).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 UTF8).mp3",
                AudioInfo.CreateForLossy("MP3", 2, 44100, 125568, 129170),
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
            ),

            #endregion

            #region Ogg Vorbis

            new(
                "Vorbis Quality 3 8000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 8000, 22515, 31800),
                new()
            ),
            new(
                "Vorbis Quality 3 44100Hz Mono.ogg",
                AudioInfo.CreateForLossy("Vorbis", 1, 44100, 124112, 80000),
                new()
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
                new()
            ),
            new(
                "Vorbis Quality 3 48000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 48000, 135087, 112000),
                new()
            ),
            new(
                "Vorbis Quality 3 96000Hz Stereo.ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 96000, 270174),
                new()
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using mixed-case fields).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using COMMENT).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TOTALTRACKS).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TRACKCOUNT).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using TRACKTOTAL).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using YEAR).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using extended DATE).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid DATE).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid TRACKNUMBER).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using ReplayGain with '+' sign).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using ReplayGain with missing 'dB').ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using invalid ReplayGain fields).ogg",
                AudioInfo.CreateForLossy("Vorbis", 2, 44100, 124112, 112000),
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
            ),

            #endregion

            #region Opus

            new(
                "Opus VBR 8000Hz Stereo.opus",
                AudioInfo.CreateForLossy("Opus", 2, 8000, 22515),
                new()
            ),
            new(
                "Opus VBR 44100Hz Mono.opus",
                AudioInfo.CreateForLossy("Opus", 1, 44100, 124112),
                new()
            ),
            new(
                "Opus VBR 44100Hz Stereo.opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
                new()
            ),
            new(
                "Opus VBR 48000Hz Stereo.opus",
                AudioInfo.CreateForLossy("Opus", 2, 48000, 135087),
                new()
            ),
            new(
                "Opus VBR 96000Hz Stereo.opus",
                AudioInfo.CreateForLossy("Opus", 2, 96000, 270174),
                new()
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using mixed-case fields).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using COMMENT).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using TOTALTRACKS).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using TRACKCOUNT).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using TRACKTOTAL).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using YEAR).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using extended DATE).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using invalid DATE).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using invalid TRACKNUMBER).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN with '+' sign).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using REPLAYGAIN with missing 'dB').opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            ),
            new(
                "Opus VBR 44100Hz Stereo (Tagged using R128).opus",
                AudioInfo.CreateForLossy("Opus", 2, 44100, 124112),
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
            )

            #endregion
        ];

        public static IEnumerable<TheoryDataRow<string>> FileNames =>
            _data.Select(item => new TheoryDataRow<string>(item.Data.Item1) { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string>> FileNamesSaveCompatible =>
            _data.Where(item => !Path.GetExtension(item.Data.Item1).Equals(".wav", System.StringComparison.OrdinalIgnoreCase))
                .Select(item => new TheoryDataRow<string>(item.Data.Item1) { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string, AudioInfo>> FileNamesAndAudioInfo =>
            _data.Select(item => new TheoryDataRow<string, AudioInfo>(item.Data.Item1, item.Data.Item2)
                { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string, AudioMetadata>> FileNamesAndMetadata =>
            _data.Select(item => new TheoryDataRow<string, AudioMetadata>(item.Data.Item1, item.Data.Item3)
                { Skip = item.Skip });
    }
}
