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

using AudioWorks.Common;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsCommon.Clear, "AudioMetadata"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ClearAudioMetadataCommand : LoggingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter]
        public SwitchParameter Title { get; set; }

        [Parameter]
        public SwitchParameter Artist { get; set; }

        [Parameter]
        public SwitchParameter Album { get; set; }

        [Parameter]
        public SwitchParameter AlbumArtist { get; set; }

        [Parameter]
        public SwitchParameter Composer { get; set; }

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
        public SwitchParameter Loudness { get; set; }

        [Parameter]
        public SwitchParameter CoverArt { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            if (Title) AudioFile!.Metadata.Title = string.Empty;
            if (Artist) AudioFile!.Metadata.Artist = string.Empty;
            if (Album) AudioFile!.Metadata.Album = string.Empty;
            if (AlbumArtist) AudioFile!.Metadata.AlbumArtist = string.Empty;
            if (Composer) AudioFile!.Metadata.Composer = string.Empty;
            if (Genre) AudioFile!.Metadata.Genre = string.Empty;
            if (Comment) AudioFile!.Metadata.Comment = string.Empty;
            if (Day) AudioFile!.Metadata.Day = string.Empty;
            if (Month) AudioFile!.Metadata.Month = string.Empty;
            if (Year) AudioFile!.Metadata.Year = string.Empty;
            if (TrackNumber) AudioFile!.Metadata.TrackNumber = string.Empty;
            if (TrackCount) AudioFile!.Metadata.TrackCount = string.Empty;
            if (Loudness)
            {
                AudioFile!.Metadata.TrackPeak = string.Empty;
                AudioFile.Metadata.AlbumPeak = string.Empty;
                AudioFile.Metadata.TrackGain = string.Empty;
                AudioFile.Metadata.AlbumGain = string.Empty;
            }

            if (CoverArt) AudioFile!.Metadata.CoverArt = null;

            // If no switches were specified, clear everything
            if (!(Title || Artist || Album || AlbumArtist || Composer || Genre || Comment ||
                  Day || Month || Year || TrackNumber || TrackCount || Loudness || CoverArt))
                AudioFile!.Metadata.Clear();

            ProcessLogMessages();

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
