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

using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Id3
{
    [AudioMetadataDecoderExport(".mp3")]
    public sealed class Id3AudioMetadataDecoder : IAudioMetadataDecoder
    {
        const string _format = "ID3";

        public string Format => _format;

        public AudioMetadata ReadMetadata(Stream stream)
        {
            try
            {
                // Check for an ID3v2 tag first
                var tagModel = TagManager.Deserialize(stream);
                return new TagModelToMetadataAdapter(tagModel);
            }
            catch (TagNotFoundException)
            {
                try
                {
                    // If no ID3v2 tag was found, check for ID3v1
                    var v1Tag = new Id3V1();
                    v1Tag.Deserialize(stream);
                    return new Id3V1ToMetadataAdapter(v1Tag);
                }
                catch (TagNotFoundException e)
                {
                    throw new AudioUnsupportedException(e.Message);
                }
            }
        }
    }
}
