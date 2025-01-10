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
using System.Management.Automation;
using AudioWorks.Common;
using Moq;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class SetAudioMetadataTests(ModuleFixture moduleFixture) : IClassFixture<ModuleFixture>
    {
        [Fact(DisplayName = "Set-AudioMetadata command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
                try
                {
                    ps.Invoke();
                }
                catch (Exception e) when (e is not CommandNotFoundException)
                {
                    // CommandNotFoundException is the only type we are testing for
                }

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an AudioFile parameter")]
        public void AcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddArgument(new Mock<ITaggedAudioFile>().Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Set-AudioMetadata");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Title parameter")]
        public void AcceptsTitleParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Title", "Test Title");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Title Parameter sets the Title")]
        public void TitleParameterSetsTitle()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Title", "Test Title");

                ps.Invoke();

                Assert.Equal("Test Title", mock.Object.Metadata.Title);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Title is null")]
        public void TitleNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Title", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Artist parameter")]
        public void AcceptsArtistParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Artist", "Test Artist");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Artist Parameter sets the Artist")]
        public void ArtistParameterSetsArtist()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Artist", "Test Artist");

                ps.Invoke();

                Assert.Equal("Test Artist", mock.Object.Metadata.Artist);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Artist is null")]
        public void ArtistNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Artist", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Album parameter")]
        public void AcceptsAlbumParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Album", "Test Album");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Album Parameter sets the Album")]
        public void AlbumParameterSetsAlbum()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Album", "Test Album");

                ps.Invoke();

                Assert.Equal("Test Album", mock.Object.Metadata.Album);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Album is null")]
        public void AlbumNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Album", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an AlbumArtist parameter")]
        public void AcceptsAlbumArtistParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("AlbumArtist", "Test Album Artist");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's AlbumArtist Parameter sets the AlbumArtist")]
        public void AlbumArtistParameterSetsAlbumArtist()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("AlbumArtist", "Test Album Artist");

                ps.Invoke();

                Assert.Equal("Test Album Artist", mock.Object.Metadata.AlbumArtist);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if AlbumArtist is null")]
        public void AlbumArtistNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("AlbumArtist", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Composer parameter")]
        public void AcceptsComposerParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Composer", "Test Composer");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Composer Parameter sets the Composer")]
        public void ComposerParameterSetsComposer()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Composer", "Test Composer");

                ps.Invoke();

                Assert.Equal("Test Composer", mock.Object.Metadata.Composer);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Composer is null")]
        public void ComposerNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Composer", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Genre parameter")]
        public void AcceptsGenreParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Genre", "Test Genre");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Genre Parameter sets the Genre")]
        public void GenreParameterSetsGenre()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Genre", "Test Genre");

                ps.Invoke();

                Assert.Equal("Test Genre", mock.Object.Metadata.Genre);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Genre is null")]
        public void GenreNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Genre", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Comment parameter")]
        public void AcceptsCommentParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Comment", "Test Comment");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Comment Parameter sets the Comment")]
        public void CommentParameterSetsComment()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Comment", "Test Comment");

                ps.Invoke();

                Assert.Equal("Test Comment", mock.Object.Metadata.Comment);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Comment is null")]
        public void CommentNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Comment", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Day parameter")]
        public void AcceptsDayParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day", "31");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Day Parameter sets the Day")]
        public void DayParameterSetsDay()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day", "31");

                ps.Invoke();

                Assert.Equal("31", mock.Object.Metadata.Day);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Day is null")]
        public void DayNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Day", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Day is invalid")]
        public void DayInvalidReturnsError()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day", "0");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioMetadataInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Month parameter")]
        public void AcceptsMonthParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month", "1");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Month Parameter sets the Month")]
        public void MonthParameterSetsMonth()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month", "1");

                ps.Invoke();

                Assert.Equal("01", mock.Object.Metadata.Month);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Month is null")]
        public void MonthNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Month", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Month is invalid")]
        public void MonthInvalidReturnsError()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month", "0");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioMetadataInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Year parameter")]
        public void AcceptsYearParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month", "1");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Year Parameter sets the Year")]
        public void YearParameterSetsYear()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Year", "2017");

                ps.Invoke();

                Assert.Equal("2017", mock.Object.Metadata.Year);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Year is null")]
        public void YearNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Year", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Year is invalid")]
        public void YearInvalidReturnsError()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Year", "0");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioMetadataInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackNumber parameter")]
        public void AcceptsTrackNumberParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber", "1");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackNumber Parameter sets the TrackNumber")]
        public void TrackNumberParameterSetsTrackNumber()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber", "1");

                ps.Invoke();

                Assert.Equal("01", mock.Object.Metadata.TrackNumber);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackNumber is null")]
        public void TrackNumberNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("TrackNumber", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackNumber is invalid")]
        public void TrackNumberInvalidReturnsError()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber", "0");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioMetadataInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackCount parameter")]
        public void AcceptsTrackCountParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount", "12");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackCount Parameter sets the TrackCount")]
        public void TrackCountParameterSetsTrackCount()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount", "12");

                ps.Invoke();

                Assert.Equal("12", mock.Object.Metadata.TrackCount);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a CoverArt parameter")]
        public void AcceptsCoverArtParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("CoverArt", new Mock<ICoverArt>().Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's CoverArt Parameter sets the CoverArt")]
        public void CoverArtParameterSetsCoverArt()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            var coverArt = new Mock<ICoverArt>().Object;
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("CoverArt", coverArt);

                ps.Invoke();

                Assert.Equal(coverArt, mock.Object.Metadata.CoverArt);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackCount is null")]
        public void TrackCountNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("TrackCount", null);

                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackCount is invalid")]
        public void TrackCountInvalidReturnsError()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount", "0");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioMetadataInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            var audioFile = new Mock<ITaggedAudioFile>().Object;
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");

                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Set-AudioMetadata");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }
    }
}
