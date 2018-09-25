/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections.Generic;
using System.Globalization;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class IlstAtomToMetadataAdapter : AudioMetadata
    {
        internal IlstAtomToMetadataAdapter(
            [NotNull] Mp4Model mp4,
            [NotNull, ItemNotNull] IEnumerable<AtomInfo> atoms)
        {
            foreach (var atom in atoms)
            {
                var atomData = mp4.ReadAtom(atom);

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (atom.FourCc)
                {
                    case "©nam":
                        Title = new TextAtom(atomData).Value;
                        break;

                    case "©ART":
                        Artist = new TextAtom(atomData).Value;
                        break;

                    case "©alb":
                        Album = new TextAtom(atomData).Value;
                        break;

                    case "aART":
                        AlbumArtist = new TextAtom(atomData).Value;
                        break;

                    case "©wrt":
                        Composer = new TextAtom(atomData).Value;
                        break;

                    case "©gen":
                        Genre = new TextAtom(atomData).Value;
                        break;

                    case "©cmt":
                        Comment = new TextAtom(atomData).Value;
                        break;

                    case "©day":
                        // The ©day atom may contain a full date, or only the year:
                        var dayAtom = new TextAtom(atomData);
                        if (DateTime.TryParse(dayAtom.Value, CultureInfo.CurrentCulture,
                                DateTimeStyles.NoCurrentDateDefault, out var result))
                        {
                            Day = result.Day.ToString(CultureInfo.InvariantCulture);
                            Month = result.Month.ToString(CultureInfo.InvariantCulture);
                            Year = result.Year.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                            Year = dayAtom.Value;
                        break;

                    case "trkn":
                        var trackNumberAtom = new TrackNumberAtom(atomData);
                        TrackNumber = trackNumberAtom.TrackNumber.ToString(CultureInfo.InvariantCulture);
                        if (trackNumberAtom.TrackCount > 0)
                            TrackCount = trackNumberAtom.TrackCount.ToString(CultureInfo.InvariantCulture);
                        break;

                    case "covr":
                        CoverArt = new CoverAtom(atomData).Value;
                        break;
                }
            }
        }
    }
}