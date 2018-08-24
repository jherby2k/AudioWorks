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
    /// <para type="synopsis">Exports an audio file.</para>
    /// <para type="description">The Export-AudioFile cmdlet creates a new audio file using the specified encoder.
    /// </para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsData.Export, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ExportAudioFileCommand : LoggingPSCmdlet, IDynamicParameters, IDisposable
    {
        [NotNull] readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        [NotNull, ItemNotNull] readonly List<ITaggedAudioFile> _sourceAudioFiles = new List<ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the encoder to use.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(EncoderCompleter))]
        public string Encoder { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output directory path.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 1)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies the source audio file.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output file name.</para>
        /// <para type="description">The file extension will be selected automatically and should not be included. If
        /// this parameter is omitted, the name will be the same as the source audio file.</para>
        /// </summary>
        [CanBeNull]
        [Parameter]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates that existing files should be replaced.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Replace { get; set; }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            Telemetry.TrackFirstLaunch();
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            _sourceAudioFiles.Add(AudioFile);
        }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var encoder = new AudioFileEncoder(
                    Encoder,
                    SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path),
                    Name,
                    SettingAdapter.ParametersToSettings(_parameters))
                { Overwrite = Replace };

            var activity = $"Encoding {_sourceAudioFiles.Count} audio files in {Encoder} format";
            var totalFrames = (double) _sourceAudioFiles.Sum(audioFile => audioFile.Info.FrameCount);
            var lastAudioFilesCompleted = 0;
            var lastPercentComplete = 0;

            using (var messageQueue = new BlockingCollection<object>())
            {
                var progress = new SimpleProgress<ProgressToken>(token =>
                {
                    var percentComplete = (int) Math.Round(token.FramesCompleted / totalFrames * 100);

                    // Only report progress if something has changed
                    if (percentComplete <= lastPercentComplete && token.AudioFilesCompleted <= lastAudioFilesCompleted)
                        return;

                    lastAudioFilesCompleted = token.AudioFilesCompleted;
                    lastPercentComplete = percentComplete;

                    // ReSharper disable once AccessToDisposedClosure
                    messageQueue.Add(new ProgressRecord(0, activity,
                        $"{token.AudioFilesCompleted} of {_sourceAudioFiles.Count} audio files encoded")
                    {
                        // If the audio files have estimated frame counts, make sure this doesn't go over 100%
                        PercentComplete = Math.Min(percentComplete, 100)
                    });

                    // Send any new log messages to the output queue
                    while (CmdletLoggerProvider.Instance.TryDequeueMessage(out var logMessage))
                        // ReSharper disable once AccessToDisposedClosure
                        messageQueue.Add(logMessage);
                });

                var encodeTask = encoder.EncodeAsync(progress, _cancellationSource.Token, _sourceAudioFiles.ToArray());
                // ReSharper disable once AccessToDisposedClosure
                encodeTask.ContinueWith(task => messageQueue.CompleteAdding(), TaskScheduler.Current);

                this.OutputMessages(messageQueue, _cancellationSource.Token);

                WriteObject(encodeTask.Result, true);
            }
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
            if (Encoder == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioEncoderManager.GetSettingInfo(Encoder));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _cancellationSource.Dispose();
        }
    }
}
