using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Analyzes an audio file.</para>
    /// <para type="description">The Measure-AudioFile cmdlet performs analysis on an audio file, then stores these
    /// measurements as metadata.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsDiagnostic.Measure, "AudioFile"), OutputType(typeof(IAnalyzableAudioFile))]
    public sealed class MeasureAudioFileCommand : Cmdlet, IDynamicParameters
    {
        [NotNull] readonly List<IAnalyzableAudioFile> _audioFiles = new List<IAnalyzableAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the type of analysis to perform.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Analyzer { get; set; }

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public IAnalyzableAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Returns an object representing the item with which you are working. By default,
        /// this cmdlet does not generate any output.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            _audioFiles.Add(AudioFile);
        }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            using (var groupToken = new GroupToken(_audioFiles.Count))
                AnalyzeParallel(groupToken, SettingAdapter.ParametersToSettings(_parameters));

            if (PassThru)
                WriteObject(_audioFiles, true);
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

        void AnalyzeParallel([NotNull] GroupToken groupToken, [CanBeNull] SettingDictionary settings)
        {
            var block = new ActionBlock<IAnalyzableAudioFile>(audioFile =>
                    audioFile.AnalyzeAsync(Analyzer, settings, groupToken, CancellationToken.None),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
                    SingleProducerConstrained = true
                });

            foreach (var audioFile in _audioFiles)
                block.Post(audioFile);

            block.Complete();
            block.Completion.Wait();
        }
    }
}
