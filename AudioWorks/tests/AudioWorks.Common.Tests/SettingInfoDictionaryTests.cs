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
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class SettingInfoDictionaryTests
    {
        [Fact(DisplayName = "ValidateSettings throws an exception if settings is null")]
        public void ValidateSettingsNullSettingsThrowsException() =>
            Assert.Throws<ArgumentNullException>(() =>
                SettingInfoDictionary.Empty.ValidateSettings(null!));

        [Fact(DisplayName = "ValidateSettings throws an exception if a setting is not in the dictionary")]
        public void ValidateSettingsSettingNotInDictionaryThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                SettingInfoDictionary.Empty.ValidateSettings(new() { ["Foo"] = "Bar" }));

        [Fact(DisplayName = "ValidateSettings throws an exception if a setting is the wrong type")]
        public void ValidateSettingsSettingWrongTypeThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new BoolSettingInfo() })
                    .ValidateSettings(new() { ["Foo"] = "Bar" }));

        [Fact(DisplayName = "ValidateSettings throws an exception if an integer setting is too high")]
        public void ValidateSettingsIntSettingTooHighThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new IntSettingInfo(0, 1) })
                    .ValidateSettings(new() { ["Foo"] = 2 }));

        [Fact(DisplayName = "ValidateSettings throws an exception if an integer setting is too low")]
        public void ValidateSettingsIntSettingTooLowThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new IntSettingInfo(0, 1) })
                    .ValidateSettings(new() { ["Foo"] = -1 }));

        [Fact(DisplayName = "ValidateSettings returns without error if an integer setting is in range")]
        public void ValidateSettingsIntInRangePasses()
        {
            new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new IntSettingInfo(0, 1) })
                .ValidateSettings(new() { ["Foo"] = 0 });
            Assert.True(true);
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if a string setting is not in the list")]
        public void ValidateSettingsStringSettingNotInListThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new StringSettingInfo("Valid") })
                    .ValidateSettings(new() { ["Foo"] = "NotValid" }));

        [Fact(DisplayName = "ValidateSettings returns without error if a string setting is in the list")]
        public void ValidateSettingsStringInListPasses()
        {
            new SettingInfoDictionary(new Dictionary<string, SettingInfo>() { ["Foo"] = new StringSettingInfo("Valid") })
                .ValidateSettings(new() { ["Foo"] = "Valid" });
            Assert.True(true);
        }
    }
}
