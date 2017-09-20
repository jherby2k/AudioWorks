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
        public void SetAudioMetadataCommandExists()
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
        public void SetAudioMetadataAcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()));
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata requires the AudioFile parameter")]
        public void SetAudioMetadataRequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() =>
                    ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void SetAudioMetadataAcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddArgument(new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()));
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void SetAudioMetadataAcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
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
        public void SetAudioMetadataAcceptsTitleParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Title", "Test Title");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Title Parameter sets the Title")]
        public void SetAudioMetadataTitleParameterSetsTitle()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Title", "Test Title");
                ps.Invoke();
                Assert.Equal("Test Title", metadata.Title);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Title is null")]
        public void SetAudioMetadataTitleNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Title", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Artist parameter")]
        public void SetAudioMetadataAcceptsArtistParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Artist", "Test Artist");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Artist Parameter sets the Artist")]
        public void SetAudioMetadataArtistParameterSetsArtist()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Artist", "Test Artist");
                ps.Invoke();
                Assert.Equal("Test Artist", metadata.Artist);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Artist is null")]
        public void SetAudioMetadataArtistNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Artist", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts an Album parameter")]
        public void SetAudioMetadataAcceptsAlbumParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Album", "Test Album");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Album Parameter sets the Album")]
        public void SetAudioMetadataAlbumParameterSetsAlbum()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Album", "Test Album");
                ps.Invoke();
                Assert.Equal("Test Album", metadata.Album);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Album is null")]
        public void SetAudioMetadataAlbumNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Album", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Genre parameter")]
        public void SetAudioMetadataAcceptsGenreParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Genre", "Test Genre");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Genre Parameter sets the Genre")]
        public void SetAudioMetadataGenreParameterSetsGenre()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Genre", "Test Genre");
                ps.Invoke();
                Assert.Equal("Test Genre", metadata.Genre);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Genre is null")]
        public void SetAudioMetadataGenreNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Genre", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Comment parameter")]
        public void SetAudioMetadataAcceptsCommentParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Comment", "Test Comment");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Comment Parameter sets the Comment")]
        public void SetAudioMetadataCommentParameterSetsComment()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Comment", "Test Comment");
                ps.Invoke();
                Assert.Equal("Test Comment", metadata.Comment);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Comment is null")]
        public void SetAudioMetadataCommentNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Comment", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Day parameter")]
        public void SetAudioMetadataAcceptsDayParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Day", "31");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Day Parameter sets the Day")]
        public void SetAudioMetadataDayParameterSetsDay()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Day", "31");
                ps.Invoke();
                Assert.Equal("31", metadata.Day);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Day is null")]
        public void SetAudioMetadataDayNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Day", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Month parameter")]
        public void SetAudioMetadataAcceptsMonthParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Month Parameter sets the Month")]
        public void SetAudioMetadataMonthParameterSetsMonth()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.Equal("01", metadata.Month);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Month is null")]
        public void SetAudioMetadataMonthNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Month", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Year parameter")]
        public void SetAudioMetadataAcceptsYearParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Month", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Year Parameter sets the Year")]
        public void SetAudioMetadataYearParameterSetsYear()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Year", "2017");
                ps.Invoke();
                Assert.Equal("2017", metadata.Year);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if Year is null")]
        public void SetAudioMetadataYearNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Year", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackNumber parameter")]
        public void SetAudioMetadataAcceptsTrackNumberParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("TrackNumber", "1");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackNumber Parameter sets the TrackNumber")]
        public void SetAudioMetadataTrackNumberParameterSetsTrackNumber()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("TrackNumber", "1");
                ps.Invoke();
                Assert.Equal("01", metadata.TrackNumber);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackNumber is null")]
        public void SetAudioMetadataTrackNumberNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("TrackNumber", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a TrackCount parameter")]
        public void SetAudioMetadataAcceptsTrackCountParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("TrackCount", "12");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's TrackCount Parameter sets the TrackCount")]
        public void SetAudioMetadataTrackCountParameterSetsTrackCount()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("TrackCount", "12");
                ps.Invoke();
                Assert.Equal("12", metadata.TrackCount);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata returns an error if TrackCount is null")]
        public void SetAudioMetadataTrackCountNullReturnsError()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("TrackCount", null);
                // Actual exception type ParameterBindingValidationException is Internal
                Assert.ThrowsAny<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata with PassThru returns the same AudioFile")]
        public void SetAudioMetadataWithPassThruReturnsSameAudioFile()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(null, null, null);
            // ReSharper restore AssignNullToNotNullAttribute
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");
                var result = ps.Invoke();
                Assert.Equal(audioFile, result[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata has an OutputType of AudioFile")]
        public void SetAudioMetadataOutputTypeIsAudioFile()
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
                var result = ps.Invoke();
                Assert.Equal(typeof(AudioFile), (Type) result[0].BaseObject);
            }
        }
    }
}
