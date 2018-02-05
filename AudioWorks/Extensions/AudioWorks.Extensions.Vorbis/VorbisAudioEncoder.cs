using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioEncoderExport("Vorbis")]
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
            if (settings.TryGetValue("BitRate", out var bitRateValue))
                _encoder = new VorbisEncoder(info.Channels, info.SampleRate, (int) bitRateValue * 1000,
                    settings.TryGetValue("Managed", out var managedValue) && (bool) managedValue);
            else
                _encoder = new VorbisEncoder(info.Channels, info.SampleRate,
                    settings.TryGetValue("Quality", out var qualityValue)
                        ? (int) qualityValue / 10f
                        : 0.5f);

            // Write the header
            using (var comment = new MetadataToVorbisCommentAdapter(metadata))
            {
                comment.HeaderOut(_encoder.DspState, out var first, out var second, out var third);
                _oggStream.PacketIn(ref first);
                _oggStream.PacketIn(ref second);
                _oggStream.PacketIn(ref third);
            }

            while (_oggStream.Flush(out var page))
                WritePage(page, _fileStream);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException",
            Justification = "Initialize is always called first")]
        public void Submit(SampleCollection samples)
        {
            if (samples.Frames > 0)
            {
                // Request an unmanaged buffer, then copy the samples to it
                var buffers = new IntPtr[samples.Channels];
                Marshal.Copy(_encoder.GetBuffer(samples.Frames), buffers, 0, buffers.Length);

                for (var i = 0; i < samples.Channels; i++)
                    Marshal.Copy(samples[i], 0, buffers[i], samples[i].Length);

                WriteToFileStream(samples.Frames);
            }
        }

        public void Finish()
        {
            WriteToFileStream(0);
        }

        public void Dispose()
        {
            _encoder?.Dispose();
            _oggStream?.Dispose();
        }

        void WritePage(OggPage page, [NotNull] Stream stream)
        {
            WritePointer(page.Header, page.HeaderLength, stream);
            WritePointer(page.Body, page.BodyLength, stream);
        }

        void WritePointer(IntPtr location, int length, [NotNull] Stream stream)
        {
            if (_buffer == null)
                _buffer = new byte[4096];

            var offset = 0;
            while (offset < length)
            {
                var bytesCopied = Math.Min(length - offset, _buffer.Length);
                Marshal.Copy(IntPtr.Add(location, offset), _buffer, 0, bytesCopied);
                stream.Write(_buffer, 0, bytesCopied);
                offset += bytesCopied;
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException",
            Justification = "Initialize is always called first")]
        void WriteToFileStream(int frames)
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
    }
}
