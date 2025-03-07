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
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AudioWorks.TestUtilities.DataSources;
using Moq;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class ExportAudioCoverArtTests(ModuleFixture moduleFixture, ITestOutputHelper output)
        : IClassFixture<ModuleFixture>
    {
        [Fact(DisplayName = "Export-AudioCoverArt command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Export-AudioCoverArt");
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

        [Fact(DisplayName = "Export-AudioCoverArt requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Export-AudioCoverArt")
                    .AddParameter("Path", "Foo");

                // ReSharper disable once AccessToDisposedClosure
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Export-AudioCoverArt requires the Path parameter")]
        public void RequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Export-AudioCoverArt")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                // ReSharper disable once AccessToDisposedClosure
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Export-AudioCoverArt throws an exception if an encoded path references an invalid metadata field")]
        public void PathInvalidEncodingThrowsException()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Export-AudioCoverArt")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Path", "{Invalid}");

                Assert.IsType<ArgumentException>(
                    // ReSharper disable once AccessToDisposedClosure
                    Assert.Throws<CmdletInvocationException>(() => ps.Invoke())
                        .InnerException);
            }
        }

        [Fact(DisplayName = "Export-AudioCoverArt has an OutputType of FileInfo")]
        public void OutputTypeIsFileInfo()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Export-AudioCoverArt");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(FileInfo), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Export-AudioCoverArt creates the expected image file")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.IndexedFileNamesAndDataHash), MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CreatesExpectedImageFile(int index, string sourceFileName, string expectedHash)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Export-AudioCoverArt")
                    .AddParameter("AudioFile",
                        new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", sourceFileName)))
                    .AddParameter("Path", Path.Combine("Output", "Export-AudioCoverArt"))
                    .AddParameter("Name", $"{index:000} - {Path.GetFileNameWithoutExtension(sourceFileName)}")
                    .AddParameter("Force");

                var result = ps.Invoke();
                output.WriteStreams(ps);

                if (string.IsNullOrEmpty(expectedHash))
                    Assert.Empty(result);
                else
                {
                    Assert.Single(result);
                    Assert.Equal(expectedHash, HashUtility.CalculateHash(((FileInfo) result[0].BaseObject).FullName));
                }
            }
        }
    }
}
