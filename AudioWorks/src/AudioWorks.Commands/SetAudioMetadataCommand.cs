/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Management.Automation;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Set, "AudioMetadata"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class SetAudioMetadataCommand : LoggingCmdlet
    {
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

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
        public string AlbumArtist { get; set; }

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public string Composer { get; set; }

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

        [CanBeNull]
        [Parameter, ValidateNotNull]
        public ICoverArt CoverArt { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

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

            ProcessLogMessages();

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
