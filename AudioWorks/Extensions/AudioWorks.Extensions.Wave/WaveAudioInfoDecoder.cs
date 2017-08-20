using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Wave
{
    [AudioInfoDecoderExport(".wav")]
    class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
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

                    switch (reader.ReadUInt16())
                    {
                        case 1:
                        case 0xFFFE:
                            break;
                        default:
                            throw new UnsupportedFileException("Only PCM wave files are supported.", stream.Name);
                    }

                    var channels = reader.ReadUInt16();
                    stream.Seek(10, SeekOrigin.Current);
                    var bitsPerSample = reader.ReadUInt16();

                    return new AudioInfo("LPCM", channels, bitsPerSample);
                }
                catch (IOException e)
                {
                    throw new InvalidFileException(e.Message, stream.Name);
                }
            }
        }
    }
}
