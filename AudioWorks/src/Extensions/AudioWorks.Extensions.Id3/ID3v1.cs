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
using System.Globalization;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    class Id3V1
    {
        static readonly byte[] _id3 = { 0x54, 0x41, 0x47 }; //"TAG"

        static readonly string[] _genres =
        {
            "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal",
            "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", "Industrial",
            "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop",
            "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House",
            "Game", "Sound Clip", "Gospel", "Noise", "Alternative Rock", "Bass", "Soul", "Punk", "Space",
            "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic",
            "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream",
            "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle",
            "Native American", "Cabaret", "New Wave", "Psychadelic", "Rave", "Showtunes", "Trailer", "Lo-Fi",
            "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk",
            "Folk/Rock", "National Folk", "Swing", "Fast-Fusion", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass",
            "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock",
            "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music",
            "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club",
            "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhytmic Soul", "Freestyle", "Duet",
            "Punk Rock", "Drum Solo", "Acapella", "Euro-House", "Dance Hall", "Goa", "Drum & Bass", "Club-House",
            "Hardcore", "Terror", "Indie", "BritPop", "Negerpunk", "Polsk Punk", "Beat", "Christian Gangsta Rap",
            "Heavy Metal", "Black Metal", "Crossover", "Contemporary Christian",
            "Christian Rock", "Merengue", "Salsa", "Trash Metal", "Anime", "JPop", "SynthPop"
        };

        string _title = string.Empty;
        string _artist = string.Empty;
        string _album = string.Empty;
        string _year = string.Empty;
        string _comment = string.Empty;
        byte _trackNumber;
        byte _genreIndex = 255;

        internal TagModel TagModel
        {
            get => GetTagModel();
            set => SetFrameModel(value);
        }

        internal void Deserialize(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                var encoding = CodePagesEncodingProvider.Instance.GetEncoding(1252); // Should be ASCII

                // check for ID3v1 tag
                reader.BaseStream.Seek(-128, SeekOrigin.End);

                var idTag = new byte[3];

                // Read the tag identifier
                reader.Read(idTag, 0, 3);

                // Compare the read tag
                if (Memory.Compare(_id3, idTag) != true)
                    throw new TagNotFoundException("ID3v1 tag was not found");

                var tag = new byte[30]; // Allocate ID3 tag

                reader.Read(tag, 0, 30);
                _title = GetString(encoding, tag);

                reader.Read(tag, 0, 30);
                _artist = GetString(encoding, tag);

                reader.Read(tag, 0, 30);
                _album = GetString(encoding, tag);

                reader.Read(tag, 0, 4);
                _year = tag[0] != 0 && tag[1] != 0 && tag[2] != 0 && tag[3] != 0
                    ? encoding.GetString(tag, 0, 4)
                    : string.Empty;

                reader.Read(tag, 0, 30);
                if (tag[28] == 0) //Track number was stored at position 29 later hack of the original standard.
                {
                    _trackNumber = tag[29];
                    _comment = encoding.GetString(tag, 0, Memory.FindByte(tag, 0x00, 0));
                }
                else
                {
                    _trackNumber = 0;
                    _comment = GetString(encoding, tag);
                }

                _genreIndex = reader.ReadByte();
            }
        }

        internal void Serialize(Stream stream)
        {
            var idTag = new byte[3];

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                // check for ID3v1 tag
                reader.BaseStream.Seek(-128, SeekOrigin.End);

                // Read the tag identifier
                reader.Read(idTag, 0, 3);

                // Is there a ID3v1 tag already?
                if (Memory.Compare(_id3, idTag))
                {
                    //Found a ID3 tag so we will over write the old tag
                    // (and the 'TAG' label too, so WriteAtStreamPosition is clean)
                    stream.Seek(-128, SeekOrigin.End);

                    Write(stream);
                }
                else
                {
                    //Create a new Tag
                    var position = stream.Position;
                    stream.Seek(0, SeekOrigin.End);
                    //TODO: fix this ugly code, at first make the error visible to the user by some means,
                    // a start can be throwing the exception in an inner exception
                    try
                    {
                        Write(stream);
                    }
                    catch (Exception)
                    {
                        // There was an error while creating the tag
                        // Restore the file to the original state, I hope.
                        stream.SetLength(position);
                        throw;
                    }
                }
            }
        }

        internal void Write(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                var encoding = CodePagesEncodingProvider.Instance.GetEncoding(1252); // Should be ASCII

                writer.Write(_id3, 0, 3); // Write the ID3 TAG ID: 'TAG'

                var tag = new byte[30];

                if (_title.Length > 30)
                    _title = _title.Substring(0, 30);
                encoding.GetBytes(_title, 0, _title.Length, tag, 0);

                writer.Write(tag, 0, 30);

                Memory.Clear(tag, 0, 30);
                if (_artist.Length > 30)
                    _artist = _artist.Substring(0, 30);
                encoding.GetBytes(_artist, 0, _artist.Length, tag, 0);

                writer.Write(tag, 0, 30);

                Memory.Clear(tag, 0, 30);
                if (_album.Length > 30)
                    _album = _album.Substring(0, 30);
                encoding.GetBytes(_album, 0, _album.Length, tag, 0);

                writer.Write(tag, 0, 30);

                Memory.Clear(tag, 0, 30);
                if (!string.IsNullOrEmpty(_year))
                {
                    if (!ushort.TryParse(_year, out var year) || year > 9999)
                        _year = "0";
                    encoding.GetBytes(_year, 0, _year.Length, tag, 0);
                }

                writer.Write(tag, 0, 4);

                Memory.Clear(tag, 0, 30);
                if (_comment.Length > 28)
                    _comment = _comment.Substring(0, 28);
                encoding.GetBytes(_comment, 0, _comment.Length, tag, 0);

                writer.Write(tag, 0, 28);

                writer.Write((byte) 0);
                writer.Write(_trackNumber);
                writer.Write(_genreIndex);
            }
        }

        TagModel GetTagModel()
        {
            var tagModel = new TagModel
            {
                new FrameText("TIT2")
                {
                    TextType = TextType.Ascii,
                    Text = _title
                },
                new FrameText("TPE1")
                {
                    TextType = TextType.Ascii,
                    Text = _artist
                },
                new FrameText("TALB")
                {
                    TextType = TextType.Ascii,
                    Text = _album
                },
                new FrameText("TYER")
                {
                    TextType = TextType.Ascii,
                    Text = _year
                },
                new FrameText("TRCK")
                {
                    TextType = TextType.Ascii,
                    Text = _trackNumber.ToString(CultureInfo.InvariantCulture)
                },
                new FrameFullText
                {
                    TextType = TextType.Ascii,
                    Language = "eng",
                    Description = "",
                    Text = _comment
                }
            };

            if (_genreIndex < _genres.Length)
                tagModel.Add(new FrameText("TCON")
                {
                    TextType = TextType.Ascii,
                    Text = _genres[_genreIndex]
                });

            //TODO: Fix this code!!!!!!!!
            tagModel.Header.TagSize = 0; //TODO: Invalid size, not filled in until write
            tagModel.Header.Version = 3; // ID3v2.[3].[0]
            tagModel.Header.Revision = 0;
            tagModel.Header.Unsync = false;
            tagModel.Header.Experimental = false;
            tagModel.Header.Footer = false;
            tagModel.Header.ExtendedHeader = false;

            return tagModel;
        }

        void SetFrameModel(TagModel tagModel)
        {
            _title = string.Empty;
            _artist = string.Empty;
            _album = string.Empty;
            _year = string.Empty;
            _comment = string.Empty;
            _trackNumber = 0;
            _genreIndex = 255;

            foreach (var frame in tagModel)
                switch (frame.FrameId)
                {
                    case "TIT2":
                        _title = GetTagText(frame);
                        break;
                    case "TPE1":
                        _artist = GetTagText(frame);
                        break;
                    case "TALB":
                        _album = GetTagText(frame);
                        break;
                    case "TYER":
                        _year = GetTagText(frame);
                        break;
                    case "TRCK":
                        if (!byte.TryParse(GetTagText(frame), out _trackNumber))
                            _trackNumber = 0;
                        break;
                    case "TCON":
                        _genreIndex = ParseGenre(GetTagText(frame));
                        break;
                    case "COMM":
                        _comment = GetTagText(frame);
                        break;
                }
        }

        static string GetTagText(FrameBase frame) => ((FrameText) frame).Text;

        static byte ParseGenre(string genre)
        {
            if (string.IsNullOrEmpty(genre)) return 255;

            // test for a simple number in the field
            if (byte.TryParse(genre, out var genreIndex))
                return genreIndex;

            // "References to the ID3v1 genres can be made by, as first byte,
            // enter "(" followed by a number from the genres list (appendix A)
            // and ended with a ")" character."
            if (genre[0] == '(' && genre[1] != '(')
            {
#if NETSTANDARD2_0
                var close = genre.IndexOf(')');
#else
                var close = genre.IndexOf(')', StringComparison.Ordinal);
#endif
                if (close != -1 && byte.TryParse(genre.Substring(1, close - 1), out genreIndex))
                    return genreIndex;
            }

            // not a number, see if it's one of the predefined genre name strings
            for (byte index = 0; index < _genres.Length; index++)
                if (genre.Equals(_genres[index].Trim(), StringComparison.OrdinalIgnoreCase))
                    return index;

            return 12; // "Other"
        }

        static string GetString(Encoding encoding, byte[] tag)
        {
            // I had to use Memory.FindByte function because the encoding function
            // retrieves the 0x00 values and doesn't stop on the first zero. so makes strings with
            // many trailing zeros at the end when converted to XML these are added in binary text.
            var index = Memory.FindByte(tag, 0x00, 0);
            return index < 0 ? encoding.GetString(tag) : encoding.GetString(tag, 0, index);
        }
    }
}