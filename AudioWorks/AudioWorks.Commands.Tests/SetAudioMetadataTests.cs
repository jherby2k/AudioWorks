using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public sealed class SetAudioMetadataTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public SetAudioMetadataTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Set-AudioMetadata command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
                try
                {
                    ps.Invoke();
                }
                catch (Exception e) when (!(e is CommandNotFoundException))
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
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, null, null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Set-AudioMetadata");
                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound"))
                        throw error.Exception;
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Title parameter")]
        public void AcceptsTitleParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title", "Test Title");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Title Parameter sets the Title")]
        public void TitleParameterSetsTitle()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title", "Test Title");
                ps.Invoke();
                Assert.Equal("Test Title", metadata.Title);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Title is null")]
        public void TitleNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Artist parameter")]
        public void AcceptsArtistParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Artist", "Test Artist");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Artist Parameter sets the Artist")]
        public void ArtistParameterSetsArtist()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Artist", "Test Artist");
                ps.Invoke();
                Assert.Equal("Test Artist", metadata.Artist);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Artist is null")]
        public void ArtistNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Artist", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Album parameter")]
        public void AcceptsAlbumParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Album", "Test Album");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Album Parameter sets the Album")]
        public void AlbumParameterSetsAlbum()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Album", "Test Album");
                ps.Invoke();
                Assert.Equal("Test Album", metadata.Album);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Album is null")]
        public void AlbumNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Album", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Genre parameter")]
        public void AcceptsGenreParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Genre", "Test Genre");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Genre Parameter sets the Genre")]
        public void GenreParameterSetsGenre()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Genre", "Test Genre");
                ps.Invoke();
                Assert.Equal("Test Genre", metadata.Genre);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Genre is null")]
        public void GenreNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Genre", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Comment parameter")]
        public void AcceptsCommentParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Comment", "Test Comment");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Comment Parameter sets the Comment")]
        public void CommentParameterSetsComment()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Comment", "Test Comment");
                ps.Invoke();
                Assert.Equal("Test Comment", metadata.Comment);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Comment is null")]
        public void CommentNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Comment", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Day parameter")]
        public void AcceptsDayParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day", "31");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Day Parameter sets the Day")]
        public void DayParameterSetsDay()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day", "31");
                ps.Invoke();
                Assert.Equal("31", metadata.Day);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Day is null")]
        public void DayNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Day is invalid")]
        public void DayInvalidReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day", "0");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioMetadataInvalidException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Month parameter")]
        public void AcceptsMonthParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Month Parameter sets the Month")]
        public void MonthParameterSetsMonth()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.Equal("01", metadata.Month);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Month is null")]
        public void MonthNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Month is invalid")]
        public void MonthInvalidReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month", "0");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioMetadataInvalidException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Year parameter")]
        public void AcceptsYearParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Year Parameter sets the Year")]
        public void YearParameterSetsYear()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Year", "2017");
                ps.Invoke();
                Assert.Equal("2017", metadata.Year);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Year is null")]
        public void YearNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Year", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Year is invalid")]
        public void YearInvalidReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Year", "0");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioMetadataInvalidException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackNumber parameter")]
        public void AcceptsTrackNumberParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackNumber Parameter sets the TrackNumber")]
        public void TrackNumberParameterSetsTrackNumber()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber", "1");
                ps.Invoke();
                Assert.Equal("01", metadata.TrackNumber);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackNumber is null")]
        public void TrackNumberNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackNumber is invalid")]
        public void TrackNumberInvalidReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber", "0");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioMetadataInvalidException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackCount parameter")]
        public void AcceptsTrackCountParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount", "12");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackCount Parameter sets the TrackCount")]
        public void TrackCountParameterSetsTrackCount()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount", "12");
                ps.Invoke();
                Assert.Equal("12", metadata.TrackCount);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackCount is null")]
        public void TrackCountNullReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackCount is invalid")]
        public void TrackCountInvalidReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount", "0");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioMetadataInvalidException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioMetadataInvalidException)},AudioWorks.Commands.SetAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(null, null, null, null);
            // ReSharper restore AssignNullToNotNullAttribute
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");
                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata has an OutputType of AudioFile")]
        public void OutputTypeIsAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Set-AudioMetadata");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");
                Assert.Equal(typeof(AudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }
    }
}
