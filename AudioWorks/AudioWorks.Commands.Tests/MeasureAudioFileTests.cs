using AudioWorks.Api;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
using ObjectsComparer;
using System;
using System.IO;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class MeasureAudioFileTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public MeasureAudioFileTests([NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Measure-AudioFile command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile");
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

        [Fact(DisplayName = "Measure-AudioFile accepts an AudioFile parameter")]
        public void AcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", new Mock<IAnalyzableAudioFile>().Object)
                    .AddParameter("Analyzer", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", "Foo");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Measure-AudioFile accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(new Mock<IAnalyzableAudioFile>().Object)
                    .AddParameter("Analyzer", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new Mock<IAnalyzableAudioFile>().Object)
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", "Foo");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile accepts an Analyzer parameter")]
        public void AcceptsAnalyzerParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", new Mock<IAnalyzableAudioFile>().Object)
                    .AddParameter("Analyzer", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile requires the Analyzer parameter")]
        public void RequiresAnalyzerParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", new Mock<IAnalyzableAudioFile>().Object);

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Measure-AudioFile accepts the Analyzer parameter as the second argument")]
        public void AcceptsAudioFileParameterAsSecondArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(new Mock<IAnalyzableAudioFile>().Object)
                    .AddArgument("Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }


        [Fact(DisplayName = "Measure-AudioFile accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", new Mock<IAnalyzableAudioFile>().Object)
                    .AddParameter("Analyzer", "Foo")
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            var audioFile = new Mock<IAnalyzableAudioFile>().Object;
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("Analyzer", "Foo")
                    .AddParameter("PassThru");

                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Measure-AudioFile has an OutputType of IAnalyzableAudioFile")]
        public void OutputTypeIsAnalyzableAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Measure-AudioFile");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(IAnalyzableAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void CreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzer,
            [CanBeNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata)
        {
            var audioFile = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(audioFile)
                    .AddArgument(analyzer);
                if (settings != null)
                    foreach (var item in settings)
                        ps.AddParameter(item.Key, item.Value);

                ps.Invoke();
            }

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences), string.Join(' ', differences));
        }
    }
}
