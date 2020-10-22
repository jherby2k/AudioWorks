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
        static readonly Dictionary<string, Type> _frameTypes = new Dictionary<string, Type>();

        static FrameFactory()
        {
            // Search the assembly for defined frame types
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (var frameAttribute in (FrameAttribute[]) type.GetCustomAttributes<FrameAttribute>(false))
                _frameTypes.Add(frameAttribute.FrameId, type);
        }

        internal static FrameBase Build(string frameId)
        {
            //Try to find the most specific frame first
            if (_frameTypes.TryGetValue(frameId, out var type))
                return (FrameBase) Activator.CreateInstance(type);

            //Get the T*** frame
            if (_frameTypes.TryGetValue(frameId.Substring(0, 1), out type))
                return (FrameBase) Activator.CreateInstance(type, frameId);

            throw new ArgumentException($"'{frameId}' is not a supported frame ID.", nameof(frameId));
        }
    }
}