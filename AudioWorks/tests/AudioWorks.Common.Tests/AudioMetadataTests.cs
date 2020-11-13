﻿/* Copyright © 2018 Jeremy Herbison

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
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AudioWorks.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataTests
    {
        public AudioMetadataTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioMetadata throws an exception if Title is null")]
        public void TitleNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata{ Title = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Title")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidTitle()
        {
            new AudioMetadata { Title = "Test Title" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Title")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyTitle()
        {
            new AudioMetadata { Title = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Title defaults to an empty string")]
        public void TitleDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Title);

        [Fact(DisplayName = "AudioMetadata's Title is properly serialized")]
        public void TitleIsSerialized() =>
            Assert.Equal("Test Title", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Title = "Test Title" }))?.Title);

        [Fact(DisplayName = "AudioMetadata throws an exception if Artist is null")]
        public void ArtistNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Artist = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Artist")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidArtist()
        {
            new AudioMetadata { Artist = "Test Artist" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Artist")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyArtist()
        {
            new AudioMetadata { Artist = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Artist defaults to an empty string")]
        public void ArtistDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Artist);

        [Fact(DisplayName = "AudioMetadata's Artist is properly serialized")]
        public void ArtistIsSerialized() =>
            Assert.Equal("Test Artist", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Artist = "Test Artist" }))?.Artist);

        [Fact(DisplayName = "AudioMetadata throws an exception if Album is null")]
        public void AlbumNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Album = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Album")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidAlbum()
        {
            new AudioMetadata { Album = "Test Album" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Album")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyAlbum()
        {
            new AudioMetadata { Album = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Album defaults to an empty string")]
        public void AlbumDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Album);

        [Fact(DisplayName = "AudioMetadata's Album is properly serialized")]
        public void AlbumIsSerialized() =>
            Assert.Equal("Test Album", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Album = "Test Album" }))?.Album);

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumArtist is null")]
        public void AlbumArtistNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { AlbumArtist = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid AlbumArtist")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidAlbumArtist()
        {
            new AudioMetadata { AlbumArtist = "Test Album Artist" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty AlbumArtist")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyAlbumArtist()
        {
            new AudioMetadata { AlbumArtist = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's AlbumArtist defaults to an empty string")]
        public void AlbumArtistDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().AlbumArtist);

        [Fact(DisplayName = "AudioMetadata's AlbumArtist is properly serialized")]
        public void AlbumArtistIsSerialized() =>
            Assert.Equal("Test Album Artist", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { AlbumArtist = "Test Album Artist" }))?.AlbumArtist);

        [Fact(DisplayName = "AudioMetadata throws an exception if Composer is null")]
        public void ComposerNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Composer = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Composer")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidComposer()
        {
            new AudioMetadata { Composer = "Test Composer" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Composer")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyComposer()
        {
            new AudioMetadata { Composer = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Composer defaults to an empty string")]
        public void ComposerDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Composer);

        [Fact(DisplayName = "AudioMetadata's Composer is properly serialized")]
        public void ComposerIsSerialized() =>
            Assert.Equal("Test Composer", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Composer = "Test Composer" }))?.Composer);

        [Fact(DisplayName = "AudioMetadata throws an exception if Genre is null")]
        public void GenreNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Genre = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Genre")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidGenre()
        {
            new AudioMetadata { Genre = "Test Genre" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Genre")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyGenre()
        {
            new AudioMetadata { Genre = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Genre defaults to an empty string")]
        public void GenreDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Genre);

        [Fact(DisplayName = "AudioMetadata's Genre is properly serialized")]
        public void GenreIsSerialized() =>
            Assert.Equal("Test Genre", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Genre = "Test Genre" }))?.Genre);

        [Fact(DisplayName = "AudioMetadata throws an exception if Comment is null")]
        public void CommentNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Comment = null! });

        [Fact(DisplayName = "AudioMetadata accepts a valid Comment")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidComment()
        {
            new AudioMetadata { Comment = "Test Comment" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Comment")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyComment()
        {
            new AudioMetadata { Comment = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Comment defaults to an empty string")]
        public void CommentDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Comment);

        [Fact(DisplayName = "AudioMetadata's Comment is properly serialized")]
        public void CommentIsSerialized() =>
            Assert.Equal("Test Comment", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Comment = "Test Comment" }))?.Comment);

        [Fact(DisplayName = "AudioMetadata throws an exception if Day is null")]
        public void DayNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Day = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if Day is zero")]
        public void DayZeroThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Day = "0" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Day is greater than 31")]
        public void DayTooHighThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Day = "32" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Day isn't numeric")]
        public void DayNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Day = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid Day")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidDay()
        {
            new AudioMetadata { Day = "31" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Day")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyDay()
        {
            new AudioMetadata { Day = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Day defaults to an empty string")]
        public void DayDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Day);

        [Fact(DisplayName = "AudioMetadata normalizes Day to 2 digits")]
        public void NormalizesDay() =>
            Assert.Equal("01", new AudioMetadata { Day = "1" }.Day);

        [Fact(DisplayName = "AudioMetadata's Day is properly serialized")]
        public void DayIsSerialized() =>
            Assert.Equal("31", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Day = "31" }))?.Day);

        [Fact(DisplayName = "AudioMetadata throws an exception if Month is null")]
        public void MonthNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Month = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if Month is zero")]
        public void MonthZeroThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Month = "0" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Month is greater than 12")]
        public void MonthTooHighThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Month = "13" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Month isn't numeric")]
        public void MonthNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Month = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid Month")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidMonth()
        {
            new AudioMetadata { Month = "12" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Month")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyMonth()
        {
            new AudioMetadata { Month = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Month defaults to an empty string")]
        public void MonthDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Month);

        [Fact(DisplayName = "AudioMetadata normalizes Month to 2 digits")]
        public void NormalizesMonth() =>
            Assert.Equal("01", new AudioMetadata { Month = "1" }.Month);

        [Fact(DisplayName = "AudioMetadata's Month is properly serialized")]
        public void MonthIsSerialized() =>
            Assert.Equal("01", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Month = "01" }))?.Month);

        [Fact(DisplayName = "AudioMetadata throws an exception if Year is null")]
        public void YearNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { Year = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if Year is less than 4 characters")]
        public void YearTooShortThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Year = "999" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Year is more than 4 characters")]
        public void YearTooLongThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Year = "10000" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Year isn't numeric")]
        public void YearNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Year = "Test" });

        [Fact(DisplayName = "AudioMetadata throws an exception if Year starts with a zero")]
        public void YearStartsWithZeroThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { Year = "0100" });

        [Fact(DisplayName = "AudioMetadata accepts a valid Year")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidYear()
        {
            new AudioMetadata { Year = "2000" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Year")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyYear()
        {
            new AudioMetadata { Year = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Year defaults to an empty string")]
        public void YearDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().Year);

        [Fact(DisplayName = "AudioMetadata's Year is properly serialized")]
        public void YearIsSerialized() =>
            Assert.Equal("2017", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { Year = "2017" }))?.Year);

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackNumber is null")]
        public void TrackNumberNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { TrackNumber = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackNumber is zero")]
        public void TrackNumberZeroThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackNumber = "0" });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackNumber is more than 2 characters")]
        public void TrackNumberTooLongThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackNumber = "100" });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackNumber isn't numeric")]
        public void TrackNumberNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackNumber = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackNumber")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidTrackNumber()
        {
            new AudioMetadata { TrackNumber = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackNumber to 2 digits")]
        public void NormalizesTrackNumber() =>
            Assert.Equal("01", new AudioMetadata { TrackNumber = "1" }.TrackNumber);

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackNumber")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyTrackNumber()
        {
            new AudioMetadata { TrackNumber = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackNumber defaults to an empty string")]
        public void TrackNumberDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().TrackNumber);

        [Fact(DisplayName = "AudioMetadata's TrackNumber is properly serialized")]
        public void TrackNumberIsSerialized() =>
            Assert.Equal("01", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { TrackNumber = "01" }))?.TrackNumber);

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackCount is null")]
        public void TrackCountNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { TrackCount = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackCount is zero")]
        public void TrackCountZeroThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackCount = "0" });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackCount is more than 2 characters")]
        public void TrackCountTooLongThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackCount = "100" });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackCount isn't numeric")]
        public void TrackCountNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackCount = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackCount")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidTrackCount()
        {
            new AudioMetadata { TrackCount = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackCount to 2 digits")]
        public void NormalizesTrackCount() =>
            Assert.Equal("01", new AudioMetadata { TrackCount = "1" }.TrackCount);

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackCount")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyTrackCount()
        {
            new AudioMetadata { TrackCount = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackCount defaults to an empty string")]
        public void TrackCountDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().TrackCount);

        [Fact(DisplayName = "AudioMetadata's TrackCount is properly serialized")]
        public void TrackCountIsSerialized() =>
            Assert.Equal("12", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { TrackCount = "12" }))?.TrackCount);

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackPeak is null")]
        public void TrackPeakNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { TrackPeak = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackPeak is negative")]
        public void TrackPeakNegativeThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackPeak = "-0.1" });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackPeak isn't numeric")]
        public void TrackPeakNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackPeak = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackPeak")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidTrackPeak()
        {
            new AudioMetadata { TrackPeak = "0.5" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackPeak to 6 decimal places")]
        public void NormalizesTrackPeak() =>
            Assert.Equal("0.500000", new AudioMetadata { TrackPeak = "0.5" }.TrackPeak);

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackPeak")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyTrackPeak()
        {
            new AudioMetadata { TrackPeak = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackPeak defaults to an empty string")]
        public void TrackPeakDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().TrackPeak);

        [Fact(DisplayName = "AudioMetadata's TrackPeak is properly serialized")]
        public void TrackPeakIsSerialized() =>
            Assert.Equal("0.500000", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { TrackPeak = "0.500000" }))?.TrackPeak);

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumPeak is null")]
        public void AlbumPeakNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { AlbumPeak = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumPeak is negative")]
        public void AlbumPeakNegativeThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { AlbumPeak = "-0.1" });

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumPeak isn't numeric")]
        public void AlbumPeakNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { AlbumPeak = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid AlbumPeak")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidAlbumPeak()
        {
            new AudioMetadata { AlbumPeak = "0.6" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes AlbumPeak to 6 decimal places")]
        public void NormalizesAlbumPeak() =>
            Assert.Equal("0.600000", new AudioMetadata { AlbumPeak = "0.6" }.AlbumPeak);

        [Fact(DisplayName = "AudioMetadata accepts an empty AlbumPeak")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyAlbumPeak()
        {
            new AudioMetadata { AlbumPeak = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's AlbumPeak defaults to an empty string")]
        public void AlbumPeakDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().AlbumPeak);

        [Fact(DisplayName = "AudioMetadata's AlbumPeak is properly serialized")]
        public void AlbumPeakIsSerialized() =>
            Assert.Equal("0.600000", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { AlbumPeak = "0.600000" }))?.AlbumPeak);

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackGain is null")]
        public void TrackGainNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { TrackGain = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if TrackGain isn't numeric")]
        public void TrackGainNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { TrackGain = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackGain")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidTrackGain()
        {
            new AudioMetadata { TrackGain = "0.7" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackGain to 2 decimal places")]
        public void NormalizesTrackGain() =>
            Assert.Equal("0.70", new AudioMetadata { TrackGain = "0.7" }.TrackGain);

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackGain")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyTrackGain()
        {
            new AudioMetadata { TrackGain = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackGain defaults to an empty string")]
        public void TrackGainDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().TrackGain);

        [Fact(DisplayName = "AudioMetadata's TrackGain is properly serialized")]
        public void TrackGainIsSerialized() =>
            Assert.Equal("0.70", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { TrackGain = "0.70" }))?.TrackGain);

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumGain is null")]
        public void AlbumGainNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioMetadata { AlbumGain = null! });

        [Fact(DisplayName = "AudioMetadata throws an exception if AlbumGain isn't numeric")]
        public void AlbumGainNotNumericThrowsException() =>
            Assert.Throws<AudioMetadataInvalidException>(() => new AudioMetadata { AlbumGain = "##" });

        [Fact(DisplayName = "AudioMetadata accepts a valid AlbumGain")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsValidAlbumGain()
        {
            new AudioMetadata { AlbumGain = "0.8" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes AlbumGain to 2 decimal places")]
        public void NormalizesAlbumGain() =>
            Assert.Equal("0.80", new AudioMetadata { AlbumGain = "0.8" }.AlbumGain);

        [Fact(DisplayName = "AudioMetadata accepts an empty AlbumGain")]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Test confirms instantiation works without error")]
        public void AcceptsEmptyAlbumGain()
        {
            new AudioMetadata { AlbumGain = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's AlbumGain defaults to an empty string")]
        public void AlbumGainDefaultsToEmpty() =>
            Assert.Equal(string.Empty, new AudioMetadata().AlbumGain);

        [Fact(DisplayName = "AudioMetadata's AlbumGain is properly serialized")]
        public void AlbumGainIsSerialized() =>
            Assert.Equal("0.80", JsonSerializer.Deserialize<AudioMetadata>(JsonSerializer.Serialize(
                new AudioMetadata { AlbumGain = "0.80" }))?.AlbumGain);
    }
}
