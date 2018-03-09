using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    [AudioEncoderExport("LameMP3", "MPEG Audio Layer 3 via Lame")]
    public sealed class LameAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] Encoder _encoder;

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
            if (samples.Frames <= 0) return;

            // If there is only one channel, set the right channel to null
            _encoder.Encode(samples.GetSamples(0), samples.Channels == 1 ? null : samples.GetSamples(1));
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
