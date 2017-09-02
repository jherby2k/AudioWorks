using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Mp4
{
    [AudioInfoDecoderExport(".m4a")]
    class Mp4AudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            try
            {
                var mp4 = new Mp4(stream);

                mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stts");
                var stts = new SttsAtom(mp4.ReadAtom(mp4.CurrentAtom));

                var sampleCount = stts.PacketCount * stts.PacketSize;

                mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stsd", "mp4a", "esds");
                var esds = new EsdsAtom(mp4.ReadAtom(mp4.CurrentAtom));
                if (esds.SampleRate > 0)
                    return new AudioInfo("AAC", esds.Channels, 0, (int) esds.SampleRate, sampleCount);

                // Apple Lossless files have their own atom for storing audio info
                mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stsd", "alac");
                var alac = new AlacAtom(mp4.ReadAtom(mp4.CurrentAtom));
                return new AudioInfo("ALAC", alac.Channels, alac.BitsPerSample, (int) alac.SampleRate, sampleCount);
            }
            catch (EndOfStreamException e)
            {
                throw new AudioInvalidException(e.Message, stream.Name);
            }
        }
    }
}
