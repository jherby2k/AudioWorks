using System;
using System.Collections.Concurrent;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Performs analysis on one or more audio files.
    /// </summary>
    public sealed class AudioFileAnalyzer
    {
        [NotNull] readonly ExportFactory<IAudioAnalyzer> _analyzerFactory;
        [NotNull] readonly SettingDictionary _settings;
        [NotNull] readonly string _progressDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileAnalyzer"/> class.
        /// </summary>
        /// <param name="name">The name of the analyzer.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available analyzer.
        /// </exception>
        public AudioFileAnalyzer([NotNull] string name, [CanBeNull] SettingDictionary settings = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _analyzerFactory = ExtensionProvider.GetFactories<IAudioAnalyzer>("Name", name).SingleOrDefault();
            if (_analyzerFactory == null)
                throw new ArgumentException($"No '{name}' analyzer is available.", nameof(name));

            _settings = settings ?? new SettingDictionary();
            _progressDescription = $"Performing {name} analysis";
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public void Analyze(
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            Analyze(CancellationToken.None, audioFiles);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public void Analyze(
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            Analyze(null, cancellationToken, audioFiles);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="progressQueue">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public void Analyze(
            [CanBeNull] BlockingCollection<ProgressToken> progressQueue,
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            if (audioFiles == null)
                throw new ArgumentNullException(nameof(audioFiles));
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            progressQueue?.Add(new ProgressToken(_progressDescription, 0, audioFiles.Length), cancellationToken);

            using (var groupToken = new GroupToken())
            {
                var analyzerExports = new Export<IAudioAnalyzer>[audioFiles.Length];
                try
                {
                    // Initialize the analyzers sequentially
                    for (var i = 0; i < audioFiles.Length; i++)
                    {
                        analyzerExports[i] = _analyzerFactory.CreateExport();
                        analyzerExports[i].Value.Initialize(audioFiles[i].Info, _settings, groupToken);
                    }

                    // Process the audio files in parallel
                    var complete = 0;
                    Parallel.For(0, audioFiles.Length, new ParallelOptions { CancellationToken = cancellationToken },
                        i =>
                        {
                            ProcessSingle(audioFiles[i], analyzerExports[i].Value, cancellationToken);
                            progressQueue?.Add(
                                new ProgressToken(_progressDescription, Interlocked.Increment(ref complete),
                                    audioFiles.Length),
                                cancellationToken);
                        });

                    // Obtain the group results sequentially
                    for (var i = 0; i < audioFiles.Length; i++)
                        CopyFields(analyzerExports[i].Value.GetGroupResult(), audioFiles[i].Metadata);
                }
                finally
                {
                    foreach (var analyzerExport in analyzerExports)
                        analyzerExport?.Dispose();
                }
            }
        }

        static void ProcessSingle(
            [NotNull] ITaggedAudioFile audioFile,
            [NotNull] IAudioAnalyzer audioAnalyzer,
            CancellationToken cancellationToken)
        {
            using (var fileStream = File.OpenRead(audioFile.Path))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioDecoder>(
                    "Extension", Path.GetExtension(audioFile.Path)))
                    try
                    {
                        // Use an action block to process the sample collections asynchronously
                        var block = new ActionBlock<SampleCollection>(samples => audioAnalyzer.Submit(samples),
                            new ExecutionDataflowBlockOptions
                            {
                                CancellationToken = cancellationToken,
                                SingleProducerConstrained = true
                            });

                        using (var decoderExport = decoderFactory.CreateExport())
                        {
                            decoderExport.Value.Initialize(fileStream);
                            while (!decoderExport.Value.Finished)
                                if (!block.Post(decoderExport.Value.DecodeSamples()))
                                    break;
                        }

                        block.Complete();
                        block.Completion.Wait(cancellationToken);

                        CopyFields(audioAnalyzer.GetResult(), audioFile.Metadata);

                        return;
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another:
                        fileStream.Position = 0;
                    }

                throw new AudioUnsupportedException("No supporting decoders are available.");
            }
        }

        static void CopyFields([NotNull] AudioMetadata source, [NotNull] AudioMetadata destination)
        {
            // Copy every non-blank field from source to destination
            foreach (var property in typeof(AudioMetadata).GetProperties())
            {
                var value = property.GetValue(source);
                if (!string.IsNullOrEmpty((string)value))
                    property.SetValue(destination, value);
            }
        }
    }
}