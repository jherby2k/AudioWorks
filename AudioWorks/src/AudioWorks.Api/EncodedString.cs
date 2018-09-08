using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Api
{
    sealed class EncodedPath
    {
        [NotNull] static readonly Regex _replacer = new Regex(@"\{[^{]+\}");
        [NotNull] static readonly char[] _invalidChars = Path.GetInvalidFileNameChars();
        [NotNull] readonly string _encoded;

        internal EncodedPath([NotNull] string encoded)
        {
            var validProperties = typeof(AudioMetadata).GetProperties()
                .Select(propertyInfo => propertyInfo.Name).ToArray();
            if (_replacer.Matches(encoded)
#if !NETCOREAPP2_1
                .Cast<Match>()
#endif
                .Select(match => match.Value.Substring(1, match.Value.Length - 2))
                .Except(validProperties).Any())
                throw new ArgumentException("Parameter contains one or more invalid metadata properties.",
                    nameof(encoded));

            _encoded = encoded;
        }

        [NotNull]
        internal string ReplaceWith([NotNull] AudioMetadata metadata)
        {
            var result = _replacer.Replace(_encoded, match =>
            {
                var propertyName = match.Value.Substring(1, match.Value.Length - 2);
                // ReSharper disable once PossibleNullReferenceException
                var propertyValue = (string) typeof(AudioMetadata).GetProperty(propertyName).GetValue(metadata);

                if (string.IsNullOrEmpty(propertyValue))
                    return $"Unknown {propertyName}";

                var sanitizedPropertyValue =
                    new string(propertyValue.Where(character => !_invalidChars.Contains(character)).ToArray());

                // Remove any double spaces introduced in sanitization
#if NETCOREAPP2_1
                if (sanitizedPropertyValue.Contains("  ", StringComparison.Ordinal) &&
                    !propertyValue.Contains("  ", StringComparison.Ordinal))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ", StringComparison.Ordinal);
#else
                if (sanitizedPropertyValue.Contains("  ") && !propertyValue.Contains("  "))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ");
#endif

                return sanitizedPropertyValue;
            });

            LoggingManager.LoggerFactory.CreateLogger<EncodedPath>()
                .LogDebug("Replacing encoded string '{0}' with '{1}'.", _encoded, result);

            return result;
        }
    }
}
