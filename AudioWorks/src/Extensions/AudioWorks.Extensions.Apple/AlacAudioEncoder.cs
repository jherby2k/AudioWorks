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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("ALAC", "Apple Lossless Audio Codec")]
    sealed class AlacAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] Stream _stream;
        [CanBeNull] AudioMetadata _metadata;
        [CanBeNull] SettingDictionary _settings;
        int _bitsPerSample;
        [CanBeNull] ExtendedAudioFile _audioFile;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                // Use the external MP4 encoder's SettingInfo
                var metadataEncoderFactory =
                    ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
                if (metadataEncoderFactory == null) return new SettingInfoDictionary();
                using (var export = metadataEncoderFactory.CreateExport())
                    return export.Value.SettingInfo;
            }
        }

        public string FileExtension { get; } = ".m4a";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _stream = stream;
            _metadata = metadata;
            _settings = settings;
            _bitsPerSample = info.BitsPerSample;

            // Pre-allocate the whole stream (estimate worst case compression, plus cover art)
            stream.SetLength(info.FrameCount * info.Channels * (long) Math.Ceiling(info.BitsPerSample / 8.0)
                             + (metadata.CoverArt?.Data.Length ?? 0));

            var inputDescription = GetInputDescription(info);
            _audioFile = new ExtendedAudioFile(GetOutputDescription(inputDescription), AudioFileType.M4A, stream);
            _audioFile.SetProperty(ExtendedAudioFilePropertyId.ClientDataFormat, inputDescription);
        }

        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            Span<int> buffer = stackalloc int[samples.Frames * samples.Channels];
            samples.CopyToInterleaved(buffer, _bitsPerSample);

            var bufferList = new AudioBufferList
            {
                NumberBuffers = 1,
                Buffers = new AudioBuffer[1]
            };
            bufferList.Buffers[0].NumberChannels = (uint) samples.Channels;
            bufferList.Buffers[0].DataByteSize = (uint) (buffer.Length * Marshal.SizeOf<int>());
            bufferList.Buffers[0].Data = new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)));

            // ReSharper disable once PossibleNullReferenceException
            var status = _audioFile.Write(bufferList, (uint) samples.Frames);
            if (status != ExtendedAudioFileStatus.Ok)
                throw new AudioEncodingException($"Apple Lossless encoder encountered error '{status}'.");
        }

        public void Finish()
        {
            // ReSharper disable once PossibleNullReferenceException
            _audioFile.Dispose();
            _audioFile = null;

            // ReSharper disable once PossibleNullReferenceException
            _stream.Position = 0;

            // Call the external MP4 encoder for writing iTunes-compatible atoms
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory == null) return;
            using (var export = metadataEncoderFactory.CreateExport())
                // ReSharper disable twice AssignNullToNotNullAttribute
                export.Value.WriteMetadata(_stream, _metadata, _settings);
        }

        public void Dispose()
        {
            _audioFile?.Dispose();
        }

        [Pure]
        static AudioStreamBasicDescription GetInputDescription([NotNull] AudioInfo info)
        {
            return new AudioStreamBasicDescription
            {
                SampleRate = info.SampleRate,
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsSignedInteger,
                BytesPerPacket = (uint) (Marshal.SizeOf<int>() * info.Channels),
                FramesPerPacket = 1,
                BytesPerFrame = (uint) (Marshal.SizeOf<int>() * info.Channels),
                ChannelsPerFrame = (uint) info.Channels,
                BitsPerChannel = (uint) info.BitsPerSample
            };
        }

        [Pure]
        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            var result = new AudioStreamBasicDescription
            {
                SampleRate = inputDescription.SampleRate,
                FramesPerPacket = 4096,
                AudioFormat = AudioFormat.AppleLossless,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame
            };

            switch (inputDescription.BitsPerChannel)
            {
                case 16:
                    result.Flags = AudioFormatFlags.Alac16BitSourceData;
                    break;
                case 20:
                    result.Flags = AudioFormatFlags.Alac20BitSourceData;
                    break;
                case 24:
                    result.Flags = AudioFormatFlags.Alac24BitSourceData;
                    break;
                case 32:
                    result.Flags = AudioFormatFlags.Alac32BitSourceData;
                    break;
                default:
                    throw new AudioUnsupportedException(
                        $"ALAC does not support {inputDescription.BitsPerChannel}-bit audio.");
            }

            return result;
        }
    }
}
