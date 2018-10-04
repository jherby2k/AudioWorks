/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Mp4
{
    [AudioMetadataDecoderExport(".m4a")]
    public sealed class ItunesAudioMetadataDecoder : IAudioMetadataDecoder
    {
        const string _format = "iTunes";

        public string Format => _format;

        public AudioMetadata ReadMetadata(FileStream stream)
        {
            var mp4 = new Mp4Model(stream);
            if (mp4.DescendToAtom("moov", "udta", "meta", "ilst"))
                return new IlstAtomToMetadataAdapter(mp4, mp4.GetChildAtomInfo());

            throw new AudioUnsupportedException("No ilst atom found.");
        }
    }
}