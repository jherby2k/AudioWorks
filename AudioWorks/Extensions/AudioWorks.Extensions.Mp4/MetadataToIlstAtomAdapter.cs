using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class MetadataToIlstAtomAdapter
    {
        [NotNull, ItemNotNull] readonly List<WritableAtom> _atoms = new List<WritableAtom>();

        public MetadataToIlstAtomAdapter([NotNull] AudioMetadata metadata)
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

            _atoms.Add(new TrackNumberAtom(metadata.TrackNumber, metadata.TrackCount));
        }

        [Pure, NotNull]
        public byte[] GetBytes()
        {
            return _atoms.SelectMany(x => x.GetBytes()).ToArray();
        }
    }
}