using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Wave
{
    [AudioInfoDecoderExport(".wav")]
    class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
        enum FormatTag : ushort
        {
            WaveFormatPcm = 1,
            WaveFormatExtensible = 0xFFFE
        }

        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var reader = new RiffReader(stream))
            {
                try
                {
                    reader.Initialize();

                    if (stream.Length != reader.RiffChunkSize + 8)
                        throw new InvalidFileException("File is unexpectedly truncated.", stream.Name);

                    if (reader.ReadFourcc() != "WAVE")
                        throw new InvalidFileException("Not a Wave file.", stream.Name);

                    var fmtChunkSize = reader.SeekToChunk("fmt ");
                    if (fmtChunkSize == 0)
                        throw new InvalidFileException("Missing 'fmt' chunk.", stream.Name);

                    var isExtensible = false;
                    switch ((FormatTag)reader.ReadUInt16())
                    {
                        case FormatTag.WaveFormatPcm:
                            break;
                        case FormatTag.WaveFormatExtensible:
                            isExtensible = true;
                            break;
                        default:
                            throw new UnsupportedFileException("Only PCM wave files are supported.", stream.Name);
                    }

                    var channels = reader.ReadUInt16();
                    var sampleRate = reader.ReadUInt32();

                    // Ignore nAvgBytesPerSec
                    stream.Seek(4, SeekOrigin.Current);

                    var blockAlign = reader.ReadUInt16();

                    // Use wValidBitsPerSample if this is WAVE_FORMAT_EXTENSIBLE
                    if (isExtensible)
                        stream.Seek(4, SeekOrigin.Current);

                    return new AudioInfo("LPCM", channels, reader.ReadUInt16(), (int) sampleRate,
                        reader.SeekToChunk("data") / blockAlign);
                }
                catch (IOException e)
                {
                    throw new InvalidFileException(e.Message, stream.Name);
                }
            }
        }
    }
}
