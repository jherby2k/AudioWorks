﻿/* Copyright © 2020 Jeremy Herbison

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
using System.Runtime.Serialization;

namespace AudioWorks.Extensions.Id3
{
    [Serializable]
    public sealed class InvalidTagException : InvalidStructureException
    {
        public InvalidTagException()
        {
        }

        public InvalidTagException(string message)
            : base(message)
        {
        }

        public InvalidTagException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        InvalidTagException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}