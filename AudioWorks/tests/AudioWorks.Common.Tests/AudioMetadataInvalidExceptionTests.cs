/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataInvalidExceptionTests
    {
        static AudioMetadataInvalidExceptionTests()
        {
            XUnitLoggerProvider.Instance.Enable(LoggingManager.LoggerFactory);
        }

        public AudioMetadataInvalidExceptionTests([NotNull] ITestOutputHelper outputHelper)
        {
            XUnitLoggerProvider.Instance.OutputHelper = outputHelper;
        }

        [Fact(DisplayName = "AudioMetadataInvalidException is an AudioException")]
        public void IsException()
        {
            Assert.IsAssignableFrom<AudioException>(new AudioInvalidException());
        }
    }
}
