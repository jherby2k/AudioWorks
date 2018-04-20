using System;
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class SettingInfoDictionaryTests
    {
        [Fact(DisplayName = "ValidateSettings throws an exception if settings is null")]
        public void ValidateSettingsNullSettingsThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new SettingInfoDictionary().ValidateSettings(null));
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if a setting is not in the dictionary")]
        public void ValidateSettingsSettingNotInDictionaryThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary().ValidateSettings(new SettingDictionary { ["Foo"] = "Bar" }));
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if a setting is the wrong type")]
        public void ValidateSettingsSettingWrongTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary { ["Foo"] = new BoolSettingInfo() }
                    .ValidateSettings(new SettingDictionary { ["Foo"] = "Bar" }));
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if an integer setting is too high")]
        public void ValidateSettingsIntSettingTooHighThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary { ["Foo"] = new IntSettingInfo(0, 1) }
                    .ValidateSettings(new SettingDictionary { ["Foo"] = 2 }));
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if an integer setting is too low")]
        public void ValidateSettingsIntSettingTooLowThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary { ["Foo"] = new IntSettingInfo(0, 1) }
                    .ValidateSettings(new SettingDictionary { ["Foo"] = -1 }));
        }

        [Fact(DisplayName = "ValidateSettings returns without error if an integer setting is in range")]
        public void ValidateSettingsIntInRangePasses()
        {
            new SettingInfoDictionary { ["Foo"] = new IntSettingInfo(0, 1) }
                .ValidateSettings(new SettingDictionary { ["Foo"] = 0 });
            Assert.True(true);
        }

        [Fact(DisplayName = "ValidateSettings throws an exception if a string setting is not in the list")]
        public void ValidateSettingsStringSettingNotInListThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SettingInfoDictionary { ["Foo"] = new StringSettingInfo("Valid") }
                    .ValidateSettings(new SettingDictionary { ["Foo"] = "NotValid" }));
        }

        [Fact(DisplayName = "ValidateSettings returns without error if a string setting is in the list")]
        public void ValidateSettingsStringInListPasses()
        {
            new SettingInfoDictionary { ["Foo"] = new StringSettingInfo("Valid") }
                .ValidateSettings(new SettingDictionary { ["Foo"] = "Valid" });
            Assert.True(true);
        }
    }
}
