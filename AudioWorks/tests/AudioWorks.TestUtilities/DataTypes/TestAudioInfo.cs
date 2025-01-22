/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using Xunit.Sdk;

namespace AudioWorks.TestUtilities.DataTypes
{
    public sealed class TestAudioInfo : IXunitSerializable
    {
        public string Format { get; init; } = string.Empty;

        public int Channels { get; init; }

        public int BitsPerSample { get; init; }

        public int SampleRate { get; init; }

        public int BitRate { get; init; }

        public long SampleCount { get; init; }

        public void Deserialize(IXunitSerializationInfo info)
        {
            foreach (var property in GetType().GetProperties())
                property.SetValue(this, info.GetValue(property.Name));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            foreach (var property in GetType().GetProperties())
                info.AddValue(property.Name, property.GetValue(this));
        }
    }
}
