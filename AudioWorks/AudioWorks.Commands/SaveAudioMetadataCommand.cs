using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsData.Save, "AudioMetadata", SupportsShouldProcess = true), OutputType(typeof(ITaggedAudioFile))]
    public sealed class SaveAudioMetadataCommand : Cmdlet, IDynamicParameters
    {
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                if (ShouldProcess(AudioFile.FileInfo.Name))
                    AudioFile.SaveMetadata(SettingAdapter.ParametersToSettings(_parameters));
            }
            catch (AudioUnsupportedException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, AudioFile));
            }

            if (PassThru)
                WriteObject(AudioFile);
        }

        [CanBeNull]
        public object GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (AudioFile == null) return null;

            _parameters = SettingAdapter.SettingInfoToParameters(
                AudioMetadataEncoderManager.GetSettingInfo(AudioFile.FileInfo.Extension));
            return _parameters;
        }
    }
}
