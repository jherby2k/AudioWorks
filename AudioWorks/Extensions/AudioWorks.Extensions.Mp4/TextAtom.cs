using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TextAtom
    {
        [NotNull]
        public string Value { get; }

        internal TextAtom([NotNull] IReadOnlyCollection<byte> data)
        {
            Value = new string(Encoding.UTF8.GetChars(data.Skip(24).Take(data.Count - 24).ToArray()));
        }
    }
}