using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
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
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object);
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
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddArgument(mock.Object);
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(mock.Object)
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
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object);
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == string.Empty &&
                mock.Object.Metadata.Artist == string.Empty &&
                mock.Object.Metadata.Album == string.Empty &&
                mock.Object.Metadata.Genre == string.Empty &&
                mock.Object.Metadata.Comment == string.Empty &&
                mock.Object.Metadata.Day == string.Empty &&
                mock.Object.Metadata.Month == string.Empty &&
                mock.Object.Metadata.Year == string.Empty &&
                mock.Object.Metadata.TrackNumber == string.Empty &&
                mock.Object.Metadata.TrackCount == string.Empty);
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Title switch")]
        public void AcceptsTitleSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Title");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Title switch clears only the Title")]
        public void TitleSwitchClearsTitle()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Title");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == string.Empty &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Artist switch")]
        public void AcceptsArtistSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Artist");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Artist switch clears only the Artist")]
        public void ArtistSwitchClearsArtist()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Artist");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == string.Empty &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts an Album switch")]
        public void AcceptsAlbumSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Album");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Album switch clears only the Album")]
        public void AlbumSwitchClearsAlbum()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Album");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == string.Empty &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Genre switch")]
        public void AcceptsGenreSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Genre");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Genre switch clears only the Genre")]
        public void GenreSwitchClearsGenre()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Genre");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == string.Empty &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Comment switch")]
        public void AcceptsCommentSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Comment");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Comment switch clears only the Comment")]
        public void CommentSwitchClearsComment()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Comment");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == string.Empty &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Day switch")]
        public void AcceptsDaySwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Day switch clears only the Day")]
        public void DaySwitchClearsDay()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == string.Empty &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Month switch")]
        public void AcceptsMonthSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Month switch clears only the Month")]
        public void MonthSwitchClearsMonth()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == string.Empty &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Year switch")]
        public void AcceptsYearSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Year");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Year switch clears only the Year")]
        public void YearSwitchClearsYear()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Year");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == string.Empty &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a TrackNumber switch")]
        public void AcceptsTrackNumberSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with TrackNumber switch clears only the TrackNumber")]
        public void TrackNumberSwitchClearsTrackNumber()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == string.Empty &&
                mock.Object.Metadata.TrackCount == "12");
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a TrackCount switch")]
        public void AcceptsTrackCountSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with TrackCount switch clears only the TrackCount")]
        public void TrackCountSwitchClearsTrackCount()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata
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
            });

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount");
                ps.Invoke();
            }

            Assert.True(
                mock.Object.Metadata.Title == "Test Title" &&
                mock.Object.Metadata.Artist == "Test Artist" &&
                mock.Object.Metadata.Album == "Test Album" &&
                mock.Object.Metadata.Genre == "Test Genre" &&
                mock.Object.Metadata.Comment == "Test Comment" &&
                mock.Object.Metadata.Day == "31" &&
                mock.Object.Metadata.Month == "01" &&
                mock.Object.Metadata.Year == "2017" &&
                mock.Object.Metadata.TrackNumber == "01" &&
                mock.Object.Metadata.TrackCount == string.Empty);
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("PassThru");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("PassThru");
                Assert.Equal(mock.Object, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata has an OutputType of ITaggedAudioFile")]
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
                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }
    }
}
