using AudioWorks.Common;
using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioMetadata")]
    public sealed class GetAudioMetadataCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public AudioFile AudioFile { get; set; }
    }
}
