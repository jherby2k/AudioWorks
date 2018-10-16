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
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    [AudioEncoderExport("LameMP3", "Lame MPEG Audio Layer 3")]
    public sealed class LameAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] Encoder _encoder;
        [CanBeNull] Export<IAudioFilter> _replayGainExport;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                var result = new SettingInfoDictionary
                {
                    ["VBRQuality"] = new IntSettingInfo(0, 9),
                    ["BitRate"] = new IntSettingInfo(8, 320),
                    ["ForceCBR"] = new BoolSettingInfo()
                };

                // Merge the external ID3 encoder's SettingInfo
                var metadataEncoderFactory =
                    ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
                if (metadataEncoderFactory != null)
                    using (var export = metadataEncoderFactory.CreateExport())
                        foreach (var settingInfo in export.Value.SettingInfo)
                            result.Add(settingInfo.Key, settingInfo.Value);

                // Merge the external ReplayGain filter's SettingInfo
                var filterFactory =
                    ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
                // ReSharper disable once InvertIf
                if (filterFactory != null)
                    using (var export = filterFactory.CreateExport())
                        foreach (var settingInfo in export.Value.SettingInfo)
                            result.Add(settingInfo.Key, settingInfo.Value);

                return result;
            }
        }

        public string FileExtension { get; } = ".mp3";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            InitializeReplayGainFilter(info, metadata, settings);

            // Call the external ID3 encoder, if available
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory != null)
                using (var export = metadataEncoderFactory.CreateExport())
                    export.Value.WriteMetadata(fileStream, metadata, settings);

            _encoder = new Encoder(fileStream);
            _encoder.SetChannels(info.Channels);
            _encoder.SetSampleRate(info.SampleRate);
            if (info.FrameCount > 0)
                _encoder.SetSampleCount((uint) info.FrameCount);

            if (settings.TryGetValue<int>("BitRate", out var bitRate))
            {
                // Use ABR, unless ForceCBR is set to true
                if (settings.TryGetValue<bool>("ForceCBR", out var forceCbr) && forceCbr)
                    _encoder.SetBitRate(bitRate);
                else
                {
                    _encoder.SetVbrMeanBitRate(bitRate);
                    _encoder.SetVbrMode(VbrMode.Abr);
                }
            }
            else
            {
                // Use VBR quality 3 if nothing else is specified
                _encoder.SetVbrQuality(
                    settings.TryGetValue<int>("VBRQuality", out var vbrQuality)
                        ? vbrQuality
                        : 3);
                _encoder.SetVbrMode(VbrMode.Mtrh);
            }

            _encoder.InitializeParameters();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            if (_replayGainExport != null)
                samples = _replayGainExport.Value.Process(samples);

            if (samples.Channels == 1)
            {
                Span<float> monoSamples = stackalloc float[samples.Frames];
                samples.CopyTo(monoSamples);
                _encoder.Encode(monoSamples, null);
            }
            else if (!samples.IsInterleaved)
            {
                Span<float> leftSamples = stackalloc float[samples.Frames];
                Span<float> rightSamples = stackalloc float[samples.Frames];
                samples.CopyTo(leftSamples, rightSamples);
                _encoder.Encode(leftSamples, rightSamples);
            }
            else
            {
                Span<float> interleavedSamples = stackalloc float[samples.Frames * samples.Channels];
                samples.CopyToInterleaved(interleavedSamples);
                _encoder.EncodeInterleaved(interleavedSamples, samples.Frames);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Finish()
        {
            _encoder.Flush();
            _encoder.UpdateLameTag();
        }

        public void Dispose()
        {
            _encoder?.Dispose();
            _replayGainExport?.Dispose();
        }

        void InitializeReplayGainFilter(
            [NotNull] AudioInfo info,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings)
        {
            var filterFactory =
                ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
            if (filterFactory == null) return;

            _replayGainExport = filterFactory.CreateExport();
            // ReSharper disable once PossibleNullReferenceException
            _replayGainExport.Value.Initialize(info, metadata, settings);
        }
    }
}
