using JetBrains.Annotations;
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
        public AtomInfo CurrentAtom => _atomInfoStack.Peek();

        public Mp4([NotNull] Stream stream)
        {
            _stream = stream;
        }

        internal bool DescendToAtom([NotNull, ItemNotNull] params string[] hierarchy)
        {
            _stream.Position = 0;
            _atomInfoStack.Clear();

            using (var reader = new Mp4Reader(_stream))
            {
                foreach (var fourCc in hierarchy)
                    do
                    {
                        var subAtom = new AtomInfo((uint)_stream.Position, reader.ReadUInt32BigEndian(),
                            reader.ReadFourCc());

                        if (subAtom.End > _stream.Length)
                            throw new EndOfStreamException();

                        if (subAtom.FourCc == fourCc)
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

            return _atomInfoStack.Peek().FourCc == hierarchy.Last();
        }

        [NotNull]
        internal byte[] ReadAtom([NotNull] AtomInfo atom)
        {
            _stream.Position = atom.Start;

            using (var reader = new Mp4Reader(_stream))
                return reader.ReadBytes((int)atom.Size);
        }
    }
}
