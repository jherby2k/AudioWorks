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
using System.IO;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Api;
#if LINUX
using AudioWorks.TestUtilities;
#endif
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using Moq;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class MeasureAudioFileTests : IClassFixture<ModuleFixture>
    {
        readonly ModuleFixture _moduleFixture;

        public MeasureAudioFileTests(ModuleFixture moduleFixture) => _moduleFixture = moduleFixture;

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
        public void AcceptsAnalyzerParameter(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile",
                        new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts the Analyzer parameter as the first argument")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAnalyzerParameterAsFirstArgument(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName)
                    .AddParameter("AudioFile",
                        new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts an AudioFile parameter")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAudioFileParameter(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile",
                        new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile requires the AudioFile parameter")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Analyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void RequiresAudioFileParameter(string analyzer)
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
        public void AcceptsAudioFileParameterAsSecondArgument(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddArgument(analyzerName)
                    .AddArgument(new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)));

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile accepts the AudioFile parameter from the pipeline")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AcceptsAudioFileParameterFromPipeline(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)))
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
        public void AcceptsPassThruSwitch(string fileName, string analyzerName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Measure-AudioFile")
                    .AddParameter("Analyzer", analyzerName)
                    .AddParameter("AudioFile",
                        new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)))
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Theory(DisplayName = "Measure-AudioFile with PassThru switch returns the AudioFile")]
        [MemberData(nameof(AnalyzeValidFileDataSource.FileNamesAndAnalyzers), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void PassThruSwitchReturnsAudioFile(string fileName, string analyzerName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));
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
            string fileName,
            string analyzerName,
            TestSettingDictionary settings,
#if LINUX
            TestAudioMetadata expectedUbuntu1604Metadata,
            TestAudioMetadata expectedUbuntu1804Metadata)
#else
            TestAudioMetadata expectedMetadata)
#endif
        {
            var audioFile = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));
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
            string[] fileNames,
            string analyzerName,
            TestSettingDictionary settings,
#if LINUX
            TestAudioMetadata[] expectedUbuntu1604Metadata,
            TestAudioMetadata[] expectedUbuntu1804Metadata)
#else
            TestAudioMetadata[] expectedMetadata)
#endif
        {
            var audioFiles = fileNames.Select(fileName =>
                    new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)))
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
                    string.Join(" ", differences)));
        }
    }
}
