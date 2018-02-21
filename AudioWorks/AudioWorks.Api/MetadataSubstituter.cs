using System;
using System.Linq;
using System.Text.RegularExpressions;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    abstract class MetadataSubstituter
    {
        [NotNull] static readonly Regex _replacer = new Regex(@"\{[^{]+\}");
        [NotNull] readonly string _encoded;
        [NotNull] readonly char[] _invalidChars;

        internal MetadataSubstituter([NotNull] string encoded, [NotNull] char[] invalidChars)
        {
            var validProperties = typeof(AudioMetadata).GetProperties()
                .Select(propertyInfo => propertyInfo.Name).ToArray();
            if (_replacer.Matches(encoded).Cast<Match>()
                .Select(match => match.Value.Substring(1, match.Value.Length - 2))
                .Except(validProperties).Any())
                throw new ArgumentException("Parameter contains one or more invalid metadata properties.",
                    nameof(encoded));

            _encoded = encoded;
            _invalidChars = invalidChars;
        }

        [NotNull]
        internal string Substitute([NotNull] AudioMetadata metadata) =>
            _replacer.Replace(_encoded, match =>
            {
                var propertyName = match.Value.Substring(1, match.Value.Length - 2);
                var propertyValue = (string) typeof(AudioMetadata).GetProperty(propertyName).GetValue(metadata);

                if (string.IsNullOrEmpty(propertyValue))
                    return $"Unknown {propertyName}";

                var sanitizedPropertyValue =
                    new string(propertyValue.Where(character => !_invalidChars.Contains(character)).ToArray());

                // Remove any double spaces introduced in sanitization
                if (sanitizedPropertyValue.Contains("  ") && !propertyValue.Contains("  "))
                    sanitizedPropertyValue = sanitizedPropertyValue.Replace("  ", " ");

                return sanitizedPropertyValue;
            });
    }
}
