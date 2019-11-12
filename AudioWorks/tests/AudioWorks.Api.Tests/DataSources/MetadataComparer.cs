/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using AudioWorks.Common;
using System;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests.DataSources
{
    public sealed class MetadataComparer : IEqualityComparer<AudioMetadata>
    {
        public bool Equals(AudioMetadata? x, AudioMetadata? y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;

            return x.Title.Equals(y.Title, StringComparison.Ordinal) &&
                   x.Artist.Equals(y.Artist, StringComparison.Ordinal) &&
                   x.Album.Equals(y.Album, StringComparison.Ordinal) &&
                   x.AlbumArtist.Equals(y.AlbumArtist, StringComparison.Ordinal) &&
                   x.Composer.Equals(y.Composer, StringComparison.Ordinal) &&
                   x.Genre.Equals(y.Genre, StringComparison.Ordinal) &&
                   x.Comment.Equals(y.Comment, StringComparison.Ordinal) &&
                   x.Day.Equals(y.Day, StringComparison.Ordinal) &&
                   x.Month.Equals(y.Month, StringComparison.Ordinal) &&
                   x.Year.Equals(y.Year, StringComparison.Ordinal) &&
                   x.TrackNumber.Equals(y.TrackNumber, StringComparison.Ordinal) &&
                   x.TrackCount.Equals(y.TrackCount, StringComparison.Ordinal) &&
                   x.TrackPeak.Equals(y.TrackPeak, StringComparison.Ordinal) &&
                   x.AlbumPeak.Equals(y.AlbumPeak, StringComparison.Ordinal) &&
                   x.TrackGain.Equals(y.TrackGain, StringComparison.Ordinal) &&
                   x.AlbumGain.Equals(y.AlbumGain, StringComparison.Ordinal);
        }

        public int GetHashCode(AudioMetadata obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Title);
            hashCode.Add(obj.Artist);
            hashCode.Add(obj.Album);
            hashCode.Add(obj.AlbumArtist);
            hashCode.Add(obj.Composer);
            hashCode.Add(obj.Genre);
            hashCode.Add(obj.Comment);
            hashCode.Add(obj.Day);
            hashCode.Add(obj.Month);
            hashCode.Add(obj.Year);
            hashCode.Add(obj.TrackNumber);
            hashCode.Add(obj.TrackCount);
            hashCode.Add(obj.TrackPeak);
            hashCode.Add(obj.AlbumPeak);
            hashCode.Add(obj.TrackGain);
            hashCode.Add(obj.AlbumGain);
            return hashCode.ToHashCode();
        }
    }
}