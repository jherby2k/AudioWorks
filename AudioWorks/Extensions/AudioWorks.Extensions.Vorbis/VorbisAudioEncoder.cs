using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioEncoderExport("Vorbis", "Ogg Vorbis")]
    public sealed class VorbisAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] FileStream _fileStream;
        [CanBeNull] OggStream _oggStream;
        [CanBeNull] VorbisEncoder _encoder;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["SerialNumber"] = new IntSettingInfo(int.MinValue, int.MaxValue),
            ["Quality"] = new IntSettingInfo(-1, 10),
            ["BitRate"] = new IntSettingInfo(32, 500),
            ["Managed"] = new BoolSettingInfo()
        };

        public string FileExtension { get; } = ".ogg";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _fileStream = fileStream;
            _oggStream = new OggStream(settings.TryGetValue("SerialNumber", out var serialNumberValue)
                ? (int) serialNumberValue
                : new Random().Next());

            // Default to a quality setting of 5
            if (settings.TryGetValue<int>("BitRate", out var bitRate))
                _encoder = new VorbisEncoder(info.Channels, info.SampleRate, bitRate * 1000,
                    settings.TryGetValue<bool>("Managed", out var managed) && managed);
            else
                _encoder = new VorbisEncoder(info.Channels, info.SampleRate,
                    settings.TryGetValue<int>("Quality", out var quality)
                        ? quality / 10f
                        : 0.5f);

            // Write the header
            using (var comment = new MetadataToVorbisCommentAdapter(metadata))
            {
                comment.HeaderOut(_encoder.DspState, out var first, out var second, out var third);
                _oggStream.PacketIn(ref first);
                _oggStream.PacketIn(ref second);
                _oggStream.PacketIn(ref third);
            }

            // ReSharper disable once PossibleNullReferenceException
            while (_oggStream.Flush(out var page))
                WritePage(page, _fileStream);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames <= 0) return;

            // Request an unmanaged buffer for each channel, then copy the samples to to them
            var buffers = new Span<IntPtr>(_encoder.GetBuffer(samples.Frames).ToPointer(), samples.Channels);
            for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
            {
                var channelBuffer = new Span<float>(buffers[channelIndex].ToPointer(), samples.Frames);
                samples.GetSamples(channelIndex).CopyTo(channelBuffer);
            }

            WriteFrames(samples.Frames);
        }

        public void Finish()
        {
            WriteFrames(0);
        }

        public void Dispose()
        {
            _encoder?.Dispose();
            _oggStream?.Dispose();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        void WriteFrames(int frames)
        {
            _encoder.Wrote(frames);

            while (_encoder.BlockOut())
            {
                _encoder.Analysis(IntPtr.Zero);
                _encoder.AddBlock();

                while (_encoder.FlushPacket(out OggPacket packet))
                {
                    _oggStream.PacketIn(ref packet);

                    while (_oggStream.PageOut(out OggPage page))
                        WritePage(page, _fileStream);
                }
            }
        }

        static unsafe void WritePage(OggPage page, [NotNull] Stream stream)
        {
#if (WINDOWS)
            stream.Write(new Span<byte>(page.Header.ToPointer(), page.HeaderLength).ToArray(), 0, page.HeaderLength);
            stream.Write(new Span<byte>(page.Body.ToPointer(), page.BodyLength).ToArray(), 0, page.BodyLength);
#else
            stream.Write(new Span<byte>(page.Header.ToPointer(), (int) page.HeaderLength).ToArray(), 0,
                (int) page.HeaderLength);
            stream.Write(new Span<byte>(page.Body.ToPointer(), (int) page.BodyLength).ToArray(), 0,
                (int) page.BodyLength);
#endif
        }
    }
}
