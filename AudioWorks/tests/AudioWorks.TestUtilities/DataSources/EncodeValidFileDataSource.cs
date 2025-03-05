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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class EncodeValidFileDataSource
    {
        static readonly IEnumerable<TheoryDataRow<string, string, SettingDictionary, string[]>> _data =
        [
            #region Wave Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Wave",
                [],
                [
                    "7E-65-3B-E9-72-C4-45-EB"
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "F5-C6-67-6D-47-AE-A7-D4"
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "AF-AE-6C-F1-37-3F-6A-96"
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]
            ),
            new(
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            ),
            new(
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            ),
            new(
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]   
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },

            #endregion

            #region FLAC Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "67-3A-41-2C-F5-3B-C6-24", // FLAC 1.3.3 (Ubuntu 22.04)
                    "EA-F9-18-34-14-93-E5-34", // FLAC 1.4.3 (Ubuntu 24.04)
                    "77-85-5F-1C-76-D9-FF-95" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                [],
                [
                    "04-F0-CB-82-FF-F6-27-95", // FLAC 1.3.3 (Ubuntu 22.04)
                    "2F-E2-E5-7D-46-45-1E-FE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "BE-A7-E3-09-E9-B9-DD-45" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "DB-7B-58-FC-11-F1-A4-8B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "64-EF-2F-36-1C-D6-C5-A4", // FLAC 1.4.3 (Ubuntu 24.04)
                    "4E-A1-65-5B-D2-F8-57-F6" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "FF-B4-0D-B9-F9-9A-7B-79", // FLAC 1.3.3 (Ubuntu 22.04)
                    "DB-91-51-55-39-C4-DE-F2", // FLAC 1.4.3 (Ubuntu 24.04)
                    "EF-36-3E-CE-6B-73-D7-A4" // FLAC 1.5.0
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "6A-19-19-4A-E3-71-E3-95", // FLAC 1.3.3 (Ubuntu 22.04)
                    "30-56-D7-7A-03-CA-9E-06", // FLAC 1.4.3 (Ubuntu 24.04)
                    "A2-42-13-03-B7-3E-7B-D5" // FLAC 1.5.0
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "73-AE-57-E4-9C-B6-54-B5", // FLAC 1.3.3 (Ubuntu 22.04)
                    "41-DE-C9-E0-B9-DB-E8-8C", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8F-D9-39-8A-97-1D-98-49" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                [],
                [
                    "92-78-07-20-32-FB-A4-A1", // FLAC 1.3.3 (Ubuntu 22.04)
                    "67-73-BF-B8-6F-36-2E-7E", // FLAC 1.4.3 (Ubuntu 24.04)
                    "0B-39-56-AB-84-D6-9E-95" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                [],
                [
                    "18-8A-91-25-28-9C-20-CC", // FLAC 1.3.3 (Ubuntu 22.04)
                    "8F-C3-6F-2A-AF-46-EC-61", // FLAC 1.4.3  (Ubuntu 24.04)
                    "3C-6F-52-00-B7-B9-BF-B6" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                [],
                [
                    "74-8F-22-E4-6B-97-8F-D7", // FLAC 1.3.3 (Ubuntu 22.04)
                    "0E-6D-7B-32-64-8F-AC-18", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8C-1F-BF-4E-9B-32-A5-F0" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default compression
                    ["CompressionLevel"] = 5
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Minimum compression
                    ["CompressionLevel"] = 0
                },
                [
                    "99-98-53-36-8F-15-6C-4C", // FLAC 1.3.3 (Ubuntu 22.04)
                    "EC-17-4A-C8-C4-8F-D3-AE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "BC-D8-DE-03-33-9D-45-7D" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum compression
                    ["CompressionLevel"] = 8
                },
                [
                    "95-D2-FF-25-7C-74-1B-86", // FLAC 1.3.3 (Ubuntu 22.04)
                    "09-E9-A5-AF-D7-08-4A-68", // FLAC 1.4.3 (Ubuntu 24.04)
                    "DF-8E-EF-4F-3C-F9-78-93" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default seek point interval
                    ["SeekPointInterval"] = 10
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Disabled seek points
                    ["SeekPointInterval"] = 0
                },
                [
                    "60-2C-A4-D8-C2-61-4B-A5", // FLAC 1.3.3 (Ubuntu 22.04)
                    "82-4F-D8-9B-2F-A2-43-00", // FLAC 1.4.3 (Ubuntu 24.04)
                    "03-BC-0C-AC-77-14-86-A9" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum seek point interval
                    ["SeekPointInterval"] = 600
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default padding
                    ["Padding"] = 8192
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Disabled padding
                    ["Padding"] = 0
                },
                [
                    "18-53-4C-9C-DB-14-4D-0A", // FLAC 1.3.3 (Ubuntu 22.04)
                    "CA-71-17-17-4F-3B-49-06", // FLAC 1.4.3 (Ubuntu 24.04)
                    "03-5D-35-08-D6-4F-54-FB" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum padding
                    ["Padding"] = 16_775_369
                },
                [
                    "72-95-92-9B-D3-4A-D0-05", // FLAC 1.3.3 (Ubuntu 22.04)
                    "7A-6C-C4-81-BA-09-50-31", // FLAC 1.4.3 (Ubuntu 24.04)
                    "3D-30-B1-95-DC-0E-94-A2" // FLAC 1.5.0
                ]
            ),

            #endregion

            #region ALAC Encoding

            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "1B-97-2F-7F-27-3E-58-B6", // MacOS
                    "CD-99-66-A2-86-65-BF-48" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "2C-7A-B7-77-0A-C0-5F-16", // MacOS
                    "41-B9-DB-52-F3-2C-86-E7" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "0A-92-6C-AA-C2-B6-79-88", // MacOS
                    "72-E8-0A-53-90-E3-E1-7B" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "DD-96-54-5E-F7-FD-78-6B", // MacOS
                    "67-F6-50-0C-1F-5F-B5-99" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "A-law 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "17-DB-B1-B2-AA-B0-C0-4F", // MacOS
                    "EF-BA-B4-2A-50-22-3E-D7" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "µ-law 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "AE-14-71-CB-A3-70-32-E3", // MacOS
                    "25-3D-26-3D-EE-F4-A7-A4" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "9F-67-96-43-29-A8-AF-E0", // MacOS
                    "DF-E8-54-BD-B6-C6-CB-8B" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E6-00-72-36-F0-09-C0-95", // MacOS
                    "E9-0D-87-4E-AD-D9-2E-C0" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "15-06-BE-F4-05-36-B5-9E", // MacOS
                    "52-26-8E-9F-64-89-C8-F6" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Different creation time
                    ["CreationTime"] = new DateTime(2016, 12, 1),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "36-B5-0D-C9-C0-17-17-AC", // MacOS
                    "DD-2D-DA-72-AD-83-C0-85" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Different modification time
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2018, 12, 1)
                },
                [
                    "96-47-BC-9B-0D-90-2A-EE", // MacOS
                    "05-BF-8D-9E-F9-5C-39-AB" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Default padding (explicit)
                    ["Padding"] = 2048,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "2C-7A-B7-77-0A-C0-5F-16", // MacOS
                    "41-B9-DB-52-F3-2C-86-E7" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Disabled padding
                    ["Padding"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "F9-9A-95-42-87-19-C4-28", // MacOS
                    "71-0D-61-58-29-75-1A-2F" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Maximum padding
                    ["Padding"] = 16_777_216,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D4-E8-06-8C-58-82-15-20", // MacOS
                    "5C-99-2E-53-F1-89-28-F8" // Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },

            #endregion

            #region Apple AAC Encoding

            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A7-D2-2A-61-0A-DB-A2-DA", // MacOS on Intel
                    "B5-89-9B-F2-95-78-53-99", // MacOS on ARM
                    "09CD8B8C8E9D8BC09121D8C9F871F9B7", // 32-bit Windows on Intel
                    "AA-14-94-9A-39-F1-26-3D", // 32-bit Windows on AMD
                    "75D127D9FCD7720CBE92C0670A93A880", // 64-bit Windows on Intel
                    "35-3E-A8-A6-1A-E4-37-4D", // 64-bit Windows on Ryzen 5600X
                    "D0-C1-81-B9-A5-D6-F1-31" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D8-F1-C9-8F-AA-70-66-A1", // MacOS on Intel
                    "9A-43-DD-A6-8B-BA-9B-40", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D1-80-5C-EC-9E-42-F8-1B", // MacOS on Intel
                    "75-6D-55-ED-B8-4F-24-03", // MacOS on ARM
                    "CB39DFBF414790022574435C2D30297D", // 32-bit Windows on Intel
                    "3D-90-DA-92-BC-41-4B-5A", // 32-bit Windows on AMD
                    "E0A80A6B32CD5A8FA5C62B44F28C4A87", // 64-bit Windows on Intel
                    "E3-95-7F-57-F1-93-A9-14", // 64-bit Windows on Ryzen 5600X
                    "A3-EE-43-38-A8-9C-83-91" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "7E-D4-AF-E9-78-EB-FC-9B", // MacOS on Intel
                    "76-46-87-88-6A-22-09-58", // MacOS on ARM
                    "E0C34EA1479C8979D3AF3A2C98D4E699", // 32-bit Windows on Intel
                    "53-18-0D-CD-CE-FF-8E-D9", // 32-bit Windows on AMD
                    "ED307F76DD052720321284BAD8876AB2", // 64-bit Windows on Intel
                    "85-67-33-D1-41-55-69-38", // 64-bit Windows on Ryzen 5600X
                    "42-1F-5C-AE-32-8B-4B-1E" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "A-law 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "01-85-13-DD-A6-C5-6E-CA", // MacOS on Intel
                    "34-53-29-45-43-61-6B-20", // MacOS on ARM
                    "6E08F885FEC4094041F6A0B4A02F10AB", // 32-bit Windows on Intel
                    "194D40FBCAE58B4A01095DD89CE70A2D", // 32-bit Windows on AMD
                    "369DAA1350BB9C45BAF84F7769221F00", // 64-bit Windows on Intel
                    "E9-41-0A-C8-25-11-86-F7", // 64-bit Windows on Ryzen 5600X
                    "C6-7A-19-E1-7A-97-5F-2D" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "µ-law 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "9C-A0-7E-3B-B2-D1-BC-C4", // MacOS on Intel
                    "BA-17-CA-F1-AB-A7-77-AF", // MacOS on AMD
                    "D41235E8E642C5773C499DCE06A72CC8", // 32-bit Windows on Intel
                    "CA-F9-FB-5A-AD-F2-FF-C2", // 32-bit Windows on AMD
                    "A86E9A3D4A9479A44F852FA42BA0C9C2", // 64-bit Windows on Intel
                    "72-0B-E6-82-18-51-F2-4C", // 64-bit Windows on Ryzen 5600X
                    "53-F4-E9-DC-D6-BB-AF-D4" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "3B-A6-EA-0B-2D-EA-2A-FF", // MacOS on Intel
                    "42-55-FB-C0-30-95-32-6E", // MacOS on ARM
                    "7BAD797AA7C5F71C7168C24077271029", // 32-bit Windows on Intel
                    "54-D1-D2-6B-FF-E6-65-38", // 32-bit Windows on AMD
                    "102A8F21E39D364419B9CF5BFB386631", // 64-bit Windows on Intel
                    "90-99-EF-1F-75-05-BB-1B", // 64-bit Windows on Ryzen 5600X
                    "F5-71-CE-E7-3A-0D-1F-92" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "02-DE-4C-F4-43-02-18-D9", // MacOS on Intel
                    "30-2D-BF-95-3E-A3-D3-9F", // MacOS on ARM
                    "937750181287505A92B933F8A815D4C2", // 32-bit Windows on Intel
                    "38-C0-2C-7D-67-59-C1-96", // 32-bit Windows on AMD
                    "AAF40EB4D4AC1832D979D6EBDE9C5BDD", // 64-bit Windows on Intel
                    "6B-B4-0A-03-39-1C-F0-DF", // 64-bit Windows on Ryzen 5600X
                    "59-5F-A0-8F-29-95-A0-7F" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "43-C3-33-19-80-7C-BE-C6", // MacOS on Intel
                    "A8-2C-79-F9-B6-A0-A7-C0", // MacOS on ARM
                    "9AC3DEF9B464D0E1AB2D4F91C1A08B83", // 32-bit Windows on Intel
                    "CA-20-41-98-E4-02-3E-0C", // 32-bit Windows on AMD
                    "2863A63E2060267B6A6151CA90239BC6", // 64-bit Windows on Intel
                    "CC-E3-27-EB-55-D5-11-6B", // 64-bit Windows on Ryzen 5600X
                    "AD-38-A6-16-DA-76-0F-C6" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Default VBR quality
                    ["VBRQuality"] = 9,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D8-F1-C9-8F-AA-70-66-A1", // MacOS on Intel
                    "9A-43-DD-A6-8B-BA-9B-40", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "33-E2-A2-75-A5-22-0E-BA", // MacOS on Intel
                    "55-44-84-FA-BE-5A-16-28", // MacOS on ARM
                    "78299761793D1A6EC79CBB9233156FD8", // 32-bit Windows on Intel
                    "E53BA332FDCFBE927A81040DB480688B", // 32-bit Windows on AMD
                    "93D67A9C673E7ABE3929846DBE5DBF97", // 64-bit Windows on Intel
                    "03-CE-28-10-F4-67-A7-B7", // 64-bit Windows on Ryzen 5600X
                    "38-18-6E-0B-2B-E2-C2-4A" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 14,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E6-E2-90-70-75-10-CC-18", // MacOS on Intel
                    "62-51-EB-63-F8-7F-E5-60", // MacOS on ARM
                    "7EDD94F25082AEEE82B2AA87E795AB6D", // 32-bit Windows on Intel
                    "9D-FD-71-7F-11-4C-7C-D7", // 32-bit Windows on AMD
                    "A1CD6AC102BA40A728B2C7E00B1E786D", // 64-bit Windows on Intel
                    "C5-49-22-F7-D8-22-EA-64", // 64-bit Windows on Ryzen 5600X
                    "41-D2-1B-97-BC-22-00-E4" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Minimum bit rate (stereo is automatically increased to 64)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "3A-2C-E0-BB-C8-3C-2A-F8", // MacOS on Intel
                    "1F-C0-6A-65-96-20-03-09", // MacOS on ARM
                    "0177BB1DEB19854CA8495C4CBBB25366", // 32-bit Windows on Intel
                    "7A-92-5E-0F-FF-CE-D0-FC", // 32-bit Windows on AMD
                    "38D28BD3802566CB30D3B824D7FF593F", // 64-bit Windows on Intel
                    "FA-0F-C7-19-0F-26-BB-AA", // 64-bit Windows on Ryzen 5600X
                    "B8-42-9B-41-89-06-AC-B6" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Minimum bit rate (mono)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A7-0C-58-93-A1-D3-56-04", // MacOS
                    "9E77C0824474E3600F1A919715609A1B", // 32-bit Windows
                    "04-50-78-A0-C1-3F-32-A7" // 64-bit Windows
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Maximum bit rate (stereo)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A3-1E-42-BB-E4-C7-B1-90", // MacOS on Intel
                    "40-B8-14-8A-F0-D3-D4-64", // MacOS on ARM
                    "EBD496E30A953A8D0FE11C2609EFABC3", // 32-bit Windows on Intel
                    "E9-3C-D8-9E-D6-48-4E-7D", // 32-bit Windows on AMD
                    "2AD5FC82A78732A66B8F04387D7D412B", // 64-bit Windows on Intel
                    "66-48-1D-33-22-43-53-FF", // 64-bit Windows on Ryzen 5600X
                    "F1-A4-9F-B8-8E-59-B0-BF" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Maximum bit rate (mono is automatically reduced to 256)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E7-E6-F3-09-69-F4-0B-C9", // MacOS on Intel
                    "58-CB-13-FF-D5-D1-18-EB", // MacOS on ARM
                    "DE5F94EC1EACB75A3D049AE9960A7ACB", // 32-bit Windows on Intel
                    "97-CE-44-9B-71-22-CC-C9", // 32-bit Windows on AMD
                    "298A2B946AA53102FD025DDD9D273B21", // 64-bit Windows on Intel
                    "FF-B3-B1-3F-D0-D9-26-A1", // 64-bit Windows on Ryzen 5600X
                    "79-05-CC-7A-EE-CF-1A-E6" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constrained bit rate mode (default)
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [

                    "E0-91-F3-FD-0E-3C-F5-1A", // MacOS on Intel
                    "2C-F5-B4-05-2D-2C-FB-DA", // MacOS on ARM
                    "D4-96-9B-4B-59-5F-B5-CE", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "5B-E0-72-79-CF-69-6E-91", // 64-bit Windows on Ryzen 5600X
                    "0A-9B-19-DA-40-0A-D1-0F" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constrained bit rate mode (explicit)
                    ["ControlMode"] = "Constrained",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E0-91-F3-FD-0E-3C-F5-1A", // MacOS on Intel
                    "2C-F5-B4-05-2D-2C-FB-DA", // MacOS on ARM
                    "D4-96-9B-4B-59-5F-B5-CE", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "5B-E0-72-79-CF-69-6E-91", // 64-bit Windows on Ryzen 5600X
                    "0A-9B-19-DA-40-0A-D1-0F" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Average bit rate mode
                    ["ControlMode"] = "Average",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "95-A5-76-FE-A3-32-33-72", // MacOS on Intel
                    "55-83-14-EE-1E-DE-D7-9B", // MacOS on ARM
                    "47-ED-90-FB-48-2E-19-55", // 32-bit Windows
                    "D4A9A3FFC75AC0383B68BADA43E23C3D", // 64-bit Windows on Intel
                    "B8-F5-05-D4-50-4C-35-AA", // 64-bit Windows on Ryzen 5600X
                    "2B-8C-3C-6F-C7-46-66-66" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constant bit rate mode
                    ["ControlMode"] = "Constant",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "61-11-48-3A-D6-7F-C3-28", // MacOS on Intel
                    "A3-32-D7-07-D1-E5-55-80", // MacOS on ARM
                    "365D7E965534C8690B4694B27D0CF1C9", // 32-bit Windows on Intel
                    "98-55-BA-2D-4A-72-F1-1A", // 32-bit Windows on AMD
                    "08686D04EFF88BC663C469F2DD224020", // 64-bit Windows on Intel
                    "BC-47-2F-B6-C5-50-ED-17", // 64-bit Windows on Ryzen 5600X
                    "AE-A9-BC-CB-C8-16-55-F6" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D8-F1-C9-8F-AA-70-66-A1", // MacOS on Intel
                    "9A-43-DD-A6-8B-BA-9B-40", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "F7-57-90-77-62-D5-F6-4B", // MacOS on Intel
                    "A6-88-93-15-5B-B8-3B-CA", // MacOS on ARM
                    "DDA8DBB070EA36F77455A41A2628B6AA", // 32-bit Windows on Intel
                    "F2-7E-DD-D0-8F-A4-88-B5", // 32-bit Windows on AMD
                    "B49EC8F6428A1CDEBA4F0728FC1BF8E5", // 64-bit Windows on Intel
                    "C9-58-58-74-36-AD-27-F5", // 64-bit Windows on Ryzen 5600X
                    "03-6D-BD-AB-2C-26-82-33" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "56-91-5D-62-00-A0-10-10", // MacOS on Intel
                    "E1-76-F5-FC-7C-A7-5C-F6", // MacOS on ARM
                    "5502D724D98AA24FE49FA8AFB0FC63A6", // 32-bit Windows on Intel
                    "6A-C8-FA-AB-6D-4D-B2-A5", // 32-bit Windows on AMD
                    "19940A1BA1D575D9E165584C24A955F4", // 64-bit Windows on Intel
                    "34-90-BE-A7-5B-15-FA-72", // 64-bit Windows on Ryzen 5600X
                    "BD-40-59-07-7A-39-ED-D4" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },

            #endregion

            #region Lame MP3 Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "37-89-CF-B9-AD-11-FE-10", // Lame 3.100 (Ubuntu and MacOS)
                    "EC-CA-7C-D9-86-6B-5E-6C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                [],
                [
                    "F0-94-E7-B4-66-43-B4-06", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "83-96-E5-7B-DD-FC-97-CB", // Lame 3.100 (MacOS on ARM)
                    "65-6E-9C-0F-7A-81-D4-3C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "4B-FE-37-D5-81-89-22-16", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "6B-B6-75-59-B9-FB-5E-65", // Lame 3.100 (MacOS on ARM)
                    "2E-BE-F3-A0-4B-CB-79-AC" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "E9-EC-15-D8-B2-E0-91-6D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "CC-15-84-E1-5B-39-3F-DD", // Lame 3.100 (MacOS on ARM)
                    "5B-80-48-5D-A6-B1-22-5C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "1F-2A-23-34-9D-2F-6E-0D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "5E-46-C2-35-69-2C-4D-2F", // Lame 3.100 (MacOS on ARM)
                    "BF-DA-07-84-22-66-32-BB" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "24-34-8E-86-D4-DB-9B-7F", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "4A-C1-76-E4-25-DC-40-9E", // Lame 3.100 (MacOS on ARM)
                    "1A-B5-6B-E4-A7-65-B1-D3" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                [],
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                [],
                [
                    "3A-A5-7A-01-8E-DB-0B-13", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "B3-B3-BC-FE-D9-5C-A7-A8", // Lame 3.100 (MacOS on ARM)
                    "34-36-E0-A3-31-49-3F-8D" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                [],
                [
                    "C1-29-F7-C0-DA-BD-9B-02", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "64-87-AF-6B-AA-5F-E9-DC", // Lame 3.100 (MacOS on ARM)
                    "02-18-09-07-B4-3B-B1-72" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag version
                    ["TagVersion"] = "2.3"
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Tag version 2.4
                    ["TagVersion"] = "2.4"
                },
                [
                    "9F-EE-55-1E-12-70-63-C5", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-25-31-8B-E5-67-7D-CC", // Lame 3.100 (MacOS on ARM)
                    "71-E8-2E-C0-61-10-47-69" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag encoding
                    ["TagEncoding"] = "Latin1"
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-16 tag encoding
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "25-E9-A0-CB-0D-3E-50-CD", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "40-53-53-F6-0E-DD-67-77", // Lame 3.100 (MacOS on ARM)
                    "95-4D-90-F8-08-38-CD-39" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, implicit tag version 2.4
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, explicit tag version 2.4
                    ["TagVersion"] = "2.4",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, ignored tag version 2.3
                    ["TagVersion"] = "2.3",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag padding (explicit)
                    ["TagPadding"] = 2048
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Tag padding disabled
                    ["TagPadding"] = 0
                },
                [
                    "70-23-C1-4C-76-D0-97-9B", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E7-2C-0C-BF-B2-B0-FB-52", // Lame 3.100 (MacOS on ARM)
                    "71-CA-62-E2-A8-06-F5-31" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Maximum tag padding
                    ["TagPadding"] = 16_777_216
                },
                [
                    "5A-0D-3B-82-4C-5C-E6-E0", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "C0-5E-50-42-3D-CB-C4-50", // Lame 3.100 (MacOS on ARM)
                    "F6-D8-85-E6-25-FC-3B-B4" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag version does nothing without metadata
                    ["TagVersion"] = "2.4"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag encoding does nothing without metadata
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag padding does nothing without metadata
                    ["TagPadding"] = 100
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Default VBR quality
                    ["VBRQuality"] = 3
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 9
                },
                [
                    "B1-18-EF-8F-81-1A-BE-1A", // Lame 3.100 (Ubuntu and MacOS)
                    "B2-C7-B0-17-E1-AC-49-20" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 0
                },
                [
                    "F3-76-6B-5D-4C-71-B6-D6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "51-C7-76-DB-2F-47-96-1B", // Lame 3.100 (MacOS on ARM)
                    "0D-14-25-F0-35-BD-AD-EB" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 8
                },
                [
                    "CA-25-80-FB-9E-FB-4A-30", // Lame 3.100 (Ubuntu and MacOS)
                    "4D-7E-24-C8-7F-5D-16-2E" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 320
                },
                [
                    "4F-27-D2-D4-C5-C9-CA-DC", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "06-18-B3-99-8D-D6-09-02", // Lame 3.100 (MacOS on ARM)
                    "54-5F-2C-AC-90-8A-7A-E3" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate explicitly disabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true
                },
                [
                    "53-DC-C3-6F-0D-BC-D3-C6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E9-D0-7D-BD-09-41-F1-9B", // Lame 3.100 (MacOS on ARM)
                    "07-D4-A6-00-D7-DB-01-05" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // VBR quality ignored with bit rate
                    ["VBRQuality"] = 3,
                    ["BitRate"] = 128
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track"
                },
                [
                    "66-7D-6B-28-F3-54-BF-A3", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "53-C5-E7-DE-6C-0B-4F-8C", // Lame 3.100 (MacOS on ARM)
                    "00-FC-27-73-8D-4F-82-C2" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album"
                },
                [
                    "E8-43-CC-9F-92-D5-16-13", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E5-68-30-CA-53-75-88-22", // Lame 3.100 (MacOS on ARM)
                    "83-45-FA-75-27-7B-96-FB" // Lame 3.100 (Windows)
                ]
            ),

            #endregion

            #region Ogg Vorbis Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "54-B9-54-1F-BA-EA-51-20", // Vorbis 1.3.7 (Ubuntu)
                    "6D-4C-1D-DC-9F-1A-79-E8", // Vorbis 1.3.7 (MacOS on Intel)
                    "61-76-72-8A-57-1F-18-CE", // Vorbis 1.3.7 (MacOS on ARM)
                    "CE-1A-5C-8D-18-ED-CC-12" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "98-95-AD-1E-8D-CC-B4-5D", // Vorbis 1.3.7 (Ubuntu)
                    "E0-5D-1C-78-BF-8D-C2-5C", // Vorbis 1.3.7 (MacOS on Intel)
                    "57-F7-00-B1-93-DB-A4-23", // Vorbis 1.3.7 (MacOS on ARM)
                    "62C6F8889AA6CBE4A80750EFF33D9FDA", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "92-B0-23-16-4F-17-F1-40" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "7B-F4-E5-1A-63-65-17-F2", // Vorbis 1.3.7 (Ubuntu)
                    "57-52-C9-44-15-B0-BC-EA", // Vorbis 1.3.7 (MacOS on Intel)
                    "F6-69-65-9A-08-E2-7F-9E", // Vorbis 1.3.7 (MacOS on ARM)
                    "01-C9-CB-29-C7-10-E9-72" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "9A-41-46-BD-F6-06-42-19", // Vorbis 1.3.7 (Ubuntu)
                    "7C-E9-0D-49-14-8B-E5-8B", // Vorbis 1.3.7 (MacOS on Intel)
                    "CB-2B-4F-CB-0A-DD-86-DB", // Vorbis 1.3.7 (MacOS on ARM)
                    "30-AE-39-33-7E-9F-53-71" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "3B-00-E8-75-90-45-56-55", // Vorbis 1.3.7 (Ubuntu)
                    "5F-F2-6C-F8-31-B5-07-92", // Vorbis 1.3.7 (MacOS on Intel)
                    "06-03-4E-E0-ED-EC-23-AA", // Vorbis 1.3.7 (MacOS on ARM)
                    "12-24-F2-BC-FA-5C-7B-CD" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "CA-31-9D-C8-BD-05-37-EC", // Vorbis 1.3.7 (Ubuntu)
                    "2A-B0-16-9B-E8-D7-2E-F8", // Vorbis 1.3.7 (MacOS on Intel)
                    "BC-BD-52-A6-3D-71-1F-52", // Vorbis 1.3.7 (MacOS on ARM)
                    "4E69AF464154872795B7AD87BA762870", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "A6-8D-CF-02-3D-84-57-BB" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "F3-B4-1E-71-91-8E-46-C7", // Vorbis 1.3.7 (Ubuntu)
                    "C7-07-85-AC-0A-32-59-76", // Vorbis 1.3.7 (MacOS on Intel)
                    "45-B3-5C-CA-3D-D4-7A-FE", // Vorbis 1.3.7 (MacOS on ARM)
                    "66-86-D8-7E-07-36-48-8F" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "8E-0A-1E-18-89-E9-3C-B9", // Vorbis 1.3.7 (Ubuntu)
                    "AB-8A-CC-8A-A5-DE-20-EA", // Vorbis 1.3.7 (MacOS on Intel)
                    "A9-77-06-3C-EB-6E-8E-CB", // Vorbis 1.3.7 (MacOS on ARM)
                    "1B-25-B7-62-81-AF-5A-7C" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "F4-39-E3-17-47-27-66-C6", // Vorbis 1.3.7 (Ubuntu)
                    "BD-31-0A-73-77-C1-A5-F1", // Vorbis 1.3.7 (MacOS on Intel)
                    "C9-E0-AE-15-F5-F0-82-DD", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-9B-28-EA-48-0B-20-DE" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "D3-40-F7-C6-41-76-16-8A", // Vorbis 1.3.7 (Ubuntu)
                    "29-DA-6A-15-5A-FC-E1-0D", // Vorbis 1.3.7 (MacOS on Intel)
                    "AC-10-8B-AC-74-D0-DD-08", // Vorbis 1.3.7 (MacOS on ARM)
                    "15-D0-92-19-0E-C4-38-6A" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "42-B5-34-C2-8E-C7-A6-03", // Vorbis 1.3.7 (Ubuntu)
                    "C6-F4-D3-E7-70-F5-77-20", // Vorbis 1.3.7 (MacOS on Intel)
                    "F8-27-96-4D-02-16-3E-55", // Vorbis 1.3.7 (MacOS on ARM)
                    "85-F5-33-EC-75-EA-3F-25" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Default quality (explicit)
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum quality
                    ["Quality"] = -1,
                    ["SerialNumber"] = 1
                },
                [
                    "98-C4-E1-84-EB-7F-61-52", // Vorbis 1.3.7 (Ubuntu)
                    "82-CC-2B-EB-43-50-B4-50", // Vorbis 1.3.7 (MacOS on Intel)
                    "60-53-00-7A-F0-4F-56-FA", // Vorbis 1.3.7 (MacOS on ARM)
                    "79C966C3D6728C49723640C0D7B9330B", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "2F-2B-AD-1A-25-C4-6F-00" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)

                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum quality
                    ["Quality"] = 10,
                    ["SerialNumber"] = 1
                },
                [
                    "ED-B2-EF-24-F7-3E-80-70", // Vorbis 1.3.7 (Ubuntu)
                    "2A-B3-88-46-0B-F7-A0-D7", // Vorbis 1.3.7 (MacOS on Intel)
                    "86-30-CA-4A-69-45-26-D4", // Vorbis 1.3.7 (MacOS on ARM)
                    "4B2B694BD0D42994F4A1911FBCB2ABF8", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "DC-0B-7C-C4-AD-53-C8-28" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 45,
                    ["SerialNumber"] = 1
                },
                [
                    "74-4A-E3-12-85-C8-A5-55", // Vorbis 1.3.7 (Ubuntu)
                    "86-F9-8A-70-29-95-C5-9F", // Vorbis 1.3.7 (MacOS on Intel)
                    "3A-D1-B3-90-8D-AB-7D-BF", // Vorbis 1.3.7 (MacOS on ARM)
                    "CAEAE4C932830A8C0A41BC5C79DC80D5", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EB-D8-C7-67-24-BC-50-20" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 500,
                    ["SerialNumber"] = 1
                },
                [
                    "F2-C3-FA-E0-D4-6F-B3-BA", // Vorbis 1.3.7 (Ubuntu)
                    "96-90-94-99-0F-97-E8-4E", // Vorbis 1.3.7 (MacOS on Intel)
                    "EC-52-72-4C-50-34-39-60", // Vorbis 1.3.7 (MacOS on ARM)
                    "215FA0E953F4BB520A46A3B44B68CC92", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "60-4A-A5-B9-DE-35-98-83" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate disabled (explicit)
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "F5-8B-06-6A-B5-F0-6C-7E", // Vorbis 1.3.7 (Ubuntu)
                    "D9-5F-7B-E5-A4-F0-4F-B8", // Vorbis 1.3.7 (MacOS on Intel)
                    "15-AF-C8-95-A6-E5-F5-DA", // Vorbis 1.3.7 (MacOS on ARM)
                    "C84819FCFA2F25FCDB3E5490E54949B4", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "16-02-3E-BF-B2-20-34-8D" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Quality ignored with bit rate
                    ["Quality"] = 3,
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "06-EF-8A-60-69-69-5C-F2", // Vorbis 1.3.7 (Ubuntu)
                    "3B-D8-37-9D-10-CD-E4-7A", // Vorbis 1.3.7 (MacOS on Intel)
                    "A2-CD-A0-5C-F9-05-2C-E0", // Vorbis 1.3.7 (MacOS on ARM)
                    "805F62BDFE149898E21C9448F4335BAC", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "57-A6-44-D0-49-38-29-3E" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "15-92-D2-A9-0B-46-46-96", // Vorbis 1.3.7 (Ubuntu)
                    "F6-CB-E3-5A-B6-83-BC-01", // Vorbis 1.3.7 (MacOS on Intel)
                    "4C-AF-38-66-16-C1-C2-1D", // Vorbis 1.3.7 (MacOS on ARM)
                    "34BA39848B7D78D7FE1D2B30999DF6A9", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "7F-37-C9-AA-CF-F8-0F-B7" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ),

            #endregion

            #region Opus Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "4D-18-86-BA-F9-3E-C4-A5", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "99-E7-6E-6D-61-01-97-69", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "EA-8B-4F-7A-A7-AD-B8-01", // Opus 1.5.2 (MacOS on Intel)
                    "BC-18-10-E5-D9-B0-B6-C4", // Opus 1.5.2 (MacOS on ARM)
                    "47359005DE5203C8AF0FDDE7F34713D9", // Opus 1.5.2 (32-bit Windows on Intel)
                    "24-0A-93-E2-B2-A4-59-F2", // Opus 1.5.2 (32-bit Windows on AMD)
                    "A2EDE18F0E554FDC9EE5DD9C62622236", // Opus 1.5.2 (64-bit Windows on Intel)
                    "92-71-61-35-21-92-BB-2D" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "18-66-64-09-DF-53-24-26", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "26-8D-CE-A9-BD-22-F7-DF", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "0D-70-48-A1-81-1E-04-FB", // Opus 1.5.2 (MacOS on Intel)
                    "54-D7-D7-08-3D-DF-80-83", // Opus 1.5.2 (MacOS on ARM)
                    "6F0174BCFBF9AE02ACFCC0B2E3B834B8", // Opus 1.5.2 (Windows on Intel)
                    "1B-DD-B3-B2-65-76-86-88" // Opus 1.5.2 (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "13-77-D7-3E-62-41-3E-5A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6A-EE-F6-02-4C-E2-FE-83", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "26-CB-03-BB-31-D8-6F-79", // Opus 1.5.2 (MacOS on Intel)
                    "E4-11-3B-34-D9-6A-05-CF", // Opus 1.5.2 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "54-00-2E-64-5E-A3-CB-1F", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "1F-10-2A-07-1B-A1-16-A6", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "79-BF-E6-84-59-14-7C-09", // Opus 1.5.2 (MacOS on Intel)
                    "05-0B-39-E2-A3-5A-63-C4", // Opus 1.5.2 (MacOS on ARM)
                    "AB158A17CCED018828790DAA396BAB93", // Opus 1.5.2 (32-bit Windows on Intel)
                    "53-DF-9C-FA-95-2E-A0-0B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "F4D51B165CDACF9CC1740A023FC832F7", // Opus 1.5.2 (64-bit Windows on Intel)
                    "7F-F2-47-47-96-95-17-44" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "92-4B-3F-32-9B-B1-0A-ED", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "38-49-4D-D7-07-67-B0-1F", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "9D-8C-40-EB-5C-E5-2D-66", // Opus 1.5.2 (MacOS on Intel)
                    "5D-94-59-FE-62-39-63-5D", // Opus 1.5.2 (MacOS on ARM)
                    "DA42257349612217D56C05879F45DD52", // Opus 1.5.2 (32-bit Windows on Intel)
                    "4D-8D-D5-E5-C9-1D-77-1E", // Opus 1.5.2 (32-bit Windows on AMD)
                    "48D4A34C2C1BB5B6F1230BFE64764ED6", // Opus 1.5.2 (64-bit Windows on Intel)
                    "0F-34-1A-5D-FB-3A-A9-67" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "1E-6A-6E-4B-67-D1-F5-BB", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "9D-F3-E4-58-82-19-17-C2", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "25-06-66-0A-E7-C9-2D-DA", // Opus 1.5.2 (MacOS on Intel)
                    "AD-44-82-D4-73-01-9C-9E", // Opus 1.5.2 (MacOS on ARM)
                    "8BB79D2D7415D4082679D416B2F41D93", // Opus 1.5.2 (32-bit Windows on Intel)
                    "56-29-04-78-6D-AD-0C-B7", // Opus 1.5.2 (32-bit Windows on AMD)
                    "AE7BD2C79045C6DD3AFD6D20A98902D6", // Opus 1.5.2 (64-bit Windows on Intel)
                    "5B-B0-B3-74-2D-6B-0F-51" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "5E-BF-ED-40-0E-B4-78-BF", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6B-DD-C0-E2-E9-94-00-88", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "E7-EF-9B-A1-6C-3F-C0-94", // Opus 1.5.2 (MacOS on Intel)
                    "26-FC-82-7E-3F-85-C9-CA", // Opus 1.5.2 (MacOS on ARM)
                    "F8C93268C74883B727D78FBDB38F831F", // Opus 1.5.2 (32-bit Windows on Intel)
                    "3C-99-48-CC-0E-F3-EE-42", // Opus 1.5.2 (32-bit Windows on AMD)
                    "DCB3078A6720C49D9726C64C0DCCCD7E", // Opus 1.5.2 (64-bit Windows on Intel)
                    "93-3D-6E-04-C2-68-E7-D9" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "32-05-62-75-0B-54-4A-9D", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "76-D6-E9-EF-06-6D-67-96", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "CC-8F-52-F1-EA-62-C7-C1", // Opus 1.5.2 (MacOS on Intel)
                    "30-EC-A5-6C-64-65-2E-7B", // Opus 1.5.2 (MacOS on ARM)
                    "C461EAB699527B90A919D2B26979A30A", // Opus 1.5.2 (32-bit Windows on Intel)
                    "73-80-4E-A5-13-A2-0A-6D", // Opus 1.5.2 (32-bit Windows on AMD)
                    "F6F88CF0DC907202CE5F91CC706A8ADD", // Opus 1.5.2 (64-bit Windows on Intel)
                    "AD-63-20-AC-EA-79-E9-66" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "3A-67-4E-24-B8-A3-E1-45", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "9C-2E-E2-DC-D4-3B-91-3B", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "FF-D9-AF-4A-50-EE-C6-D6", // Opus 1.5.2 (MacOS on Intel)
                    "38-04-12-50-66-56-7C-8F", // Opus 1.5.2 (MacOS on ARM)
                    "F24CCDB06BAFA257E2276FC09D2E3100", // Opus 1.5.2 (32-bit Windows on Intel)
                    "A5-23-52-3D-A8-3E-C9-B2", // Opus 1.5.2 (32-bit Windows on AMD)
                    "013902EE71579EDC9C477629381B5706", // Opus 1.5.2 (64-bit Windows on Intel)
                    "BF-D3-36-61-1B-61-CB-86" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "59-61-98-BE-00-DF-DE-C0", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "BE-BA-6B-6B-E6-A3-AA-D6", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "ED-EB-4E-BE-C3-E6-A6-D5", // Opus 1.5.2 (MacOS on Intel)
                    "B6-1C-64-4E-0C-1B-48-E9", // Opus 1.5.2 (MacOS on ARM)
                    "AA83D462884ABCC559AFAB31D08652CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "E8-E6-55-C8-C1-77-47-2C", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BDD151A5D31382F430EA2139D18DD46A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "64-BC-46-7F-DA-27-9E-B4" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "06-66-0A-F6-AF-07-4C-2B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "7D-A8-9F-03-65-EC-2E-C8", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "65-D7-87-94-A9-E9-E7-BD", // Opus 1.5.2 (MacOS on Intel)
                    "64-24-6F-D3-84-A1-EF-1C", // Opus 1.5.2 (MacOS on ARM)
                    "C768F0F87DAD97B1FF7C0A03FE6C2950", // Opus 1.5.2 (32-bit Windows on Intel)
                    "57-C3-D6-08-B5-11-78-A6", // Opus 1.5.2 (32-bit Windows on AMD)
                    "828B1323F1F7F6E9589249130098DB64", // Opus 1.5.2 (64-bit Windows on Intel)
                    "CD-84-6F-09-08-78-6A-9B" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "51-FF-C3-03-D8-B2-98-39", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "5E-77-C4-DD-72-BA-FE-D5", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "E9-3C-9C-89-CE-C6-DD-71", // Opus 1.5.2 (MacOS on Intel)
                    "04-73-D9-F3-C4-63-83-E6", // Opus 1.5.2 (MacOS on ARM)
                    "B3D8AECD20CEC76CD140DA44979A60E4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "0F-F5-62-B5-00-AF-0D-7D", // Opus 1.5.2 (32-bit Windows on AMD)
                    "4195302C28B204B94F945E14A4A9672B", // Opus 1.5.2 (64-bit Windows on Intel)
                    "10-3E-23-D0-2C-F3-05-52" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 5,
                    ["SerialNumber"] = 1
                },
                [
                    "2B-0E-A7-92-42-A8-30-FB", // Opus 1.3.1 (Ubuntu 22.04)
                    "4E-56-99-1B-2A-FE-B4-BF", // Opus 1.4.0 (Ubuntu 24.04)
                    "9B-89-72-86-CF-FC-9E-DE", // Opus 1.5.2 (MacOS on Intel)
                    "6A-04-91-67-5A-F7-FD-F1", // Opus 1.5.2 (MacOS on ARM)
                    "F1-FA-1B-A9-3B-51-2C-49" // Opus 1.5.2 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 512,
                    ["SerialNumber"] = 1
                },
                [
                    "C0-80-59-CC-D2-7D-08-AA", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "50-15-21-02-0E-28-68-F6", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "D2-74-4E-6F-86-57-ED-B0", // Opus 1.5.2 (MacOS on Intel)
                    "1F-4A-FC-4B-5A-89-AF-64", // Opus 1.5.2 (MacOS on ARM)
                    "6B422669A0FCB242E0E15204F5FDCC47", // Opus 1.3.1 (32-bit Windows on Intel)
                    "C4-2A-4B-CD-42-71-E8-B2", // Opus 1.5.2 (32-bit Windows on AMD)
                    "CA4D9FA683B85ADCF4132828402D9994", // Opus 1.5.2 (64-bit Windows on Intel)
                    "C8-D1-8B-E2-72-87-B5-49" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Variable control mode (default, explicit)
                    ["ControlMode"] = "Variable",
                    ["SerialNumber"] = 1
                },
                [
                    "13-77-D7-3E-62-41-3E-5A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6A-EE-F6-02-4C-E2-FE-83", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "26-CB-03-BB-31-D8-6F-79", // Opus 1.5.2 (MacOS on Intel)
                    "E4-11-3B-34-D9-6A-05-CF", // Opus 1.5.2 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Constrained VBR mode
                    ["ControlMode"] = "Constrained",
                    ["SerialNumber"] = 1
                },
                [
                    "19-5D-E0-D8-D5-6D-E0-1E", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "48-31-E2-1D-B6-08-37-A9", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "3A-83-E3-04-6E-54-7C-B1", // Opus 1.5.2 (MacOS on Intel)
                    "A3-8E-11-D6-B9-DD-9C-C9", // Opus 1.5.2 (MacOS on ARM)
                    "93110D453D9EF4DF0EF57B0B758092D7", // Opus 1.5.2 (32-bit Windows on Intel)
                    "A9-B7-3F-59-FC-4E-8E-0A", // Opus 1.5.2 (32-bit Windows on AMD)
                    "1C8382776908484524B1F8287A133A1F", // Opus 1.5.2 (64-bit Windows on Intel)
                    "F6-F8-7D-60-46-4A-C0-50" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // CBR mode
                    ["ControlMode"] = "Constant",
                    ["SerialNumber"] = 1
                },
                [
                    "1D-F1-A9-CE-D8-8E-23-F0", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "D6-71-3B-9F-80-3B-DB-2C", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "C2-DB-76-2D-08-CA-C5-B1", // Opus 1.5.2 (MacOS on Intel)
                    "5C-82-04-7E-53-90-E0-07", // Opus 1.5.2 (MacOS on ARM)
                    "3CE799E222F941AC4BE899AB1B5236B4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "7F-40-F2-64-3F-CB-02-20", // Opus 1.5.2 (32-bit Windows on AMD)
                    "682884B45E806A93559FD12B759992D7", // Opus 1.5.2 (64-bit Windows on Intel)
                    "02-4A-9A-CF-6F-2E-C3-61" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Music signal type (default)
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "FD-6D-78-EB-8E-7E-C8-AE", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B1-4C-57-71-E1-72-82-CC", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6D-4D-93-AD-68-72-DC-82", // Opus 1.5.2 (MacOS on Intel)
                    "78-4D-24-AF-8D-A2-62-5A", // Opus 1.5.2 (MacOS on ARM)
                    "47CA0622B6C24FEF2BDA687457AF6EE3", // Opus 1.5.2 (Windows on Intel)
                    "AB-47-63-8F-6B-5F-E6-F9" // Opus 1.5.2 (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Music signal type (explicit)
                    ["SignalType"] = "Music",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "FD-6D-78-EB-8E-7E-C8-AE", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B1-4C-57-71-E1-72-82-CC", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6D-4D-93-AD-68-72-DC-82", // Opus 1.5.2 (MacOS on Intel)
                    "78-4D-24-AF-8D-A2-62-5A", // Opus 1.5.2 (MacOS on ARM)
                    "47CA0622B6C24FEF2BDA687457AF6EE3", // Opus 1.5.2 (Windows on Intel)
                    "AB-47-63-8F-6B-5F-E6-F9" // Opus 1.5.2 (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Speech signal type
                    ["SignalType"] = "Speech",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "7A-B8-FF-08-6E-DC-6C-2B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "07-CC-93-B7-3B-3C-F9-46", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "07-48-7A-BE-D6-C1-64-6B", // Opus 1.5.2 (MacOS on Intel)
                    "A2-1E-D0-33-0A-AE-1F-85", // Opus 1.5.2 (MacOS on ARM)
                    "C1E6A103E4499035344A3B913611C0E0", // Opus 1.5.2 (Windows on Intel)
                    "CB-F0-EB-C6-A6-5D-5F-79" // Opus 1.5.2 (Windows on AMD)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "13-77-D7-3E-62-41-3E-5A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6A-EE-F6-02-4C-E2-FE-83", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "26-CB-03-BB-31-D8-6F-79", // Opus 1.5.2 (MacOS on Intel)
                    "E4-11-3B-34-D9-6A-05-CF", // Opus 1.5.2 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "C9-41-5E-A2-A8-67-FF-65", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "C1-01-89-41-A0-DD-C0-55", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "F2-39-7D-F4-8F-BF-AB-83", // Opus 1.5.2 (MacOS on Intel)
                    "A8-35-6E-0F-7B-E9-C2-13", // Opus 1.5.2 (MacOS on ARM)
                    "E199F6D24770B47A9AF906F3ECA8E863", // Opus 1.5.2 (32-bit Windows on Intel)
                    "E0-9F-C7-27-01-4B-BA-0C", // Opus 1.5.2 (32-bit Windows on AMD)
                    "D6414CE58A6E7C07C17C9CC11A8CD966", // Opus 1.5.2 (64-bit Windows on Intel)
                    "B6-6D-CD-F5-86-E8-3C-D1" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "DE-EE-83-C7-63-E3-B2-B1", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "3C-8F-1B-DD-22-D9-F0-D9", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "F1-BF-D8-0A-3F-2F-CD-85", // Opus 1.5.2 (MacOS on Intel)
                    "77-CB-F0-A8-49-89-A8-7F", // Opus 1.5.2 (MacOS on ARM)
                    "E8199A697B010856D7ED9F4C67A82481", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-97-34-77-2C-BE-0C-79", // Opus 1.5.2 (32-bit Windows on AMD)
                    "1C7A4661C5DD9C5CD8FC8CED950E2C80", // Opus 1.5.2 (64-bit Windows on Intel)
                    "A0-DE-10-DD-85-E3-AA-56" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            )

            #endregion
        ];

        public static IEnumerable<TheoryDataRow<int, string, string, SettingDictionary, string[]>> Data =>
            _data.Select((item, index) =>
                new TheoryDataRow<int, string, string, SettingDictionary, string[]>(
                        index, item.Data.Item1, item.Data.Item2, item.Data.Item3, item.Data.Item4)
                    { Skip = item.Skip });
    }
}