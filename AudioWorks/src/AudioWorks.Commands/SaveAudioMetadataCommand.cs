using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Saves an audio file's metadata to disk.</para>
    /// <para type="description">The Save-AudioMetadata cmdlet persists changes to an audio file's metadata. Depending
    /// on the file extension, various optional parameters may be available.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsData.Save, "AudioMetadata", SupportsShouldProcess = true), OutputType(typeof(ITaggedAudioFile))]
    public sealed class SaveAudioMetadataCommand : Cmdlet, IDynamicParameters
    {
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
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
            try
            {
                // ReSharper disable twice PossibleNullReferenceException
                if (ShouldProcess(AudioFile.Path))
                    AudioFile.SaveMetadata(SettingAdapter.ParametersToSettings(_parameters));
            }
            catch (AudioUnsupportedException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, AudioFile));
            }

            if (PassThru)
                WriteObject(AudioFile);
        }

        /// <inheritdoc/>
        [CanBeNull]
        public object GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (AudioFile == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioMetadataEncoderManager.GetSettingInfo(Path.GetExtension(AudioFile.Path)));
        }
    }
}
