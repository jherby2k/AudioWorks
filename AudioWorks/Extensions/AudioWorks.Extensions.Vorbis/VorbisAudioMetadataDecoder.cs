using AudioWorks.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioMetadataDecoderExport(".ogg")]
    sealed class VorbisAudioMetadataDecoder : IAudioMetadataDecoder
    {
        public AudioMetadata ReadMetadata(FileStream stream)
        {
            var buffer = new byte[4096];

            NativeOggStream oggStream = null;
            SafeNativeMethods.VorbisCommentInitialize(out var vorbisComment);

            try
            {
                using (var sync = new NativeOggSync())
                using (var decoder = new NativeVorbisDecoder())
                {
                    OggPage page;

                    do
                    {
                        // Read from the buffer into a page
                        while (!sync.PageOut(out page))
                        {
                            var bytesRead = stream.Read(buffer, 0, buffer.Length);
                            var nativeBuffer = sync.Buffer(bytesRead);
                            Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                            sync.Wrote(bytesRead);
                        }

                        if (oggStream == null)
                            oggStream = new NativeOggStream(SafeNativeMethods.OggPageGetSerialNumber(ref page));

                        oggStream.PageIn(ref page);

                        while (oggStream.PacketOut(out var packet))
                        {
                            decoder.HeaderIn(ref vorbisComment, ref packet);

                            if (packet.PacketNumber == 1)
                                return new VorbisCommentToMetadataAdapter(vorbisComment);
                        }
                    } while (!SafeNativeMethods.OggPageEndOfStream(ref page));

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
