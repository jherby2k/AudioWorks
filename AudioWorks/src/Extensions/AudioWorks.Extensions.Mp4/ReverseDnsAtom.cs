#if NETCOREAPP2_1
using System;
#endif
using System.IO;
#if !NETCOREAPP2_1
using System.Linq;
#endif
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class ReverseDnsAtom : WritableAtom
    {
        [NotNull] readonly byte[] _data;

        [NotNull]
#if NETCOREAPP2_1
        internal string Name
        {
            get
            {
                Span<char> charBuffer = stackalloc char[8];
                CodePagesEncodingProvider.Instance.GetEncoding(1252)
                    .GetChars(_data.AsSpan().Slice(48, 8), charBuffer);
                return new string(charBuffer);
            }
        }
#else
        internal string Name => new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
            .GetChars(_data.Skip(48).Take(8).ToArray()));
#endif

        internal ReverseDnsAtom([NotNull] byte[] data)
        {
            _data = data;
        }

        internal override void Write(Stream output)
        {
            output.Write(_data, 0, _data.Length);
        }
    }
}