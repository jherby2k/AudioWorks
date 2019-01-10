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

using System;
using System.Composition;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Opus
{
    [AudioEncoderExport("Opus", "Opus")]
    public sealed class OpusAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] Encoder _encoder;
        [CanBeNull] Export<IAudioFilter> _replayGainExport;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                var result = new SettingInfoDictionary
                {
                    ["SerialNumber"] = new IntSettingInfo(int.MinValue, int.MaxValue)
                };

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

        public string FileExtension { get; } = ".opus";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            InitializeReplayGainFilter(info, metadata, settings);

            _encoder = new Encoder(stream, info.SampleRate, info.Channels);

            if (!settings.TryGetValue("SerialNumber", out int serialNumber))
                serialNumber = new Random().Next();
            _encoder.SetSerialNumber(serialNumber);
        }

        public void Submit(SampleBuffer samples)
        {
            if (_replayGainExport != null)
                samples = _replayGainExport.Value.Process(samples);

            Span<float> buffer = stackalloc float[samples.Channels * samples.Frames];
            samples.CopyToInterleaved(buffer);

            // ReSharper disable once PossibleNullReferenceException
            _encoder.Write(buffer);
        }

        public void Finish()
        {
            // ReSharper disable once PossibleNullReferenceException
            _encoder.Drain();
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
