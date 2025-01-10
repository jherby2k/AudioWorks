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

using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using Xunit;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class RenameValidFileDataSource
    {
        public static TheoryData<string, TestAudioMetadata, string, string> Data { get; } = new()
        {
            // Basic rename
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "Testing 123",
                "Testing 123.wav"
            },

            // Metadata substitution
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title"
                },
                "{Title}",
                "Test Title.wav"
            },

            // Composite of multiple metadata fields
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist"
                },
                "{Title} by {Artist}",
                "Test Title by Test Artist.wav"
            },

            // Requested metadata not present
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "{Title} by {Artist}",
                "Unknown Title by Unknown Artist.wav"
            },

            // Metadata with invalid characters
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
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
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "LPCM 16-bit 44100Hz Stereo",
                "LPCM 16-bit 44100Hz Stereo.wav"
            }
        };

        public static TheoryData<string> FileNames =>
            new(Data.Select(item => item.Data.Item1).Distinct());
    }
}
