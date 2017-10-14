using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4
    {
        [NotNull] readonly Stream _stream;
        [NotNull] readonly Stack<AtomInfo> _atomInfoStack = new Stack<AtomInfo>();

        [NotNull]
        internal AtomInfo CurrentAtom => _atomInfoStack.Peek();

        internal Mp4([NotNull] Stream stream)
        {
            _stream = stream;
        }

        [NotNull, ItemNotNull]
        internal AtomInfo[] GetChildAtomInfo()
        {
            var result = new List<AtomInfo>();

            using (var reader = new Mp4Reader(_stream))
            {
                _stream.Position = _atomInfoStack.Count == 0 ? 0 : _atomInfoStack.Peek().Start + 8;

                while (_stream.Position < (_atomInfoStack.Count == 0 ? _stream.Length : _atomInfoStack.Peek().End))
                {
                    var childAtom = new AtomInfo(
                        (uint) _stream.Position,
                        reader.ReadUInt32BigEndian(),
                        reader.ReadFourCc());
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
                        var subAtom = new AtomInfo((uint)_stream.Position, reader.ReadUInt32BigEndian(),
                            reader.ReadFourCc());

                        if (subAtom.End > _stream.Length)
                            throw new EndOfStreamException($"{fourCc} atom is missing.");

                        if (string.CompareOrdinal(subAtom.FourCc, fourCc) == 0)
                        {
                            _atomInfoStack.Push(subAtom);

                            // Some containers also contain data, which needs to be skipped:
                            switch (fourCc)
                            {
                                case "meta":
                                    _stream.Seek(4, SeekOrigin.Current);
                                    break;
                                case "stsd":
                                    _stream.Seek(8, SeekOrigin.Current);
                                    break;
                                case "mp4a":
                                    _stream.Seek(28, SeekOrigin.Current);
                                    break;
                            }

                            break;
                        }

                        _stream.Seek(subAtom.End, SeekOrigin.Begin);

                    } while (_stream.Position < (_atomInfoStack.Count == 0 ? _stream.Length : _atomInfoStack.Peek().End));
            }

            return string.CompareOrdinal(_atomInfoStack.Peek().FourCc, hierarchy.Last()) == 0;
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
    }
}
