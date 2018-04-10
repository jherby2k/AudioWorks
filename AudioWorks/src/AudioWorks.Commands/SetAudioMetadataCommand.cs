using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Sets one or more metadata fields on an audio file.</para>
    /// <para type="description">The Set-AudioMetadata cmdlet changes the value of one or more metadata fields. Note
    /// that these changes are not persisted to disk unless followed by a call to Save-AudioMetadata.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Set, "AudioMetadata"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class SetAudioMetadataCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Sets the title.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Title { get; set; }

        /// <summary>
        /// <para type="description">Sets the artist.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Artist { get; set; }

        /// <summary>
        /// <para type="description">Sets the album.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Album { get; set; }

        /// <summary>
        /// <para type="description">Sets the album artist.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string AlbumArtist { get; set; }

        /// <summary>
        /// <para type="description">Sets the composer.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Composer { get; set; }

        /// <summary>
        /// <para type="description">Sets the genre.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Genre { get; set; }

        /// <summary>
        /// <para type="description">Sets the comment.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Comment { get; set; }

        /// <summary>
        /// <para type="description">Sets the day of the month.</para>
        /// <para type="description">Should be a number between 1 and 31.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Day { get; set; }

        /// <summary>
        /// <para type="description">Sets the month.</para>
        /// <para type="description">Should be a number between 1 and 12.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Month { get; set; }

        /// <summary>
        /// <para type="description">Sets the year.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Year { get; set; }

        /// <summary>
        /// <para type="description">Sets the track number.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string TrackNumber { get; set; }

        /// <summary>
        /// <para type="description">Sets the track number.</para>
        /// </summary>
        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string TrackCount { get; set; }

        /// <summary>
        /// <para type="description">Sets the cover art.</para>
        /// </summary>
        [CanBeNull]
        [Parameter]
        public ICoverArt CoverArt { get; set; }

        /// <summary>
        /// <para type="description">Returns an object representing the item with which you are working. By default,
        /// this cmdlet does not generate any output.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            try
            {
                if (Title != null)
                    AudioFile.Metadata.Title = Title;
                if (Artist != null)
                    AudioFile.Metadata.Artist = Artist;
                if (Album != null)
                    AudioFile.Metadata.Album = Album;
                if (AlbumArtist != null)
                    AudioFile.Metadata.AlbumArtist = AlbumArtist;
                if (Composer != null)
                    AudioFile.Metadata.Composer = Composer;
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
                if (CoverArt != null)
                    AudioFile.Metadata.CoverArt = CoverArt;
            }
            catch (AudioMetadataInvalidException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, AudioFile));
            }

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
