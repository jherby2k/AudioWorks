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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Flac
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioDecoderExport(".flac")]
    sealed class FlacAudioDecoder : IAudioDecoder, IDisposable
    {
        AudioStreamDecoder? _decoder;

        public bool Finished { get; private set; }

        public void Initialize(Stream stream)
        {
            _decoder = new(stream);
            _decoder.Initialize();
        }

        public SampleBuffer DecodeSamples()
        {
            while (_decoder!.GetState() != DecoderState.EndOfStream)
            {
                if (!_decoder.ProcessSingle())
                    throw new AudioInvalidException($"libFLAC failed to decode the samples: {_decoder.GetState()}.");

                if (_decoder.Samples == null)
                    continue;

                var result = _decoder.Samples;
                _decoder.Samples = null;
                return result;
            }

            _decoder.Finish();
            Finished = true;
            return SampleBuffer.Empty;
        }

        public void Dispose() => _decoder?.Dispose();
    }
}
