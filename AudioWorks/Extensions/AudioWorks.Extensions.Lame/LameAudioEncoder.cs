using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    [AudioEncoderExport("LameMP3")]
    public sealed class LameAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] Encoder _encoder;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                var result = new SettingInfoDictionary
                {
                    ["BitRate"] = new IntSettingInfo(8, 320),
                    ["ForceCBR"] = new BoolSettingInfo()
                };

                // Call the external ID3 encoder, if available
                var metadataEncoderFactory =
                    ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
                if (metadataEncoderFactory != null)
                    using (var export = metadataEncoderFactory.CreateExport())
                        foreach (var settingInfo in export.Value.SettingInfo)
                            result.Add(settingInfo.Key, settingInfo.Value);

                return result;
            }
        }

        public string FileExtension { get; } = ".mp3";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            // Call the external ID3 encoder, if available
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory != null)
                using (var export = metadataEncoderFactory.CreateExport())
                    export.Value.WriteMetadata(fileStream, metadata, settings);

            _encoder = new Encoder(fileStream);
            _encoder.SetChannels(info.Channels);
            _encoder.SetSampleRate(info.SampleRate);
            if (info.SampleCount > 0)
                _encoder.SetSampleCount((uint) info.SampleCount);

            // Use ABR unless ForceCBR is specified
            var forceCbr = false;
            if (!settings.TryGetValue("ForceCBR", out var forceCbrValue) || !(forceCbr = (bool) forceCbrValue))
                _encoder.SetVbrMode(VbrMode.Abr);

            if (settings.TryGetValue("BitRate", out var bitRateValue))
                if (forceCbr)
                    _encoder.SetBitRate((int) bitRateValue);
                else
                    _encoder.SetMeanBitRate((int) bitRateValue);

            _encoder.InitializeParameters();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleCollection samples)
        {
            if (samples.Frames <= 0) return;

            // If there is only one channel, set the right channel to null
            _encoder.Encode(samples[0], samples.Channels == 1 ? null : samples[1]);
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
        }
    }
}
