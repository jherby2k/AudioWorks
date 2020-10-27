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

using System;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    sealed class Id3V1
    {
        static readonly string[] _genres =
        {
            "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal",
            "New Age", "Oldies", "Other", "Pop", "Rhythm and Blues", "Rap", "Reggae", "Rock", "Techno", "Industrial",
            "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop",
            "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game",
            "Sound Clip", "Gospel", "Noise", "Alternative Rock", "Bass", "Soul", "Punk", "Space", "Meditative",
            "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", "Electronic",
            "Pop-Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap",
            "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychedelic", "Rave", "Showtunes",
            "Trailer", "Lo-Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock 'n' Roll",
            "Hard Rock", "Folk", "Folk/Rock", "National Folk", "Swing", "Fast-Fusion", "Bebop", "Latin", "Revival",
            "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock",
            "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech",
            "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire",
            "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle",
            "Duet", "Punk Rock", "Drum Solo", "A capella", "Euro-House", "Dance Hall", "Goa", "Drum & Bass",
            "Club-House", "Hardcore", "Terror", "Indie", "BritPop", "Negerpunk", "Polsk Punk", "Beat",
            "Christian Gangsta Rap", "Heavy Metal", "Black Metal", "Crossover", "Contemporary Christian",
            "Christian Rock", "Merengue", "Salsa", "Trash Metal", "Anime", "JPop", "Synthpop", "Abstract", "Art Rock",
            "Baroque", "Bhangra", "Big beat", "Breakbeat", "Chillout", "Downtempo", "Dub", "EBM", "Eclectic",
            "Electro", "Electroclash", "Emo", "Experimental", "Garage", "Global", "IDM", "Illbient", "Industro-Goth",
            "Jam Band", "Krautrock", "Leftfield", "Lounge", "Math Rock", "New Romantic", "Nu-Breakz", "Post-Punk",
            "Post-Rock", "Psytrance", "Shoegaze", "Space Rock", "Trop Rock", "World Music", "Neoclassical",
            "Audiobook", "Audio Theatre", "Neue Deutsche Welle", "Podcast", "Indie-Rock", "G-Funk", "Dubstep",
            "Garage Rock", "Psybient"
        };

        internal string Title { get; private set; } = string.Empty;

        internal string Artist { get; private set; } = string.Empty;

        internal string Album { get; private set; } = string.Empty;

        internal string Year { get; private set; } = string.Empty;

        internal string Comment { get; private set; } = string.Empty;

        internal int TrackNumber { get; private set; }

        internal string Genre { get; private set; } = string.Empty;

        internal void Deserialize(Stream stream)
        {
            using (var reader = new BinaryReader(stream, CodePagesEncodingProvider.Instance.GetEncoding(1252), true))
            {
                reader.BaseStream.Seek(-128, SeekOrigin.End);

#if NETSTANDARD2_0
                var buffer = reader.ReadChars(3);
#else
                Span<char> buffer = stackalloc char[3];
                reader.Read(buffer);
#endif
                if (!new string(buffer).Equals("TAG", StringComparison.Ordinal))
                    throw new TagNotFoundException("ID3v1 tag was not found");

#if NETSTANDARD2_0
                Title = new string(reader.ReadChars(30)).TrimEnd('\0');
                Artist = new string(reader.ReadChars(30)).TrimEnd('\0');
                Album = new string(reader.ReadChars(30)).TrimEnd('\0');
                Year = new string(reader.ReadChars(4)).TrimEnd('\0');
                var commentChars = reader.ReadChars(30);
                if (commentChars[28] == 0)
                {
                    Comment = new string(commentChars, 0, 28).TrimEnd('\0');
                    TrackNumber = commentChars[29];
                }
                else
                    Comment = new string(commentChars).TrimEnd('\0');
#else
                buffer = stackalloc char[30];

                reader.Read(buffer);
                Title = new string(buffer.Slice(0, buffer.IndexOf('\0')));
                reader.Read(buffer);
                Artist = new string(buffer.Slice(0, buffer.IndexOf('\0')));
                reader.Read(buffer);
                Album = new string(buffer.Slice(0, buffer.IndexOf('\0')));
                reader.Read(buffer.Slice(0, 4));
                Year = new string(buffer.Slice(0, Math.Min(4, buffer.IndexOf('\0'))));
                reader.Read(buffer);
                Comment = new string(buffer.Slice(0, buffer.IndexOf('\0')));
                if (buffer[28] == 0)
                    TrackNumber = buffer[29];
#endif

                var genreIndex = reader.ReadByte();
                if (genreIndex < _genres.Length)
                    Genre = _genres[genreIndex];
            }
        }
    }
}