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

using System.Collections.Generic;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class MetadataToIlstAtomAdapter
    {
        [NotNull, ItemNotNull] readonly List<WritableAtom> _atoms = new List<WritableAtom>();

        internal MetadataToIlstAtomAdapter([NotNull] AudioMetadata metadata, bool compressCoverArt)
        {
            if (!string.IsNullOrEmpty(metadata.Title))
                _atoms.Add(new TextAtom("©nam", metadata.Title));
            if (!string.IsNullOrEmpty(metadata.Artist))
                _atoms.Add(new TextAtom("©ART", metadata.Artist));
            if (!string.IsNullOrEmpty(metadata.Album))
                _atoms.Add(new TextAtom("©alb", metadata.Album));
            if (!string.IsNullOrEmpty(metadata.AlbumArtist))
                _atoms.Add(new TextAtom("aART", metadata.AlbumArtist));
            if (!string.IsNullOrEmpty(metadata.Composer))
                _atoms.Add(new TextAtom("©wrt", metadata.Composer));
            if (!string.IsNullOrEmpty(metadata.Genre))
                _atoms.Add(new TextAtom("©gen", metadata.Genre));
            if (!string.IsNullOrEmpty(metadata.Comment))
                _atoms.Add(new TextAtom("©cmt", metadata.Comment));

            if (!string.IsNullOrEmpty(metadata.Day) &&
                !string.IsNullOrEmpty(metadata.Month) &&
                !string.IsNullOrEmpty(metadata.Year))
                _atoms.Add(new TextAtom("©day",
                    $"{metadata.Year}-{metadata.Month}-{metadata.Day}"));
            else if (!string.IsNullOrEmpty(metadata.Year))
                _atoms.Add(new TextAtom("©day", metadata.Year));

            if (!string.IsNullOrEmpty(metadata.TrackNumber))
                _atoms.Add(new TrackNumberAtom(metadata.TrackNumber, metadata.TrackCount));

            if (metadata.CoverArt != null)
                _atoms.Add(new CoverAtom(compressCoverArt
                    ? CoverArtFactory.ConvertToLossy(metadata.CoverArt)
                    : metadata.CoverArt));
        }

        internal void Prepend([NotNull] WritableAtom atom)
        {
            _atoms.Insert(0, atom);
        }

        internal void Write([NotNull] Stream output)
        {
            if (_atoms.Count == 0) return;

            var startPosition = output.Position;

            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header, but we don't know the size yet
                writer.Seek(4, SeekOrigin.Current);
                writer.WriteBigEndian(0x696c7374); // 'ilst'

                foreach (var atom in _atoms)
                    atom.Write(output);

                // Update the size
                var size = output.Position - startPosition;
                writer.BaseStream.Position = startPosition;
                writer.WriteBigEndian((uint) size);
                writer.Seek((int) size - 4, SeekOrigin.Current);
            }
        }
    }
}