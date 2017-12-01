using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioInfoDecoderExport(".ogg")]
    public sealed class VorbisAudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            var buffer = new byte[4096];

            OggStream oggStream = null;
            SafeNativeMethods.VorbisCommentInit(out var vorbisComment);

            try
            {
                using (var sync = new OggSync())
                using (var decoder = new VorbisDecoder())
                {
                    OggPage page;

                    do
                    {
                        // Read from the buffer into a page:
                        while (!sync.PageOut(out page))
                        {
                            var bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                throw new AudioInvalidException("No Ogg stream was found.", stream.Name);

                            var nativeBuffer = sync.Buffer(bytesRead);
                            Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                            sync.Wrote(bytesRead);
                        }

                        if (oggStream == null)
                            oggStream = new OggStream(SafeNativeMethods.OggPageSerialNo(ref page));

                        oggStream.PageIn(ref page);

                        while (oggStream.PacketOut(out var packet))
                        {
                            if (!SafeNativeMethods.VorbisSynthesisIdHeader(ref packet))
                                throw new AudioUnsupportedException("Not a Vorbis file.", stream.Name);

                            decoder.HeaderIn(ref vorbisComment, ref packet);

                            var info = decoder.GetInfo();
                            return AudioInfo.CreateForLossy(
                                "Vorbis",
                                info.Channels,
                                info.Rate,
                                0,
                                info.BitrateNominal > 0 ? info.BitrateNominal : 0);
                        }
                    } while (!SafeNativeMethods.OggPageEos(ref page));

                    throw new AudioInvalidException("The end of the Ogg stream was reached without finding the header.",
                        stream.Name);
                }
            }
            finally
            {
                SafeNativeMethods.VorbisCommentClear(ref vorbisComment);
                oggStream?.Dispose();
            }
        }
    }
}
