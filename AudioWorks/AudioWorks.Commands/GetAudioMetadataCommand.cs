using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioMetadata")]
    public sealed class GetAudioMetadataCommand : Cmdlet
    {
    }
}
