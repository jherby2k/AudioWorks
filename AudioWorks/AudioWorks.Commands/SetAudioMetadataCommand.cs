using AudioWorks.Common;
using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Set, "AudioMetadata")]
    public sealed class SetAudioMetadataCommand : Cmdlet
    {
        [Parameter]
        public AudioFile AudioFile { get; set; }
    }
}
