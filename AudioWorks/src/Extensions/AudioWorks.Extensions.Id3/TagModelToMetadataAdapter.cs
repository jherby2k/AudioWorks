﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class TagModelToMetadataAdapter : AudioMetadata
    {
        internal TagModelToMetadataAdapter(TagModel tagModel)
        {
            foreach (var frame in tagModel.Frames)
                switch (frame)
                {
                    case FrameFullText frameFullText:
                        if (frameFullText.FrameId.Equals("COMM", StringComparison.OrdinalIgnoreCase) &&
                            string.IsNullOrEmpty(frameFullText.Description))
                            Comment = frameFullText.Text;
                        break;

                    case FrameTextUserDef frameTextUserDef:
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (frameTextUserDef.Description.ToUpperInvariant())
                        {
                            case "REPLAYGAIN_TRACK_PEAK":
                                TrackPeak = frameTextUserDef.Text;
                                break;

                            case "REPLAYGAIN_ALBUM_PEAK":
                                AlbumPeak = frameTextUserDef.Text;
                                break;

                            case "REPLAYGAIN_TRACK_GAIN":
                                TrackGain = frameTextUserDef.Text.Replace(" dB", string.Empty,
                                    StringComparison.OrdinalIgnoreCase);
                                break;

                            case "REPLAYGAIN_ALBUM_GAIN":
                                AlbumGain = frameTextUserDef.Text.Replace(" dB", string.Empty,
                                    StringComparison.OrdinalIgnoreCase);
                                break;
                        }
                        break;

                    case FrameText frameText:
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (frameText.FrameId.ToUpperInvariant())
                        {
                            case "TIT2":
                                Title = frameText.Text;
                                break;

                            case "TPE1":
                                Artist = frameText.Text;
                                break;

                            case "TALB":
                                Album = frameText.Text;
                                break;

                            case "TPE2":
                                AlbumArtist = frameText.Text;
                                break;

                            case "TCOM":
                                Composer = frameText.Text;
                                break;

                            case "TCON":
                                Genre = frameText.Text;
                                break;

                            // The TDAT frame contains the day and the month
                            case "TDAT":
                                Day = frameText.Text[..2];
                                Month = frameText.Text[2..];
                                break;

                            case "TYER":
                                Year = frameText.Text;
                                break;

                            // The TDRC frame can contain just the year, the year and month, or the year month and day
                            case "TDRC":
                                var splitRecordingTime = frameText.Text.Split('-');
                                Year = splitRecordingTime[0];
                                if (splitRecordingTime.Length > 1)
                                {
                                    Month = splitRecordingTime[1];
                                    if (splitRecordingTime.Length > 2)
                                        Day = splitRecordingTime[2][..2];
                                }

                                break;

                            // The TRCK frame contains the track number and (optionally) the track count
                            case "TRCK":
                                var splitTrackNumber = frameText.Text.Split('/');
                                TrackNumber = splitTrackNumber[0];
                                if (splitTrackNumber.Length > 1)
                                    TrackCount = splitTrackNumber[1];
                                break;
                        }
                        break;

                    case FramePicture framePicture:
                        CoverArt = CoverArtFactory.GetOrCreate(framePicture.PictureData);
                        break;
                }
        }
    }
}