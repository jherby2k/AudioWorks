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

        [NotNull] readonly AudioMetadata _metadata;

        public MetadataSubstituter([NotNull] AudioMetadata metadata, [NotNull] char[] invalidChars)
        {
            _metadata = metadata;
            _invalidChars = invalidChars;
        }

        public string Substitute([NotNull] string path) => _replacer.Replace(path, match =>
        {
            var propertyName = match.Value.Substring(1, match.Value.Length - 2);
            var propertyValue = new string(((string) typeof(AudioMetadata).GetProperty(propertyName)
                .GetValue(_metadata)).Where(character => !_invalidChars.Contains(character)).ToArray());

            // If a property isn't set
            if (string.IsNullOrEmpty(propertyValue))
                propertyValue = $"Unknown {propertyName}";
            return propertyValue;
        });
    }
}
