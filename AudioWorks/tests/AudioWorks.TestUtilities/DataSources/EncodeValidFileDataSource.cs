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
            ) { Label = "Wave LPCM 8-bit 8000Hz Stereo -> Wave" },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Mono -> Wave" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Wave" },
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            ) { Label = "Wave LPCM 16-bit 48000Hz Stereo -> Wave" },
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            ) { Label = "Wave LPCM 24-bit 96000Hz Stereo -> Wave" },
            new(
                "A-law 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "F5-C6-67-6D-47-AE-A7-D4"
                ]
            ) { Label = "Wave A-law 44100Hz Stereo -> Wave" },
            new(
                "µ-law 44100Hz Stereo.wav",
                "Wave",
                [],
                [
                    "AF-AE-6C-F1-37-3F-6A-96"
                ]
            ) { Label = "Wave µ-law 44100Hz Stereo -> Wave" },
            new(
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Mono -> Wave" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo -> Wave" },
            new(
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            ) { Label = "FLAC Level 5 16-bit 48000Hz Stereo -> Wave" },
            new(
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            ) { Label = "FLAC Level 5 24-bit 96000Hz Stereo -> Wave" },
            new(
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                [],
                [
                    "EC-CF-6D-8A-B5-2B-65-3E"
                ]
            )
            {
                Label = "ALAC 16-bit 44100Hz Mono -> Wave",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "07-B0-94-C5-7C-28-85-1C"
                ]
            )
            {
                Label = "ALAC 16-bit 44100Hz Stereo -> Wave",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "D1-C8-44-3D-CD-57-C6-3F"
                ]
            )
            {
                Label = "ALAC 16-bit 48000Hz Stereo -> Wave",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                [],
                [
                    "22-E0-8E-CC-CB-A7-2F-9D"
                ]
            )
            {
                Label = "ALAC 24-bit 96000Hz Stereo -> Wave",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },

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
            ) { Label = "Wave LPCM 8-bit 8000Hz Stereo -> FLAC" },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                [],
                [
                    "04-F0-CB-82-FF-F6-27-95", // FLAC 1.3.3 (Ubuntu 22.04)
                    "2F-E2-E5-7D-46-45-1E-FE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "BE-A7-E3-09-E9-B9-DD-45" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Mono -> FLAC" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC" },
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "DB-7B-58-FC-11-F1-A4-8B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "64-EF-2F-36-1C-D6-C5-A4", // FLAC 1.4.3 (Ubuntu 24.04)
                    "4E-A1-65-5B-D2-F8-57-F6" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 48000Hz Stereo -> FLAC" },
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "FF-B4-0D-B9-F9-9A-7B-79", // FLAC 1.3.3 (Ubuntu 22.04)
                    "DB-91-51-55-39-C4-DE-F2", // FLAC 1.4.3 (Ubuntu 24.04)
                    "EF-36-3E-CE-6B-73-D7-A4" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 24-bit 96000Hz Stereo -> FLAC" },
            new(
                "A-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "6A-19-19-4A-E3-71-E3-95", // FLAC 1.3.3 (Ubuntu 22.04)
                    "30-56-D7-7A-03-CA-9E-06", // FLAC 1.4.3 (Ubuntu 24.04)
                    "A2-42-13-03-B7-3E-7B-D5" // FLAC 1.5.0
                ]
            ) { Label = "Wave A-law 44100Hz Stereo -> FLAC" },
            new(
                "µ-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "73-AE-57-E4-9C-B6-54-B5", // FLAC 1.3.3 (Ubuntu 22.04)
                    "41-DE-C9-E0-B9-DB-E8-8C", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8F-D9-39-8A-97-1D-98-49" // FLAC 1.5.0
                ]
            ) { Label = "Wave µ-law 44100Hz Stereo -> FLAC" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                [],
                [
                    "92-78-07-20-32-FB-A4-A1", // FLAC 1.3.3 (Ubuntu 22.04)
                    "67-73-BF-B8-6F-36-2E-7E", // FLAC 1.4.3 (Ubuntu 24.04)
                    "0B-39-56-AB-84-D6-9E-95" // FLAC 1.5.0
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> FLAC" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                [],
                [
                    "18-8A-91-25-28-9C-20-CC", // FLAC 1.3.3 (Ubuntu 22.04)
                    "8F-C3-6F-2A-AF-46-EC-61", // FLAC 1.4.3  (Ubuntu 24.04)
                    "3C-6F-52-00-B7-B9-BF-B6" // FLAC 1.5.0
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> FLAC" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                [],
                [
                    "74-8F-22-E4-6B-97-8F-D7", // FLAC 1.3.3 (Ubuntu 22.04)
                    "0E-6D-7B-32-64-8F-AC-18", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8C-1F-BF-4E-9B-32-A5-F0" // FLAC 1.5.0
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> FLAC" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["CompressionLevel"] = 5
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, default CompressionLevel (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["CompressionLevel"] = 0
                },
                [
                    "99-98-53-36-8F-15-6C-4C", // FLAC 1.3.3 (Ubuntu 22.04)
                    "EC-17-4A-C8-C4-8F-D3-AE", // FLAC 1.4.3 (Ubuntu 24.04)
                    "BC-D8-DE-03-33-9D-45-7D" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, minimum CompressionLevel" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["CompressionLevel"] = 8
                },
                [
                    "95-D2-FF-25-7C-74-1B-86", // FLAC 1.3.3 (Ubuntu 22.04)
                    "09-E9-A5-AF-D7-08-4A-68", // FLAC 1.4.3 (Ubuntu 24.04)
                    "DF-8E-EF-4F-3C-F9-78-93" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, maximum CompressionLevel" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["SeekPointInterval"] = 10
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, default SeekPointInterval (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["SeekPointInterval"] = 0
                },
                [
                    "60-2C-A4-D8-C2-61-4B-A5", // FLAC 1.3.3 (Ubuntu 22.04)
                    "82-4F-D8-9B-2F-A2-43-00", // FLAC 1.4.3 (Ubuntu 24.04)
                    "03-BC-0C-AC-77-14-86-A9" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, disabled SeekPointInterval" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["SeekPointInterval"] = 600
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, maximum SeekPointInterval" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["Padding"] = 8192
                },
                [
                    "C2-FB-C1-A8-1E-4A-FF-3B", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A2-10-C8-F2-50-2F-8C-2D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, default Padding (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["Padding"] = 0
                },
                [
                    "18-53-4C-9C-DB-14-4D-0A", // FLAC 1.3.3 (Ubuntu 22.04)
                    "CA-71-17-17-4F-3B-49-06", // FLAC 1.4.3 (Ubuntu 24.04)
                    "03-5D-35-08-D6-4F-54-FB" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, disabled Padding" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    ["Padding"] = 16_775_369
                },
                [
                    "72-95-92-9B-D3-4A-D0-05", // FLAC 1.3.3 (Ubuntu 22.04)
                    "7A-6C-C4-81-BA-09-50-31", // FLAC 1.4.3 (Ubuntu 24.04)
                    "3D-30-B1-95-DC-0E-94-A2" // FLAC 1.5.0
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> FLAC, maximum Padding" },

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
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Mono -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "Wave LPCM 16-bit 48000Hz Stereo -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "Wave LPCM 24-bit 96000Hz Stereo -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "Wave A-law 44100Hz Stereo -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "Wave µ-law 44100Hz Stereo -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> ALAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2016, 12, 1),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "36-B5-0D-C9-C0-17-17-AC", // MacOS
                    "DD-2D-DA-72-AD-83-C0-85" // Windows
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC, different creation time",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2018, 12, 1)
                },
                [
                    "96-47-BC-9B-0D-90-2A-EE", // MacOS
                    "05-BF-8D-9E-F9-5C-39-AB" // Windows
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC, different modification time",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["Padding"] = 2048,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "2C-7A-B7-77-0A-C0-5F-16", // MacOS
                    "41-B9-DB-52-F3-2C-86-E7" // Windows
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC, default Padding (explicit)",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["Padding"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "F9-9A-95-42-87-19-C4-28", // MacOS
                    "71-0D-61-58-29-75-1A-2F" // Windows
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC, disabled Padding",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["Padding"] = 16_777_216,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D4-E8-06-8C-58-82-15-20", // MacOS
                    "5C-99-2E-53-F1-89-28-F8" // Windows
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> ALAC, maximum Padding",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },

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
                    "D4-CF-89-9B-1D-04-8F-12", // 32-bit Windows on Core i5-8250U
                    "AA-14-94-9A-39-F1-26-3D", // 32-bit Windows on Ryzen 5600X
                    "38-BF-6E-85-B0-B2-A3-30", // 64-bit Windows on Core i5-8250U
                    "35-3E-A8-A6-1A-E4-37-4D", // 64-bit Windows on Ryzen 5600X
                    "D0-C1-81-B9-A5-D6-F1-31" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Mono -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "10-D3-D6-A6-48-57-42-86", // 32-bit Windows on Intel Corfe
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on Ryzen 5600X
                    "AE-1A-BB-51-7E-C2-C9-65", // 64-bit Windows on Core i5-8250U
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "2A-0F-E6-31-F9-83-7E-8C", // 32-bit Windows on Core i5-8250U
                    "3D-90-DA-92-BC-41-4B-5A", // 32-bit Windows on Ryzen 5600X
                    "96-0D-1B-19-13-2C-30-72", // 64-bit Windows on Core i5-8250U
                    "E3-95-7F-57-F1-93-A9-14", // 64-bit Windows on Ryzen 5600X
                    "A3-EE-43-38-A8-9C-83-91" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 48000Hz Stereo -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "F0-B5-16-C6-48-0C-10-85", // 32-bit Windows on Core i5-8250U
                    "53-18-0D-CD-CE-FF-8E-D9", // 32-bit Windows on Ryzen 5600X
                    "29-E8-89-F9-7F-44-90-C5", // 64-bit Windows on Core i5-8250U
                    "85-67-33-D1-41-55-69-38", // 64-bit Windows on Ryzen 5600X
                    "42-1F-5C-AE-32-8B-4B-1E" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 24-bit 96000Hz Stereo -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "F0-D3-61-79-CF-A4-FF-10", // 32-bit Windows on Core i5-8250U
                    "65-64-CD-70-4F-77-11-84", // 64-bit Windows on Core i5-8250U
                    "E9-41-0A-C8-25-11-86-F7", // Windows on Ryzen 5600X
                    "C6-7A-19-E1-7A-97-5F-2D" // Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave A-law 44100Hz Stereo -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "D3-B5-EC-5A-0E-01-8F-FD", // 32-bit Windows on Core i5-8250U
                    "CA-F9-FB-5A-AD-F2-FF-C2", // 32-bit Windows on Ryzen 5600X
                    "2E-39-5B-BB-9C-20-3D-63", // 64-bit Windows on Core i5-8250U
                    "72-0B-E6-82-18-51-F2-4C", // 64-bit Windows on Ryzen 5600X
                    "53-F4-E9-DC-D6-BB-AF-D4" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave µ-law 44100Hz Stereo -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "D5-D6-84-36-74-B8-A5-7C", // 32-bit Windows on Core i5-8250U
                    "54-D1-D2-6B-FF-E6-65-38", // 32-bit Windows on Ryzen 5600X
                    "37-4A-AE-00-A5-76-64-47", // 64-bit Windows on Core i5-8250U
                    "90-99-EF-1F-75-05-BB-1B", // 64-bit Windows on Ryzen 5600X
                    "F5-71-CE-E7-3A-0D-1F-92" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "4E-5D-01-50-57-AD-B6-66", // 32-bit Windows on Core i5-8250U
                    "38-C0-2C-7D-67-59-C1-96", // 32-bit Windows on Ryzen 5600X
                    "E5-B4-BF-16-F0-AB-14-4F", // 64-bit Windows on Core i5-8250U
                    "6B-B4-0A-03-39-1C-F0-DF", // 64-bit Windows on Ryzen 5600X
                    "59-5F-A0-8F-29-95-A0-7F" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
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
                    "D8-94-9F-67-F5-F0-C2-B7", // 32-bit Windows on Core i5-8250U
                    "CA-20-41-98-E4-02-3E-0C", // 32-bit Windows on Ryzen 5600X
                    "29-0F-5F-3E-A2-94-6B-52", // 64-bit Windows on Core i5-8250U
                    "CC-E3-27-EB-55-D5-11-6B", // 64-bit Windows on Ryzen 5600X
                    "AD-38-A6-16-DA-76-0F-C6" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> AppleAAC",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["VBRQuality"] = 9,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D8-F1-C9-8F-AA-70-66-A1", // MacOS on Intel
                    "9A-43-DD-A6-8B-BA-9B-40", // MacOS on ARM
                    "10-D3-D6-A6-48-57-42-86", // 32-bit Windows on Core i5-8250U
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on Ryzen 5600X
                    "AE-1A-BB-51-7E-C2-C9-65", // 64-bit Windows on Core i5-8250U
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, default VBRQuality (explicit)",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["VBRQuality"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "33-E2-A2-75-A5-22-0E-BA", // MacOS on Intel
                    "55-44-84-FA-BE-5A-16-28", // MacOS on ARM
                    "C4-EC-1F-47-E1-4C-19-58", // Windows on Core i5-8250U
                    "03-CE-28-10-F4-67-A7-B7", // Windows on Ryzen 5600X
                    "38-18-6E-0B-2B-E2-C2-4A" // Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, minimum VBRQuality",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["VBRQuality"] = 14,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E6-E2-90-70-75-10-CC-18", // MacOS on Intel
                    "62-51-EB-63-F8-7F-E5-60", // MacOS on ARM
                    "A4-C9-89-D2-C2-D3-30-E5", // 32-bit Windows on Core i5-8250U
                    "9D-FD-71-7F-11-4C-7C-D7", // 32-bit Windows on Ryzen 5600X
                    "4D-2A-95-7A-94-93-97-9F", // 64-bit Windows on Core i5-8250U
                    "C5-49-22-F7-D8-22-EA-64", // 64-bit Windows on Ryzen 5600X
                    "41-D2-1B-97-BC-22-00-E4" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, maximum VBRQuality",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "3A-2C-E0-BB-C8-3C-2A-F8", // MacOS on Intel
                    "1F-C0-6A-65-96-20-03-09", // MacOS on ARM
                    "A3-7A-F0-F6-CC-12-A4-F4", // 32-bit Windows on Core i5-8250U
                    "7A-92-5E-0F-FF-CE-D0-FC", // 32-bit Windows on Ryzen 5600X
                    "E1-49-CF-69-80-87-CD-33", // 64-bit Windows on Core i5-8250U
                    "FA-0F-C7-19-0F-26-BB-AA", // 64-bit Windows on Ryzen 5600X
                    "B8-42-9B-41-89-06-AC-B6" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, minimum BitRate",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A7-0C-58-93-A1-D3-56-04", // MacOS
                    "04-50-78-A0-C1-3F-32-A7", // Windows on AMD
                    "DA-1C-61-0E-5A-64-52-98" // Windows on Intel
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Mono -> AppleAAC, minimum BitRate",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A3-1E-42-BB-E4-C7-B1-90", // MacOS on Intel
                    "40-B8-14-8A-F0-D3-D4-64", // MacOS on ARM
                    "8A-DA-BF-59-EC-A4-76-9E", // 32-bit Windows on Core i5-8250U
                    "E9-3C-D8-9E-D6-48-4E-7D", // 32-bit Windows on Ryzen 5600X
                    "42-B1-6E-BB-04-D7-9D-64", // 64-bit Windows on Core i5-8250U
                    "66-48-1D-33-22-43-53-FF", // 64-bit Windows on Ryzen 5600X
                    "F1-A4-9F-B8-8E-59-B0-BF" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, maximum BitRate",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E7-E6-F3-09-69-F4-0B-C9", // MacOS on Intel
                    "58-CB-13-FF-D5-D1-18-EB", // MacOS on ARM
                    "18-42-3B-F8-27-DC-C7-FC", // 32-bit Windows on Core i5-8250U
                    "97-CE-44-9B-71-22-CC-C9", // 32-bit Windows on Ryzen 5600X
                    "EA-C4-69-24-DF-5C-B8-B3", // 64-bit Windows on Core i5-8250U
                    "FF-B3-B1-3F-D0-D9-26-A1", // 64-bit Windows on Ryzen 5600X
                    "79-05-CC-7A-EE-CF-1A-E6" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Mono -> AppleAAC, maximum BitRate",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "xE0-91-F3-FD-0E-3C-F5-1A", // MacOS on Intel
                    "x2C-F5-B4-05-2D-2C-FB-DA", // MacOS on ARM
                    "0E-9B-8F-80-52-E2-49-82", // 32-bit Windows
                    "xB5-95-50-67-B9-AE-AB-4D", // 64-bit Windows on Core i5-8250U
                    "04-56-0A-06-D5-AA-3B-90", // 64-bit Windows on Ryzen 5600X
                    "59-01-C4-A2-76-6C-3A-E0" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, ControlMode = Constrained (default)",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["ControlMode"] = "Constrained",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "xE0-91-F3-FD-0E-3C-F5-1A", // MacOS on Intel
                    "x2C-F5-B4-05-2D-2C-FB-DA", // MacOS on ARM
                    "0E-9B-8F-80-52-E2-49-82", // 32-bit Windows
                    "xB5-95-50-67-B9-AE-AB-4D", // 64-bit Windows on Core i5-8250U
                    "04-56-0A-06-D5-AA-3B-90", // 64-bit Windows on Ryzen 5600X
                    "59-01-C4-A2-76-6C-3A-E0" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, ControlMode = Constrained (explicit)",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["ControlMode"] = "Average",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "x95-A5-76-FE-A3-32-33-72", // MacOS on Intel
                    "x55-83-14-EE-1E-DE-D7-9B", // MacOS on ARM
                    "0E-9B-8F-80-52-E2-49-82", // 32-bit Windows
                    "x9F-7D-0C-AF-47-8B-65-2C", // 64-bit Windows on Core i5-8250U
                    "04-56-0A-06-D5-AA-3B-90", // 64-bit Windows on Ryzen 5600X
                    "59-01-C4-A2-76-6C-3A-E0" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, ControlMode = Average",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["ControlMode"] = "Constant",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "x61-11-48-3A-D6-7F-C3-28", // MacOS on Intel
                    "xA3-32-D7-07-D1-E5-55-80", // MacOS on ARM
                    "x9C-FF-C2-AE-1F-C3-F1-66", // 32-bit Windows on Core i5-8250U
                    "93-E9-95-1D-EF-48-81-DB", // 32-bit Windows on Ryzen 5600X
                    "x2A-87-45-4D-37-34-AF-94", // 64-bit Windows on Core i5-8250U
                    "29-13-DA-5D-8C-06-18-5A", // 64-bit Windows on Ryzen 5600X
                    "0F-8C-2A-CD-06-94-4E-8C" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, ControlMode = Constant",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D8-F1-C9-8F-AA-70-66-A1", // MacOS on Intel
                    "9A-43-DD-A6-8B-BA-9B-40", // MacOS on ARM
                    "10-D3-D6-A6-48-57-42-86", // 32-bit Windows on Core i5-8250U
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on Ryzen 5600X
                    "AE-1A-BB-51-7E-C2-C9-65", // 64-bit Windows on Core i5-8250U
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "BD-E6-79-C8-B1-B3-3E-C1" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "Wave LPCM 16-bit 44100Hz Stereo -> AppleAAC, ApplyGain does nothing without metadata",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "F7-57-90-77-62-D5-F6-4B", // MacOS on Intel
                    "A6-88-93-15-5B-B8-3B-CA", // MacOS on ARM
                    "68-DC-E3-82-6E-DD-B1-5D", // 32-bit Windows on Core i5-8250U
                    "F2-7E-DD-D0-8F-A4-88-B5", // 32-bit Windows on Ryzen 5600X
                    "C7-AD-7C-A3-D4-AE-38-00", // 64-bit Windows on Core i5-8250U
                    "C9-58-58-74-36-AD-27-F5", // 64-bit Windows on Ryzen 5600X
                    "03-6D-BD-AB-2C-26-82-33" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> AppleAAC, ApplyGain = Track",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    ["ApplyGain"] = "Album",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "56-91-5D-62-00-A0-10-10", // MacOS on Intel
                    "E1-76-F5-FC-7C-A7-5C-F6", // MacOS on ARM
                    "CB-AE-DC-10-C4-26-16-80", // 32-bit Windows on Core i5-8250U
                    "6A-C8-FA-AB-6D-4D-B2-A5", // 32-bit Windows on Ryzen 5600X
                    "C2-65-EC-3C-8E-36-4E-81", // 64-bit Windows on Core i5-8250U
                    "34-90-BE-A7-5B-15-FA-72", // 64-bit Windows on Ryzen 5600X
                    "BD-40-59-07-7A-39-ED-D4" // 64-bit Windows on EPYC 7763
                ]
            )
            {
                Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> AppleAAC, ApplyGain = Album",
                Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null
            },

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
            ) { Label = "Wave LPCM 8-bit 8000Hz Stereo -> LameMP3" },
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                [],
                [
                    "F0-94-E7-B4-66-43-B4-06", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "83-96-E5-7B-DD-FC-97-CB", // Lame 3.100 (MacOS on ARM)
                    "65-6E-9C-0F-7A-81-D4-3C" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Mono -> LameMP3" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3" },
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "4B-FE-37-D5-81-89-22-16", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "6B-B6-75-59-B9-FB-5E-65", // Lame 3.100 (MacOS on ARM)
                    "2E-BE-F3-A0-4B-CB-79-AC" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 48000Hz Stereo -> LameMP3" },
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "E9-EC-15-D8-B2-E0-91-6D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "CC-15-84-E1-5B-39-3F-DD", // Lame 3.100 (MacOS on ARM)
                    "5B-80-48-5D-A6-B1-22-5C" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 24-bit 96000Hz Stereo -> LameMP3" },
            new(
                "A-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "1F-2A-23-34-9D-2F-6E-0D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "5E-46-C2-35-69-2C-4D-2F", // Lame 3.100 (MacOS on ARM)
                    "BF-DA-07-84-22-66-32-BB" // Lame 3.100 (Windows)
                ]
            ) { Label = "A-law 44100Hz Stereo -> LameMP3" },
            new(
                "µ-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "24-34-8E-86-D4-DB-9B-7F", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "4A-C1-76-E4-25-DC-40-9E", // Lame 3.100 (MacOS on ARM)
                    "1A-B5-6B-E4-A7-65-B1-D3" // Lame 3.100 (Windows)
                ]
            ) { Label = "µ-law 44100Hz Stereo -> LameMP3" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                [],
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                [],
                [
                    "3A-A5-7A-01-8E-DB-0B-13", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "B3-B3-BC-FE-D9-5C-A7-A8", // Lame 3.100 (MacOS on ARM)
                    "34-36-E0-A3-31-49-3F-8D" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> LameMP3" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                [],
                [
                    "C1-29-F7-C0-DA-BD-9B-02", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "64-87-AF-6B-AA-5F-E9-DC", // Lame 3.100 (MacOS on ARM)
                    "02-18-09-07-B4-3B-B1-72" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> LameMP3" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagVersion"] = "2.3"
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, TagVersion 2.3 (explicit)"
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagVersion"] = "2.4"
                },
                [
                    "9F-EE-55-1E-12-70-63-C5", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-25-31-8B-E5-67-7D-CC", // Lame 3.100 (MacOS on ARM)
                    "71-E8-2E-C0-61-10-47-69" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, TagVersion 2.4" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagEncoding"] = "Latin1"
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, Latin1 TagEncoding (explicit)"
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "25-E9-A0-CB-0D-3E-50-CD", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "40-53-53-F6-0E-DD-67-77", // Lame 3.100 (MacOS on ARM)
                    "95-4D-90-F8-08-38-CD-39" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, UTF16 TagEncoding" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, UTF8 TagEncoding" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagVersion"] = "2.4",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, UTF8 TagEncoding, TagVersion 2.4 (explicit)"
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagVersion"] = "2.3",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "0A-9C-A5-40-8B-EC-BE-27", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "79-D1-5B-F7-68-CC-4E-81", // Lame 3.100 (MacOS on ARM)
                    "FF-64-0F-49-D8-DF-D9-90" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, UTF8 TagEncoding, TagVersion 2.3 (ignored)"
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagPadding"] = 2048
                },
                [
                    "AF-7E-18-DB-33-2A-9E-34", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8A-ED-2A-9F-8E-23-89-1B", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, default TagPadding (explicit)"
            },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagPadding"] = 0
                },
                [
                    "70-23-C1-4C-76-D0-97-9B", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E7-2C-0C-BF-B2-B0-FB-52", // Lame 3.100 (MacOS on ARM)
                    "71-CA-62-E2-A8-06-F5-31" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, disabled TagPadding" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["TagPadding"] = 16_777_216
                },
                [
                    "5A-0D-3B-82-4C-5C-E6-E0", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "C0-5E-50-42-3D-CB-C4-50", // Lame 3.100 (MacOS on ARM)
                    "F6-D8-85-E6-25-FC-3B-B4" // Lame 3.100 (Windows)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, maximum TagPadding" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["TagVersion"] = "2.4"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, TagVersion does nothing without metadata"
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, TagEncoding does nothing without metadata"
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["TagPadding"] = 100
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            )
            {
                Label =
                    "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, TagPadding does nothing without metadata"
            },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["VBRQuality"] = 3
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, default VBRQuality (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["VBRQuality"] = 9
                },
                [
                    "B1-18-EF-8F-81-1A-BE-1A", // Lame 3.100 (Ubuntu and MacOS)
                    "B2-C7-B0-17-E1-AC-49-20" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, minimum VBRQuality" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["VBRQuality"] = 0
                },
                [
                    "F3-76-6B-5D-4C-71-B6-D6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "51-C7-76-DB-2F-47-96-1B", // Lame 3.100 (MacOS on ARM)
                    "0D-14-25-F0-35-BD-AD-EB" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, maximum VBRQuality" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["BitRate"] = 8
                },
                [
                    "CA-25-80-FB-9E-FB-4A-30", // Lame 3.100 (Ubuntu and MacOS)
                    "4D-7E-24-C8-7F-5D-16-2E" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, minimum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["BitRate"] = 320
                },
                [
                    "4F-27-D2-D4-C5-C9-CA-DC", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "06-18-B3-99-8D-D6-09-02", // Lame 3.100 (MacOS on ARM)
                    "54-5F-2C-AC-90-8A-7A-E3" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, maximum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["BitRate"] = 128
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, ForceCBR disabled (default)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, ForceCBR disabled (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true
                },
                [
                    "53-DC-C3-6F-0D-BC-D3-C6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E9-D0-7D-BD-09-41-F1-9B", // Lame 3.100 (MacOS on ARM)
                    "07-D4-A6-00-D7-DB-01-05" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, ForceCBR enabled" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["ForceCBR"] = true
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, ForceCBR ignored without BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["VBRQuality"] = 3,
                    ["BitRate"] = 128
                },
                [
                    "83-8F-85-5D-04-D7-15-D1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "81-5E-D1-F6-44-F4-D5-17", // Lame 3.100 (MacOS on ARM)
                    "6B-C4-BB-00-CF-5F-DB-D5" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, VBRQuality ignored with BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    ["ApplyGain"] = "Track"
                },
                [
                    "6B-87-06-6A-2F-A0-AC-1E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "63-6C-40-8F-48-8F-C0-5E", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> LameMP3, ApplyGain does nothing without metadata" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["ApplyGain"] = "Track"
                },
                [
                    "66-7D-6B-28-F3-54-BF-A3", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "53-C5-E7-DE-6C-0B-4F-8C", // Lame 3.100 (MacOS on ARM)
                    "00-FC-27-73-8D-4F-82-C2" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, ApplyGain = Track" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    ["ApplyGain"] = "Album"
                },
                [
                    "E8-43-CC-9F-92-D5-16-13", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E5-68-30-CA-53-75-88-22", // Lame 3.100 (MacOS on ARM)
                    "83-45-FA-75-27-7B-96-FB" // Lame 3.100 (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo (Tagged using defaults) -> LameMP3, ApplyGain = Album" },

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
            ) { Label = "Wave LPCM 8-bit 8000Hz Stereo -> Vorbis" },
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
                    "D2-7F-37-41-82-B4-2A-12", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "92-B0-23-16-4F-17-F1-40" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Mono -> Vorbis" },
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
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis" },
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
            ) { Label = "Wave LPCM 16-bit 48000Hz Stereo -> Vorbis" },
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
            ) { Label = "Wave LPCM 24-bit 96000Hz Stereo -> Vorbis" },
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
            ) { Label = "Wave A-law 44100Hz Stereo -> Vorbis" },
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
                    "57-F8-A1-6C-68-28-89-1C", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "A6-8D-CF-02-3D-84-57-BB" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave µ-law 44100Hz Stereo -> Vorbis" },
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
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> Vorbis" },
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
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> Vorbis" },
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
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> Vorbis" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "D3-40-F7-C6-41-76-16-8A", // Vorbis 1.3.7 (Ubuntu)
                    "29-DA-6A-15-5A-FC-E1-0D", // Vorbis 1.3.7 (MacOS on Intel)
                    "AC-10-8B-AC-74-D0-DD-08", // Vorbis 1.3.7 (MacOS on ARM)
                    "15-D0-92-19-0E-C4-38-6A" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, minimum SerialNumber" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "42-B5-34-C2-8E-C7-A6-03", // Vorbis 1.3.7 (Ubuntu)
                    "C6-F4-D3-E7-70-F5-77-20", // Vorbis 1.3.7 (MacOS on Intel)
                    "F8-27-96-4D-02-16-3E-55", // Vorbis 1.3.7 (MacOS on ARM)
                    "85-F5-33-EC-75-EA-3F-25" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, maximum SerialNumber" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, default Quality (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["Quality"] = -1,
                    ["SerialNumber"] = 1
                },
                [
                    "98-C4-E1-84-EB-7F-61-52", // Vorbis 1.3.7 (Ubuntu)
                    "82-CC-2B-EB-43-50-B4-50", // Vorbis 1.3.7 (MacOS on Intel)
                    "60-53-00-7A-F0-4F-56-FA", // Vorbis 1.3.7 (MacOS on ARM)
                    "DA-CE-8D-8B-C9-67-7B-E5", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "2F-2B-AD-1A-25-C4-6F-00" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)

                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, minimum Quality" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["Quality"] = 10,
                    ["SerialNumber"] = 1
                },
                [
                    "ED-B2-EF-24-F7-3E-80-70", // Vorbis 1.3.7 (Ubuntu)
                    "2A-B3-88-46-0B-F7-A0-D7", // Vorbis 1.3.7 (MacOS on Intel)
                    "86-30-CA-4A-69-45-26-D4", // Vorbis 1.3.7 (MacOS on ARM)
                    "8B-54-C9-0F-99-F5-02-3C", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "DC-0B-7C-C4-AD-53-C8-28" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, maximum Quality" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["BitRate"] = 45,
                    ["SerialNumber"] = 1
                },
                [
                    "74-4A-E3-12-85-C8-A5-55", // Vorbis 1.3.7 (Ubuntu)
                    "86-F9-8A-70-29-95-C5-9F", // Vorbis 1.3.7 (MacOS on Intel)
                    "3A-D1-B3-90-8D-AB-7D-BF", // Vorbis 1.3.7 (MacOS on ARM)
                    "0E-70-66-99-F2-6D-6C-78", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EB-D8-C7-67-24-BC-50-20" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, minimum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["BitRate"] = 500,
                    ["SerialNumber"] = 1
                },
                [
                    "F2-C3-FA-E0-D4-6F-B3-BA", // Vorbis 1.3.7 (Ubuntu)
                    "96-90-94-99-0F-97-E8-4E", // Vorbis 1.3.7 (MacOS on Intel)
                    "EC-52-72-4C-50-34-39-60", // Vorbis 1.3.7 (MacOS on ARM)
                    "C4-38-28-B2-C7-E8-65-16", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "60-4A-A5-B9-DE-35-98-83" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, maximum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "BF-A5-41-45-7F-AC-8A-0F", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, ForceCBR disabled (default)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "BF-A5-41-45-7F-AC-8A-0F", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, ForceCBR disabled (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "F5-8B-06-6A-B5-F0-6C-7E", // Vorbis 1.3.7 (Ubuntu)
                    "D9-5F-7B-E5-A4-F0-4F-B8", // Vorbis 1.3.7 (MacOS on Intel)
                    "15-AF-C8-95-A6-E5-F5-DA", // Vorbis 1.3.7 (MacOS on ARM)
                    "02-C2-C2-5C-9C-7A-13-3F", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "16-02-3E-BF-B2-20-34-8D" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, ForceCBR enabled" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, ForceCBR ignored without BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["Quality"] = 3,
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "2F-59-3E-E4-4B-A5-00-0F", // Vorbis 1.3.7 (Ubuntu)
                    "FE-A6-A0-AD-25-70-6B-6E", // Vorbis 1.3.7 (MacOS on Intel)
                    "7D-FD-0C-59-C3-EF-45-CF", // Vorbis 1.3.7 (MacOS on ARM)
                    "BF-A5-41-45-7F-AC-8A-0F", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "4F-3E-91-28-11-3A-3D-38" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, Quality ignored with BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "0E-5B-29-B4-5E-EE-98-8C", // Vorbis 1.3.7 (Ubuntu)
                    "01-83-C8-FB-B1-4A-A2-9B", // Vorbis 1.3.7 (MacOS on Intel)
                    "78-84-64-83-93-9B-C5-30", // Vorbis 1.3.7 (MacOS on ARM)
                    "41-4D-DF-C7-B7-D4-6F-A8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Vorbis, ApplyGain ignored without metadata" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "06-EF-8A-60-69-69-5C-F2", // Vorbis 1.3.7 (Ubuntu)
                    "3B-D8-37-9D-10-CD-E4-7A", // Vorbis 1.3.7 (MacOS on Intel)
                    "A2-CD-A0-5C-F9-05-2C-E0", // Vorbis 1.3.7 (MacOS on ARM)
                    "6D-80-D4-C5-3B-4C-BF-93", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "57-A6-44-D0-49-38-29-3E" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo (Tagged using defaults) -> Vorbis, ApplyGain = Track" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "15-92-D2-A9-0B-46-46-96", // Vorbis 1.3.7 (Ubuntu)
                    "F6-CB-E3-5A-B6-83-BC-01", // Vorbis 1.3.7 (MacOS on Intel)
                    "4C-AF-38-66-16-C1-C2-1D", // Vorbis 1.3.7 (MacOS on ARM)
                    "63-A1-55-E1-3E-1A-94-8A", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "7F-37-C9-AA-CF-F8-0F-B7" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo (Tagged using defaults) -> Vorbis, ApplyGain = Album" },

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
                    "36-30-13-0C-81-0E-07-1D", // Opus 1.5.2 (32-bit Windows on Intel)
                    "24-0A-93-E2-B2-A4-59-F2", // Opus 1.5.2 (32-bit Windows on AMD)
                    "54-FE-C5-54-0B-3E-3D-BE", // Opus 1.5.2 (64-bit Windows on Intel)
                    "92-71-61-35-21-92-BB-2D" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 8-bit 8000Hz Stereo -> Opus" },
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
                    "86-86-C6-06-43-E5-00-A0", // Opus 1.5.2 (Windows on Intel)
                    "1B-DD-B3-B2-65-76-86-88" // Opus 1.5.2 (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Mono -> Opus" },
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
                    "D2-9C-39-DD-B7-DD-73-CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "66-AA-01-F3-F6-9B-CF-5A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus" },
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
                    "EE-4B-1B-C7-81-A8-05-5D", // Opus 1.5.2 (32-bit Windows on Intel)
                    "53-DF-9C-FA-95-2E-A0-0B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "7B-4D-4D-02-69-1C-80-28", // Opus 1.5.2 (64-bit Windows on Intel)
                    "7F-F2-47-47-96-95-17-44" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 48000Hz Stereo -> Opus" },
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
                    "20-05-EA-EF-96-02-65-36", // Opus 1.5.2 (32-bit Windows on Intel)
                    "4D-8D-D5-E5-C9-1D-77-1E", // Opus 1.5.2 (32-bit Windows on AMD)
                    "5A-8C-89-BB-89-0E-04-AB", // Opus 1.5.2 (64-bit Windows on Intel)
                    "0F-34-1A-5D-FB-3A-A9-67" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 24-bit 96000Hz Stereo -> Opus" },
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
                    "20-5F-FB-86-4A-00-53-14", // Opus 1.5.2 (32-bit Windows on Intel)
                    "56-29-04-78-6D-AD-0C-B7", // Opus 1.5.2 (32-bit Windows on AMD)
                    "C7-2B-C9-88-7C-16-2D-B3", // Opus 1.5.2 (64-bit Windows on Intel)
                    "5B-B0-B3-74-2D-6B-0F-51" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave A-law 44100Hz Stereo -> Opus" },
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
                    "F0-35-94-CE-0B-A3-96-68", // Opus 1.5.2 (32-bit Windows on Intel)
                    "3C-99-48-CC-0E-F3-EE-42", // Opus 1.5.2 (32-bit Windows on AMD)
                    "7D-38-EF-3F-A1-D9-71-57", // Opus 1.5.2 (64-bit Windows on Intel)
                    "93-3D-6E-04-C2-68-E7-D9" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave µ-law 44100Hz Stereo -> Opus" },
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
                    "AA-0F-70-1F-AF-D1-EB-A8", // Opus 1.5.2 (32-bit Windows on Intel)
                    "73-80-4E-A5-13-A2-0A-6D", // Opus 1.5.2 (32-bit Windows on AMD)
                    "A4-46-E9-FC-BB-11-5E-0B", // Opus 1.5.2 (64-bit Windows on Intel)
                    "AD-63-20-AC-EA-79-E9-66" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> Opus" },
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
                    "0E-6B-53-F3-88-66-FD-CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "A5-23-52-3D-A8-3E-C9-B2", // Opus 1.5.2 (32-bit Windows on AMD)
                    "AF-D0-AB-1C-DE-1B-DA-43", // Opus 1.5.2 (64-bit Windows on Intel)
                    "BF-D3-36-61-1B-61-CB-86" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG) -> Opus" },
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
                    "43-69-FE-44-39-9B-14-F4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "E8-E6-55-C8-C1-77-47-2C", // Opus 1.5.2 (32-bit Windows on AMD)
                    "62-6D-44-F8-25-C2-05-6A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "64-BC-46-7F-DA-27-9E-B4" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG) -> Opus" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "06-66-0A-F6-AF-07-4C-2B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "7D-A8-9F-03-65-EC-2E-C8", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "65-D7-87-94-A9-E9-E7-BD", // Opus 1.5.2 (MacOS on Intel)
                    "64-24-6F-D3-84-A1-EF-1C", // Opus 1.5.2 (MacOS on ARM)
                    "7E-A5-51-62-0F-9D-45-F0", // Opus 1.5.2 (32-bit Windows on Intel)
                    "57-C3-D6-08-B5-11-78-A6", // Opus 1.5.2 (32-bit Windows on AMD)
                    "35-30-F5-C7-42-7A-A7-80", // Opus 1.5.2 (64-bit Windows on Intel)
                    "CD-84-6F-09-08-78-6A-9B" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, minimum SerialNumber" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "51-FF-C3-03-D8-B2-98-39", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "5E-77-C4-DD-72-BA-FE-D5", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "E9-3C-9C-89-CE-C6-DD-71", // Opus 1.5.2 (MacOS on Intel)
                    "04-73-D9-F3-C4-63-83-E6", // Opus 1.5.2 (MacOS on ARM)
                    "17-30-30-3F-D0-85-3D-07", // Opus 1.5.2 (32-bit Windows on Intel)
                    "0F-F5-62-B5-00-AF-0D-7D", // Opus 1.5.2 (32-bit Windows on AMD)
                    "7E-3C-D8-41-39-C3-5B-46", // Opus 1.5.2 (64-bit Windows on Intel)
                    "10-3E-23-D0-2C-F3-05-52" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, maximum SerialNumber" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
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
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, minimum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["BitRate"] = 512,
                    ["SerialNumber"] = 1
                },
                [
                    "C0-80-59-CC-D2-7D-08-AA", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "50-15-21-02-0E-28-68-F6", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "D2-74-4E-6F-86-57-ED-B0", // Opus 1.5.2 (MacOS on Intel)
                    "1F-4A-FC-4B-5A-89-AF-64", // Opus 1.5.2 (MacOS on ARM)
                    "C4-2A-4B-CD-42-71-E8-B2", // Opus 1.5.2 (32-bit Windows)
                    "52-99-1C-C0-51-C8-95-9F", // Opus 1.5.2 (64-bit Windows on Intel)
                    "C8-D1-8B-E2-72-87-B5-49" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, maximum BitRate" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["ControlMode"] = "Variable",
                    ["SerialNumber"] = 1
                },
                [
                    "13-77-D7-3E-62-41-3E-5A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6A-EE-F6-02-4C-E2-FE-83", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "26-CB-03-BB-31-D8-6F-79", // Opus 1.5.2 (MacOS on Intel)
                    "E4-11-3B-34-D9-6A-05-CF", // Opus 1.5.2 (MacOS on ARM)
                    "D2-9C-39-DD-B7-DD-73-CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "66-AA-01-F3-F6-9B-CF-5A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, default ControlMode (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["ControlMode"] = "Constrained",
                    ["SerialNumber"] = 1
                },
                [
                    "19-5D-E0-D8-D5-6D-E0-1E", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "48-31-E2-1D-B6-08-37-A9", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "3A-83-E3-04-6E-54-7C-B1", // Opus 1.5.2 (MacOS on Intel)
                    "A3-8E-11-D6-B9-DD-9C-C9", // Opus 1.5.2 (MacOS on ARM)
                    "79-99-13-0D-48-08-D8-AF", // Opus 1.5.2 (32-bit Windows on Intel)
                    "A9-B7-3F-59-FC-4E-8E-0A", // Opus 1.5.2 (32-bit Windows on AMD)
                    "26-52-72-E9-21-A0-E3-3E", // Opus 1.5.2 (64-bit Windows on Intel)
                    "F6-F8-7D-60-46-4A-C0-50" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, ControlMode = Constrained" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["ControlMode"] = "Constant",
                    ["SerialNumber"] = 1
                },
                [
                    "1D-F1-A9-CE-D8-8E-23-F0", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "D6-71-3B-9F-80-3B-DB-2C", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "C2-DB-76-2D-08-CA-C5-B1", // Opus 1.5.2 (MacOS on Intel)
                    "5C-82-04-7E-53-90-E0-07", // Opus 1.5.2 (MacOS on ARM)
                    "AE-8F-41-FF-20-B0-77-A4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "7F-40-F2-64-3F-CB-02-20", // Opus 1.5.2 (32-bit Windows on AMD)
                    "30-DE-FA-51-45-A6-B1-ED", // Opus 1.5.2 (64-bit Windows on Intel)
                    "02-4A-9A-CF-6F-2E-C3-61" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, ControlMode = Constant" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "FD-6D-78-EB-8E-7E-C8-AE", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B1-4C-57-71-E1-72-82-CC", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6D-4D-93-AD-68-72-DC-82", // Opus 1.5.2 (MacOS on Intel)
                    "78-4D-24-AF-8D-A2-62-5A", // Opus 1.5.2 (MacOS on ARM)
                    "06-16-7D-E1-FC-E6-F0-93", // Opus 1.5.2 (Windows on Intel)
                    "AB-47-63-8F-6B-5F-E6-F9" // Opus 1.5.2 (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, SignalType = Music (default)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SignalType"] = "Music",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "FD-6D-78-EB-8E-7E-C8-AE", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B1-4C-57-71-E1-72-82-CC", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6D-4D-93-AD-68-72-DC-82", // Opus 1.5.2 (MacOS on Intel)
                    "78-4D-24-AF-8D-A2-62-5A", // Opus 1.5.2 (MacOS on ARM)
                    "06-16-7D-E1-FC-E6-F0-93", // Opus 1.5.2 (Windows on Intel)
                    "AB-47-63-8F-6B-5F-E6-F9" // Opus 1.5.2 (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, SignalType = Music (explicit)" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SignalType"] = "Speech",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "7A-B8-FF-08-6E-DC-6C-2B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "07-CC-93-B7-3B-3C-F9-46", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "07-48-7A-BE-D6-C1-64-6B", // Opus 1.5.2 (MacOS on Intel)
                    "A2-1E-D0-33-0A-AE-1F-85", // Opus 1.5.2 (MacOS on ARM)
                    "AE-2F-2B-C4-36-74-67-2C", // Opus 1.5.2 (Windows on Intel)
                    "CB-F0-EB-C6-A6-5D-5F-79" // Opus 1.5.2 (Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, SignalType = Speech" },
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "13-77-D7-3E-62-41-3E-5A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "6A-EE-F6-02-4C-E2-FE-83", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "26-CB-03-BB-31-D8-6F-79", // Opus 1.5.2 (MacOS on Intel)
                    "E4-11-3B-34-D9-6A-05-CF", // Opus 1.5.2 (MacOS on ARM)
                    "D2-9C-39-DD-B7-DD-73-CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-08-5F-EE-DA-54-2F-A5", // Opus 1.5.2 (32-bit Windows on AMD)
                    "66-AA-01-F3-F6-9B-CF-5A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "1B-E2-44-92-2D-3C-B9-14" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "Wave LPCM 16-bit 44100Hz Stereo -> Opus, ApplyGain does nothing without metadata" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "C9-41-5E-A2-A8-67-FF-65", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "C1-01-89-41-A0-DD-C0-55", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "F2-39-7D-F4-8F-BF-AB-83", // Opus 1.5.2 (MacOS on Intel)
                    "A8-35-6E-0F-7B-E9-C2-13", // Opus 1.5.2 (MacOS on ARM)
                    "D0-F2-00-1F-8E-A8-32-E1", // Opus 1.5.2 (32-bit Windows on Intel)
                    "E0-9F-C7-27-01-4B-BA-0C", // Opus 1.5.2 (32-bit Windows on AMD)
                    "5B-CE-E5-F3-B0-51-C8-83", // Opus 1.5.2 (64-bit Windows on Intel)
                    "B6-6D-CD-F5-86-E8-3C-D1" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> Opus, ApplyGain = Track" },
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "DE-EE-83-C7-63-E3-B2-B1", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "3C-8F-1B-DD-22-D9-F0-D9", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "F1-BF-D8-0A-3F-2F-CD-85", // Opus 1.5.2 (MacOS on Intel)
                    "77-CB-F0-A8-49-89-A8-7F", // Opus 1.5.2 (MacOS on ARM)
                    "7D-2E-69-D2-1A-61-67-3E", // Opus 1.5.2 (32-bit Windows on Intel)
                    "27-97-34-77-2C-BE-0C-79", // Opus 1.5.2 (32-bit Windows on AMD)
                    "04-97-2A-30-86-F7-A9-A6", // Opus 1.5.2 (64-bit Windows on Intel)
                    "A0-DE-10-DD-85-E3-AA-56" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            ) { Label = "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults) -> Opus, ApplyGain = Album" }

            #endregion
        ];

        public static IEnumerable<TheoryDataRow<int, string, string, SettingDictionary, string[]>> Data =>
            _data.Select((item, index) =>
                new TheoryDataRow<int, string, string, SettingDictionary, string[]>(
                        index, item.Data.Item1, item.Data.Item2, item.Data.Item3, item.Data.Item4)
                    { Label = item.Label, Skip = item.Skip });
    }
}