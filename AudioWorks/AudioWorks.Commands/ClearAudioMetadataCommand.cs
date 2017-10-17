using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Clears one or more metadata fields on an audio file.</para>
    /// <para type="description">The Clear-AudioMetadata cmdlet clears one or more metadata fields. Note that these
    /// changes are not persisted to disk unless followed by a call to Save-AudioMetadata.</para>
    /// <para type="description">If no metadata fields are specified, all fields are cleared.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Clear, "AudioMetadata"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ClearAudioMetadataCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Clears the title.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Title { get; set; }

        /// <summary>
        /// <para type="description">Clears the artist.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Artist { get; set; }

        /// <summary>
        /// <para type="description">Clears the album.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Album { get; set; }

        /// <summary>
        /// <para type="description">Clears the genre.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Genre { get; set; }

        /// <summary>
        /// <para type="description">Clears the comment.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Comment { get; set; }

        /// <summary>
        /// <para type="description">Clears the day of the month.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Day { get; set; }

        /// <summary>
        /// <para type="description">Clears the month.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Month { get; set; }

        /// <summary>
        /// <para type="description">Clears the year.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Year { get; set; }

        /// <summary>
        /// <para type="description">Clears the track number.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter TrackNumber { get; set; }

        /// <summary>
        /// <para type="description">Clears the track count.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter TrackCount { get; set; }

        /// <summary>
        /// <para type="description">Returns an object representing the item with which you are working. By default,
        /// this cmdlet does not generate any output.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <inheritdoc/>
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
