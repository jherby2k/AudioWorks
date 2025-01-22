﻿/* Copyright © 2020 Jeremy Herbison

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
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AudioWorks.TestUtilities.DataSources;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioAnalyzerManagerTests
    {
        public AudioAnalyzerManagerTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Theory(DisplayName = "AudioAnalyzerManager's GetAnalyzerInfo method returns the audio analyzers")]
        [MemberData(nameof(AudioAnalyzerInfoDataSource.Data), MemberType = typeof(AudioAnalyzerInfoDataSource))]
        public void GetAnalyzerInfoReturnsAudioAnalyzers(string expectedName, string expectedDescription) =>
            Assert.Contains(AudioAnalyzerManager.GetAnalyzerInfo(), info =>
                expectedName.Equals(info.Name, StringComparison.OrdinalIgnoreCase) &&
                expectedDescription.Equals(info.Description, StringComparison.OrdinalIgnoreCase));
    }
}
