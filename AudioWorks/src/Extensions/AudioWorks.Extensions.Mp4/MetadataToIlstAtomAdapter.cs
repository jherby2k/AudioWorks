using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Pure, NotNull]
        internal byte[] GetBytes()
        {
            if (_atoms.Count == 0) return Array.Empty<byte>();

            var atomData = _atoms.Select(x => x.GetBytes()).ToArray();
            var atomSize = atomData.Sum(data => data.Length) + 8;

            using (var stream = new MemoryStream(atomSize))
            using (var writer = new Mp4Writer(stream))
            {
                writer.WriteBigEndian((uint) atomSize);
                writer.Write(BitConverter.GetBytes(0x74736c69));
                foreach (var subAtomData in atomData)
                    writer.Write(subAtomData);
                return stream.GetBuffer();
            }
        }
    }
}