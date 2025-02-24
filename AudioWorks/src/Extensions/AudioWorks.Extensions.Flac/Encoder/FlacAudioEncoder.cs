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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using AudioWorks.Extensions.Flac.Metadata;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Flac.Encoder
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("FLAC", "Free Lossless Audio Codec")]
    sealed class FlacAudioEncoder : IAudioEncoder, IDisposable
    {
        readonly List<MetadataObject> _metadataBlocks = new(4);
        StreamEncoder? _encoder;
        int _bitsPerSample;

        public SettingInfoDictionary SettingInfo { get; } = new(new Dictionary<string, SettingInfo>
        {
            ["CompressionLevel"] = new IntSettingInfo(0, 8),
            ["SeekPointInterval"] = new IntSettingInfo(0, 600),
            ["Padding"] = new IntSettingInfo(0, 16_775_369)
        });

        public string FileExtension => ".flac";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            if (info.BitsPerSample == 0)
            {
                var logger = LoggerManager.LoggerFactory.CreateLogger<FlacAudioEncoder>();
                logger.LogWarning("Transcoding from a lossy to a lossless format.");

                _bitsPerSample = 16;
            }
            else
                _bitsPerSample = info.BitsPerSample;

            _encoder = new(stream);
            _encoder.SetChannels((uint) info.Channels);
            _encoder.SetBitsPerSample((uint) _bitsPerSample);
            _encoder.SetSampleRate((uint) info.SampleRate);
            if (info.FrameCount > 0)
                _encoder.SetTotalSamplesEstimate((ulong) info.FrameCount);

            // Use a default compression level of 5
            _encoder.SetCompressionLevel(
                settings.TryGetValue("CompressionLevel", out int compressionLevel)
                    ? (uint) compressionLevel
                    : 5);

            // Use a default seek point interval of 10 seconds
            if (!settings.TryGetValue("SeekPointInterval", out int seekPointInterval))
                seekPointInterval = info.FrameCount > 0 ? 10 : 0;
            if (seekPointInterval > 0)
                _metadataBlocks.Add(new SeekTableMetadataObject(
                    (uint) Math.Ceiling(info.PlayLength.TotalSeconds / seekPointInterval), (ulong) info.FrameCount));

            _metadataBlocks.Add(new MetadataToVorbisCommentAdapter(metadata));
            if (metadata.CoverArt != null)
                _metadataBlocks.Add(new CoverArtToPictureMetadataObjectAdapter(metadata.CoverArt));

            // Use a default padding of 8192
            if (!settings.TryGetValue("Padding", out int padding))
                padding = 8192;
            if (padding > 0)
                _metadataBlocks.Add(new PaddingMetadataObject(padding));

            _encoder.SetMetadata(_metadataBlocks);
            _encoder.Initialize();
        }

        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            if (samples.IsInterleaved || samples.Channels == 1)
            {
                Span<int> buffer = stackalloc int[samples.Frames * samples.Channels];
                samples.CopyToInterleaved(buffer, _bitsPerSample);
                _encoder!.ProcessInterleaved(buffer, (uint) samples.Frames);
            }
            else
            {
                // Performance optimization when the submitted samples are not interleaved
                Span<int> leftBuffer = stackalloc int[samples.Frames];
                Span<int> rightBuffer = stackalloc int[samples.Frames];
                samples.CopyTo(leftBuffer, rightBuffer, _bitsPerSample);
                _encoder!.Process(leftBuffer, rightBuffer);
            }
        }

        public void Finish()
        {
            try
            {
                _encoder!.Finish();
            }
            finally
            {
                foreach (var block in _metadataBlocks)
                    block.Dispose();
                _metadataBlocks.Clear();
            }
        }

        public void Dispose()
        {
            foreach (var block in _metadataBlocks)
                block.Dispose();
            _encoder?.Dispose();
        }
    }
}
