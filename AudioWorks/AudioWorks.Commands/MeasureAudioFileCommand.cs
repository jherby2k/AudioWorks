using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Analyzes an audio file.</para>
    /// <para type="description">The Measure-AudioFile cmdlet performs analysis on an audio file, then stores these
    /// measurements as metadata.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsDiagnostic.Measure, "AudioFile"), OutputType(typeof(IAnalyzableAudioFile))]
    public sealed class MeasureAudioFileCommand : Cmdlet
    {
        readonly List<IAnalyzableAudioFile> _audioFiles = new List<IAnalyzableAudioFile>();

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public IAnalyzableAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public string Analyzer { get; set; }

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
                AudioFile.Analyze(Analyzer, groupToken, CancellationToken.None);

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
