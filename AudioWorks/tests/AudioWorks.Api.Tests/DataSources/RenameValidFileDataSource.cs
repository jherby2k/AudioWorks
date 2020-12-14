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

using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class RenameValidFileDataSource
    {
        static readonly List<object[]> _data = new()
        {
            // Basic rename
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "Testing 123",
                "Testing 123.wav"
            },

            // Metadata substitution
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title"
                },
                "{Title}",
                "Test Title.wav"
            },

            // Composite of multiple metadata fields
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist"
                },
                "{Title} by {Artist}",
                "Test Title by Test Artist.wav"
            },

            // Requested metadata not present
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "{Title} by {Artist}",
                "Unknown Title by Unknown Artist.wav"
            },

            // Metadata with invalid characters
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata
                {
                    Title = "Test Title <> with |invalid \"characters\""
                },
                "{Title}",
#if WINDOWS
                "Test Title with invalid characters.wav"
#else
                // These characters are all valid on Linux and OSX
                "Test Title <> with |invalid \"characters\".wav"
#endif
            },

            // New name matches old
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new TestAudioMetadata(),
                "LPCM 16-bit 44100Hz Stereo",
                "LPCM 16-bit 44100Hz Stereo.wav"
            }
        };

        public static IEnumerable<object[]> Data => _data;

        public static IEnumerable<object[]> FileNames =>
            _data.Select(item => new[] { item[0] }).Distinct(new ArrayComparer());
    }
}
