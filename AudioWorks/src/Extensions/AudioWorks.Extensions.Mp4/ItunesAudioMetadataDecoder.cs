﻿using System.IO;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Mp4
{
    [AudioMetadataDecoderExport(".m4a")]
    public sealed class ItunesAudioMetadataDecoder : IAudioMetadataDecoder
    {
        public AudioMetadata ReadMetadata(FileStream stream)
        {
            var mp4 = new Mp4Model(stream);
            if (mp4.DescendToAtom("moov", "udta", "meta", "ilst"))
                return new IlstAtomToMetadataAdapter(mp4, mp4.GetChildAtomInfo());

            throw new AudioUnsupportedException("No ilst atom found.");
        }
    }
}