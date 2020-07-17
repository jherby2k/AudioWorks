/* Copyright © 2020 Jeremy Herbison

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
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileManagerTests
    {
        public AudioFileManagerTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Theory(DisplayName = "AudioFileManager's GetFormatInfo method returns the audio file formats")]
        [MemberData(nameof(AudioFileFormatInfoDataSource.Data), MemberType = typeof(AudioFileFormatInfoDataSource))]
        public void GetFormatInfoReturnsAudioFileFormats(string expectedFormat, string expectedExtension) =>
            Assert.Contains(AudioFileManager.GetFormatInfo(), info =>
                expectedFormat.Equals(info.Format, StringComparison.OrdinalIgnoreCase) &&
                expectedExtension.Equals(info.Extension, StringComparison.OrdinalIgnoreCase));
    }
}
