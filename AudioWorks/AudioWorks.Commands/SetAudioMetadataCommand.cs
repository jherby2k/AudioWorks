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
        [Parameter, ValidateNotNull]
        public string Title { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Artist { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Album { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Genre { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Comment { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Day { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Month { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Year { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string TrackNumber { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string TrackCount { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            if (Title != null)
                AudioFile.Metadata.Title = Title;
            if (Artist != null)
                AudioFile.Metadata.Artist = Artist;
            if (Album != null)
                AudioFile.Metadata.Album = Album;
            if (Genre != null)
                AudioFile.Metadata.Genre = Genre;
            if (Comment != null)
                AudioFile.Metadata.Comment = Comment;
            if (Day != null)
                AudioFile.Metadata.Day = Day;
            if (Month != null)
                AudioFile.Metadata.Month = Month;
            if (Year != null)
                AudioFile.Metadata.Year = Year;
            if (TrackNumber != null)
                AudioFile.Metadata.TrackNumber = TrackNumber;
            if (TrackCount != null)
                AudioFile.Metadata.TrackCount = TrackCount;

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
