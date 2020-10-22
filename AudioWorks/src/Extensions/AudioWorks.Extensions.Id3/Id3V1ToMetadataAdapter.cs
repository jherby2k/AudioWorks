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

using System.Globalization;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class Id3V1ToMetadataAdapter : AudioMetadata
    {
        internal Id3V1ToMetadataAdapter(Id3V1 v1Tag)
        {
            Title = v1Tag.Title;
            Artist = v1Tag.Artist;
            Album = v1Tag.Album;
            Genre = v1Tag.Genre;
            Comment = v1Tag.Comment;
            Year = v1Tag.Year;
            if (v1Tag.TrackNumber >= 1)
                TrackNumber = v1Tag.TrackNumber.ToString("00", CultureInfo.InvariantCulture);
        }
    }
}