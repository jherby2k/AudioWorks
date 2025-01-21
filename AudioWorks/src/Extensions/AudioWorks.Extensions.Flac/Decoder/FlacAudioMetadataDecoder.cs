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

using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using AudioWorks.Extensions.Flac.Metadata;

namespace AudioWorks.Extensions.Flac.Decoder
{
    [AudioMetadataDecoderExport(".flac")]
    public sealed class FlacAudioMetadataDecoder : IAudioMetadataDecoder
    {
        const string _format = "FLAC";

        public string Format => _format;

        public AudioMetadata ReadMetadata(Stream stream)
        {
            using (var decoder = new MetadataStreamDecoder(stream))
            {
                decoder.SetMetadataRespond(MetadataType.VorbisComment);
                decoder.SetMetadataRespond(MetadataType.Picture);

                decoder.Initialize();
                if (!decoder.ProcessMetadata())
                    throw new AudioInvalidException(
                        $"libFLAC was unable to read the audio metadata: {decoder.GetState()}.");
                decoder.Finish();

                return decoder.AudioMetadata;
            }
        }
    }
}
