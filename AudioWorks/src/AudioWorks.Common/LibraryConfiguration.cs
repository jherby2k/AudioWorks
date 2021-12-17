﻿/* Copyright © 2021 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Common
{
    sealed class LibraryConfiguration
    {
        internal static LibraryConfiguration GetDefaults() =>
            new()
            {
                AutomaticExtensionDownloads = true,
                AutomaticExtensionDownloadTimeout = 30,
                RequireSignedExtensions = true,
                TrustedFingerprints = new[] { "5D6F49B1DDD6D05F50200F18F1B3A08BE3DB94D6ECC78B88E79937EE76ADA703" },
                ExtensionRepository = "https://www.myget.org/F/audioworks-extensions-v5/api/v3/index.json",
                DefaultRepository = "https://api.nuget.org/v3/index.json"
            };

        public bool AutomaticExtensionDownloads { get; set; } = true;

        public int AutomaticExtensionDownloadTimeout { get; set; } = 30;

        public bool UsePreReleaseExtensions { get; set; }

        public bool RequireSignedExtensions { get; set; } = true;

        public IEnumerable<string>? TrustedFingerprints { get; set; }

        public string? ExtensionRepository { get; set; }

        public string? DefaultRepository { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AsDictionary()
        {
            foreach (var property in GetType().GetProperties())
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) &&
                    property.PropertyType != typeof(string))
                {
                    var index = 0;
                    if (property.GetValue(this) is not IEnumerable enumValue) continue;
                    foreach (var item in enumValue)
                    {
                        var value = item?.ToString();
                        if (value != null)
                            yield return new($"{property.Name}:{index++}", value);
                    }
                }
                else
                {
                    var value = property.GetValue(this)?.ToString();
                    if (value != null)
                        yield return new(property.Name, value);
                }
        }
    }
}
