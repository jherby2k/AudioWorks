using System;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    [AudioMetadataEncoderExport(".m4a")]
    public sealed class ItunesAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["CreationTime"] = new DateTimeSettingInfo(),
            ["ModificationTime"] = new DateTimeSettingInfo()
        };

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            // Create a temporary stream to hold the new atom structure
            using (var tempStream = new MemoryStream())
            {
                var originalMp4 = new Mp4Model(stream);
                var tempMp4 = new Mp4Model(tempStream);

                var topAtoms = originalMp4.GetChildAtomInfo();

                // Copy the ftyp and moov atoms to the temporary stream
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                    atom.FourCc.Equals("ftyp", StringComparison.Ordinal)), tempStream);
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                    atom.FourCc.Equals("moov", StringComparison.Ordinal)), tempStream);

                // Move to the start of the ilst atom, or where it should be
                originalMp4.DescendToAtom("moov", "udta", "meta");
                tempMp4.DescendToAtom("moov", "udta", "meta");
                tempStream.SetLength(tempStream.Position);

                var childInfo = originalMp4.GetChildAtomInfo();

                // Copy any subatoms other than ilst first (probably just hdlr)
                foreach (var childAtom in childInfo
                    .Where(info => !info.FourCc.Equals("ilst", StringComparison.Ordinal)))
                    originalMp4.CopyAtom(childAtom, tempStream);

                // Generate a new ilst
                var ilstData = GenerateIlst(originalMp4, metadata);
                tempStream.Write(ilstData, 0, ilstData.Length);

                // Update the ilst and parent atom sizes
                tempMp4.UpdateAtomSizes((uint) tempStream.Length - tempMp4.CurrentAtom.End);

                // Update the time stamps, if requested
                DateTime? newCreationTime = null;
                DateTime? newModificationTime = null;
                if (settings.TryGetValue<DateTime>("CreationTime", out var creationTime))
                    newCreationTime = creationTime;
                if (settings.TryGetValue<DateTime>("ModificationTime", out var modificationTime))
                    newModificationTime = modificationTime;
                if (newCreationTime.HasValue || newModificationTime.HasValue)
                    tempMp4.UpdateTimeStamps(newCreationTime, newModificationTime);

                // Update the stco atom to reflect the new location of mdat
                tempMp4.UpdateStco((uint) (tempStream.Length - topAtoms.Single(atom =>
                                               atom.FourCc.Equals("mdat", StringComparison.Ordinal)).Start));

                // Copy the mdat atom to the temporary stream, after the moov atom
                tempStream.Seek(0, SeekOrigin.End);
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                    atom.FourCc.Equals("mdat", StringComparison.Ordinal)), tempStream);

                // Overwrite the original stream with the new, optimized one
                stream.Position = 0;
                stream.SetLength(tempStream.Length);
                tempStream.WriteTo(stream);
            }
        }

        [NotNull]
        static byte[] GenerateIlst(
            [NotNull] Mp4Model originalMp4,
            [NotNull] AudioMetadata metadata)
        {
            // Always store images in JPEG format for AAC, since it is also lossy
            originalMp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stsd", "mp4a", "esds");
            var adaptedMetadata = new MetadataToIlstAtomAdapter(metadata,
                new EsdsAtom(originalMp4.ReadAtom(originalMp4.CurrentAtom)).IsAac);

            // If there is an existing ilst atom, The iTunSMPB "Reverse DNS" subatom should be preserved
            if (originalMp4.DescendToAtom("moov", "udta", "meta", "ilst"))
                foreach (var reverseDnsAtom in originalMp4.GetChildAtomInfo()
                    .Where(childAtom => childAtom.FourCc.Equals("----", StringComparison.Ordinal))
                    .Select(childAtom => new ReverseDnsAtom(originalMp4.ReadAtom(childAtom)))
                    .Where(reverseDnsAtom => reverseDnsAtom.Name.Equals("iTunSMPB", StringComparison.Ordinal)))
                    adaptedMetadata.Prepend(reverseDnsAtom);

            return adaptedMetadata.GetBytes();
        }
    }
}
