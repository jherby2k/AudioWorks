using System;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioMetadataEncoderExport(".ogg")]
    public sealed class VorbisAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            // This buffer is used for both reading and writing:
            var buffer = new byte[4096];

            using (var tempStream = new MemoryStream())
            {
                OggStream inputOggStream = null;
                OggStream outputOggStream = null;

                try
                {
                    using (var sync = new OggSync())
                    {
                        OggPage inPage;

                        do
                        {
                            // Read from the buffer into a page
                            while (!sync.PageOut(out inPage))
                            {
                                var nativeBuffer = sync.Buffer(buffer.Length);
                                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                                Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                                sync.Wrote(bytesRead);
                            }

                            if (inputOggStream == null)
                                inputOggStream = new OggStream(SafeNativeMethods.OggPageSerialNo(ref inPage));
                            if (outputOggStream == null)
                                outputOggStream = new OggStream(inputOggStream.SerialNumber);

                            inputOggStream.PageIn(ref inPage);

                            while (inputOggStream.PacketOut(out var packet))
                            {
                                // Substitute the new comment packet
                                if (packet.PacketNumber == 1)
                                    using (var adapter = new MetadataToVorbisCommentAdapter(metadata))
                                    {
                                        adapter.HeaderOut(out var commentPacket);
                                        outputOggStream.PacketIn(ref commentPacket);
                                    }
                                else
                                    outputOggStream.PacketIn(ref packet);

                                // Page out each packet, flushing at the end of the header
                                if (packet.PacketNumber == 2)
                                    while (outputOggStream.Flush(out var outPage))
                                        WritePage(outPage, tempStream);
                                else
                                    while (outputOggStream.PageOut(out var outPage))
                                        WritePage(outPage, tempStream);
                            }
                        } while (!SafeNativeMethods.OggPageEos(ref inPage));

                        // Once the end of the input is reached, overwrite the original file and return
                        stream.Position = 0;
                        stream.SetLength(tempStream.Length);
                        tempStream.Position = 0;
                        tempStream.CopyTo(stream);
                    }
                }
                finally
                {
                    inputOggStream?.Dispose();
                    outputOggStream?.Dispose();
                }
            }
        }

        static unsafe void WritePage(OggPage page, [NotNull] Stream stream)
        {
#if (WINDOWS)
            stream.Write(new Span<byte>(page.Header.ToPointer(), page.HeaderLength).ToArray(), 0, page.HeaderLength);
            stream.Write(new Span<byte>(page.Body.ToPointer(), page.BodyLength).ToArray(), 0, page.BodyLength);
#else
            stream.Write(new Span<byte>(page.Header.ToPointer(), (int) page.HeaderLength).ToArray(), 0,
                (int) page.HeaderLength);
            stream.Write(new Span<byte>(page.Body.ToPointer(), (int) page.BodyLength).ToArray(), 0,
                (int) page.BodyLength);
#endif
        }
    }
}
