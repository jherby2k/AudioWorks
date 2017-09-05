using AudioWorks.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioInfoDecoderExport(".ogg")]
    class VorbisAudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            var buffer = new byte[4096];

            using (var decoder = new NativeVorbisDecoder())
            {
                NativeOggStream oggStream = null;
                var vorbisComment = new VorbisComment();

                try
                {
                    vorbisComment = SafeNativeMethods.VorbisCommentInitialize();

                    using (var sync = new NativeOggSync())
                    {
                        OggPage page;

                        do
                        {
                            // Read from the buffer into a page:
                            while (sync.PageOut(out page))
                            {
                                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                                var nativeBuffer = sync.Buffer(bytesRead);
                                Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                                sync.Wrote(bytesRead);
                            }

                            if (oggStream == null)
                                oggStream = new NativeOggStream(SafeNativeMethods.OggPageGetSerialNumber(page));

                            oggStream.PageIn(page);

                            while (oggStream.PacketOut(out OggPacket packet))
                            {
                                decoder.HeaderIn(ref vorbisComment, ref packet);

                                var info = decoder.GetInfo();
                                return new AudioInfo("Vorbis", info.Channels, 0, info.Rate, 0);
                            }
                        } while (!SafeNativeMethods.OggPageEndOfStream(page));

                        throw new EndOfStreamException("TODO error message");
                    }
                }
                finally
                {
                    oggStream?.Dispose();

                    SafeNativeMethods.VorbisCommentClear(ref vorbisComment);
                }
            }
        }
    }
}
