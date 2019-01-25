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
using System.Globalization;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests.DataTypes
{
    public sealed class TestSettingDictionary : SettingDictionary, IXunitSerializable
    {
        public void Deserialize([NotNull] IXunitSerializationInfo info)
        {
            foreach (var item in info.GetValue<string[]>("Items"))
            {
                var splitItem = item.Split('|');
                Add(splitItem[0],
                    // ReSharper disable once AssignNullToNotNullAttribute
                    Convert.ChangeType(splitItem[1], Type.GetType(splitItem[2]), CultureInfo.InvariantCulture));
            }
        }

        public void Serialize([NotNull] IXunitSerializationInfo info)
        {
            info.AddValue("Items", this.Select(item =>
                $"{item.Key}|{item.Value}|{item.Value.GetType().AssemblyQualifiedName}"
            ).ToArray());
        }
    }
}
