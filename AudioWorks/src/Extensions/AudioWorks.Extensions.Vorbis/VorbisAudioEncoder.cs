﻿/* Copyright © 2018 Jeremy Herbison

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
#if NETSTANDARD2_0
using System.Buffers;
#endif
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("Vorbis", "Ogg Vorbis")]
    sealed class VorbisAudioEncoder : IAudioEncoder, IDisposable
    {
#pragma warning disable CA2213 // Disposable fields should be disposed
        Stream? _outputStream;
#pragma warning restore CA2213 // Disposable fields should be disposed
        OggStream? _oggStream;
        VorbisEncoder? _encoder;
        Export<IAudioFilter>? _replayGainExport;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                var result = new SettingInfoDictionary
                {
                    ["SerialNumber"] = new IntSettingInfo(int.MinValue, int.MaxValue),
                    ["Quality"] = new IntSettingInfo(-1, 10),
                    ["BitRate"] = new IntSettingInfo(45, 500),
                    ["ForceCBR"] = new BoolSettingInfo()
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

        public string FileExtension { get; } = ".ogg";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            InitializeReplayGainFilter(info, metadata, settings);

            _oggStream = new OggStream(settings.TryGetValue("SerialNumber", out int serialNumber)
                ? serialNumber
                : new Random().Next());

            // Default to a quality setting of 5
            if (settings.TryGetValue("BitRate", out int bitRate))
            {
                if (settings.TryGetValue("ForceCBR", out bool cbr) && cbr)
                    _encoder = new VorbisEncoder(info.Channels, info.SampleRate,
                        bitRate * 1000, bitRate * 1000, bitRate * 1000);
                else
                    _encoder = new VorbisEncoder(info.Channels, info.SampleRate,
                        -1, bitRate * 1000, -1);
            }
            else
                _encoder = new VorbisEncoder(info.Channels, info.SampleRate,
                    settings.TryGetValue("Quality", out int quality)
                        ? quality / 10f
                        : 0.5f);

            // Generate the header
            using (var comment = new MetadataToVorbisCommentAdapter(metadata))
            {
                comment.HeaderOut(_encoder.DspState, out var first, out var second, out var third);
                _oggStream.PacketIn(first);
                _oggStream.PacketIn(second);
                _oggStream.PacketIn(third);
            }

            // Buffer the header in memory
            using (var tempStream = new MemoryStream())
            {
                _outputStream = tempStream;

                while (_oggStream.Flush(out var page))
                    WritePage(page);

                // Pre-allocate the whole stream (estimate worst case of 500kbps, plus the header)
                stream.SetLength(0xFA00 * (long) info.PlayLength.TotalSeconds + tempStream.Length);

                // Flush the headers to the output stream
                tempStream.WriteTo(stream);
            }

            _outputStream = stream;
        }

        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            if (_replayGainExport != null)
                samples = _replayGainExport.Value.Process(samples);

            // Request an unmanaged buffer for each channel, then copy the samples to them
            var buffers = new Span<IntPtr>(_encoder!.GetBuffer(samples.Frames).ToPointer(), samples.Channels);
            if (samples.Channels == 1)
            {
                var monoBuffer = new Span<float>(buffers[0].ToPointer(), samples.Frames);
                samples.CopyTo(monoBuffer);
            }
            else
            {
                var leftBuffer = new Span<float>(buffers[0].ToPointer(), samples.Frames);
                var rightBuffer = new Span<float>(buffers[1].ToPointer(), samples.Frames);
                samples.CopyTo(leftBuffer, rightBuffer);
            }

            WriteFrames(samples.Frames);
        }

        public void Finish()
        {
            WriteFrames(0);

            // The pre-allocation was based on an estimated bitrate
            _outputStream!.SetLength(_outputStream.Position);
        }

        public void Dispose()
        {
            _encoder?.Dispose();
            _oggStream?.Dispose();
            _replayGainExport?.Dispose();
        }

        void InitializeReplayGainFilter(AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            var filterFactory =
                ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
            if (filterFactory == null) return;

            _replayGainExport = filterFactory.CreateExport();
            _replayGainExport.Value.Initialize(info, metadata, settings);
        }

        void WriteFrames(int frames)
        {
            _encoder!.Wrote(frames);

            while (_encoder.BlockOut())
            {
                _encoder.Analysis(IntPtr.Zero);
                _encoder.AddBlock();

                while (_encoder.FlushPacket(out var packet))
                {
                    _oggStream!.PacketIn(packet);

                    while (_oggStream.PageOut(out var page))
                        WritePage(page);
                }
            }
        }

        void WritePage(in OggPage page)
        {
#if WINDOWS
            WriteFromUnmanaged(page.Header, page.HeaderLength);
            WriteFromUnmanaged(page.Body, page.BodyLength);
#else
            WriteFromUnmanaged(page.Header, (int) page.HeaderLength);
            WriteFromUnmanaged(page.Body, (int) page.BodyLength);
#endif
        }

        unsafe void WriteFromUnmanaged(IntPtr location, int length)
        {
#if NETSTANDARD2_0
            var buffer = ArrayPool<byte>.Shared.Rent(4096);
            try
            {
                var data = new Span<byte>(location.ToPointer(), length);
                var offset = 0;

                while (offset < length)
                {
                    var bytesCopied = Math.Min(length - offset, buffer.Length);
                    data.Slice(offset, bytesCopied).CopyTo(buffer);
                    _outputStream!.Write(buffer, 0, bytesCopied);
                    offset += bytesCopied;
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#else
            _outputStream!.Write(new Span<byte>(location.ToPointer(), length));
#endif
        }
    }
}
