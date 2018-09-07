using System;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Performs analysis on one or more audio files.
    /// </summary>
    [PublicAPI]
    public sealed class AudioFileAnalyzer
    {
        [NotNull] readonly ExportFactory<IAudioAnalyzer> _analyzerFactory;
        [NotNull] readonly string _progressDescription;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        [NotNull]
        public SettingDictionary Settings { get; }

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

            _analyzerFactory = ExtensionProviderWrapper.GetFactories<IAudioAnalyzer>("Name", name).SingleOrDefault() ??
                               throw new ArgumentException($"No '{name}' analyzer is available.", nameof(name));

            using (var export = _analyzerFactory.CreateExport())
                Settings = new ValidatingSettingDictionary(export.Value.SettingInfo, settings);

            _progressDescription = $"Performing {name} analysis";
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            await AnalyzeAsync(CancellationToken.None, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            await AnalyzeAsync(null, cancellationToken, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            [CanBeNull] IProgress<ProgressToken> progress,
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            progress?.Report(new ProgressToken
            {
                AudioFilesCompleted = 0,
                FramesCompleted = 0
            });

            var groupToken = new GroupToken();
            var analyzerExports = new Export<IAudioAnalyzer>[audioFiles.Length];
            var audioFilesCompleted = 0;
            var totalFramesCompleted = 0;

            try
            {
                var processTasks = new Task[audioFiles.Length];

                for (var i = 0; i < audioFiles.Length; i++)
                {
                    analyzerExports[i] = _analyzerFactory.CreateExport();
                    analyzerExports[i].Value.Initialize(audioFiles[i].Info, Settings, groupToken);

                    var i1 = i;
                    var itemProgress = progress == null
                        ? null
                        // ReSharper disable once ImplicitlyCapturedClosure
                        : new SimpleProgress<int>(framesCompleted => progress.Report(new ProgressToken
                        {
                            AudioFilesCompleted = audioFilesCompleted,
                            FramesCompleted = Interlocked.Add(ref totalFramesCompleted, framesCompleted)
                        }));

                    // ReSharper disable once ImplicitlyCapturedClosure
                    processTasks[i] = Task.Run(() =>
                    {
                        analyzerExports[i1].Value.ProcessSamples(
                            audioFiles[i1].Path,
                            itemProgress,
                            cancellationToken);

                        CopyFields(analyzerExports[i1].Value.GetResult(), audioFiles[i1].Metadata);

                        Interlocked.Increment(ref audioFilesCompleted);
                    }, cancellationToken);
                }

                await Task.WhenAll(processTasks).ConfigureAwait(false);

                // Obtain the group results sequentially
                for (var i = 0; i < audioFiles.Length; i++)
                    CopyFields(analyzerExports[i].Value.GetGroupResult(), audioFiles[i].Metadata);
            }
            finally
            {
                foreach (var analyzerExport in analyzerExports)
                    analyzerExport?.Dispose();
                groupToken.Dispose();
            }

            progress?.Report(new ProgressToken
            {
                AudioFilesCompleted = audioFilesCompleted,
                FramesCompleted = totalFramesCompleted
            });
        }

        static void CopyFields([NotNull] AudioMetadata source, [NotNull] AudioMetadata destination)
        {
            // Copy every non-blank field from source to destination
            foreach (var property in typeof(AudioMetadata).GetProperties())
            {
                var value = property.GetValue(source);
                if (!string.IsNullOrEmpty((string) value))
                    property.SetValue(destination, value);
            }
        }
    }
}