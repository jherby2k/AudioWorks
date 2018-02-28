using System;
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

                WriteFrames(samples.Frames);
            }
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

        void WritePage(OggPage page)
        {
            WritePointer(page.Header, page.HeaderLength);
            WritePointer(page.Body, page.BodyLength);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException",
            Justification = "_fileStream is always initialized first")]
        void WritePointer(IntPtr location, int length)
        {
            if (_buffer == null)
                _buffer = new byte[4096];

            var offset = 0;
            while (offset < length)
            {
                var bytesCopied = Math.Min(length - offset, _buffer.Length);
                Marshal.Copy(IntPtr.Add(location, offset), _buffer, 0, bytesCopied);
                _fileStream.Write(_buffer, 0, bytesCopied);
                offset += bytesCopied;
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException",
            Justification = "Initialize is always called first")]
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
    }
}
