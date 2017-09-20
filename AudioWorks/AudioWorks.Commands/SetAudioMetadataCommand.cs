using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Set, "AudioMetadata")]
    public sealed class SetAudioMetadataCommand : Cmdlet
    {
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AudioFile AudioFile { get; set; }

        [CanBeNull]
        [Parameter]
        public string Title { get; set; }

        protected override void ProcessRecord()
        {
            if (Title != null)
                AudioFile.Metadata.Title = Title;
        }
    }
}
