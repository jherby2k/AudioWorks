/* Copyright © 2018 Jeremy Herbison

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
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Model
    {
        [NotNull] readonly Stream _stream;
        [NotNull] readonly Stack<AtomInfo> _atomInfoStack = new Stack<AtomInfo>();

        [NotNull]
        internal AtomInfo CurrentAtom => _atomInfoStack.Peek();

        internal Mp4Model([NotNull] Stream stream)
        {
            _stream = stream;
        }

        [NotNull, ItemNotNull]
        internal AtomInfo[] GetChildAtomInfo()
        {
            var result = new List<AtomInfo>();

            using (var reader = new Mp4Reader(_stream))
            {
                _stream.Position = _atomInfoStack.Count == 0
                    ? 0
                    : _atomInfoStack.Peek().Start + 8 + GetDataLength(_atomInfoStack.Peek().FourCc);

                while (_stream.Position < (_atomInfoStack.Count == 0 ? _stream.Length : _atomInfoStack.Peek().End))
                {
                    var childAtom = new AtomInfo(
                        (uint) _stream.Position,
                        reader.ReadUInt32BigEndian(),
                        reader.ReadFourCc());

                    // The stream might be padded with empty space
                    if (childAtom.FourCc.Equals("\0\0\0\0", StringComparison.Ordinal))
                    {
                        _stream.Position = result.Count > 0 ? result.Last().End : 0;
                        break;
                    }

                    result.Add(childAtom);
                    _stream.Position = childAtom.End;
                }
            }

            return result.ToArray();
        }

        internal bool DescendToAtom([NotNull, ItemNotNull] params string[] hierarchy)
        {
            Reset();

            using (var reader = new Mp4Reader(_stream))
            {
                foreach (var fourCc in hierarchy)
                    do
                    {
                        var subAtom = new AtomInfo((uint) _stream.Position, reader.ReadUInt32BigEndian(),
                            reader.ReadFourCc());

                        if (subAtom.End > _stream.Length)
                            throw new EndOfStreamException($"{fourCc} atom is missing.");

                        if (fourCc.Equals(subAtom.FourCc, StringComparison.Ordinal))
                        {
                            _atomInfoStack.Push(subAtom);
                            _stream.Seek(GetDataLength(fourCc), SeekOrigin.Current);
                            break;
                        }

                        _stream.Position = subAtom.End;

                    } while (_stream.Position < (_atomInfoStack.Count == 0 ? _stream.Length : _atomInfoStack.Peek().End));
            }

            return hierarchy.Last().Equals(_atomInfoStack.Peek().FourCc, StringComparison.Ordinal);
        }

        internal void Reset()
        {
            _stream.Position = 0;
            _atomInfoStack.Clear();
        }

        [NotNull]
        internal byte[] ReadAtom([NotNull] AtomInfo atom)
        {
            _stream.Position = atom.Start;

            using (var reader = new Mp4Reader(_stream))
                return reader.ReadBytes((int)atom.Size);
        }

        internal void CopyAtom([NotNull] AtomInfo atom, [NotNull] Stream output)
        {
            _stream.Position = atom.Start;

            var count = (int) atom.Size;
            var buffer = new byte[1024];
            do
            {
                var read = _stream.Read(buffer, 0, Math.Min(buffer.Length, count));
                output.Write(buffer, 0, read);
                count -= read;
            } while (count > 0);
        }

        internal void UpdateAtomSizes(uint increase)
        {
            if (_atomInfoStack.Count <= 0) return;

            using (var writer = new Mp4Writer(_stream))
            {
                do
                {
                    var currentAtom = _atomInfoStack.Pop();
                    _stream.Position = currentAtom.Start;
                    writer.WriteBigEndian(currentAtom.Size + increase);
                } while (_atomInfoStack.Count > 0);
            }
        }

        internal void UpdateStco(uint offset)
        {
            DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stco");
            _stream.Seek(4, SeekOrigin.Current);

            using (var reader = new Mp4Reader(_stream))
            using (var writer = new Mp4Writer(_stream))
            {
                var count = reader.ReadUInt32BigEndian();
                var dataStart = _stream.Position;

                for (var i = 0; i < count; i++)
                {
                    _stream.Position = dataStart + i * 4;
                    var value = reader.ReadUInt32BigEndian();
                    _stream.Seek(-4, SeekOrigin.Current);
                    writer.WriteBigEndian(value + offset);
                }
            }
        }

        internal void UpdateTimeStamps(DateTime? creationTime, DateTime? modificationTime)
        {
            UpdateTimeStamp(new[] { "moov", "mvhd" }, creationTime, modificationTime);
            UpdateTimeStamp(new[] { "moov", "trak", "tkhd" }, creationTime, modificationTime);
            UpdateTimeStamp(new[] { "moov", "trak", "mdia", "mdhd" }, creationTime, modificationTime);
        }

        void UpdateTimeStamp(
            [NotNull, ItemNotNull] string[] hierarchy,
            DateTime? creationTime,
            DateTime? modificationTime)
        {
            DescendToAtom(hierarchy);

            var version = _stream.ReadByte();
            _stream.Seek(3, SeekOrigin.Current);

            var epoch = new DateTime(1904, 1, 1);
            var creationSeconds = creationTime?.Subtract(epoch).TotalSeconds ?? 0;
            var modificationSeconds = modificationTime?.Subtract(epoch).TotalSeconds ?? 0;

            using (var writer = new Mp4Writer(_stream))
            {
                if (version == 0)
                {
                    if (creationTime.HasValue)
                        writer.WriteBigEndian((uint) creationSeconds);
                    else
                        writer.Seek(4, SeekOrigin.Current);

                    if (modificationTime.HasValue)
                        writer.WriteBigEndian((uint) modificationSeconds);
                }
                else
                {
                    if (creationTime.HasValue)
                        writer.WriteBigEndian((ulong) creationSeconds);
                    else
                        writer.Seek(8, SeekOrigin.Current);

                    if (modificationTime.HasValue)
                        writer.WriteBigEndian((ulong) modificationSeconds);
                }
            }
        }

        static int GetDataLength(string fourCc)
        {
            // Some containers also contain data, which needs to be skipped
            switch (fourCc)
            {
                case "meta":
                    return 4;
                case "stsd":
                    return 8;
                case "mp4a":
                    return 28;
                default:
                    return 0;
            }
        }
    }
}
