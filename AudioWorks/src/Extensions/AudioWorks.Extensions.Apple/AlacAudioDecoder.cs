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
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [AudioDecoderExport(".m4a")]
    public sealed class AlacAudioDecoder : IAudioDecoder, IDisposable
    {
        const uint _defaultFrameCount = 4096;

        [CanBeNull] AudioFile _audioFile;
        AudioStreamBasicDescription _outputDescription;
        [CanBeNull] AudioConverter _converter;
        IntPtr _magicCookie;

        public bool Finished { get; private set; }

        public void Initialize(Stream stream)
        {
            _audioFile = new AudioFile(AudioFileType.M4A, stream);

            var inputDescription = _audioFile.GetProperty<AudioStreamBasicDescription>(AudioFilePropertyId.DataFormat);
            if (inputDescription.AudioFormat != AudioFormat.AppleLossless)
                throw new AudioUnsupportedException("The stream is not in Apple Lossless format.");

            _outputDescription = GetOutputDescription(inputDescription);

            _converter = new AudioConverter(ref inputDescription, ref _outputDescription, _audioFile);
            _magicCookie = GetMagicCookie(_audioFile, _converter);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe SampleBuffer DecodeSamples()
        {
            Span<int> buffer = stackalloc int[(int) (_defaultFrameCount * _outputDescription.ChannelsPerFrame)];

            var bufferList = new AudioBufferList
            {
                NumberBuffers = 1,
                Buffers = new AudioBuffer[1]
            };
            bufferList.Buffers[0].NumberChannels = _outputDescription.ChannelsPerFrame;
            bufferList.Buffers[0].DataByteSize = (uint) (buffer.Length * Marshal.SizeOf<int>());
            bufferList.Buffers[0].Data = new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)));

            var frameCount = _defaultFrameCount;
            _converter.FillBuffer(ref frameCount, ref bufferList, null);

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

        void FreeUnmanaged()
        {
            Marshal.FreeHGlobal(_magicCookie);
        }

        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            uint bitsPerSample;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (inputDescription.Flags)
            {
                case AudioFormatFlags.Alac16BitSourceData:
                    bitsPerSample = 16;
                    break;
                case AudioFormatFlags.Alac20BitSourceData:
                    bitsPerSample = 20;
                    break;
                case AudioFormatFlags.Alac24BitSourceData:
                    bitsPerSample = 24;
                    break;
                case AudioFormatFlags.Alac32BitSourceData:
                    bitsPerSample = 32;
                    break;
                default:
                    throw new AudioUnsupportedException("Unknown audio format.");
            }

            return new AudioStreamBasicDescription
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

        static IntPtr GetMagicCookie([NotNull] AudioFile audioFile, [NotNull] AudioConverter converter)
        {
            audioFile.GetPropertyInfo(AudioFilePropertyId.MagicCookieData, out var dataSize, out _);
            var cookie = audioFile.GetProperty(AudioFilePropertyId.MagicCookieData, dataSize);
            converter.SetProperty(AudioConverterPropertyId.DecompressionMagicCookie, dataSize, cookie);
            return cookie;
        }

        ~AlacAudioDecoder()
        {
            FreeUnmanaged();
        }
    }
}
