using System;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Api
{
    [PublicAPI]
    public static class AudioFileFactory
    {
        [NotNull]
        public static AudioFile Create([NotNull] string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Value cannot be null or empty.", nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' cannot be found.", path);

            var fileInfo = new FileInfo(path);
            return new AudioFile(fileInfo, LoadAudioInfo(fileInfo));
        }

        [NotNull]
        static AudioInfo LoadAudioInfo([NotNull] FileInfo fileInfo)
        {
            using (var fileStream = fileInfo.OpenRead())
            {
                // Try each info decoder that supports this file extension:
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioInfoDecoder>(
                    "Extension", fileInfo.Extension))
                {
                    try
                    {
                        using (var lifetimeContext = decoderFactory.CreateExport())
                            return lifetimeContext.Value.ReadAudioInfo(fileStream);
                    }
                    catch (UnsupportedFileException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another:
                        fileStream.Position = 0;
                    }
                }
            }

            throw new UnsupportedFileException("No supporting extensions are available.");
        }
    }
}
