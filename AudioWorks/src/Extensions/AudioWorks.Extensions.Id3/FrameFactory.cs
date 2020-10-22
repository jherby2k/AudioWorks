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
using System.Reflection;

namespace AudioWorks.Extensions.Id3
{
    static class FrameFactory
    {
        static readonly Dictionary<string, Type> _frames = new Dictionary<string, Type>();

        static FrameFactory()
        {
            // Scan the assembly for frames configured
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (var frameAttribute in (FrameAttribute[]) type.GetCustomAttributes<FrameAttribute>(false))
                _frames.Add(frameAttribute.FrameId, type);
        }

        internal static FrameBase Build(string frameId)
        {
            if (frameId.Length != 4)
                throw new InvalidTagException($"Invalid frame type: '{frameId}', it must be 4 characters long.");

            //Try to find the most specific frame first
            if (_frames.TryGetValue(frameId, out var type))
                return (FrameBase) Activator.CreateInstance(type, frameId);

            //Get the T*** or U*** frame, they are all identical except for the user defined frames 'TXXX' and 'WXXX'.
            if (_frames.TryGetValue(frameId.Substring(0, 1), out type))
                return (FrameBase) Activator.CreateInstance(type, frameId);

            // Unknown tag, used as a container for unknown frames
            return new FrameUnknown(frameId);
        }
    }
}