using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
using ObjectsComparer;
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

        [Fact(DisplayName = "Measure-AudioFile requires the Analyzer parameter")]
        public void RequiresAnalyzerParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Measure-AudioFile has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
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

                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts an Analyzer parameter")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAnalyzerParameter(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile", new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts the Analyzer parameter as the first argument")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAnalyzerParameterAsFirstArgument(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName)
                    .AddParameter("AudioFile", new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts an AudioFile parameter")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAudioFileParameter(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile", new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile requires the AudioFile parameter")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Analyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void RequiresAudioFileParameter([NotNull] string analyzer)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzer);

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts the AudioFile parameter as the second argument")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAudioFileParameterAsSecondArgument(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName)
                    .AddArgument(new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts the AudioFile parameter from the pipeline")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAudioFileParameterFromPipeline(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)))
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName);

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts a PassThru switch")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsPassThruSwitch(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile", new TaggedAudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)))
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile with PassThru switch returns the AudioFile")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void PassThruSwitchReturnsAudioFile(
            [NotNull] string fileName,
            [NotNull] string analyzerName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");

                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void CreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] TestAudioMetadata expectedUbuntu1604Metadata,
            [NotNull] TestAudioMetadata expectedUbuntu1804Metadata)
#else
            [NotNull] TestAudioMetadata expectedMetadata)
#endif
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName)
                    .AddArgument(audioFile);
                if (settings != null)
                    foreach (var item in settings)
                        if (item.Value is bool boolValue)
                        {
                            if (boolValue)
                                ps.AddParameter(item.Key);
                        }
                        else
                            ps.AddParameter(item.Key, item.Value);

                ps.Invoke();
            }

            Assert.True(
#if LINUX
                new Comparer().Compare(LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                        ? expectedUbuntu1604Metadata
                        : expectedUbuntu1804Metadata,
                    audioFile.Metadata, out var differences),
#else
                new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences),
#endif
                string.Join(" ", differences));
        }

        [Theory(DisplayName = "Measure-AudioFile creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public void CreatesExpectedMetadataForGroup(
            [NotNull] string[] fileNames,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] TestAudioMetadata[] expectedUbuntu1604Metadata,
            [NotNull] TestAudioMetadata[] expectedUbuntu1804Metadata)
#else
            [NotNull] TestAudioMetadata[] expectedMetadata)
#endif
        {
            var audioFiles = fileNames.Select(fileName => new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)))
                .ToArray<ITaggedAudioFile>();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFiles")
                    .AddArgument(audioFiles)
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName);
                if (settings != null)
                    foreach (var item in settings)
                        if (item.Value is bool boolValue)
                        {
                            if (boolValue)
                                ps.AddParameter(item.Key);
                        }
                        else
                            ps.AddParameter(item.Key, item.Value);

                ps.Invoke();
            }

            var i = 0;
            var comparer = new Comparer();
            Assert.All(audioFiles, audioFile =>
#if LINUX
                Assert.True(comparer.Compare(
                        LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                            ? expectedUbuntu1604Metadata[i++]
                            : expectedUbuntu1804Metadata[i++],
                        audioFile.Metadata, out var differences),
#else
                Assert.True(comparer.Compare(expectedMetadata[i++], audioFile.Metadata, out var differences),
#endif
                    string.Join(' ', differences)));
        }
    }
}
