﻿/* Copyright © 2018 Jeremy Herbison

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

namespace AudioWorks.Extensions.Mp4
{
    sealed class ReverseDnsAtom : WritableAtom
    {
        readonly byte[] _data;

        internal string Name
        {
            get
            {
                Span<char> charBuffer = stackalloc char[8];
                (CodePagesEncodingProvider.Instance.GetEncoding(1252) ?? Encoding.ASCII)
                    .GetChars(_data.AsSpan().Slice(48, 8), charBuffer);
                return new(charBuffer);
            }
        }

        internal ReverseDnsAtom(byte[] data) => _data = data;

        internal override void Write(Stream output) => output.Write(_data, 0, _data.Length);
    }
}