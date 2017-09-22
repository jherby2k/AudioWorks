using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Clear, "AudioMetadata"), OutputType(typeof(AudioFile))]
    public sealed class ClearAudioMetadataCommand : Cmdlet
    {
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AudioFile AudioFile { get; set; }

        [Parameter]
        public SwitchParameter Title { get; set; }

        [Parameter]
        public SwitchParameter Artist { get; set; }

        [Parameter]
        public SwitchParameter Album { get; set; }

        [Parameter]
        public SwitchParameter Genre { get; set; }

        [Parameter]
        public SwitchParameter Comment { get; set; }

        [Parameter]
        public SwitchParameter Day { get; set; }

        [Parameter]
        public SwitchParameter Month { get; set; }

        [Parameter]
        public SwitchParameter Year { get; set; }

        [Parameter]
        public SwitchParameter TrackNumber { get; set; }

        [Parameter]
        public SwitchParameter TrackCount { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            if (Title)
                AudioFile.Metadata.Title = string.Empty;
            if (Artist)
                AudioFile.Metadata.Artist = string.Empty;
            if (Album)
                AudioFile.Metadata.Album = string.Empty;
            if (Genre)
                AudioFile.Metadata.Genre = string.Empty;
            if (Comment)
                AudioFile.Metadata.Comment = string.Empty;
            if (Day)
                AudioFile.Metadata.Day = string.Empty;
            if (Month)
                AudioFile.Metadata.Month = string.Empty;
            if (Year)
                AudioFile.Metadata.Year = string.Empty;
            if (TrackNumber)
                AudioFile.Metadata.TrackNumber = string.Empty;
            if (TrackCount)
                AudioFile.Metadata.TrackCount = string.Empty;

            // If no switches were specified, clear everything
            if (!(Title || Artist || Album || Genre || Comment || Day || Month || Year || TrackNumber || TrackCount))
                AudioFile.Metadata.Clear();

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
