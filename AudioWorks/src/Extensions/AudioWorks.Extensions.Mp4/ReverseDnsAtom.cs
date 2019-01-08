/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if !NETSTANDARD2_0
using System;
#endif
using System.IO;
#if NETSTANDARD2_0
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
#if NETSTANDARD2_0
        internal string Name => new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
            .GetChars(_data.Skip(48).Take(8).ToArray()));
#else
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