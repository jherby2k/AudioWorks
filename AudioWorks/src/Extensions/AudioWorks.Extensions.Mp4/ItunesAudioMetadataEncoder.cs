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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Mp4
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioMetadataEncoderExport(".m4a", "iTunes", "iTunes-compatible MPEG-4")]
    sealed class ItunesAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["CreationTime"] = new DateTimeSettingInfo(),
            ["ModificationTime"] = new DateTimeSettingInfo(),
            ["Padding"] = new IntSettingInfo(0, 16_777_216)
        };

        public void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            // Create a temporary stream to hold the new atom structure
            using (var tempStream = GetTempStream(stream.Length))
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
                //tempStream.SetLength(tempStream.Position);

                var childInfo = originalMp4.GetChildAtomInfo();

                // Copy any subatoms other than ilst and free (probably just hdlr)
                var excludedChildAtoms = new[] { "ilst", "free" };
                foreach (var childAtom in childInfo
                    .Where(info => !excludedChildAtoms.Contains(info.FourCc, StringComparer.Ordinal)))
                    originalMp4.CopyAtom(childAtom, tempStream);

                // Write the new ilst atom
                WriteIlst(originalMp4, metadata, tempStream);

                // Write a 2048 byte free atom by default
                if (!settings.TryGetValue("Padding", out int freeSize))
                    freeSize = 2048;
                new FreeAtom((uint) freeSize).Write(tempStream);

                var endOfData = tempStream.Position;

                // Update the parent atom sizes
                tempMp4.UpdateAtomSizes((uint) endOfData - tempMp4.CurrentAtom.End);

                // Update the time stamps, if requested
                DateTime? newCreationTime = null;
                DateTime? newModificationTime = null;
                if (settings.TryGetValue("CreationTime", out DateTime creationTime))
                    newCreationTime = creationTime;
                if (settings.TryGetValue("ModificationTime", out DateTime modificationTime))
                    newModificationTime = modificationTime;
                if (newCreationTime.HasValue || newModificationTime.HasValue)
                    tempMp4.UpdateTimeStamps(newCreationTime, newModificationTime);

                // Update the stco atom to reflect the new location of mdat
                tempMp4.UpdateStco((uint) (endOfData - topAtoms.Single(atom =>
                                               atom.FourCc.Equals("mdat", StringComparison.Ordinal)).Start));

                // Copy the mdat atom to the temporary stream, after the moov atom
                tempStream.Position = endOfData;
                originalMp4.CopyAtom(topAtoms.Single(atom =>
                    atom.FourCc.Equals("mdat", StringComparison.Ordinal)), tempStream);

                // The pre-allocation was based on an estimated length
                tempStream.SetLength(tempStream.Position);

                // Overwrite the original stream with the new, optimized one
                stream.Position = 0;
                stream.SetLength(tempStream.Length);
                tempStream.Position = 0;
                tempStream.CopyTo(stream);
            }
        }

        static Stream GetTempStream(long length)
        {
            // Use a memory stream for < 64 MB
            var result = length < 0x400_0000 ? new MemoryStream() : (Stream) new TempFileStream();
            result.SetLength(length);
            return result;
        }

        static void WriteIlst(Mp4Model originalMp4, AudioMetadata metadata, Stream output)
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

            adaptedMetadata.Write(output);
        }
    }
}
