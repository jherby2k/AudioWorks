using System;
using System.IO;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Wave
{
    [AudioInfoDecoderExport(".wav")]
    public sealed class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var reader = new RiffReader(stream))
            {
                try
                {
                    reader.Initialize();

                    if (stream.Length != reader.RiffChunkSize + 8)
                        throw new AudioInvalidException("File is unexpectedly truncated.", stream.Name);

                    if (!string.Equals("WAVE", reader.ReadFourCc(), StringComparison.Ordinal))
                        throw new AudioInvalidException("Not a Wave file.", stream.Name);

                    var fmtChunkSize = reader.SeekToChunk("fmt ");
                    if (fmtChunkSize == 0)
                        throw new AudioInvalidException("Missing 'fmt' chunk.", stream.Name);

                    var isExtensible = false;
                    switch (reader.ReadUInt16())
                    {
                        // WAVE_FORMAT_PCM
                        case 1:
                            break;

                        // WAVE_FORMAT_EXTENSIBLE
                        case 0xFFFE:
                            isExtensible = true;
                            break;

                        default:
                            throw new AudioUnsupportedException("Only PCM wave files are supported.", stream.Name);
                    }

                    var channels = reader.ReadUInt16();
                    var sampleRate = reader.ReadUInt32();

                    // Ignore nAvgBytesPerSec
                    stream.Seek(4, SeekOrigin.Current);

                    var blockAlign = reader.ReadUInt16();

                    // Use wValidBitsPerSample if this is WAVE_FORMAT_EXTENSIBLE
                    if (isExtensible)
                        stream.Seek(4, SeekOrigin.Current);

                    return AudioInfo.CreateForLossless(
                        "LPCM",
                        channels,
                        reader.ReadUInt16(),
                        (int) sampleRate,
                        reader.SeekToChunk("data") / blockAlign);
                }
                catch (EndOfStreamException e)
                {
                    // The end of the stream was unexpectedly reached
                    throw new AudioInvalidException(e.Message, stream.Name);
                }
            }
        }
    }
}
