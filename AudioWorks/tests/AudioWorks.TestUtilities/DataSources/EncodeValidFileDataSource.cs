﻿/* Copyright © 2018 Jeremy Herbison

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
                    "42070347011D5067A9D962DA3237EF63", // FLAC 1.3.3 (Ubuntu 22.04)
                    "5C1655ACED0E208FC231C92C678FD87B", // FLAC 1.4.3 (Ubuntu 24.04)
                    "77-85-5F-1C-76-D9-FF-95" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                [],
                [
                    "0771EF09959F087FACE194A4479F5107", // FLAC 1.3.3 (Ubuntu 22.04)
                    "0B2A54ACAA983961889A83EF85942C5D", // FLAC 1.4.3 (Ubuntu 24.04)
                    "BE-A7-E3-09-E9-B9-DD-45" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F86AA899D869835AB05C17699F7F30DC", // FLAC 1.4.3 (Ubuntu 24.04)
                    "E2-02-89-EC-91-66-2A-35" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "F0F075E05A3AFB67403CCF373932BCCA", // FLAC 1.3.3 (Ubuntu 22.04)
                    "4CF8F4F9A26506718F891820268284DD", // FLAC 1.4.3 (Ubuntu 24.04)
                    "4E-A1-65-5B-D2-F8-57-F6" // FLAC 1.5.0
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "D3A7B834DCE97F0709AEFCA45A24F5B6", // FLAC 1.3.3 (Ubuntu 22.04)
                    "99355C1C3DD53802AB84425043CF1831", // FLAC 1.4.3 (Ubuntu 24.04)
                    "EF-36-3E-CE-6B-73-D7-A4" // FLAC 1.5.0
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "10F6AC75659ECFE81D3C07D8D3074538", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F7A6A7DC6610B277E302387A9E38FB32", // FLAC 1.4.3 (Ubuntu 24.04)
                    "A2-42-13-03-B7-3E-7B-D5" // FLAC 1.5.0
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "FLAC",
                [],
                [
                    "FCBAB8A8C261456CF6F87E603B237426", // FLAC 1.3.3 (Ubuntu 22.04)
                    "2ED7D0E99EA9072FB4415FF1510D3056", // FLAC 1.4.3 (Ubuntu 24.04)
                    "8F-D9-39-8A-97-1D-98-49" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                [],
                [
                    "2F2F341FEECB7842F7FA9CE6CB110C67", // FLAC 1.3.3 (Ubuntu 22.04)
                    "D75493F3FFF379D7A14F5B0BFA5AFB8A", // FLAC 1.4.3 (Ubuntu 24.04)
                    "0B-39-56-AB-84-D6-9E-95" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                [],
                [
                    "A48820F5E30B5C21A881E01209257E21", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A7E0AA5D079DCBF97B3C03B76FA2E645", // FLAC 1.4.3  (Ubuntu 24.04)
                    "3C-6F-52-00-B7-B9-BF-B6" // FLAC 1.5.0
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                [],
                [
                    "D90693A520FA14AC987272ACB6CD8996", // FLAC 1.3.3 (Ubuntu 22.04)
                    "1ED46D08800E000E7D4FFD6B3E776C2C", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F86AA899D869835AB05C17699F7F30DC", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "A58022B124B427771041A96F65D8DF21", // FLAC 1.3.3 (Ubuntu 22.04)
                    "FED4E3CA9AB4BF04C528E16D6D19EB44", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "F341E56E68A0A168B779A4EBFD41422D", // FLAC 1.3.3 (Ubuntu 22.04)
                    "3C563D2DAF004BF8C01139C6D5A7CA8F", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F86AA899D869835AB05C17699F7F30DC", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "986464F3AC48E00D00B8ECF3AF3FD6BC", // FLAC 1.3.3 (Ubuntu 22.04)
                    "46D403F89E109773996223F339570D74", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F86AA899D869835AB05C17699F7F30DC", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 22.04)
                    "F86AA899D869835AB05C17699F7F30DC", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "662592BD8B3853B6FEC4E188F7D0F246", // FLAC 1.3.3 (Ubuntu 22.04)
                    "A1A977792BBC0D92B12674A42AAE614F", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "455753A51355171BF22CCC78647235B4", // FLAC 1.3.3 (Ubuntu 22.04)
                    "B74DC40E795B96904E4DE801F98F1162", // FLAC 1.4.3 (Ubuntu 24.04)
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
                    "24C88B615C59F3054FF0A44C677987FA", // MacOS
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
                    "04244493BA087CD69BAD989927BD1595", // MacOS
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
                    "8522CA9ADFA8E200FDF5936AEF62EA43", // MacOS
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
                    "1BB62FEE2C66905CFBB6FEC048BF9772", // MacOS
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
                    "B55FDF0750DE71AA26781CA565222D05", // MacOS
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
                    "8D7D0F67E43BECA4B9EE8AC0D552C01F", // MacOS
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
                    "C88E17FEC4F9AE6C0F8ED21E423E60D3", // MacOS
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
                    "826418694704C310B1FFFDE1D1874839", // MacOS
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
                    "E170FAAB89D07557FC15F472715168A0", // MacOS
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
                    "B9F0B2D3BCB6FD612829084D8C42C2AA", // MacOS
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
                    "EF39C30FA5D1106F655DC55806D8CB44", // MacOS
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
                    "04244493BA087CD69BAD989927BD1595", // MacOS
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
                    "771DAECD27700570C845116672E7DACC", // MacOS
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
                    "DF3747EF579BFC71DC15CC0399E8F347", // MacOS
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
                    "D921E1D8B26ED8C0425B5F3DB207E221", // MacOS on Intel
                    "7E21540C472F59D17BB67D400BFD7991", // MacOS on ARM
                    "09CD8B8C8E9D8BC09121D8C9F871F9B7", // 32-bit Windows on Intel
                    "AA-14-94-9A-39-F1-26-3D", // 32-bit Windows on AMD
                    "75D127D9FCD7720CBE92C0670A93A880", // 64-bit Windows on Intel
                    "35-3E-A8-A6-1A-E4-37-4D", // 64-bit Windows on Ryzen 5600X
                    "E4178DFAC692316A8AAF8D08D754E230" // 64-bit Windows on EPYC 7763
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
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "33E9833987EE2BF27E2C2B28FAEFE3DB" // 64-bit Windows on EPYC 7763
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
                    "9B639E598FBF1E82299E60D0260D9017", // MacOS on Intel
                    "886286A0A3516BDC86C1AF5C3E9A676A", // MacOS on ARM
                    "CB39DFBF414790022574435C2D30297D", // 32-bit Windows on Intel
                    "3D-90-DA-92-BC-41-4B-5A", // 32-bit Windows on AMD
                    "E0A80A6B32CD5A8FA5C62B44F28C4A87", // 64-bit Windows on Intel
                    "E3-95-7F-57-F1-93-A9-14", // 64-bit Windows on Ryzen 5600X
                    "04D7006A40626AE7E52FBC6ABFC5AB82" // 64-bit Windows on EPYC 7763
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
                    "EC5C3156FCA0EE4883D221DE30D01F2C", // MacOS on Intel
                    "88908D8E665C16C5B6762CD1B14F8B7C", // MacOS on ARM
                    "E0C34EA1479C8979D3AF3A2C98D4E699", // 32-bit Windows on Intel
                    "53-18-0D-CD-CE-FF-8E-D9", // 32-bit Windows on AMD
                    "ED307F76DD052720321284BAD8876AB2", // 64-bit Windows on Intel
                    "85-67-33-D1-41-55-69-38", // 64-bit Windows on Ryzen 5600X
                    "16AB3A070CCD2BE26A821EDFCCB1DB8A" // 64-bit Windows on EPYC 7763
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
                    "A31D5E1A431F346AB36174FC4862F6F3", // MacOS on Intel
                    "18E802BDE2DDECAAEF5628312EC47EC9", // MacOS on ARM
                    "6E08F885FEC4094041F6A0B4A02F10AB", // 32-bit Windows on Intel
                    "194D40FBCAE58B4A01095DD89CE70A2D", // 32-bit Windows on AMD
                    "369DAA1350BB9C45BAF84F7769221F00", // 64-bit Windows on Intel
                    "E9-41-0A-C8-25-11-86-F7" // 64-bit Windows on AMD
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
                    "E29517B127D5D1EEFB28C08E746A32BF", // MacOS on Intel
                    "C5C7CBCFBAA60956EBFB4F3D0A7D07D0", // MacOS on AMD
                    "D41235E8E642C5773C499DCE06A72CC8", // 32-bit Windows on Intel
                    "CA-F9-FB-5A-AD-F2-FF-C2", // 32-bit Windows on AMD
                    "A86E9A3D4A9479A44F852FA42BA0C9C2", // 64-bit Windows on Intel
                    "72-0B-E6-82-18-51-F2-4C", // 64-bit Windows on Ryzen 5600X
                    "B575737F74EFCF9F208BCAC93E325ABA" // 64-bit Windows on EPYC 7763
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
                    "BB98985FF7FF60D8681833A88093B2DE", // MacOS on Intel
                    "04ABF599CC89FC3478FAFD96DD393DBA", // MacOS on ARM
                    "7BAD797AA7C5F71C7168C24077271029", // 32-bit Windows on Intel
                    "54-D1-D2-6B-FF-E6-65-38", // 32-bit Windows on AMD
                    "102A8F21E39D364419B9CF5BFB386631", // 64-bit Windows on Intel
                    "90-99-EF-1F-75-05-BB-1B", // 64-bit Windows on Ryzen 5600X
                    "E6DF15DCA4E63C67DE254DF5268B9BA3" // 64-bit Windows on EPYC 7763
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
                    "7B0E359B3C464CD7A26FDFE457715DD4", // MacOS on Intel
                    "9ADEDA620A2A4C6FF5AE143C3CF9B95F", // MacOS on ARM
                    "937750181287505A92B933F8A815D4C2", // 32-bit Windows on Intel
                    "38-C0-2C-7D-67-59-C1-96", // 32-bit Windows on AMD
                    "AAF40EB4D4AC1832D979D6EBDE9C5BDD", // 64-bit Windows on Intel
                    "6B-B4-0A-03-39-1C-F0-DF", // 64-bit Windows on Ryzen 5600X
                    "4FD2E10EF9390B9338171D8E83B3CB9F" // 64-bit Windows on EPYC 7763
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
                    "F1585045E793FEAA6CB69253475843F8", // MacOS on Intel
                    "B1BD1E3A78B5E47CAAE8462B13720EDE", // MacOS on ARM
                    "9AC3DEF9B464D0E1AB2D4F91C1A08B83", // 32-bit Windows on Intel
                    "CA-20-41-98-E4-02-3E-0C", // 32-bit Windows on AMD
                    "2863A63E2060267B6A6151CA90239BC6", // 64-bit Windows on Intel
                    "CC-E3-27-EB-55-D5-11-6B", // 64-bit Windows on Ryzen 5600X
                    "AFBA08886BCC7401D3BD2915E5083A9B" // 64-bit Windows on EPYC 7763
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
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "33E9833987EE2BF27E2C2B28FAEFE3DB" // 64-bit Windows on EPYC 7763
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
                    "CBDAABA391433EDAF676BFEDCE1C0E2C", // MacOS on Intel
                    "05271FF8F0A44F51B99E73E979E58559", // MacOS on ARM
                    "78299761793D1A6EC79CBB9233156FD8", // 32-bit Windows on Intel
                    "E53BA332FDCFBE927A81040DB480688B", // 32-bit Windows on AMD
                    "93D67A9C673E7ABE3929846DBE5DBF97", // 64-bit Windows on Intel
                    "03-CE-28-10-F4-67-A7-B7" // 64-bit Windows on AMD
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
                    "888CFC0621E764E94FA60D0018E8E559", // MacOS on Intel
                    "D565DE5B4A908E077EEC1BB5B75C05C2", // MacOS on ARM
                    "7EDD94F25082AEEE82B2AA87E795AB6D", // 32-bit Windows on Intel
                    "9D-FD-71-7F-11-4C-7C-D7", // 32-bit Windows on AMD
                    "A1CD6AC102BA40A728B2C7E00B1E786D", // 64-bit Windows on Intel
                    "C5-49-22-F7-D8-22-EA-64", // 64-bit Windows on Ryzen 5600X
                    "8F91A56C65E270F4E2694DBFB88BA01F" // 64-bit Windows on EPYC 7763
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
                    "67A06D818B49EBE273108839976FF1C3", // MacOS on Intel
                    "DAA69EC398F6E9E31B67B12DD735331F", // MacOS on ARM
                    "0177BB1DEB19854CA8495C4CBBB25366", // 32-bit Windows on Intel
                    "7A-92-5E-0F-FF-CE-D0-FC", // 32-bit Windows on AMD
                    "38D28BD3802566CB30D3B824D7FF593F", // 64-bit Windows on Intel
                    "FA-0F-C7-19-0F-26-BB-AA", // 64-bit Windows on Ryzen 5600X
                    "E606F4AAEE6B94FB85E75EFF06B4F4A7" // 64-bit Windows on EPYC 7763
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
                    "C9B699DB0247E0F95DE4C3A39582C4D0", // MacOS
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
                    "A466A0B76F8B02177B203CC21656D97C", // MacOS on Intel
                    "01245B8ECCFC4C4DA1F261F691A3FF37", // MacOS on ARM
                    "EBD496E30A953A8D0FE11C2609EFABC3", // 32-bit Windows on Intel
                    "E9-3C-D8-9E-D6-48-4E-7D", // 32-bit Windows on AMD
                    "2AD5FC82A78732A66B8F04387D7D412B", // 64-bit Windows on Intel
                    "66-48-1D-33-22-43-53-FF", // 64-bit Windows on Ryzen 5600X
                    "2BAF9813F2A9BA975F918B3C248F0745" // 64-bit Windows on EPYC 7763
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
                    "184DADDB750C82434E8B491A4E146DD5", // MacOS on Intel
                    "8B21060F2783BBD1029CB5E38489EE6F", // MacOS on ARM
                    "DE5F94EC1EACB75A3D049AE9960A7ACB", // 32-bit Windows on Intel
                    "97-CE-44-9B-71-22-CC-C9", // 32-bit Windows on AMD
                    "298A2B946AA53102FD025DDD9D273B21", // 64-bit Windows on Intel
                    "FF-B3-B1-3F-D0-D9-26-A1", // 64-bit Windows on Ryzen 5600X
                    "8814F7DB0AC6247DF45E32200B050933" // 64-bit Windows on EPYC 7763
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

                    "2E531801B06854D07EE21F128D15021D", // MacOS on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS on ARM
                    "D4-96-9B-4B-59-5F-B5-CE", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "5B-E0-72-79-CF-69-6E-91", // 64-bit Windows on Ryzen 5600X
                    "31A39465557533AA9D2E6F4985AFF237" // 64-bit Windows on EPYC 7763
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
                    "2E531801B06854D07EE21F128D15021D", // MacOS on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS on ARM
                    "D4-96-9B-4B-59-5F-B5-CE", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "5B-E0-72-79-CF-69-6E-91", // 64-bit Windows on Ryzen 5600X
                    "31A39465557533AA9D2E6F4985AFF237" // 64-bit Windows on EPYC 7763
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
                    "5121C9A7D75A0718B62D7A83CAD441FE", // MacOS on Intel
                    "C17ED28BC045C9E5010DF361ADE59515", // MacOS on ARM
                    "47-ED-90-FB-48-2E-19-55", // 32-bit Windows
                    "D4A9A3FFC75AC0383B68BADA43E23C3D", // 64-bit Windows on Intel
                    "B8-F5-05-D4-50-4C-35-AA", // 64-bit Windows on Ryzen 5600X
                    "EBDEAB5BAC8FBA910667D3664192EA07" // 64-bit Windows on EPYC 7763
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
                    "8FEA4642FF03F72EF3F4E35386EEDAE4", // MacOS on Intel
                    "FB38869E1A10C837554BCD0CCDBC660A", // MacOS on ARM
                    "365D7E965534C8690B4694B27D0CF1C9", // 32-bit Windows on Intel
                    "98-55-BA-2D-4A-72-F1-1A", // 32-bit Windows on AMD
                    "08686D04EFF88BC663C469F2DD224020", // 64-bit Windows on Intel
                    "BC-47-2F-B6-C5-50-ED-17", // 64-bit Windows on Ryzen 5600X
                    "CEF70CE95C604B6720A66F9A3AFB0FB3" // 64-bit Windows on EPYC 7763
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
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "6D-AA-6B-37-21-F5-DF-31", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "A9-EE-1D-11-BE-F6-D3-C9", // 64-bit Windows on Ryzen 5600X
                    "33E9833987EE2BF27E2C2B28FAEFE3DB" // 64-bit Windows on EPYC 7763
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
                    "B189CBFFA17095BD978B3D9E1978C722", // MacOS on Intel
                    "F245069996E7B90FBC76C04515E7DB2C", // MacOS on ARM
                    "DDA8DBB070EA36F77455A41A2628B6AA", // 32-bit Windows on Intel
                    "F2-7E-DD-D0-8F-A4-88-B5", // 32-bit Windows on AMD
                    "B49EC8F6428A1CDEBA4F0728FC1BF8E5", // 64-bit Windows on Intel
                    "C9-58-58-74-36-AD-27-F5", // 64-bit Windows on Ryzen 5600X
                    "970D928358381E6D6631430F3922FB99" // 64-bit Windows on EPYC 7763
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
                    "334BA4C5FA784B2899782C2173F34076", // MacOS on Intel
                    "AF4D1BAF1D9D5DFB5F4B6E5D5D50F423", // MacOS on ARM
                    "5502D724D98AA24FE49FA8AFB0FC63A6", // 32-bit Windows on Intel
                    "6A-C8-FA-AB-6D-4D-B2-A5", // 32-bit Windows on AMD
                    "19940A1BA1D575D9E165584C24A955F4", // 64-bit Windows on Intel
                    "34-90-BE-A7-5B-15-FA-72", // 64-bit Windows on Ryzen 5600X
                    "5BF9F9B72A1FED24330665C592934812" // 64-bit Windows on EPYC 7763
                ]
            ) { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },

            #endregion

            #region Lame MP3 Encoding

            new(
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "F2BD0875E273743A8908F96DCCFDFC44", // Lame 3.100 (Ubuntu and MacOS)
                    "EC-CA-7C-D9-86-6B-5E-6C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                [],
                [
                    "1CB5B915B3A72CBE76087E16F96A0A3E", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "05E1A0F79265E0FF0322A1A65DA6DFA5", // Lame 3.100 (MacOS on ARM)
                    "65-6E-9C-0F-7A-81-D4-3C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
                    "ED-23-FA-4F-3A-93-89-98" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "1454732B48913F2A3898164BA366DA01", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "451B8138407172CADFD10A7A1E783645", // Lame 3.100 (MacOS on ARM)
                    "2E-BE-F3-A0-4B-CB-79-AC" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "AD56C3A1ACD627DBDA4B5A28AFE0355D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "1838180F6D0A46F7A8B5EFB746DBA483", // Lame 3.100 (MacOS on ARM)
                    "5B-80-48-5D-A6-B1-22-5C" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "A-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "0190385E444B8576C297E1DE837279F1", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "C8CC25D47A44B8670073A4850D3CFB08", // Lame 3.100 (MacOS on ARM)
                    "BF-DA-07-84-22-66-32-BB" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "µ-law 44100Hz Stereo.wav",
                "LameMP3",
                [],
                [
                    "3CE431DA62AC5204B9FAE63BD8E2B4A8", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "0B65CAFF2DB22905D4553AF986C71684", // Lame 3.100 (MacOS on ARM)
                    "1A-B5-6B-E4-A7-65-B1-D3" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                [],
                [
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "3BDEC21C3B97533614AEE77A2BD8BC50", // Lame 3.100 (MacOS on ARM)
                    "AF-69-AA-01-97-B5-75-2B" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                [],
                [
                    "E19CC567ECA5EA8CC06AB204F0A6DCFB", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "61C8CBA12A4A4756C0A0AA5F1F7BE425", // Lame 3.100 (MacOS on ARM)
                    "34-36-E0-A3-31-49-3F-8D" // Lame 3.100 (Windows)
                ]
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                [],
                [
                    "FB1B7DECB2C2A2C9CAA1FBB917A81472", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "D6A4A2E9487D4EC2F8CECA50DCEE1DCA", // Lame 3.100 (MacOS on ARM)
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
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "3BDEC21C3B97533614AEE77A2BD8BC50", // Lame 3.100 (MacOS on ARM)
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
                    "F69CCDFC32565F97130CBAEABFF0D13C", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "402B56D552DD68BA4EC83BF2ED09999C", // Lame 3.100 (MacOS on ARM)
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
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "3BDEC21C3B97533614AEE77A2BD8BC50", // Lame 3.100 (MacOS on ARM)
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
                    "EA1232E970C83FCDDE00D4C1D51F0446", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "8C4155ED7430FA903380E3FE3F2D3251", // Lame 3.100 (MacOS on ARM)
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
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "F90889088302F49BD11FD31C3EB25893", // Lame 3.100 (MacOS on ARM)
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
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "F90889088302F49BD11FD31C3EB25893", // Lame 3.100 (MacOS on ARM)
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
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "F90889088302F49BD11FD31C3EB25893", // Lame 3.100 (MacOS on ARM)
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
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "3BDEC21C3B97533614AEE77A2BD8BC50", // Lame 3.100 (MacOS on ARM)
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
                    "ED3A9531742553641B112C0D0A41F099", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "9924C0FA5FAA974195001279136E05F1", // Lame 3.100 (MacOS on ARM)
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
                    "6645726904A761FDF324711CFD21D477", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "5000A32754845F65E655B92081A5F639", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "65D418A236D86A8CE33E07A76C98DF08", // Lame 3.100 (Ubuntu and MacOS)
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
                    "5DE234656056DFDAAD30E4DA9FD26366", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "A26B3F367DE55447F2DC22243143BE64", // Lame 3.100 (MacOS on ARM)
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
                    "2BBC83E74AB1A4EB150BC6E1EB9920B5", // Lame 3.100 (Ubuntu and MacOS)
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
                    "BEB5029A08011BCEDFFA99173B763E7F", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "261E79502AA11D01992FFC73FA9CAAC5", // Lame 3.100 (MacOS on ARM)
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
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "96041DD62546602F0B09C3E499672AD9", // Lame 3.100 (MacOS on ARM)
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
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "96041DD62546602F0B09C3E499672AD9", // Lame 3.100 (MacOS on ARM)
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
                    "EACCA2FD6404ACA1AB46027FAE6A667B", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "E1F9B6ED40E9186DC77F2A9B24A3ABBA", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "96041DD62546602F0B09C3E499672AD9", // Lame 3.100 (MacOS on ARM)
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
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "28C1A2D0545741D0FF6AA071083E49F5", // Lame 3.100 (MacOS on ARM)
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
                    "49CB061F4DE93D7F88D3B656458C7003", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "7EDA9190AE98D184FC9BEB0085740EB1", // Lame 3.100 (MacOS on ARM)
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
                    "E804988EFC1EA58704E6C78B42CE1DF6", // Lame 3.100 (Ubuntu and MacOS on Intel)
                    "A478C869EB25627FD94360E82CD853FA", // Lame 3.100 (MacOS on ARM)
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
                    "E9053894172DBB87E7D13196DAB64E5A", // Vorbis 1.3.7 (Ubuntu)
                    "2BEFC8DFC5C86F305FBB1126CC2B2D0D", // Vorbis 1.3.7 (MacOS on Intel)
                    "42AE26A3799A5A52BD1B321F2AACED2C", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "12848C43AB864D84D152EA23AEAE79C3", // Vorbis 1.3.7 (Ubuntu)
                    "E8B8006C5EC2A3D50555B85F367F4922", // Vorbis 1.3.7 (MacOS on Intel)
                    "C5121F731129DA41F0AEA13FFE368BF2", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "3D91DE53C8A8C95AAA12C795F6EFB1B6", // Vorbis 1.3.7 (Ubuntu)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "C047BADC1EAC8CFE817AB6D0A9930567", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "1C976845289D0EEFDA1A204359B97C86", // Vorbis 1.3.7 (Ubuntu)
                    "6D4108CB330E72038EA67B4D7B335AF9", // Vorbis 1.3.7 (MacOS on Intel)
                    "292C568DF9F16641A4052A7246481683", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "33E265E6265758F5A7B0A64DF5D72539", // Vorbis 1.3.7 (Ubuntu)
                    "239E03B5B24AA65E6335303DF50FB3A2", // Vorbis 1.3.7 (MacOS on Intel)
                    "80580AAF03164EC5206F330140978B8A", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "C63EBF41E7FEC502ABF7C16934A6D399", // Vorbis 1.3.7 (Ubuntu)
                    "D48672C42031B4419ADC0F420D503A47", // Vorbis 1.3.7 (MacOS on Intel)
                    "8DEB628166FB64F5A1CA05DEFA83D21F", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "558A9BA3B4FAB6DE9BB990592F236D6E", // Vorbis 1.3.7 (Ubuntu)
                    "4721899D9DDAF7394B1A679F753F81CF", // Vorbis 1.3.7 (MacOS on Intel)
                    "5A8780449BAD236AA4DC1C4A02D780E0", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "B7F70C664E4644F9CE338110FECAD49B", // Vorbis 1.3.7 (Ubuntu)
                    "F6D78BE21567DD9244848160A2BD3889", // Vorbis 1.3.7 (MacOS on Intel)
                    "EE7EF3B2ED9DE0F2C21EFB29B08AEC0A", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "F6D1AB2B773DEFF78A5E9081D5AE1BF2", // Vorbis 1.3.7 (Ubuntu)
                    "46F9042BE0BD04F4509E76B927DC0D71", // Vorbis 1.3.7 (MacOS on Intel)
                    "51F554DDF4CE99C21261FE78041BC901", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "85E79650FAB8FA19E9389A8481897154", // Vorbis 1.3.7 (Ubuntu)
                    "AD172F94045E0970A71F8C439EEEA6C9", // Vorbis 1.3.7 (MacOS on Intel)
                    "24B2E2A64AB7B019B80EC00F038EF42E", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "6228EC13C97965B94784DBAA85F45F43", // Vorbis 1.3.7 (Ubuntu)
                    "88C186B7AA3DC3A55CD430FFF8AADCB1", // Vorbis 1.3.7 (MacOS on Intel)
                    "762E8B1010603BE9EE62836B033AF317", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "23A7A31647E7228018B1E191263A68F7", // Vorbis 1.3.7 (Ubuntu)
                    "74D8326F97A08EBCCC7FF754FE37464F", // Vorbis 1.3.7 (MacOS on Intel)
                    "5733DEA0D33C4EF12A9C7CB9D9971089", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "3D91DE53C8A8C95AAA12C795F6EFB1B6", // Vorbis 1.3.7 (Ubuntu)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "C047BADC1EAC8CFE817AB6D0A9930567", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "43AB3A7CEC20702370DE422F9DD7E40C", // Vorbis 1.3.7 (Ubuntu)
                    "9DEF0BAB12400A25E06EA7CA8C32CCC6", // Vorbis 1.3.7 (MacOS on Intel)
                    "EAE93C9DE28D76AAEC6CB2F131B85728", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "4FD538B06DF0A26D6D2437EF19EACD12", // Vorbis 1.3.7 (Ubuntu)
                    "1C21FEE55AC987FE8ACA1865353DC833", // Vorbis 1.3.7 (MacOS on Intel)
                    "75B55AE3DD2871B490D5138066B3384E", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "03D504E716FAD14A824F721FFB77E50E", // Vorbis 1.3.7 (Ubuntu)
                    "68B19B197BDAFA73A45EBF67CD961CA9", // Vorbis 1.3.7 (MacOS on Intel)
                    "52F3F5EBE126FF751CF482FFB630E84E", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "D17E9A4C98EBEC4D45DEE59015AF487F", // Vorbis 1.3.7 (Ubuntu)
                    "D37BB84F2008B1B467D54C618495C4CE", // Vorbis 1.3.7 (MacOS on Intel)
                    "1421DDEC0E70551ED9A4BC0A4902F534", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "087FD165DDF3DA8938E51EF1CE4C2FAA", // Vorbis 1.3.7 (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "649206BD1B573A8478CFA3BACFC349F9", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "087FD165DDF3DA8938E51EF1CE4C2FAA", // Vorbis 1.3.7 (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "649206BD1B573A8478CFA3BACFC349F9", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "DD3034237ED6F61F14C6DB2061CD902C", // Vorbis 1.3.7 (Ubuntu)
                    "D23564C9639B7C6490E59163C37B9C83", // Vorbis 1.3.7 (MacOS on Intel)
                    "1193BA985E76832529BBFAEA037858BF", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "3D91DE53C8A8C95AAA12C795F6EFB1B6", // Vorbis 1.3.7 (Ubuntu)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "C047BADC1EAC8CFE817AB6D0A9930567", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "087FD165DDF3DA8938E51EF1CE4C2FAA", // Vorbis 1.3.7 (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "649206BD1B573A8478CFA3BACFC349F9", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "3D91DE53C8A8C95AAA12C795F6EFB1B6", // Vorbis 1.3.7 (Ubuntu)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "C047BADC1EAC8CFE817AB6D0A9930567", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "778AE9B25F7FA5598BE66FA576DF2DAF", // Vorbis 1.3.7 (Ubuntu)
                    "466D9535B74B53EA088F13FF9720268C", // Vorbis 1.3.7 (MacOS on Intel)
                    "810FAABD5DFA17D845BB3D294C8635BE", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "C98D63CFCCAD898FE5716DE13C7D52A5", // Vorbis 1.3.7 (Ubuntu)
                    "488A2980F20619FE0206CFBB1767CBAC", // Vorbis 1.3.7 (MacOS on Intel)
                    "70D18A8C9941807E7E09E5B76BCFFECA", // Vorbis 1.3.7 (MacOS on ARM)
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
                    "2D31A0D707D3A9AB865DB00095EB08AE", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "5E705C21EEB25418F69F54E33CF65156", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "00F481AB32E95DD81715ABB8F99F50FD", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "4477BA0E09EF52AC239EE55FF44DA2A0", // Opus 1.5.2 (MacOS on Intel)
                    "589BB547277DB56BD75F4273AB6AEC9E", // Opus 1.5.2 (MacOS on ARM)
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
                    "C22E87F617B51970785E9A4C43C9FC48", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "B68DA72E32DD0B6A89138679E8716876", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "BC884D67D04AA6575DDA2C5B4E89D38C", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "A96ECE9F6FC5F2977202F2489A105967", // Opus 1.5.2 (MacOS on Intel)
                    "6BD64F39FFAF07B20DB88618621C68CB", // Opus 1.5.2 (MacOS on ARM)
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
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "C22C840F1222A58CD697B955F17ABF72", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "297F37DFFE222B31663A46099038C9A0", // Opus 1.5.2 (MacOS on ARM)
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
                    "BD5F090F921BCC80F05FCBF5725D8E0E", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "FDE97144EB743A401CD274613B32D085", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "A5FADE1AC798E0EF9ECE5DCD34CE73EE", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "FB5BA2E0CF0A5738942A622FEE822D4E", // Opus 1.5.2 (MacOS on Intel)
                    "F9160B718B747C099AC754CA8DE48140", // Opus 1.5.2 (MacOS on ARM)
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
                    "8A8FA9C452D9EBBCF8554EE3E270A538", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "F7CB34EAF50E121D9474AE51E43BBB5B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B1EC4CE270A4DC792BACFC24BDBE0389", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "2821951FD04D0E164FBF6E6604B85F86", // Opus 1.5.2 (MacOS on Intel)
                    "E13E2159ADA7DC7A516BF31B373B0B46", // Opus 1.5.2 (MacOS on ARM)
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
                    "CD27FFC8398F52FB2F5984085D2215AC", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "2018DEFDE09A503E0734CD57B0E19FC4", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "25E06E9B3675B4280703DA2A5ED9DEF7", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "5A972613B661C285E44AB07D8416DC0A", // Opus 1.5.2 (MacOS on Intel)
                    "F4695DEB0385B2BF9720D65140910251", // Opus 1.5.2 (MacOS on ARM)
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
                    "9ED53495B32D47496C1C88B75D2FEF5F", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "EF85B330CBC8A8931C2827A81F2E1532", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "496805FDFF6828A650826F5FBED5BC78", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "D432F9BAABD834950984D4DFB1F446FA", // Opus 1.5.2 (MacOS on Intel)
                    "07C3F6B6F9ED9FBA9550F0BF53984EC5", // Opus 1.5.2 (MacOS on ARM)
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
                    "5501D7D9730B0722C310D5263AFBEB77", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "5A2C444BCD2F8D898E369DFA78D2336D", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "ADCB544FB4A364B2B4E4EF605696B586", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "C110F6EFE538B370E49ECBAE4B40C8C2", // Opus 1.5.2 (MacOS on Intel)
                    "BE70C5469607ECD9F28F7282BCE77E87", // Opus 1.5.2 (MacOS on ARM)
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
                    "E00313922D18720824C1B0BB443E8679", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "281EBF57BE8284D42EC19338E0FA5AFF", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "8638BEB9617185732F24684F6A1D9D81", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "1A3E6E580E6CED2A176855154F117F55", // Opus 1.5.2 (MacOS on Intel)
                    "0D7E4E8ADAEBB92DAC1949BA2C7446EF", // Opus 1.5.2 (MacOS on ARM)
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
                    "0ADBC105EA51C62CB1ED53B978B33415", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "E170CC2F31A402071E26892426AEC65B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "2AFEC11D40998BB73E50A826B7FE4A64", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "BA2DB653CD22442197C80828E35B694E", // Opus 1.5.2 (MacOS on Intel)
                    "0DFDBEE9CEE589AE748705268B0B7FD8", // Opus 1.5.2 (MacOS on ARM)
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
                    "CDE65307AACEA4F40F22FF925622D14E", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "503CA160DEC071D27F058FAE56273C29", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "0275B493FD6A46BA48DD15C7A3081697", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6C30D9BB7917BAEB1934A89C4874ADAC", // Opus 1.5.2 (MacOS on Intel)
                    "7ECA1147747494E00A668CB62C1C2CB9", // Opus 1.5.2 (MacOS on ARM)
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
                    "3AA89123958BA0DF1611539BC909DD0B", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "E83E5634833A567959243B09B9AF666B", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "5521CF518A4D810C5D0B9B0530B15E90", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "49452D1FB45FAB63ACCA4D7B98FC3421", // Opus 1.5.2 (MacOS on Intel)
                    "8126956A0E09653509F8C13EA1CDDB41", // Opus 1.5.2 (MacOS on ARM)
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
                    "1F81AFD1976BAB56F31A4599A8AD8FF1", // Opus 1.3.1 (Ubuntu 22.04)
                    "0199AE116F3F6E1E3AE88A1F92773AED", // Opus 1.4.0 (Ubuntu 24.04)
                    "39009C873167E72F3ACD4DC0085A3183", // Opus 1.5.2 (MacOS on Intel)
                    "359005FF5CCC013B0FC6154D699C82E4", // Opus 1.5.2 (MacOS on ARM)
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
                    "785D39E686216F4958B8103B62E9E321", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "2DBEDA9428C2522EFF60481B5446FA47", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "49F92DC8C00C7E765E60BE7F43A0EBBA", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "015FC5B8C914D876461F7C481B313083", // Opus 1.5.2 (MacOS on Intel)
                    "62DE12E6046BE4A01F698A13C6C00261", // Opus 1.5.2 (MacOS on ARM)
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
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "C22C840F1222A58CD697B955F17ABF72", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "297F37DFFE222B31663A46099038C9A0", // Opus 1.5.2 (MacOS on ARM)
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
                    "B98AC6052E465C5A226D8D2905B535EC", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "713E85741D7499561FA4424B5EF74EE7", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "20A5098BCBACFBCBDBE5DDD63CC8DEF1", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "6EED767F4AD005A03CCE4535E62836D3", // Opus 1.5.2 (MacOS on Intel)
                    "EDA7AE4F7F715EF9A6808E5D547A6F0A", // Opus 1.5.2 (MacOS on ARM)
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
                    "687B2402BF33EDB0EAD683235E309BC8", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "9948DAE5FCE1CF7D6298C293E3D71B0F", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "7482F42F524F8F78A46B2DD373B60FAE", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "69CB6F9E36304A413E772A64F8930EF8", // Opus 1.5.2 (MacOS on Intel)
                    "29706654639E3EB000623EC6A7543DB8", // Opus 1.5.2 (MacOS on ARM)
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
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "BD1629D7E9272CAA8AEAC20FD576B7C6", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B9542D3665143FBC5AE1D52F1AD11156", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "8A770140717C859BE50E5A65C9FD0591", // Opus 1.5.2 (MacOS on Intel)
                    "F380C4287A80A5A255C86DC2B578C5F7", // Opus 1.5.2 (MacOS on ARM)
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
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "BD1629D7E9272CAA8AEAC20FD576B7C6", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "B9542D3665143FBC5AE1D52F1AD11156", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "8A770140717C859BE50E5A65C9FD0591", // Opus 1.5.2 (MacOS on Intel)
                    "F380C4287A80A5A255C86DC2B578C5F7", // Opus 1.5.2 (MacOS on ARM)
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
                    "84D72BBEF86EA7611518CF2862FC94BD", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "56A43E329A39A3CC568348116F5134F3", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "07DDD237320EBEA969ECE544C20A7EC7", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "D6B5EDF579DBAFF63BD7892C9BA58ADD", // Opus 1.5.2 (MacOS on Intel)
                    "4FFADA4C9433B47ABD862305B21B9A5D", // Opus 1.5.2 (MacOS on ARM)
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
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "C22C840F1222A58CD697B955F17ABF72", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "297F37DFFE222B31663A46099038C9A0", // Opus 1.5.2 (MacOS on ARM)
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
                    "B6029E9FC954A8722A1B10700F80EA19", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "1CE1BF9EA0D643C0CDB9B8C8A1492B58", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "E5E16731D7EB25CC750EA4DAF41FB4D9", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "33E6FD8F8DC37F80C467DF3EF820D424", // Opus 1.5.2 (MacOS on Intel)
                    "5928D94B6117889982136833918FEBA6", // Opus 1.5.2 (MacOS on ARM)
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
                    "EF6A93BC1F10A15E5F45255F36E90F3A", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "5B7586B7F6F2DFCCA28E3A1D4C75690A", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "630DC720FF2CBF0EF9B4D72D201856E4", // Opus 1.4.0 (Ubuntu 24.04 on AMD)
                    "070E63A0E8A8A6F69B5C4DF9A7D1C82A", // Opus 1.5.2 (MacOS on Intel)
                    "C145ED6B764A2B208355F9D4B7D5E6A3", // Opus 1.5.2 (MacOS on ARM)
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