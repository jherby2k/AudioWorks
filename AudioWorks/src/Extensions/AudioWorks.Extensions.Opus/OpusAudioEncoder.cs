/* Copyright © 2019 Jeremy Herbison

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
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Opus
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("Opus", "Opus")]
    sealed class OpusAudioEncoder : IAudioEncoder, IDisposable
    {
        MetadataToOpusCommentAdapter? _comments;
        Encoder? _encoder;

        public SettingInfoDictionary SettingInfo => new(new Dictionary<string, SettingInfo>
        {
            ["ApplyGain"] = new StringSettingInfo("Track", "Album"),
            ["BitRate"] = new IntSettingInfo(5, 512),
            ["ControlMode"] = new StringSettingInfo("Variable", "Constrained", "Constant"),
            ["SignalType"] = new StringSettingInfo("Music", "Speech"),
            ["SerialNumber"] = new IntSettingInfo(int.MinValue, int.MaxValue)
        });

        public string FileExtension => ".opus";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            var gain = 0;
            if (settings.TryGetValue("ApplyGain", out string? applyGain))
            {
                var scale = applyGain!.Equals("Track", StringComparison.OrdinalIgnoreCase)
                    ? CalculateScale(metadata.TrackGain, metadata.TrackPeak)
                    : CalculateScale(metadata.AlbumGain, metadata.AlbumPeak);

                // Adjust the metadata so that it remains valid
                metadata.TrackGain = CalculateGain(metadata.TrackGain, scale);
                metadata.AlbumGain = CalculateGain(metadata.AlbumGain, scale);

                gain = (int) Math.Round(Math.Log10(scale) * 5120);
            }

            _comments = new(metadata);
            _encoder = new(stream, info.SampleRate, info.Channels, (int) info.PlayLength.TotalSeconds,
                _comments.Handle);

            if (info.BitsPerSample > 0)
                _encoder.SetLsbDepth(Math.Min(Math.Max(info.BitsPerSample, 8), 24));

            _encoder.SetHeaderGain(gain);

            if (!settings.TryGetValue("SerialNumber", out int serialNumber))
                serialNumber = RandomNumberGenerator.GetInt32(int.MaxValue);
            _encoder.SetSerialNumber(serialNumber);

            // Default to full VBR
            if (settings.TryGetValue("ControlMode", out string? vbrMode))
                // ReSharper disable once SwitchStatementMissingSomeCases
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

            if (settings.TryGetValue("SignalType", out string? signalType))
                _encoder.SetSignal(signalType!.Equals("Speech", StringComparison.OrdinalIgnoreCase)
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

            _encoder!.Write(buffer);
        }

        public void Finish() => _encoder!.Drain();

        public void Dispose()
        {
            _encoder?.Dispose();
            _comments?.Dispose();
        }

        static float CalculateScale(string? gain, string? peak) =>
            string.IsNullOrEmpty(gain) || string.IsNullOrEmpty(peak)
                ? 1
                : Math.Min(
                    (float) Math.Pow(10, (float.Parse(gain, CultureInfo.InvariantCulture) - 5) / 20),
                    1 / float.Parse(peak, CultureInfo.InvariantCulture));

        static string CalculateGain(string? gain, float scale) =>
            string.IsNullOrEmpty(gain)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture, "{0:0.00}",
                    float.Parse(gain, CultureInfo.InvariantCulture) - Math.Log10(scale) * 20);
    }
}
