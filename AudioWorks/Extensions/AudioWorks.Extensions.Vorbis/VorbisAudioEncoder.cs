using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
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
        [CanBeNull] byte[] _buffer;

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
                WritePage(page);
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
            if (_buffer != null)
                ArrayPool<byte>.Shared.Return(_buffer);
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
                        WritePage(page);
                }
            }
        }

        void WritePage(OggPage page)
        {
#if (WINDOWS)
            WriteFromUnmanaged(page.Header, page.HeaderLength);
            WriteFromUnmanaged(page.Body, page.BodyLength);
#else
            WriteFromUnmanaged(page.Header, (int) page.HeaderLength);
            WriteFromUnmanaged(page.Body, (int) page.BodyLength);
#endif
        }

        void WriteFromUnmanaged(IntPtr location, int length)
        {
            if (_buffer == null)
                _buffer = ArrayPool<byte>.Shared.Rent(4096);

            var offset = 0;
            while (offset < length)
            {
                // ReSharper disable once PossibleNullReferenceException
                var bytesCopied = Math.Min(length - offset, _buffer.Length);
                Marshal.Copy(IntPtr.Add(location, offset), _buffer, 0, bytesCopied);
                // ReSharper disable once PossibleNullReferenceException
                _fileStream.Write(_buffer, 0, bytesCopied);
                offset += bytesCopied;
            }
        }
    }
}
