/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if !NETSTANDARD2_0
using System;
#endif
using System.IO;
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    static class TagManager
    {
        internal static TagModel Deserialize(Stream stream)
        {
            var tagModel = new TagModel();
            tagModel.Header.Deserialize(stream);
            if (tagModel.Header.Version != 3 & tagModel.Header.Version != 4)
                throw new AudioUnsupportedException("ID3v2 Version " + tagModel.Header.Version + " is not supported.");

            var id3TagSize = tagModel.Header.TagSize;

            MemoryStream? memory = null;
            try
            {
                if (tagModel.Header.Unsynchronisation)
                {
                    memory = new MemoryStream(new byte[stream.Length]);
                    id3TagSize -= Sync.Unsafe(stream, memory, id3TagSize);
                    stream = memory;
                    if (id3TagSize <= 0)
                        throw new AudioInvalidException("Data is missing after the header.");
                }

                uint rawSize;

                // Seek past the extended header
                if (tagModel.Header.HasExtendedHeader)
                {
                    tagModel.ExtendedHeader.Deserialize(stream);
                    rawSize = id3TagSize - 4 - tagModel.ExtendedHeader.Size;
                    if (id3TagSize <= 0)
                        throw new AudioInvalidException("Data is missing after the extended header.");
                }
                else
                    rawSize = id3TagSize;

                // Tags should have at least one frame
                if (rawSize <= 0)
                    throw new AudioInvalidException("No frames are present in the tag.");

                uint index = 0;
#if NETSTANDARD2_0
                var frameIdBuffer = new byte[4];
#else
                Span<byte> frameIdBuffer = stackalloc byte[4];
#endif

                while (index < rawSize)
                {
                    // Read one byte first, looking for padding
#if NETSTANDARD2_0
                    stream.Read(frameIdBuffer, 0, 1);
#else
                    stream.Read(frameIdBuffer.Slice(0, 1));
#endif

                    // We reached the padding
                    if (frameIdBuffer[0] == 0)
                    {
                        tagModel.Header.PaddingSize = rawSize - index;
                        stream.Seek(tagModel.Header.PaddingSize - 1, SeekOrigin.Current);

                        break;
                    }

                    // 10 is the minimum size of a valid frame
                    if (index + 10 > rawSize)
                        throw new AudioInvalidException("Tag is corrupt: incomplete frame.");

                    // Read the rest of the frame ID
#if NETSTANDARD2_0
                    stream.Read(frameIdBuffer, 1, 3);
#else
                    stream.Read(frameIdBuffer.Slice(1, 3));
#endif
                    index += 4;

                    using (var reader = new TagReader(stream))
                    {
                        var frameSize = tagModel.Header.Version == 4
                            ? reader.ReadUInt32SyncSafe()
                            : reader.ReadUInt32BigEndian();
                        index += 4;

                        // The size of the frame can't be larger than the available space
                        if (frameSize > rawSize - index)
                            throw new AudioInvalidException(
                                "A frame is corrupt: can't be larger than the available space remaining.");

                        var flags = new FrameFlags(reader.ReadUInt16BigEndian(), tagModel.Header.Version);
                        index += 2;

                        tagModel.Frames.Add(ReadFrame(reader,
#if NETSTANDARD2_0
                            Encoding.ASCII.GetString(frameIdBuffer, 0, 4), flags, frameSize));
#else
                            Encoding.ASCII.GetString(frameIdBuffer), flags, frameSize));
#endif

                        index += frameSize;
                    }
                }

                return tagModel;
            }
            finally
            {
                memory?.Close();
            }
        }

        static FrameBase ReadFrame(BinaryReader reader, string frameId, FrameFlags flags, uint frameSize)
        {
#if NETSTANDARD2_0
            var frameDataBuffer = reader.ReadBytes((int) frameSize);
#else
            // Use heap allocations for frames > 256kB (usually pictures)
            Span<byte> frameDataBuffer = frameSize < 0x40000
                ? stackalloc byte[(int) frameSize]
                : new byte[(int) frameSize];
            reader.Read(frameDataBuffer);
#endif
            return FrameFactory.Build(frameId, flags, frameDataBuffer);
        }

        internal static void Serialize(TagModel tagModel, Stream stream)
        {
            // Skip the header 10 bytes for now, we will come back and write the Header
            // with the correct size once have the tag size + padding
            stream.Seek(10, SeekOrigin.Begin);

            // Write the frames in binary format
            using (var writer = new TagWriter(stream))
                foreach (var frame in tagModel.Frames)
                {
#if NETSTANDARD2_0
                    writer.Write(frame.FrameId.ToCharArray());
#else
                    writer.Write(frame.FrameId.AsSpan());
#endif

                    // Skip the size bytes for now
                    var sizeIndex = stream.Position;
                    stream.Seek(4, SeekOrigin.Current);

                    // Set the FileAlter flag, if requested
                    writer.Write((short) (frame.FileAlter ? tagModel.Header.Version == 4 ? 0b0010_0000 : 0b0100_0000 : 0));

                    frame.Write(stream);
                    var frameSize = (uint) (stream.Position - sizeIndex - 6);

                    // Now update the size
                    stream.Seek(sizeIndex, SeekOrigin.Begin);
                    if (tagModel.Header.Version == 4)
                        writer.WriteSyncSafe(frameSize);
                    else
                        writer.WriteBigEndian(frameSize);
                    stream.Seek(2 + frameSize, SeekOrigin.Current);
                }

            // update the TagSize stored in the tagModel
            var id3TagSize = (uint) stream.Position - 10;
            tagModel.Header.TagSize = id3TagSize;

            // Write the padding
            for (var i = 0; i < tagModel.Header.PaddingSize; i++)
                stream.WriteByte(0);

            // Now seek back to the start and write the header
            var position = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);
            tagModel.Header.Serialize(stream);

            stream.Position = position;
        }
    }
}