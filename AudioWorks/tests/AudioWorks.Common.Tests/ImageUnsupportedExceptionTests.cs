/* Copyright � 2018 Jeremy Herbison

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
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class ImageUnsupportedExceptionTests
    {
        [Fact(DisplayName = "ImageUnsupportedException is an AudioException")]
        public void IsAudioException() =>
            Assert.IsType<AudioException>(new ImageUnsupportedException(), false);

        [Fact(DisplayName = "ImageUnsupportedException has the expected Message property value")]
        public void HasExpectedMessage()
        {
            const string message = "Testing 1-2-3";

            Assert.Equal(message, new ImageUnsupportedException(message).Message);
        }

        [Fact(DisplayName = "ImageUnsupportedException has the expected InnerException property value")]
        public void HasExpectedInnerException()
        {
            var innerException = new ArgumentException("Inner exception");

            Assert.Equal(innerException, new ImageUnsupportedException("Testing 1-2-3", innerException).InnerException);
        }
    }
}
