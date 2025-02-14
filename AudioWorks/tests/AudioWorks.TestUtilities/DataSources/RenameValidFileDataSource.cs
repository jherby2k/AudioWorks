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
using System.Runtime.InteropServices;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class RenameValidFileDataSource
    {
        public static IEnumerable<TheoryDataRow<string, AudioMetadata, string, string>> Data { get; } =
        [
            // Basic rename
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "Testing 123",
                "Testing 123.wav"
            ),

            // Metadata substitution
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title"
                },
                "{Title}",
                "Test Title.wav"
            ),

            // Composite of multiple metadata fields
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title",
                    Artist = "Test Artist"
                },
                "{Title} by {Artist}",
                "Test Title by Test Artist.wav"
            ),

            // Requested metadata not present
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "{Title} by {Artist}",
                "Unknown Title by Unknown Artist.wav"
            ),

            // Metadata with invalid characters on Windows
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title <> with |invalid \"characters\""
                },
                "{Title}",
                "Test Title with invalid characters.wav"
            ) { Skip = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows only" : null },

            // Metadata with invalid characters on Windows
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new()
                {
                    Title = "Test Title <> with |invalid \"characters\""
                },
                "{Title}",
                // These characters are all valid on Linux and MacOS
                "Test Title <> with |invalid \"characters\".wav"
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Linux and MacOS only" : null },

            // New name matches old
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                new(),
                "LPCM 16-bit 44100Hz Stereo",
                "LPCM 16-bit 44100Hz Stereo.wav"
            )
        ];

        public static IEnumerable<TheoryDataRow<string>> FileNames =>
            Data.Select(item => item.Data.Item1).Distinct().Select(item => new TheoryDataRow<string>(item));
    }
}
