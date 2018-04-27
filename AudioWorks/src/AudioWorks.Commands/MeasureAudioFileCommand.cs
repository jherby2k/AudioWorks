using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Analyzes an audio file.</para>
    /// <para type="description">The Measure-AudioFile cmdlet performs analysis on an audio file, then stores these
    /// measurements as metadata.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsDiagnostic.Measure, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class MeasureAudioFileCommand : Cmdlet, IDynamicParameters, IDisposable
    {
        [NotNull] readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        [NotNull] readonly List<ITaggedAudioFile> _audioFiles = new List<ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the type of analysis to perform.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(AnalyzerCompleter))]
        public string Analyzer { get; set; }

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Returns an object representing the item with which you are working. By default,
        /// this cmdlet does not generate any output.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            Telemetry.TrackFirstLaunch();
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            _audioFiles.Add(AudioFile);
        }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            using (var outputQueue = new BlockingCollection<int>())
            {
                Task.Factory.StartNew(q =>
                            new AudioFileAnalyzer(
                                    // ReSharper disable once AssignNullToNotNullAttribute
                                    Analyzer,
                                    SettingAdapter.ParametersToSettings(_parameters))
                                .Analyze(
                                    (BlockingCollection<int>) q,
                                    _cancellationSource.Token,
                                    _audioFiles.ToArray()),
                        outputQueue,
                        _cancellationSource.Token,
                        TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                        TaskScheduler.Default)
                    .ContinueWith((t, q) =>
                            ((BlockingCollection<int>) q).CompleteAdding(),
                        outputQueue,
                        TaskScheduler.Default);

                // Process progress notifications on the main thread
                var activity = $"Performing {Analyzer} analysis on {_audioFiles.Count} audio files";
                var totalFramesCompleted = 0L;
                var totalFrames = _audioFiles.Sum(audioFile => audioFile.Info.FrameCount);
                var percentComplete = 0;

                foreach (var framesCompleted in outputQueue.GetConsumingEnumerable(_cancellationSource.Token))
                {
                    // If the audio files have estimated frame counts, make this doesn't go over 100%
                    totalFramesCompleted = Math.Min(totalFramesCompleted + framesCompleted, totalFrames);
                    var newPercentComplete = (int) (totalFramesCompleted / (float) totalFrames * 100);
                    if (newPercentComplete <= percentComplete) continue;

                    percentComplete = newPercentComplete;
                    WriteProgress(new ProgressRecord(0, activity, $"{percentComplete}% complete")
                    {
                        PercentComplete = percentComplete
                    });
                }
            }

            if (PassThru)
                WriteObject(_audioFiles, true);
        }

        /// <inheritdoc/>
        protected override void StopProcessing()
        {
            _cancellationSource.Cancel();
        }

        /// <inheritdoc/>
        [CanBeNull]
        public object GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (Analyzer == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioAnalyzerManager.GetSettingInfo(Analyzer));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _cancellationSource.Dispose();
        }
    }
}
