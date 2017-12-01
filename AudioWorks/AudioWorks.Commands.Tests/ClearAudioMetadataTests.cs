using System;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Common;
using AutoMapper;
using JetBrains.Annotations;
using Moq;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class ClearAudioMetadataTests : IClassFixture<ModuleFixture>
    {
        static ClearAudioMetadataTests()
        {
            Mapper.Initialize(config => config.CreateMap<AudioMetadata, AudioMetadata>());
        }

        [NotNull] readonly ModuleFixture _moduleFixture;
        [NotNull] readonly AudioMetadata _testMetadata = new AudioMetadata
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
        };

        public ClearAudioMetadataTests([NotNull] ModuleFixture moduleFixture)
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
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with no switches clears all metadata fields")]
        public void NoSwitchesClearsAll()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object);

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Equal(typeof(AudioMetadata).GetProperties().Length, differences.Length);
            foreach (var difference in differences)
                Assert.True(string.IsNullOrEmpty(difference.Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Title");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Title", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Artist");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Artist", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Album");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Album", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a AlbumArtist switch")]
        public void AcceptsAlbumArtistSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("AlbumArtist");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with AlbumArtist switch clears only the Composer")]
        public void AlbumArtistSwitchClearsAlbumArtist()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("AlbumArtist");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("AlbumArtist", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Composer switch")]
        public void AcceptsComposerSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Composer");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Composer switch clears only the Composer")]
        public void ComposerSwitchClearsComposer()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Composer");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Composer", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Genre");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Genre", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Comment");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Comment", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Day");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Day", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Month");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Month", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Year");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("Year", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackNumber");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("TrackNumber", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
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
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("TrackCount");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.Single(differences);
            Assert.Equal("TrackCount", differences[0].MemberPath);
            Assert.True(string.IsNullOrEmpty(differences[0].Value2));
        }

        [Fact(DisplayName = "Clear-AudioMetadata accepts a Loudness switch")]
        public void AcceptsLoudnessSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Loudness");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Clear-AudioMetadata with Loudness switch clears only the TrackPeak, AlbumPeak, TrackGain and AlbumGain")]
        public void LoudnessSwitchClearsPeakAndGain()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(Mapper.Map<AudioMetadata>(_testMetadata));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Clear-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Loudness");

                ps.Invoke();
            }

            new Comparer().Compare(_testMetadata, mock.Object.Metadata, out var differenceEnumerable);
            var differences = differenceEnumerable.ToArray();
            Assert.True(differences.Length == 4);
            foreach (var difference in differences)
            {
                Assert.Contains(difference.MemberPath, new[] { "TrackPeak", "AlbumPeak", "TrackGain", "AlbumGain" });
                Assert.True(string.IsNullOrEmpty(difference.Value2));
            }
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
        public void OutputTypeIsITaggedAudioFile()
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
