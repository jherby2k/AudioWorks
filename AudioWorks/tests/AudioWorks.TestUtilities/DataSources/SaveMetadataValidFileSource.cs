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

using System;
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class SaveMetadataValidFileSource
    {
        static readonly IEnumerable<TheoryDataRow<string, AudioMetadata, string, SettingDictionary, string[]>> _data =
        [
            #region FLAC

            // All fields
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "AF-3A-82-82-CA-64-A3-89", // FLAC 1.3.3 (Ubuntu 22.04)
                    "1F-93-A8-53-FB-3B-B0-22", // FLAC 1.4.3 (Ubuntu 24.04)
                    "2A-41-22-DD-B8-14-0C-D3" // FLAC 1.5.0
                ]
            ),

            // Day unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "2E-3D-66-54-54-94-EC-27", // FLAC 1.3.3 (Ubuntu 22.04)
                    "0C-50-86-D5-AB-CD-71-64", // FLAC 1.4.3 (Ubuntu 24.04)
                    "23-9C-CB-3F-66-6D-10-6C" // FLAC 1.5.0
                ]
            ),

            // Month unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "2E-3D-66-54-54-94-EC-27", // FLAC 1.3.3 (Ubuntu 22.04)
                    "0C-50-86-D5-AB-CD-71-64", // FLAC 1.4.3 (Ubuntu 24.04)
                    "23-9C-CB-3F-66-6D-10-6C" // FLAC 1.5.0
                ]
            ),

            // TrackNumber unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "8B-52-E4-4F-02-7C-EA-7A", // FLAC 1.3.3 (Ubuntu 22.04)
                    "BF-10-71-E4-53-3D-9C-EC", // FLAC 1.4.3 (Ubuntu 24.04)
                    "F3-5B-84-04-C2-48-ED-2F" // FLAC 1.5.0
                ]
            ),

            // TrackCount unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "A2-C5-A7-7E-4A-55-FB-EE", // FLAC 1.3.3 (Ubuntu 22.04)
                    "B7-98-8D-AF-91-08-55-27", // FLAC 1.4.3 (Ubuntu 24.04)
                    "69-92-C3-94-61-D2-99-5E" // FLAC 1.5.0
                ]
            ),

            // Existing tag
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "AF-3A-82-82-CA-64-A3-89", // FLAC 1.3.3 (Ubuntu 22.04)
                    "1F-93-A8-53-FB-3B-B0-22", // FLAC 1.4.3 (Ubuntu 24.04)
                    "2A-41-22-DD-B8-14-0C-D3" // FLAC 1.5.0
                ]
            ),

            // No padding
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["Padding"] = 0
                },
                [
                    "CC-73-52-3E-62-0F-59-5A", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F2-23-FD-31-4D-D5-57-CF", // FLAC 1.4.3 (Ubuntu 24.04)
                    "C8-60-60-FE-8E-09-84-43" // FLAC 1.5.0
                ]
            ),

            // 100 bytes of padding
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["Padding"] = 100
                },
                [
                    "A7-80-B9-87-39-E1-6C-19", // FLAC 1.3.3 (Ubuntu 22.04)
                    "9B-D5-D5-28-77-5A-0E-AD", // FLAC 1.4.3 (Ubuntu 24.04)
                    "7E-CE-CD-BA-95-58-65-5E" // FLAC 1.5.0
                ]
            ),

            // Existing tag removal
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new(),
                string.Empty,
                [],
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "60-66-EA-A0-B4-EC-85-FE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "CC-E4-C6-E8-D5-98-2C-50" // FLAC 1.5.0
                ]
            ),

            // Nothing to do
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                string.Empty,
                [],
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "60-66-EA-A0-B4-EC-85-FE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "CC-E4-C6-E8-D5-98-2C-50" // FLAC 1.5.0
                ]
            ),

            // PNG CoverArt
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "C6-D0-76-A4-3C-7A-8F-4D", // FLAC 1.3.3 (Ubuntu 22.04)
                    "C2-09-C9-DA-B4-96-7E-FF", // FLAC 1.4.3 (Ubuntu 24.04)
                    "EA-A4-B8-36-7D-F9-C7-4C" // FLAC 1.5.0
                ]
            ),

            // JPEG CoverArt
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "DF-13-9A-85-CD-B9-D1-67", // FLAC 1.3.3 (Ubuntu 22.04)
                    "57-39-62-59-3A-82-7D-5B", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8E-9B-49-2B-B0-19-E5-65" // FLAC 1.5.0
                ]
            ),

            #endregion

            #region MP4

            // All fields
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "A3-D6-48-6B-CA-E2-D5-97"
                ]
            ),

            // Day unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "4B-68-BF-33-FB-B8-27-0A"
                ]
            ),

            // Month unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "4B-68-BF-33-FB-B8-27-0A"
                ]
            ),

            // TrackNumber unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
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
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "FC-3B-0D-B7-04-75-55-AC"
                ]
            ),

            // TrackCount unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "43-32-C2-41-EA-7F-33-1A"
                ]
            ),

            // Existing tag
            new(
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "A3-D6-48-6B-CA-E2-D5-97"
                ]
            ),

            // Updated creation time
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["CreationTime"] = new DateTime(2018, 9, 1)
                },
                [
                    "A3-73-CA-C2-70-83-6A-70"
                ]
            ),

            // Updated modification time
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["ModificationTime"] = new DateTime(2018, 9, 1)
                },
                [
                    "6A-8E-6E-E5-07-28-ED-FA"
                ]
            ),

            // Existing tag removal
            new(
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new(),
                string.Empty,
                [],
                [
                    "B6-5C-98-7A-12-EC-75-EC"
                ]
            ),

            // Nothing to do
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                [],
                [
                    "B6-5C-98-7A-12-EC-75-EC"
                ]
            ),

            // Default padding (explicit)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 2048
                },
                [
                    "B6-5C-98-7A-12-EC-75-EC"
                ]
            ),

            // Disabled padding
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 0
                },
                [
                    "B9-71-63-FE-29-1D-0A-E6"
                ]
            ),

            // Maximum padding
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 16_777_216
                },
                [
                    "31-4B-2C-9A-B1-33-BF-6F"
                ]
            ),

            // PNG CoverArt (ALAC)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "25-9F-C1-64-8C-2E-38-6D"
                ]
            ),

            // JPEG CoverArt (ALAC)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "6F-52-4E-59-2F-BA-30-BD"
                ]
            ),

            // PNG CoverArt (AAC, converted)
            new(
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "96-5A-C2-F0-3A-B9-F7-25", // Intel
                    "2C-E4-1D-38-31-48-BB-F2" // ARM
                ]
            ),

            // JPEG CoverArt (AAC)
            new(
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "BD-89-E8-65-5C-A4-96-2C"
                ]
            ),

            #endregion

            #region ID3

            // All fields
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "57-8E-2B-BB-F9-74-44-92"
                ]
            ),

            // Day unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "E0-43-FB-65-FB-BB-87-31"
                ]
            ),

            // Month unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "E0-43-FB-65-FB-BB-87-31"
                ]
            ),

            // TrackNumber unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "85-56-8B-7E-07-58-A4-60"
                ]
            ),

            // TrackCount unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "DE-60-49-12-77-CB-03-89"
                ]
            ),

            // Existing v2.3 tag
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "A8-14-E8-04-B0-98-53-2E"
                ]
            ),

            // Existing v2.4 tag
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.4 Latin1).mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "5E-A0-8C-95-72-D8-E2-86"
                ]
            ),

            // Tag version 2.4
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagVersion"] = "2.4"
                },
                [
                    "6A-E3-42-4C-20-8F-32-0C"
                ]
            ),

            // UTF-16 encoding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "74-62-41-91-56-57-DA-6E"
                ]
            ),

            // UTF-8 encoding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "02-C3-B4-52-A5-9C-96-B6"
                ]
            ),

            // Multibyte characters present (implicit UTF-16)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "8B-AA-30-14-4E-B9-98-13"
                ]
            ),

            // Multibyte characters present (explicit UTF-16)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "8B-AA-30-14-4E-B9-98-13"
                ]
            ),

            // Multibyte characters present (overridden as UTF-16)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "Latin1"
                },
                [
                    "8B-AA-30-14-4E-B9-98-13"
                ]
            ),

            // Multibyte characters present (explicit UTF-8)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "F7-4C-9F-78-16-83-0D-A1"
                ]
            ),

            // Multibyte characters present (implicit UTF-8)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagVersion"] = "2.4"
                },
                [
                    "F7-4C-9F-78-16-83-0D-A1"
                ]
            ),

            // Multibyte characters present (overridden as UTF-8)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "テストコメント",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagVersion"] = "2.4",
                    ["TagEncoding"] = "Latin1"
                },
                [
                    "F7-4C-9F-78-16-83-0D-A1"
                ]
            ),

            // Default padding (explicit)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagPadding"] = 2048
                },
                [
                    "57-8E-2B-BB-F9-74-44-92"
                ]
            ),

            // No padding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagPadding"] = 0
                },
                [
                    "50-14-36-22-35-FA-EF-56"
                ]
            ),

            // Maximum padding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                new()
                {
                    ["TagPadding"] = 16_777_216
                },
                [
                    "5F-49-B9-DF-FA-CC-2E-DE"
                ]
            ),

            // Existing v1 tag
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "57-8E-2B-BB-F9-74-44-92"
                ]
            ),

            // Existing tag removal
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new(),
                string.Empty,
                [],
                [
                    "46-88-09-DE-2B-11-6E-1A"
                ]
            ),

            // Nothing to do
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                string.Empty,
                [],
                [
                    "1D-D7-43-D4-6C-86-16-62"
                ]
            ),

            // PNG CoverArt (converted)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "94-C7-0A-15-23-0B-29-78", // Intel
                    "FA-C5-BF-D3-7E-4C-39-A8" // ARM
                ]
            ),

            // JPEG CoverArt
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "9A-46-83-DD-6B-29-CE-F1"
                ]
            ),

            #endregion

            #region Ogg Vorbis

            // All fields
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "16-A6-44-C7-4D-4A-D6-28", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "DF-99-54-20-04-92-45-96" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // Day unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "C0-88-80-B1-9F-D2-0E-F5", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "33-BD-B0-E3-16-03-0A-E2" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // Month unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "C0-88-80-B1-9F-D2-0E-F5", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "33-BD-B0-E3-16-03-0A-E2" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // TrackNumber unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
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
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "D7-99-7E-50-17-9D-8A-91", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "68-64-92-28-3D-9B-BE-CE" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // TrackCount unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "FD-F6-85-33-B3-E4-9A-44", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "02-49-45-A0-E6-C8-24-65" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // Existing tag
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                string.Empty,
                [],
                [
                    "16-A6-44-C7-4D-4A-D6-28", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "DF-99-54-20-04-92-45-96" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // Existing tag removal
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new(),
                string.Empty,
                [],
                [
                    "58-4F-37-65-C3-D1-39-CE", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "14-46-94-9D-94-B4-99-9F" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // Nothing to do
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                string.Empty,
                [],
                [
                    "58-4F-37-65-C3-D1-39-CE", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "14-46-94-9D-94-B4-99-9F" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            // PNG CoverArt (Converted)
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "7D-E9-01-03-FF-5B-14-4D", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "80-0C-7E-A6-86-25-15-E1", // Vorbis 1.3.7 (MacOS and Ubuntu on Intel)
                    "CD-D4-7B-99-FE-2A-E3-22" // Vorbis 1.3.7 (MacOS on ARM)
                ]
            ),

            // JPEG CoverArt
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "A3-5F-BD-C2-CB-CF-1C-C3", // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                    "C4-9D-42-B4-A8-D2-A0-9D" // Vorbis 1.3.7 (MacOS and Ubuntu)
                ]
            ),

            #endregion

            #region Opus

            // All fields
            new(
                "Opus VBR 44100Hz Stereo.opus",
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
                },
                string.Empty,
                [],
                [
                    "C9-B4-B0-83-AD-49-AF-40", // Opus 1.3.1 (Ubuntu 22.04)
                    "EC-82-E8-0B-85-21-C4-73", // Opus 1.4.0 (Ubuntu 24.04)
                    "57-56-1E-D3-88-59-8A-3B" // Opus 1.5.2
                ]
            ),

            // Day unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "0B-F6-81-EC-C7-83-2B-99", // Opus 1.3.1 (Ubuntu 22.04)
                    "9F-D8-65-86-39-BE-97-CC", // Opus 1.4.0 (Ubuntu 24.04)
                    "DB-24-7A-6E-D5-A7-D9-62" // Opus 1.5.2
                ]
            ),

            // Month unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "0B-F6-81-EC-C7-83-2B-99", // Opus 1.3.1 (Ubuntu 22.04)
                    "9F-D8-65-86-39-BE-97-CC", // Opus 1.4.0 (Ubuntu 24.04)
                    "DB-24-7A-6E-D5-A7-D9-62" // Opus 1.5.2
                ]
            ),

            // TrackNumber unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "50-98-B1-5D-E0-92-64-C5", // Opus 1.3.1 (Ubuntu 22.04)
                    "28-DF-77-23-AF-DB-27-69", // Opus 1.4.0 (Ubuntu 24.04)
                    "39-76-55-AE-E3-AB-5F-98" // Opus 1.5.2
                ]
            ),

            // TrackCount unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackNumber = "01"
                },
                string.Empty,
                [],
                [
                    "54-B3-49-64-05-D9-57-0E", // Opus 1.3.1 (Ubuntu 22.04)
                    "13-BF-62-84-06-2C-9E-77", // Opus 1.4.0 (Ubuntu 24.04)
                    "89-44-76-37-67-35-C5-11" // Opus 1.5.2
                ]
            ),

            // Existing tag
            new(
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
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
                },
                string.Empty,
                [],
                [
                    "22-33-C1-62-7D-9A-3F-D4", // Opus 1.3.1 (Ubuntu 22.04)
                    "17-9A-09-36-5A-B2-E4-E2", // Opus 1.4.0 (Ubuntu 24.04)
                    "AD-2C-39-1F-8B-7F-55-FE" // Opus 1.5.2
                ]
            ),

            // Existing tag removal
            new(
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new(),
                string.Empty,
                [],
                [
                    "46-AB-BF-EC-52-AF-8B-BC", // Opus 1.3.1 (Ubuntu 22.04)
                    "44-25-76-F8-74-C9-A2-F8", // Opus 1.4.0 (Ubuntu 24.04)
                    "D3-AA-F1-4B-AB-E3-6A-E1" // Opus 1.5.2
                ]
            ),

            // Nothing to do
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                string.Empty,
                [],
                [
                    "EF-63-42-BA-25-71-42-DF", // Opus 1.3.1 (Ubuntu 22.04)
                    "46-C7-BE-A2-D0-19-35-0E", // Opus 1.4.0 (Ubuntu 24.04)
                    "69-1C-BC-A0-14-50-7E-F4" // Opus 1.5.2
                ]
            ),

            // PNG CoverArt (Converted)
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "DE-E0-24-25-09-74-66-9D", // Opus 1.3.1 (Ubuntu 22.04)
                    "7A-5A-1C-65-EB-FC-54-36", // Opus 1.4.0 (Ubuntu 24.04)
                    "FC-00-36-0A-1E-9A-C9-89", // Opus 1.5.2 (Intel)
                    "E1-3A-2C-E7-53-03-A2-02" // Opus 1.5.2 (ARM)
                ]
            ),

            // JPEG CoverArt
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "6A-BE-38-6F-7A-74-8D-A9", // Opus 1.3.1 (Ubuntu 22.04)
                    "A2-E6-3B-0F-2B-3A-5F-F1", // Opus 1.4.0 (Ubuntu 24.04)
                    "3E-3D-BD-8A-3C-D5-15-B5" // Opus 1.5.2
                ]
            )

            #endregion
        ];

        public static IEnumerable<TheoryDataRow<int, string, AudioMetadata, string, SettingDictionary, string[]>> Data =>
            _data.Select((item, index) =>
                new TheoryDataRow<int, string, AudioMetadata, string, SettingDictionary, string[]>(
                        index, item.Data.Item1, item.Data.Item2, item.Data.Item3, item.Data.Item4, item.Data.Item5));
    }
}
