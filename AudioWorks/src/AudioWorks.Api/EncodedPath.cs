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
    sealed class EncodedPath
    {
        static readonly Regex _replacer = new Regex(@"\{[^{]+\}");
        static readonly char[] _invalidChars = Path.GetInvalidFileNameChars();
        readonly string _encoded;

        internal EncodedPath(string encoded)
        {
            var validProperties = typeof(AudioMetadata).GetProperties()
                .Select(propertyInfo => propertyInfo.Name).ToArray();
            if (_replacer.Matches(encoded)
#if NETSTANDARD2_0
                .Cast<Match>().Select(match => match.Value.Substring(1, match.Value.Length - 2))
#else
                .Select(match => match.Value[1..^1])
#endif
                .Except(validProperties).Any())
                throw new ArgumentException("Parameter contains one or more invalid metadata properties.",
                    nameof(encoded));

            _encoded = encoded;
        }

        internal string ReplaceWith(AudioMetadata metadata)
        {
            var result = _replacer.Replace(_encoded, match =>
            {
#if NETSTANDARD2_0
                var propertyName = match.Value.Substring(1, match.Value.Length - 2);
#else
                var propertyName = match.Value[1..^1];
#endif
                // ReSharper disable once PossibleNullReferenceException
                var propertyValue = (string) typeof(AudioMetadata).GetProperty(propertyName).GetValue(metadata);

                if (string.IsNullOrEmpty(propertyValue))
                    return $"Unknown {propertyName}";

                var sanitizedPropertyValue =
                    new string(propertyValue.Where(character => !_invalidChars.Contains(character)).ToArray());

                // Remove any double spaces introduced in sanitization
#if NETSTANDARD2_0
                if (sanitizedPropertyValue.Contains("  ") && !propertyValue.Contains("  "))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ");
#else
                if (sanitizedPropertyValue.Contains("  ", StringComparison.Ordinal) &&
                    !propertyValue.Contains("  ", StringComparison.Ordinal))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ", StringComparison.Ordinal);
#endif

                return sanitizedPropertyValue;
            });

            LoggerManager.LoggerFactory.CreateLogger<EncodedPath>()
                .LogDebug("Replacing encoded string '{0}' with '{1}'.", _encoded, result);

            return result;
        }
    }
}
