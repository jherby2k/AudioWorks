/* Copyright © 2019 Jeremy Herbison

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

namespace AudioWorks.Extensions.Opus
{
    [AudioInfoDecoderExport(".opus")]
    public sealed class OpusAudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "Opus";

        public string Format => _format;

        public AudioInfo ReadAudioInfo(Stream stream)
        {
            throw new AudioUnsupportedException("TODO: Implementation");
        }
    }
}
