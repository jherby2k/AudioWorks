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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Flac.Metadata
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioMetadataEncoderExport(".flac", "FLAC", "FLAC")]
    sealed class FlacAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new(new Dictionary<string, SettingInfo>
        {
            ["Padding"] = new IntSettingInfo(0, 16_777_216)
        });

        public void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            using (var chain = new MetadataChain(stream))
            using (var comments = new MetadataToVorbisCommentAdapter(metadata))
            {
                chain.Read();

                PictureMetadataObject? pictureBlock = null;
                PaddingMetadataObject? paddingBlock = null;
                try
                {
                    if (metadata.CoverArt != null)
                        pictureBlock = new CoverArtToPictureMetadataObjectAdapter(metadata.CoverArt);

                    var padding = GetPadding(settings);
                    if (padding > 0)
                        paddingBlock = new(padding.Value);

                    // Iterate over the existing blocks, replacing and deleting as needed
                    using (var iterator = chain.GetIterator())
                        UpdateChain(iterator, comments, pictureBlock, paddingBlock);

                    if (chain.CheckIfTempFileNeeded(!padding.HasValue))
                        using (var tempStream = new TempFileStream())
                        {
                            chain.WriteWithTempFile(!padding.HasValue, tempStream);

                            // Clear the original stream, and copy the temporary one over it
                            stream.Position = 0;
                            stream.SetLength(tempStream.Length);
                            tempStream.Position = 0;
                            tempStream.CopyTo(stream);
                        }
                    else
                        chain.Write(!padding.HasValue);
                }
                finally
                {
                    pictureBlock?.Dispose();
                    paddingBlock?.Dispose();
                }
            }
        }

        static int? GetPadding(SettingDictionary settings)
        {
            if (settings.TryGetValue("Padding", out int result))
                return result;
            return null;
        }

        static void UpdateChain(
            MetadataIterator iterator,
            MetadataObject newComments,
            MetadataObject? pictureBlock,
            MetadataObject? paddingBlock)
        {
            var metadataInserted = false;
            var pictureInserted = false;

            do
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch ((MetadataType) Marshal.ReadInt32(iterator.GetBlock()))
                {
                    // Replace the existing Vorbis comment
                    case MetadataType.VorbisComment:
                        iterator.DeleteBlock(false);
                        iterator.InsertBlockAfter(newComments);
                        metadataInserted = true;
                        break;

                    // Replace the existing Picture block:
                    case MetadataType.Picture:
                        iterator.DeleteBlock(false);
                        if (pictureBlock != null)
                            iterator.InsertBlockAfter(pictureBlock);
                        pictureInserted = true;
                        break;

                    // Delete any padding
                    case MetadataType.Padding:
                        iterator.DeleteBlock(false);
                        break;
                } while (iterator.Next());

            // If there was no existing metadata block to replace, insert it now
            if (!metadataInserted)
                iterator.InsertBlockAfter(newComments);

            // If there was no existing picture block to replace, and a new one is available, insert it now
            if (pictureBlock != null && !pictureInserted)
                iterator.InsertBlockAfter(pictureBlock);

            // If padding was explicitly requested, add it
            if (paddingBlock != null)
                iterator.InsertBlockAfter(paddingBlock);
        }
    }
}
