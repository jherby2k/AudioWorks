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
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

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
                        string.Equals("ftyp", atom.FourCc, StringComparison.Ordinal)), tempStream);
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                        string.Equals("moov", atom.FourCc, StringComparison.Ordinal)), tempStream);

                // Move to the start of the ilst atom
                originalMp4.DescendToAtom("moov", "udta", "meta", "ilst");
                tempMp4.DescendToAtom("moov", "udta", "meta", "ilst");

                // Remove any copied ilst atoms, then generate new ones
                tempStream.SetLength(tempStream.Position);
                var ilstData = GenerateIlst(originalMp4, metadata);
                tempStream.Write(ilstData, 0, ilstData.Length);

                // Update the ilst and parent atom sizes:
                tempMp4.UpdateAtomSizes((uint) tempStream.Length - tempMp4.CurrentAtom.End);

                // Update the stco atom to reflect the new location of mdat:
                tempMp4.UpdateStco((uint) (tempStream.Length - topAtoms.Single(atom =>
                                               string.Equals("mdat", atom.FourCc, StringComparison.Ordinal)).Start));

                // Copy the mdat atom to the temporary stream, after the moov atom
                tempStream.Seek(0, SeekOrigin.End);
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                    string.Equals("mdat", atom.FourCc, StringComparison.Ordinal)), tempStream);

                // Overwrite the original stream with the new, optimized one
                stream.SetLength(tempStream.Length);
                stream.Position = 0;
                tempStream.Position = 0;
                tempStream.CopyTo(stream);
            }
        }

        [NotNull]
        static byte[] GenerateIlst(
            [NotNull] Mp4Model originalMp4,
            [NotNull] AudioMetadata metadata)
        {
            using (var resultStream = new MemoryStream())
            {
                var adaptedMetadata = new MetadataToIlstAtomAdapter(metadata);

                // "Reverse DNS" atoms may need to be preserved:
                foreach (var reverseDnsAtom in originalMp4.GetChildAtomInfo()
                    .Where(childAtom => string.Equals("----", childAtom.FourCc, StringComparison.Ordinal))
                    .Select(childAtom => new ReverseDnsAtom(originalMp4.ReadAtom(childAtom))))
                    switch (reverseDnsAtom.Name)
                    {
                        // Always preserve the iTunSMPB (gapless playback) atom:
                        case "iTunSMPB":
                            resultStream.Write(reverseDnsAtom.GetBytes(), 0, reverseDnsAtom.GetBytes().Length);
                            break;
                    }

                var atomData = adaptedMetadata.GetBytes();
                resultStream.Write(atomData, 0, atomData.Length);

                return resultStream.ToArray();
            }
        }
    }
}
