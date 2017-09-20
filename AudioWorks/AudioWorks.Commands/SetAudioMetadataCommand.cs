using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Set, "AudioMetadata")]
    public sealed class SetAudioMetadataCommand : Cmdlet
    {
    }
}
