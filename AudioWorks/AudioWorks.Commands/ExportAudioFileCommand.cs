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
    [Cmdlet(VerbsData.Export, "AudioFile", DefaultParameterSetName = "ByPath"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ExportAudioFileCommand : PSCmdlet, IDynamicParameters, IDisposable
    {
        [NotNull] readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        [NotNull] readonly List<ITaggedAudioFile> _sourceAudioFiles = new List<ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the encoder to use.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Encoder { get; set; }

        /// <summary>
        /// <para type="description">Specifies the source audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output directory path.</para>
        /// </summary>
        [Parameter]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output file name.</para>
        /// <para type="description">The file extension will be selected automatically and should not be included. If
        /// this parameter is omitted, the name will be the same as the source audio file.</para>
        /// </summary>
        [Parameter]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates that existing files should be replaced.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Replace { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            _sourceAudioFiles.Add(AudioFile);
        }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            using (var outputQueue = new BlockingCollection<int>())
            {
                var encodeTask = Task<IEnumerable<ITaggedAudioFile>>.Factory.StartNew(q =>
                        new AudioFileEncoder(
                                Encoder,
                                SettingAdapter.ParametersToSettings(_parameters),
                                SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path, out var _, out var _),
                                Name,
                                Replace)
                            .Encode(
                                (BlockingCollection<int>) q,
                                _cancellationSource.Token,
                                _sourceAudioFiles.ToArray()),
                    outputQueue,
                    _cancellationSource.Token,
                    TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);

                encodeTask.ContinueWith((t, q) =>
                        ((BlockingCollection<int>) q).CompleteAdding(),
                    outputQueue,
                    TaskScheduler.Default);

                // Process progress notifications on the main thread
                var activity = $"Encoding {_sourceAudioFiles.Count} audio files in {Encoder} format";
                var totalFramesCompleted = 0L;
                var totalFrames = (double) _sourceAudioFiles.Sum(audioFile => audioFile.Info.SampleCount);

                foreach (var framesCompleted in outputQueue.GetConsumingEnumerable(_cancellationSource.Token))
                {
                    // If the audio files have estimated frame counts, make this doesn't go over 100%
                    totalFramesCompleted = (long) Math.Min(totalFramesCompleted + framesCompleted, totalFrames);
                    var percentComplete =
                        (int) Math.Floor(totalFramesCompleted / totalFrames * 100);
                    WriteProgress(new ProgressRecord(0, activity, $"{percentComplete}% complete")
                    {
                        PercentComplete = percentComplete
                    });
                }

                if (!encodeTask.IsCanceled)
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
