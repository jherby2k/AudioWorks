using AudioWorks.Common;
using Id3Lib;
using Id3Lib.Frames;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Id3
{
    sealed class TagModelToMetadataAdapter : AudioMetadata
    {
        internal TagModelToMetadataAdapter([NotNull] TagModel tagModel)
        {
            foreach (var frame in tagModel)
                switch (frame)
                {
                    case FrameText frameText:
                        switch (frameText.FrameId)
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

                            // The TDAT frame contains the day and the month:
                            case "TDAT":
                                Day = frameText.Text.Substring(0, 2);
                                Month = frameText.Text.Substring(2);
                                break;

                            case "TYER":
                                Year = frameText.Text;
                                break;

                            // The TRCK frame contains the track number and (optionally) the track count:
                            case "TRCK":
                                var segments = frameText.Text.Split('/');
                                TrackNumber = segments[0];
                                if (segments.Length > 1)
                                    TrackCount = segments[1];
                                break;
                        }
                        break;

                    case FrameFullText frameFullText:
                        if (string.CompareOrdinal("COMM", frameFullText.FrameId) == 0 && string.IsNullOrEmpty(frameFullText.Description))
                            Comment = frameFullText.Text;
                        break;

                    case FrameTextUserDef frameTextUserDef:
                        switch (frameTextUserDef.Description)
                        {
                            case "REPLAYGAIN_TRACK_PEAK":
                                TrackPeak = frameTextUserDef.Text;
                                break;

                            case "REPLAYGAIN_ALBUM_PEAK":
                                AlbumPeak = frameTextUserDef.Text;
                                break;

                            case "REPLAYGAIN_TRACK_GAIN":
                                TrackGain = frameTextUserDef.Text.Replace(" dB", string.Empty);
                                break;

                            case "REPLAYGAIN_ALBUM_GAIN":
                                AlbumGain = frameTextUserDef.Text.Replace(" dB", string.Empty);
                                break;
                        }
                        break;
                }
        }
    }
}