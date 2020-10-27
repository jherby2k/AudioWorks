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
            tagModel.Header.Deserialize(stream); // load the ID3v2 header
            if (tagModel.Header.Version != 3 & tagModel.Header.Version != 4)
                throw new AudioUnsupportedException("ID3v2 Version " + tagModel.Header.Version + " is not supported.");

            var id3TagSize = tagModel.Header.TagSize;

            MemoryStream? memory = null;
            try
            {
                if (tagModel.Header.Unsync)
                {
                    memory = new MemoryStream();
                    id3TagSize -= Sync.Unsafe(stream, memory, id3TagSize);
                    stream = memory; // This is now the stream
                    if (id3TagSize <= 0)
                        throw new AudioInvalidException("Data is missing after the header.");
                }

                uint rawSize;
                // load the extended header
                if (tagModel.Header.HasExtendedHeader)
                {
                    tagModel.ExtendedHeader.Deserialize(stream);
                    rawSize = id3TagSize - tagModel.ExtendedHeader.Size;
                    if (id3TagSize <= 0)
                        throw new AudioInvalidException("Data is missing after the extended header.");
                }
                else
                    rawSize = id3TagSize;

                // Read the frames
                if (rawSize <= 0)
                    throw new AudioInvalidException("No frames are present in the tag.");

                // Load the tag frames
                uint index = 0;
                var frameHelper = new FrameHelper(tagModel.Header);
                // repeat while there is at least one complete frame available, 10 is the minimum size of a valid frame
                // but what happens when there's only, say, 5 bytes of padding?
                // we need to read a single byte to inspect for padding, then if not, read a whole tag.
                while (index < rawSize)
                {
                    var frameId = new byte[4];
                    stream.Read(frameId, 0, 1);

                    // We reached the padding
                    if (frameId[0] == 0)
                    {
                        tagModel.Header.PaddingSize = rawSize - index;
                        stream.Seek(tagModel.Header.PaddingSize - 1, SeekOrigin.Current);

                        break;
                    }

                    // 10 is the minimum size of a valid frame;
                    // we read one already, if less than 9 chars left it's an error.
                    if (index + 10 > rawSize)
                        throw new AudioInvalidException("Tag is corrupt: must be formed of complete frames.");

                    // read another 3 chars
                    stream.Read(frameId, 1, 3);
                    index += 4; // have read 4 bytes
                    //TODO: Validate key valid ranges
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                    {
                        var frameSize = Swap.UInt32(reader.ReadUInt32());
                        index += 4; // have read 4 bytes
                        // ID3v2.4 now has sync-safe sizes
                        if (tagModel.Header.Version == 4)
                            frameSize = Sync.Unsafe(frameSize);

                        // The size of the frame can't be larger than the available space
                        if (frameSize > rawSize - index)
                            throw new AudioInvalidException(
                                "A frame is corrupt: can't be larger than the available space remaining.");

                        var flags = Swap.UInt16(reader.ReadUInt16());
                        index += 2; // read 2 bytes
                        var frameData = new byte[frameSize];
                        reader.Read(frameData, 0, (int) frameSize);
                        index += frameSize; // read more bytes
                        tagModel.Frames.Add(frameHelper.Build(Encoding.UTF8.GetString(frameId, 0, 4), flags, frameData));
                    }
                }

                return tagModel;
            }
            finally
            {
                memory?.Close();
            }
        }

        internal static void Serialize(TagModel tagModel, Stream stream)
        {
            // Skip the header 10 bytes for now, we will come back and write the Header
            // with the correct size once have the tag size + padding
            stream.Seek(10, SeekOrigin.Begin);

            // Write the frames in binary format
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true))
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
                    writer.Write((short) (frame.FileAlter ? tagModel.Header.Version == 4 ? 0x20 : 0x40 : 0));

                    frame.Write(stream);
                    var frameSize = (uint) (stream.Position - sizeIndex - 6);
                    if (tagModel.Header.Version == 4)
                        frameSize = Sync.Safe(frameSize);

                    // Now update the size
                    stream.Seek(sizeIndex, SeekOrigin.Begin);
                    writer.Write(Swap.UInt32(frameSize));
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