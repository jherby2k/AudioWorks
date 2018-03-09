using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    [AudioDecoderExport(".flac")]
    public sealed class FlacAudioDecoder : IAudioDecoder, IDisposable
    {
        [CanBeNull] string _fileName;
        [CanBeNull] AudioStreamDecoder _decoder;

        public bool Finished { get; private set; }

        public void Initialize(FileStream fileStream)
        {
            _fileName = fileStream.Name;
            _decoder = new AudioStreamDecoder(fileStream);
            _decoder.Initialize();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public SampleBuffer DecodeSamples()
        {
            while (_decoder.GetState() != DecoderState.EndOfStream)
            {
                if (!_decoder.ProcessSingle())
                    throw new AudioInvalidException($"libFLAC failed to decode the samples: {_decoder.GetState()}.",
                        _fileName);

                if (_decoder.Samples == null)
                    continue;

                var result = _decoder.Samples;
                _decoder.Samples = null;
                return result;
            }

            _decoder.Finish();
            Finished = true;
            return new SampleBuffer(_decoder.AudioInfo.Channels, 0);
        }

        public void Dispose()
        {
            _decoder?.Dispose();
        }
    }
}
