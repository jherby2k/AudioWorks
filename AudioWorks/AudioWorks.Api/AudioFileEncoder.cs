using System;
using System.Collections.Concurrent;
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
        [NotNull] MetadataSubstituter _fileNameSubstituter = new MetadataSubstituter(Path.GetInvalidFileNameChars());
        [NotNull] MetadataSubstituter _directoryNameSubstituter = new MetadataSubstituter(Path.GetInvalidPathChars());
        [NotNull] readonly ExportFactory<IAudioEncoder> _encoderFactory;
        [NotNull] readonly SettingDictionary _settings;
        [CanBeNull] readonly string _encodedDirectoryName;
        [CanBeNull] readonly string _encodedFileName;
        readonly bool _overwrite;
        [NotNull] readonly string _progressDescription;

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

            _encoderFactory = ExtensionProvider.GetFactories<IAudioEncoder>("Name", name).SingleOrDefault();
            if (_encoderFactory == null)
                throw new ArgumentException($"No '{name}' encoder is available.", nameof(name));

            _settings = settings ?? new SettingDictionary();
            _encodedDirectoryName = encodedDirectoryName;
            _encodedFileName = encodedFileName;
            _overwrite = overwrite;
            _progressDescription = $"Encoding to {name} format";
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public IEnumerable<ITaggedAudioFile> Encode(
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            return Encode(CancellationToken.None, audioFiles);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public IEnumerable<ITaggedAudioFile> Encode(
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            return Encode(null, cancellationToken, audioFiles);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="progressQueue">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        [NotNull, ItemNotNull]
        public IEnumerable<ITaggedAudioFile> Encode(
            [CanBeNull] BlockingCollection<ProgressToken> progressQueue,
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            progressQueue?.Add(new ProgressToken(_progressDescription, 0, audioFiles.Length), cancellationToken);

            var finalOutputPaths = new string[audioFiles.Length];

            var complete = 0;
            Parallel.For(0, audioFiles.Length, new ParallelOptions { CancellationToken = cancellationToken }, i =>
            {
                string tempOutputPath = null;

                try
                {
                    Export<IAudioEncoder> encoderExport = null;
                    FileStream outputStream = null;

                    try
                    {
                        // The output directory defaults to the each audiofile's current directory
                        var outputDirectory = _encodedDirectoryName == null
                            ? Path.GetDirectoryName(audioFiles[i].Path)
                            : _directoryNameSubstituter.Substitute(_encodedDirectoryName, audioFiles[i].Metadata);

                        Directory.CreateDirectory(outputDirectory);

                        // The output file names default to the input file names
                        var outputFileName = _encodedFileName == null
                            ? Path.GetFileNameWithoutExtension(audioFiles[i].Path)
                            : _fileNameSubstituter.Substitute(_encodedFileName, audioFiles[i].Metadata);

                        encoderExport = _encoderFactory.CreateExport();

                        tempOutputPath = finalOutputPaths[i] = Path.Combine(outputDirectory,
                            outputFileName + encoderExport.Value.FileExtension);

                        // If the output file already exists, write to a temporary file first
                        if (File.Exists(finalOutputPaths[i]))
                        {
                            if (!_overwrite)
                                throw new IOException($"The file '{finalOutputPaths[i]}' already exists.");

                            tempOutputPath = Path.Combine(outputDirectory, Path.GetRandomFileName());
                        }

                        outputStream = File.Open(tempOutputPath, FileMode.OpenOrCreate);

                        encoderExport.Value.Initialize(outputStream, audioFiles[i].Info,
                            audioFiles[i].Metadata,
                            _settings);
                        encoderExport.Value.ProcessSamples(audioFiles[i].Path, cancellationToken);
                        encoderExport.Value.Finish();
                    }
                    finally
                    {
                        // Dispose the encoders before closing the streams
                        encoderExport?.Dispose();
                        outputStream?.Dispose();
                    }

                    if (string.Equals(tempOutputPath, finalOutputPaths[i], StringComparison.OrdinalIgnoreCase)) return;

                    // If writing to a temporary file, replace the original
                    File.Delete(finalOutputPaths[i]);
                    File.Move(tempOutputPath, finalOutputPaths[i]);

                    progressQueue?.Add(
                        new ProgressToken(_progressDescription, Interlocked.Increment(ref complete),
                            audioFiles.Length),
                        cancellationToken);
                }
                catch (Exception)
                {
                    if (tempOutputPath != null)
                        File.Delete(tempOutputPath);
                    throw;
                }
            });

            return finalOutputPaths.Select(path => new TaggedAudioFile(path));
        }
    }
}
