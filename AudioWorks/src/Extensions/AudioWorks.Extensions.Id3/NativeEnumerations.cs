/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Extensions.Id3
{
    [SuppressMessage("Design", "CA1028:Enum Storage should be Int32")]
    enum TextType : byte
    {
        Ascii = 0x00,
        Utf16 = 0x01,
        Utf16BigEndian = 0x02,
        Utf8 = 0x03
    }

    [SuppressMessage("Design", "CA1028:Enum Storage should be Int32")]
    enum PictureType : byte
    {
        Other = 0x00,
        Icon = 0x01,
        OtherIcon = 0x02,
        CoverFront = 0x03,
        CoverBack = 0x04,
        Leaflet = 0x05,
        Media = 0x06,
        LeadArtist = 0x07,
        Artist = 0x08,
        Conductor = 0x09,
        Orchestra = 0x0A,
        Composer = 0x0B,
        Lyricist = 0x0C,
        Location = 0x0D,
        Recording = 0x0E,
        Performance = 0x0F,
        Movie = 0x10,
        Fish = 0x11,
        Illustration = 0x12,
        BandLogo = 0x13,
        StudioLogo = 0x14
    }
}
