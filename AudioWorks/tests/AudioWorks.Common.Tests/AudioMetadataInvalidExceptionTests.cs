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
using AudioWorks.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataInvalidExceptionTests
    {
        public AudioMetadataInvalidExceptionTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioMetadataInvalidException is an AudioException")]
        public void IsException() =>
            Assert.IsAssignableFrom<AudioException>(new AudioMetadataInvalidException());

        [Fact(DisplayName = "AudioMetadataInvalidException has the expected Message property value")]
        public void HasExpectedMessage()
        {
            const string message = "Testing 1-2-3";

            Assert.Equal(message, new AudioMetadataInvalidException(message).Message);
        }

        [Fact(DisplayName = "AudioMetadataInvalidException has the expected InnerException property value")]
        public void HasExpectedInnerException()
        {
            var innerException = new ArgumentException("Inner exception");

            Assert.Equal(innerException, new AudioMetadataInvalidException("Testing 1-2-3", innerException).InnerException);
        }
    }
}
