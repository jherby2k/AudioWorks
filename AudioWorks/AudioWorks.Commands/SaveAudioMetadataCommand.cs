using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsData.Save, "AudioMetadata"), OutputType(typeof(AudioFile))]
    public sealed class SaveAudioMetadataCommand : Cmdlet
    {
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AudioFile AudioFile { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                AudioFile.SaveMetadata();
            }
            catch (AudioUnsupportedException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, AudioFile));
            }

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
