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
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AudioWorks.TestUtilities.DataSources;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class GetAudioCoverArtTests(ModuleFixture moduleFixture) : IClassFixture<ModuleFixture>
    {
        [Fact(DisplayName = "Get-AudioCoverArt command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt");
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

        [Fact(DisplayName = "Get-AudioCoverArt accepts a Path parameter")]
        public void AcceptsPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddParameter("Path", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt requires the Path parameter")]
        public void RequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt accepts the Path parameter as the first argument")]
        public void AcceptsPathParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument("Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt accepts the Path parameter from the pipeline")]
        public void AcceptsPathParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("path")
                    .AddArgument("Foo")
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Get-AudioCoverArt");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt has an OutputType of CoverArt")]
        public void OutputTypeIsCoverArt()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioCoverArt");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                var result = ps.Invoke();

                Assert.Equal(typeof(ICoverArt), (Type) result[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt returns an error if the Path can't be found")]
        public void PathNotFoundReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument("Foo");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<ItemNotFoundException>(errors[0].Exception);
                Assert.Equal($"{nameof(ItemNotFoundException)},AudioWorks.Commands.GetAudioCoverArtCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.ObjectNotFound, errors[0].CategoryInfo.Category);
            }
        }

        [Theory(DisplayName = "Get-AudioCoverArt returns a CoverArt")]
        [MemberData(nameof(ValidImageFileDataSource.FileNames), MemberType = typeof(ValidImageFileDataSource))]
        public void ReturnsCoverArt(string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));

                Assert.IsAssignableFrom<CoverArt>(ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Get-AudioCoverArt returns an error if the Path is an unsupported file")]
        [MemberData(nameof(UnsupportedImageFileDataSource.Data), MemberType = typeof(UnsupportedImageFileDataSource))]
        public void PathUnsupportedReturnsError(string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument(Path.Combine(PathUtility.GetTestFileRoot(), "Unsupported", fileName));

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<ImageUnsupportedException>(errors[0].Exception);
                Assert.Equal($"{nameof(ImageUnsupportedException)},AudioWorks.Commands.GetAudioCoverArtCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Theory(DisplayName = "Get-AudioCoverArt returns an error if the Path is an invalid file")]
        [MemberData(nameof(InvalidImageFileDataSource.Data), MemberType = typeof(InvalidImageFileDataSource))]
        public void PathInvalidReturnsError(string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument(Path.Combine(PathUtility.GetTestFileRoot(), "Invalid", fileName));

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<ImageInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(ImageInvalidException)},AudioWorks.Commands.GetAudioCoverArtCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }
    }
}
