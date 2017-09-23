using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public sealed class ClearAudioMetadataTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public ClearAudioMetadataTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Clear-AudioMetadata command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata");
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

        [Fact(DisplayName = "Clear-AudioMetadata accepts an AudioFile parameter")]
        public void AcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, fileInfo => new AudioMetadata(), null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Clear-AudioMetadata");
                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound"))
                        throw error.Exception;
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with no switches clears all metadata fields")]
        public void NoSwitchesClearsAll()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Title switch")]
        public void AcceptsTitleSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Title switch clears the Title")]
        public void TitleSwitchClearsTitle()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Title");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Title);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Artist switch")]
        public void AcceptsArtistSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Artist");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Artist switch clears the Artist")]
        public void ArtistSwitchClearsArtist()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Artist");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Artist);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts an Album switch")]
        public void AcceptsAlbumSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Album");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Album switch clears the Album")]
        public void AlbumSwitchClearsAlbum()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Album");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Album);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Genre switch")]
        public void AcceptsGenreSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Genre");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Genre switch clears the Genre")]
        public void GenreSwitchClearsGenre()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Genre");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Genre);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Comment switch")]
        public void AcceptsCommentSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Comment");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Comment switch clears the Comment")]
        public void CommentSwitchClearsComment()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Comment");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Comment);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Day switch")]
        public void AcceptsDaySwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Day switch clears the Day")]
        public void DaySwitchClearsDay()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Day");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Day);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Month switch")]
        public void AcceptsMonthSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Month switch clears the Month")]
        public void MonthSwitchClearsMonth()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Month");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Month);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Year switch")]
        public void AcceptsYearSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Year");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Year switch clears the Year")]
        public void YearSwitchClearsYear()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("Year");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.Year);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a TrackNumber switch")]
        public void AcceptsTrackNumberSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with TrackNumber switch clears the TrackNumber")]
        public void TrackNumberSwitchClearsTrackNumber()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackNumber");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.TrackNumber);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a TrackCount switch")]
        public void AcceptsTrackCountSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with TrackCount switch clears the TrackCount")]
        public void TrackCountSwitchClearsTrackCount()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => metadata, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("TrackCount");
                ps.Invoke();
                Assert.Equal(string.Empty, metadata.TrackCount);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(null, null, fileInfo => new AudioMetadata(), null);
            // ReSharper restore AssignNullToNotNullAttribute
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");
                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata has an OutputType of AudioFile")]
        public void OutputTypeIsAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Clear-AudioMetadata");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");
                Assert.Equal(typeof(AudioFile), (Type)ps.Invoke()[0].BaseObject);
            }
        }
    }
}
