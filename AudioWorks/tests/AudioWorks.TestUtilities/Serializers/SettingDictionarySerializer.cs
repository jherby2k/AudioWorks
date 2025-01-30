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

using System;
using System.Diagnostics.CodeAnalysis;
using AudioWorks.Common;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace AudioWorks.TestUtilities.Serializers
{
    public sealed class SettingDictionarySerializer : IXunitSerializer
    {
#pragma warning disable CA2326
        static readonly JsonSerializerSettings _settings = new() { TypeNameHandling = TypeNameHandling.Auto };
#pragma warning restore CA2326

        public bool IsSerializable(Type type, object? value, [NotNullWhen(false)] out string? failureReason)
        {
            if (type == typeof(SettingDictionary))
            {
                failureReason = string.Empty;
                return true;
            }

            failureReason = "Not a SettingDictionary";
            return false;
        }

        public string Serialize(object value) => JsonConvert.SerializeObject(value, _settings);

        public object Deserialize(Type type, string serializedValue) =>
            JsonConvert.DeserializeObject(serializedValue, _settings) ?? new object();
    }
}
