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

using System;
using System.Collections.Generic;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    [AudioEncoderExport("FLAC", "Free Lossless Audio Codec")]
    public sealed class FlacAudioEncoder : IAudioEncoder, IDisposable
    {
        [NotNull] readonly List<MetadataBlock> _metadataBlocks = new List<MetadataBlock>(4);
        [CanBeNull] StreamEncoder _encoder;
        int _bitsPerSample;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["CompressionLevel"] = new IntSettingInfo(0, 8),
            ["SeekPointInterval"] = new IntSettingInfo(0, 600),
            ["Padding"] = new IntSettingInfo(0, 16_775_369)
        };

        public string FileExtension { get; } = ".flac";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _encoder = new StreamEncoder(fileStream);
            _encoder.SetChannels((uint) info.Channels);
            _encoder.SetBitsPerSample((uint) info.BitsPerSample);
            _encoder.SetSampleRate((uint) info.SampleRate);
            if (info.FrameCount > 0)
                _encoder.SetTotalSamplesEstimate((ulong) info.FrameCount);

            // Use a default compression level of 5
            _encoder.SetCompressionLevel(
                settings.TryGetValue<int>("CompressionLevel", out var compressionLevel)
                    ? (uint) compressionLevel
                    : 5);

            // Use a default seek point interval of 10 seconds
            if (!settings.TryGetValue<int>("SeekPointInterval", out var seekPointInterval))
                seekPointInterval = info.FrameCount > 0 ? 10 : 0;
            if (seekPointInterval > 0)
                _metadataBlocks.Add(new SeekTableBlock(
                    (uint) Math.Ceiling(info.PlayLength.TotalSeconds / seekPointInterval), (ulong) info.FrameCount));

            _metadataBlocks.Add(new MetadataToVorbisCommentAdapter(metadata));
            if (metadata.CoverArt != null)
                _metadataBlocks.Add(new CoverArtToPictureBlockAdapter(metadata.CoverArt));

            // Use a default padding of 8192
            if (!settings.TryGetValue<int>("Padding", out var padding))
                padding = 8192;
            if (padding > 0)
                _metadataBlocks.Add(new PaddingBlock(padding));

            _encoder.SetMetadata(_metadataBlocks);
            _encoder.Initialize();

            _bitsPerSample = info.BitsPerSample;
        }

        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            if (samples.IsInterleaved || samples.Channels == 1)
            {
                Span<int> buffer = stackalloc int[samples.Frames * samples.Channels];
                samples.CopyToInterleaved(buffer, _bitsPerSample);
                // ReSharper disable once PossibleNullReferenceException
                _encoder.ProcessInterleaved(buffer, (uint) samples.Frames);
            }
            else
            {
                // Performance optimization when the submitted samples are not interleaved
                Span<int> leftBuffer = stackalloc int[samples.Frames];
                Span<int> rightBuffer = stackalloc int[samples.Frames];
                samples.CopyTo(leftBuffer, rightBuffer, _bitsPerSample);
                // ReSharper disable once PossibleNullReferenceException
                _encoder.Process(leftBuffer, rightBuffer);
            }
        }

        public void Finish()
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                _encoder.Finish();
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
