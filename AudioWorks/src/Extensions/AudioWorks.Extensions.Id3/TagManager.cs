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

using System;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    static class TagManager
    {
        internal static TagModel Deserialize(Stream stream)
        {
            var tagModel = new TagModel();
            tagModel.Header.Deserialize(stream); // load the ID3v2 header
            if (tagModel.Header.Version != 3 & tagModel.Header.Version != 4)
                throw new NotImplementedException("ID3v2 Version " + tagModel.Header.Version + " is not supported.");

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
                        throw new InvalidTagException("Data is missing after the header.");
                }

                uint rawSize;
                // load the extended header
                if (tagModel.Header.HasExtendedHeader)
                {
                    tagModel.ExtendedHeader.Deserialize(stream);
                    rawSize = id3TagSize - tagModel.ExtendedHeader.Size;
                    if (id3TagSize <= 0)
                        throw new InvalidTagException("Data is missing after the extended header.");
                }
                else
                    rawSize = id3TagSize;

                // Read the frames
                if (rawSize <= 0)
                    throw new InvalidTagException(
                        "No frames are present in the Tag, there must be at least one present.");

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
                        throw new InvalidTagException("Tag is corrupt, must be formed of complete frames.");

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
                            throw new InvalidFrameException(
                                "A frame is corrupt, it can't be larger than the available space remaining.");

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
            using (var memory = new MemoryStream())
            using (var writer = new BinaryWriter(memory, Encoding.UTF8, true))
            {
                var frameHelper = new FrameHelper(tagModel.Header);

                // Write the frames in binary format
                foreach (var frame in tagModel.Frames)
                {
                    //TODO: Do validations on tag name correctness
                    var frameId = new byte[4];
                    Encoding.UTF8.GetBytes(frame.FrameId, 0, 4, frameId, 0);
                    writer.Write(frameId); // Write the 4 byte text tag
                    var buffer = frameHelper.Make(frame, out var flags);
                    var frameSize = (uint) buffer.Length;

                    if (tagModel.Header.Version == 4)
                        frameSize = Sync.Safe(frameSize);

                    writer.Write(Swap.UInt32(frameSize));
                    writer.Write(Swap.UInt16(flags));
                    writer.Write(buffer);
                }

                var id3TagSize = (uint) memory.Position;

                // Skip the header 10 bytes for now, we will come back and write the Header
                // with the correct size once have the tag size + padding
                stream.Seek(10, SeekOrigin.Begin);

                // TODO: Add extended header handling
                if (tagModel.Header.Unsync)
                    id3TagSize += Sync.Safe(memory, stream, id3TagSize);
                else
                    memory.WriteTo(stream);

                // update the TagSize stored in the tagModel
                tagModel.Header.TagSize = id3TagSize;

                // If padding + tag size is too big, shrink the padding (rather than throwing an exception)
                tagModel.Header.PaddingSize =
                    Math.Min(tagModel.Header.PaddingSize, 0x10000000 - id3TagSize);

                // next write the padding of zeros, if any
                if (tagModel.Header.PaddingSize > 0)
                    for (var i = 0; i < tagModel.Header.PaddingSize; i++)
                        stream.WriteByte(0);

                // Now seek back to the start and write the header
                var position = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);
                tagModel.Header.Serialize(stream);

                // reset position to the end of the tag
                stream.Position = position;
            }
        }
    }
}