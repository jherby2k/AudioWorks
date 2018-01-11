using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    sealed class MetadataSubstituter
    {
        static readonly char[] _invalidChars = Path.GetInvalidFileNameChars();
        static readonly Regex _replacer = new Regex(@"\{[^{]+\}");

        [NotNull] readonly AudioMetadata _metadata;

        internal MetadataSubstituter([NotNull] AudioMetadata metadata)
        {
            _metadata = metadata;
        }

        [ContractAnnotation("null => null")]
        internal string Substitute(string path)
        {
            return path != null
                ? _replacer.Replace(path, match =>
                    new string(((string) typeof(AudioMetadata)
                            .GetProperty(match.Value.Substring(1, match.Value.Length - 2)).GetValue(_metadata))
                        .Where(character => !_invalidChars.Contains(character)).ToArray()))
                : null;
        }
    }
}
