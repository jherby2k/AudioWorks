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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AudioWorks.Common;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Api
{
    sealed partial class EncodedPath
    {
        [GeneratedRegex(@"\{[^{]+\}")]
        private static partial Regex ReplacerRegex();

        // '[' and ']' are included because they don't work well in PowerShell
        static readonly char[] _invalidChars = [.. Path.GetInvalidFileNameChars(), '[', ']'];
        readonly string _encoded;

        internal EncodedPath(string encoded)
        {
            var validProperties = typeof(AudioMetadata).GetProperties()
                .Select(propertyInfo => propertyInfo.Name).ToArray();
            if (ReplacerRegex().Matches(encoded)
                .Select(match => match.Value[1..^1])
                .Except(validProperties).Any())
                throw new ArgumentException("Parameter contains one or more invalid metadata properties.",
                    nameof(encoded));

            _encoded = encoded;
        }

        internal string ReplaceWith(AudioMetadata metadata)
        {
            var result = ReplacerRegex().Replace(_encoded, match =>
            {
                var propertyName = match.Value[1..^1];
                var propertyValue = (string) typeof(AudioMetadata).GetProperty(propertyName)!.GetValue(metadata)!;

                if (string.IsNullOrEmpty(propertyValue))
                    return $"Unknown {propertyName}";

                var sanitizedPropertyValue =
                    new string([.. propertyValue.Where(character => !_invalidChars.Contains(character))]);

                // Remove any double spaces introduced in sanitization
                if (sanitizedPropertyValue.Contains("  ", StringComparison.Ordinal) &&
                    !propertyValue.Contains("  ", StringComparison.Ordinal))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ", StringComparison.Ordinal);

                return sanitizedPropertyValue;
            });

            LoggerManager.LoggerFactory.CreateLogger<EncodedPath>()
                .LogDebug("Replacing encoded string '{encoded}' with '{result}'.", _encoded, result);

            return result;
        }
    }
}
