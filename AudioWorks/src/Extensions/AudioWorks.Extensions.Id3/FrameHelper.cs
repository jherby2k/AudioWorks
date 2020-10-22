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
using ICSharpCode.SharpZipLib.Zip.Compression;
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

                    if (frame.Encryption)
                        throw new NotImplementedException(
                            "Encryption is not implemented, consequently it is not supported.");

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

        internal byte[] Make(FrameBase frame, out ushort flags)
        {
            flags = GetFlags(frame);
            var buffer = frame.Make();

            var memoryStream = new MemoryStream();
            var streamsToClose = new List<Stream>(2) { memoryStream };
            try
            {
                using (var writer = new BinaryWriter(memoryStream, Encoding.UTF8, true))
                {
                    if (frame.Group.HasValue)
                        writer.Write((byte) frame.Group);

                    if (frame.Compression)
                    {
                        switch (_version)
                        {
                            case 3:
                                writer.Write(Swap.Int32(buffer.Length));
                                break;
                            case 4:
                                writer.Write(Sync.UnsafeBigEndian(Swap.UInt32((uint) buffer.Length)));
                                break;
                            default:
                                throw new NotImplementedException($"ID3v2 Version {_version} is not supported.");
                        }

                        var buf = new byte[2048];
                        var deflater = new Deflater(Deflater.BEST_COMPRESSION);
                        deflater.SetInput(buffer, 0, buffer.Length);
                        deflater.Finish();

                        while (!deflater.IsNeedingInput)
                        {
                            var len = deflater.Deflate(buf, 0, buf.Length);
                            if (len <= 0) break;
                            memoryStream.Write(buf, 0, len);
                        }

                        //TODO: Skip and remove invalid frames.
                        if (!deflater.IsNeedingInput)
                            throw new InvalidFrameException($"Can't decompress frame '{frame.FrameId}' missing data");
                    }
                    else
                        memoryStream.Write(buffer, 0, buffer.Length);

                    //TODO: Encryption
                    if (frame.Encryption)
                        throw new NotImplementedException(
                            "Encryption is not implemented, consequently it is not supported.");

                    if (frame.Unsynchronisation)
                    {
                        var synchStream = new MemoryStream();
                        streamsToClose.Add(synchStream);
                        Sync.Unsafe(memoryStream, synchStream, (uint) memoryStream.Position);
                        memoryStream = synchStream;
                    }

                    return memoryStream.ToArray();
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
            frame.Encryption = GetEncryption(flags);
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

        ushort GetFlags(FrameBase frame)
        {
            ushort flags = 0;
            SetTagAlter(frame.TagAlter, ref flags);
            SetFileAlter(frame.FileAlter, ref flags);
            SetReadOnly(frame.ReadOnly, ref flags);
            SetGrouping(frame.Group.HasValue, ref flags);
            SetCompression(frame.Compression, ref flags);
            SetEncryption(frame.Encryption, ref flags);
            SetUnsynchronisation(frame.Unsynchronisation, ref flags);
            SetDataLength(frame.DataLength, ref flags);
            return flags;
        }

        void SetTagAlter(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    flags = value ? (ushort) (flags | 0x8000) : (ushort) (flags & unchecked((ushort) ~0x8000));
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x4000) : (ushort) (flags & unchecked((ushort) ~0x4000));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetFileAlter(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    flags = value ? (ushort) (flags | 0x4000) : (ushort) (flags & unchecked((ushort) ~0x4000));
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x2000) : (ushort) (flags & unchecked((ushort) ~0x2000));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetReadOnly(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                {
                    flags = value ? (ushort) (flags | 0x2000) : (ushort) (flags & unchecked((ushort) ~0x2000));
                    break;
                }
                case 4:
                {
                    flags = value ? (ushort) (flags | 0x1000) : (ushort) (flags & unchecked((ushort) ~0x1000));
                    break;
                }
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetGrouping(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    flags = value ? (ushort) (flags | 0x0020) : (ushort) (flags & unchecked((ushort) ~0x0020));
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x0040) : (ushort) (flags & unchecked((ushort) ~0x0040));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetCompression(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    flags = value ? (ushort) (flags | 0x0080) : (ushort) (flags & unchecked((ushort) ~0x0080));
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x0008) : (ushort) (flags & unchecked((ushort) ~0x0008));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetEncryption(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    flags = value ? (ushort) (flags | 0x0040) : (ushort) (flags & unchecked((ushort) ~0x0040));
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x0004) : (ushort) (flags & unchecked((ushort) ~0x0004));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetUnsynchronisation(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x0002) : (ushort) (flags & unchecked((ushort) ~0x0002));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }

        void SetDataLength(bool value, ref ushort flags)
        {
            switch (_version)
            {
                case 3:
                    break;
                case 4:
                    flags = value ? (ushort) (flags | 0x0001) : (ushort) (flags & unchecked((ushort) ~0x0001));
                    break;
                default:
                    throw new InvalidOperationException($"ID3v2 Version {_version} is not supported.");
            }
        }
    }
}