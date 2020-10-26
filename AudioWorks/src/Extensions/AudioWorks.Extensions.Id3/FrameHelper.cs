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
using System.Collections.Generic;
using System.IO;
using System.Text;
using AudioWorks.Common;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace AudioWorks.Extensions.Id3
{
    sealed class FrameHelper
    {
        readonly byte _version;

        internal FrameHelper(TagHeader header) => _version = header.Version;

        internal FrameBase Build(string frameId, ushort flags, byte[] buffer)
        {
            // Build a frame
            var frame = FrameFactory.Build(frameId);
            SetFlags(frame, flags);

            var index = 0;
            var size = (uint) buffer.Length;
            Stream stream = new MemoryStream(buffer, false);
            var streamsToClose = new List<Stream>(3) { stream };
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    if (GetGrouping(flags))
                    {
                        frame.Group = reader.ReadByte();
                        index++;
                    }

                    if (frame.Compression)
                    {
                        switch (_version)
                        {
                            case 3:
                                size = Swap.UInt32(reader.ReadUInt32());
                                break;
                            case 4:
                                size = Swap.UInt32(Sync.UnsafeBigEndian(reader.ReadUInt32()));
                                break;
                            default:
                                throw new NotImplementedException($"ID3v2 Version {_version} is not supported.");
                        }

                        index = 0;
                        stream = new InflaterInputStream(stream);
                        streamsToClose.Add(stream);
                    }

                    if (frame.Unsynchronisation)
                    {
                        var memoryStream = new MemoryStream();
                        streamsToClose.Add(memoryStream);
                        size = Sync.Unsafe(stream, memoryStream, size);
                        index = 0;
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        stream = memoryStream;
                    }

                    var frameBuffer = new byte[size - index];
                    stream.Read(frameBuffer, 0, (int) (size - index));
                    frame.Parse(frameBuffer);
                    return frame;
                }
            }
            finally
            {
                foreach (var streamToClose in streamsToClose)
                    streamToClose.Close();
            }
        }

        void SetFlags(FrameBase frame, ushort flags)
        {
            frame.TagAlter = GetTagAlter(flags);
            frame.FileAlter = GetFileAlter(flags);
            frame.ReadOnly = GetReadOnly(flags);
            frame.Compression = GetCompression(flags);
            if (GetEncryption(flags))
                throw new AudioUnsupportedException("Encrypted frames are not supported.");
            frame.Unsynchronisation = GetUnsynchronisation(flags);
            frame.DataLength = GetDataLength(flags);
        }

        bool GetTagAlter(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x8000) > 0;
                case 4:
                    return (flags & 0x4000) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetFileAlter(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x4000) > 0;
                case 4:
                    return (flags & 0x2000) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetReadOnly(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x2000) > 0;
                case 4:
                    return (flags & 0x1000) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetGrouping(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x0020) > 0;
                case 4:
                    return (flags & 0x0040) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetCompression(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x0080) > 0;
                case 4:
                    return (flags & 0x0008) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetEncryption(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return (flags & 0x0040) > 0;
                case 4:
                    return (flags & 0x0004) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetUnsynchronisation(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return false;
                case 4:
                    return (flags & 0x0002) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        bool GetDataLength(ushort flags)
        {
            switch (_version)
            {
                case 3:
                    return false;
                case 4:
                    return (flags & 0x0001) > 0;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }
    }
}