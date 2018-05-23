using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class ReverseDnsAtom : WritableAtom
    {
        [NotNull] readonly byte[] _data;

        [NotNull]
        internal string Name => new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
            .GetChars(_data.Skip(48).Take(8).ToArray()));

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