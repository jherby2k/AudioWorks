using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
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
            if (info.SampleCount > 0)
                _encoder.SetTotalSamplesEstimate((ulong) info.SampleCount);

            // Use a default compression level of 5
            _encoder.SetCompressionLevel(
                settings.TryGetValue<int>("CompressionLevel", out var compressionLevel)
                    ? (uint) compressionLevel
                    : 5);

            // Use a default seek point interval of 10 seconds
            if (!settings.TryGetValue<int>("SeekPointInterval", out var seekPointInterval))
                seekPointInterval = info.SampleCount > 0 ? 10 : 0;
            if (seekPointInterval > 0)
                _metadataBlocks.Add(new SeekTableBlock(
                    (uint) Math.Ceiling(info.PlayLength.TotalSeconds / seekPointInterval), (ulong) info.SampleCount));

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

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            var bufferSize = samples.Frames * samples.Channels;
            var bufferAddress = Marshal.AllocHGlobal(bufferSize * Marshal.SizeOf<int>());
            try
            {
                var buffer = new Span<int>(bufferAddress.ToPointer(), bufferSize);
                samples.CopyToInterleaved(buffer, _bitsPerSample);

                _encoder.ProcessInterleaved(bufferAddress, (uint) samples.Frames);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferAddress);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Finish()
        {
            _encoder.Finish();
            foreach (var block in _metadataBlocks)
                block.Dispose();
            //TODO check the result and throw an exception if necessary.
        }

        public void Dispose()
        {
            _encoder?.Dispose();
        }
    }
}
