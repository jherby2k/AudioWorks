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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Apple
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioDecoderExport(".m4a")]
    sealed class AlacAudioDecoder : IAudioDecoder, IDisposable
    {
        const uint _defaultFrameCount = 4096;

        AudioFile? _audioFile;
        AudioStreamBasicDescription _outputDescription;
        AudioConverter? _converter;
        IntPtr _magicCookie;

        public bool Finished { get; private set; }

        public void Initialize(Stream stream)
        {
            _audioFile = new(AudioFileType.M4A, stream);

            var inputDescription = _audioFile.GetProperty<AudioStreamBasicDescription>(AudioFilePropertyId.DataFormat);
            if (inputDescription.AudioFormat != AudioFormat.AppleLossless)
                throw new AudioUnsupportedException("The stream is not in Apple Lossless format.");

            _outputDescription = GetOutputDescription(inputDescription);

            _converter = new(ref inputDescription, ref _outputDescription, _audioFile);
            _magicCookie = GetMagicCookie(_audioFile, _converter);
        }

        public unsafe SampleBuffer DecodeSamples()
        {
            Span<int> buffer = stackalloc int[(int) (_defaultFrameCount * _outputDescription.ChannelsPerFrame)];

            var bufferList = new AudioBufferList
            {
                NumberBuffers = 1,
                Buffers = new AudioBuffer[1]
            };
            bufferList.Buffers[0].NumberChannels = _outputDescription.ChannelsPerFrame;
            bufferList.Buffers[0].DataByteSize = (uint) (buffer.Length * sizeof(int));
            bufferList.Buffers[0].Data = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)));

            var frameCount = _defaultFrameCount;
            _converter!.FillBuffer(ref frameCount, ref bufferList, null);

            if (frameCount == 0)
                Finished = true;

            var result = new SampleBuffer(
                buffer.Slice(0, (int) (frameCount * _outputDescription.ChannelsPerFrame)),
                (int) _outputDescription.ChannelsPerFrame, 32);

            return result;
        }

        public void Dispose()
        {
            _converter?.Dispose();
            _audioFile?.Dispose();
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged() => Marshal.FreeHGlobal(_magicCookie);

        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            var bitsPerSample = inputDescription.Flags switch
            {
                AudioFormatFlags.Alac16BitSourceData => 16u,
                AudioFormatFlags.Alac20BitSourceData => 20u,
                AudioFormatFlags.Alac24BitSourceData => 24u,
                AudioFormatFlags.Alac32BitSourceData => 32u,
                _ => throw new AudioUnsupportedException("Unknown audio format.")
            };

            return new()
            {
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsSignedInteger | AudioFormatFlags.PcmIsAlignedHigh,
                BytesPerPacket = sizeof(int) * inputDescription.ChannelsPerFrame,
                FramesPerPacket = 1,
                BytesPerFrame = sizeof(int) * inputDescription.ChannelsPerFrame,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame,
                BitsPerChannel = bitsPerSample,
                SampleRate = inputDescription.SampleRate
            };
        }

        static IntPtr GetMagicCookie(AudioFile audioFile, AudioConverter converter)
        {
            audioFile.GetPropertyInfo(AudioFilePropertyId.MagicCookieData, out var dataSize, out _);
            var cookie = audioFile.GetProperty(AudioFilePropertyId.MagicCookieData, dataSize);
            converter.SetProperty(AudioConverterPropertyId.DecompressionMagicCookie, dataSize, cookie);
            return cookie;
        }

        ~AlacAudioDecoder() => FreeUnmanaged();
    }
}
