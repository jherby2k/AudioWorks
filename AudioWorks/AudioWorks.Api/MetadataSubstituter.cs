using System.Linq;
using System.Text.RegularExpressions;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    public sealed class MetadataSubstituter
    {
        static readonly Regex _replacer = new Regex(@"\{[^{]+\}");
        readonly char[] _invalidChars;

        public MetadataSubstituter([NotNull] char[] invalidChars)
        {
            _invalidChars = invalidChars;
        }

        [NotNull] public string Substitute([NotNull] string path, [NotNull] AudioMetadata metadata) => _replacer.Replace(path, match =>
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
