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
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Opus
{
    [AudioEncoderExport("Opus", "Opus")]
    public sealed class OpusAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] MetadataToOpusCommentAdapter _comments;
        [CanBeNull] Encoder _encoder;

        public SettingInfoDictionary SettingInfo => new SettingInfoDictionary
        {
            ["BitRate"] = new IntSettingInfo(5, 512),
            ["ControlMode"] = new StringSettingInfo("Variable", "Constrained", "Constant"),
            ["SignalType"] = new StringSettingInfo("Music", "Speech"),
            ["SerialNumber"] = new IntSettingInfo(int.MinValue, int.MaxValue)
        };

        public string FileExtension { get; } = ".opus";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _comments = new MetadataToOpusCommentAdapter(metadata);
            _encoder = new Encoder(stream, info.SampleRate, info.Channels, (int) info.PlayLength.TotalSeconds,
                _comments.Handle);

            if (!settings.TryGetValue("SerialNumber", out int serialNumber))
                serialNumber = new Random().Next();
            _encoder.SetSerialNumber(serialNumber);

            if (info.BitsPerSample > 0)
                _encoder.SetLsbDepth(Math.Min(Math.Max(info.BitsPerSample, 8), 24));

            // Default to full VBR
            if (settings.TryGetValue("ControlMode", out string vbrMode))
                switch (vbrMode)
                {
                    case "Variable":
                        _encoder.SetVbrConstraint(false);
                        break;

                    // 'Constrained' is the libopusenc default

                    case "Constant":
                        _encoder.SetVbr(false);
                        break;
                }
            else
                _encoder.SetVbrConstraint(false);

            if (settings.TryGetValue("BitRate", out int bitRate))
                _encoder.SetBitRate(bitRate);

            if (settings.TryGetValue("SignalType", out string signalType))
                _encoder.SetSignal(signalType.Equals("Speech", StringComparison.OrdinalIgnoreCase)
                    ? SignalType.Speech
                    : SignalType.Music);
            else
                _encoder.SetSignal(SignalType.Music);
        }

        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

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
            _comments?.Dispose();
        }
    }
}
