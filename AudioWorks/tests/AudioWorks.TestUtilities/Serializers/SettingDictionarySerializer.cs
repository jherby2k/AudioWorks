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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AudioWorks.Common;
using Xunit.Sdk;

namespace AudioWorks.TestUtilities.Serializers
{
    public sealed class SettingDictionarySerializer : IXunitSerializer
    {
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

        public string Serialize(object value) => JsonSerializer.Serialize((SettingDictionary) value);

        public object Deserialize(Type type, string serializedValue)
        {
            var intermediate = JsonSerializer.Deserialize<IDictionary<string, JsonElement>>(serializedValue) ??
                               new Dictionary<string, JsonElement>();
            var result = new SettingDictionary();

            // System.Text.Json does not support polymorphic object deserialization by design, so extract known types only
            foreach (var item in intermediate)
                if (item.Value.TryGetInt32(out var intValue))
                    result.Add(item.Key, intValue);
                else if (item.Value.TryGetDateTime(out var dateValue))
                    result.Add(item.Key, dateValue);
                else
                {
                    var stringValue = item.Value.ToString();
                    if (bool.TryParse(stringValue, out var boolValue))
                        result.Add(item.Key, boolValue);
                    else
                        result.Add(item.Key, stringValue);
                }

            return result;
        }
    }
}
