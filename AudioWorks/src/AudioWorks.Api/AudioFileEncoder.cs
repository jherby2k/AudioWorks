using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        [CanBeNull] readonly MetadataSubstituter _fileNameSubstituter;
        [CanBeNull] readonly MetadataSubstituter _directoryNameSubstituter;
        [NotNull] readonly ExportFactory<IAudioEncoder> _encoderFactory;
        [NotNull] readonly SettingDictionary _settings;
        readonly bool _overwrite;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileEncoder"/> class.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="encodedDirectoryName">The encoded directory name, or null.</param>
        /// <param name="encodedFileName">The encode file name, or null.</param>
        /// <param name="overwrite">if set to <c>true</c>, any existing file will be overwritten.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available encoder.
        /// </exception>
        public AudioFileEncoder(
            [NotNull] string name,
            [CanBeNull] SettingDictionary settings = null,
            [CanBeNull] string encodedDirectoryName = null,
            [CanBeNull] string encodedFileName = null,
            bool overwrite = false)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _encoderFactory = ExtensionProvider.GetFactories<IAudioEncoder>("Name", name).SingleOrDefault() ??
                              throw new ArgumentException($"No '{name}' encoder is available.", nameof(name));

            if (settings != null)
            {
                // Make sure the provided settings are clean
                AudioEncoderManager.GetSettingInfo(name).ValidateSettings(settings);
                _settings = settings;
            }
            else
                _settings = new SettingDictionary();

            if (encodedDirectoryName != null)
                _directoryNameSubstituter = new DirectoryNameSubstituter(encodedDirectoryName);
            if (encodedFileName != null)
                _fileNameSubstituter = new FileNameSubstituter(encodedFileName);
            _overwrite = overwrite;
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            return await EncodeAsync(CancellationToken.None, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            return await EncodeAsync(null, cancellationToken, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            [CanBeNull] IProgress<int> progress,
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            var encodeTasks = audioFiles.Select(audioFile =>
                Task.Run(() =>
                {
                    string tempOutputPath = null;

                    try
                    {
                        Export<IAudioEncoder> encoderExport = null;
                        FileStream outputStream = null;
                        string finalOutputPath;

                        try
                        {
                            // The output directory defaults to the audiofile's current directory
                            var outputDirectory = _directoryNameSubstituter?.Substitute(audioFile.Metadata) ??
                                                  Path.GetDirectoryName(audioFile.Path);

                            // ReSharper disable once AssignNullToNotNullAttribute
                            Directory.CreateDirectory(outputDirectory);

                            // The output file names default to the input file names
                            var outputFileName = _fileNameSubstituter?.Substitute(audioFile.Metadata) ??
                                                 Path.GetFileNameWithoutExtension(audioFile.Path);

                            encoderExport = _encoderFactory.CreateExport();

                            tempOutputPath = finalOutputPath = Path.Combine(outputDirectory,
                                outputFileName + encoderExport.Value.FileExtension);

                            // If the output file already exists, write to a temporary file first
                            if (File.Exists(finalOutputPath))
                            {
                                if (!_overwrite)
                                    throw new IOException($"The file '{finalOutputPath}' already exists.");

                                tempOutputPath = Path.Combine(outputDirectory, Path.GetRandomFileName());
                            }

                            outputStream = File.Open(tempOutputPath, FileMode.OpenOrCreate);

                            encoderExport.Value.Initialize(outputStream, audioFile.Info,
                                audioFile.Metadata,
                                _settings);

                            encoderExport.Value.ProcessSamples(
                                audioFile.Path,
                                progress,
                                (int) (audioFile.Info.FrameCount / 100),
                                cancellationToken);

                            encoderExport.Value.Finish();
                        }
                        finally
                        {
                            // Dispose the encoders before closing the streams
                            encoderExport?.Dispose();
                            outputStream?.Dispose();
                        }

                        // If writing to a temporary file, replace the original
                        if (!tempOutputPath.Equals(finalOutputPath, StringComparison.OrdinalIgnoreCase))
                        {
                            File.Delete(finalOutputPath);
                            File.Move(tempOutputPath, finalOutputPath);
                        }

                        return new TaggedAudioFile(finalOutputPath);
                    }
                    catch (Exception)
                    {
                        if (tempOutputPath != null)
                            File.Delete(tempOutputPath);
                        throw;
                    }
                }, cancellationToken));

            return await Task.WhenAll(encodeTasks).ConfigureAwait(false);
        }
    }
}
