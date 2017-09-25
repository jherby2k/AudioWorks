using System;
using System.Collections.ObjectModel;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsData.Save, "AudioMetadata"), OutputType(typeof(AudioFile))]
    public sealed class SaveAudioMetadataCommand : Cmdlet, IDynamicParameters
    {
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

        [CanBeNull]
        public object GetDynamicParameters()
        {
            var result = new RuntimeDefinedParameterDictionary();
            if (AudioFile?.FileInfo.Extension == ".mp3")
                result.Add("Padding",
                new RuntimeDefinedParameter("Padding", typeof(int),
                    new Collection<Attribute> { new ParameterAttribute() }));
            return null;
        }
    }
}
