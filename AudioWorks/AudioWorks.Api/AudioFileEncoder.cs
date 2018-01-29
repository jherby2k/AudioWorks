using System;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Encodes one or more audio files in a new audio format.
    /// </summary>
    [PublicAPI]
    public sealed class AudioFileEncoder
    {
        [NotNull] readonly ExportFactory<IAudioEncoder> _encoderFactory;
        [NotNull] readonly SettingDictionary _settings;
        [NotNull] readonly string _progressDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileEncoder"/> class.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available encoder.
        /// </exception>
        public AudioFileEncoder([NotNull] string name, [CanBeNull] SettingDictionary settings = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _encoderFactory = ExtensionProvider.GetFactories<IAudioEncoder>("Name", name).SingleOrDefault();
            if (_encoderFactory == null)
                throw new ArgumentException($"No '{name}' encoder is available.", nameof(name));

            _settings = settings ?? new SettingDictionary();
            _progressDescription = $"Exporting to {name} format";
        }

        /// <summary>
        /// Exports the specified audio file.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="outputDirectory">The output directory, or null.</param>
        /// <param name="outputFileName">The name of the output file, (without the path or extension) or null.</param>
        /// <param name="overwrite">if set to <c>true</c>, any existing file will be overwritten.</param>
        /// <returns>A new audio file.</returns>
        [NotNull]
        public ITaggedAudioFile Export([NotNull] ITaggedAudioFile audioFile,
            [CanBeNull] DirectoryInfo outputDirectory = null,
            [CanBeNull] string outputFileName = null,
            bool overwrite = false)
        {
            return Export(audioFile, CancellationToken.None, outputDirectory, outputFileName, overwrite);
        }

        /// <summary>
        /// Exports the specified audio file.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="outputDirectory">The output directory, or null.</param>
        /// <param name="outputFileName">The name of the output file, (without the path or extension) or null.</param>
        /// <param name="overwrite">if set to <c>true</c>, any existing file will be overwritten.</param>
        /// <returns>A new audio file.</returns>
        [NotNull]
        public ITaggedAudioFile Export(
            [NotNull] ITaggedAudioFile audioFile,
            CancellationToken cancellationToken,
            [CanBeNull] DirectoryInfo outputDirectory = null,
            [CanBeNull] string outputFileName = null,
            bool overwrite = false)
        {
            if (audioFile == null) throw new ArgumentNullException(nameof(audioFile));

            // The output directory defaults to the input directory
            if (outputDirectory == null)
                outputDirectory = new DirectoryInfo(Path.GetDirectoryName(audioFile.Path));
            else
                outputDirectory.Create();

            // The output file name defaults to the input file name
            if (outputFileName == null)
                outputFileName = Path.GetFileNameWithoutExtension(audioFile.Path);

            string outputFilePath = null;
            string finalOutputFilePath;

            try
            {
                // Need to close these in reverse order, so can't use "using"
                Export<IAudioEncoder> encoderExport = null;
                FileStream outputStream = null;

                try
                {
                    encoderExport = _encoderFactory.CreateExport();

                    outputFilePath = Path.Combine(outputDirectory.FullName,
                        outputFileName + encoderExport.Value.FileExtension);
                    finalOutputFilePath = outputFilePath;

                    // If the output file already exists, write to a temporary file first:
                    if (File.Exists(outputFilePath))
                    {
                        if (!overwrite)
                            throw new IOException($"The file '{outputFilePath}' already exists.");

                        outputFilePath = Path.Combine(outputDirectory.FullName, Path.GetRandomFileName());
                    }

                    outputStream = File.Open(outputFilePath, FileMode.OpenOrCreate);

                    encoderExport.Value.Initialize(outputStream, audioFile.Info, audioFile.Metadata, _settings);
                    encoderExport.Value.ProcessSamples(audioFile.Path, cancellationToken);
                    encoderExport.Value.Finish();
                }
                finally
                {
                    encoderExport?.Dispose();
                    outputStream?.Dispose();
                }
            }
            catch (Exception)
            {
                if (outputFilePath != null)
                    File.Delete(outputFilePath);
                throw;
            }

            // If using a temporary file, replace the original
            if (!string.Equals(outputFilePath, finalOutputFilePath, StringComparison.Ordinal))
            {
                File.Delete(finalOutputFilePath);
                File.Move(outputFilePath, finalOutputFilePath);
            }

            return new TaggedAudioFile(finalOutputFilePath);
        }
    }
}
