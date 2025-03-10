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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Id3
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioMetadataEncoderExport(".mp3", "ID3", "ID3 version 2.x")]
    sealed class Id3AudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new(new Dictionary<string, SettingInfo>
        {
            ["TagVersion"] = new StringSettingInfo("2.3", "2.4"),
            ["TagEncoding"] = new StringSettingInfo("Latin1", "UTF16", "UTF8"),
            ["TagPadding"] = new IntSettingInfo(0, 16_777_216)
        });

        public void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<Id3AudioMetadataEncoder>();

            var existingTag = GetExistingTag(stream);

            var encoding = "Latin1";
            if (settings.TryGetValue("TagEncoding", out string? encodingValue))
                encoding = encodingValue!;

            // Default to the existing tag version, if present. Otherwise use 2.3 for compatibility
            var version = existingTag == null ? 3 : existingTag.Header.Version;
            if (settings.TryGetValue("TagVersion", out string? versionValue))
            {
                if (versionValue!.Equals("2.3", StringComparison.Ordinal) &&
                    encoding.Equals("UTF8", StringComparison.Ordinal))
                    logger.LogWarning("ID3 version 2.3 tags don't support UTF-8. Using version 2.4.");

                if (versionValue.Equals("2.4", StringComparison.Ordinal))
                    version = 4;
            }

            // Use unicode if there are multibyte characters present
            if (encoding == "Latin1" && ContainsUnicode(metadata))
            {
                encoding = version == 3 ? "UTF16" : "UTF8";
                logger.LogWarning("Multibyte characters detected. Using {encoding} encoding instead of Latin1.", encoding);
            }

            // Force version 2.4 when UTF-8 is requested
            if (encoding.Equals("UTF8", StringComparison.Ordinal))
                version = 4;

            var existingTagLength = existingTag == null
                ? 0
                : existingTag.Header.TagSizeWithHeaderFooter - existingTag.Header.PaddingSize;

            var tagModel = new MetadataToTagModelAdapter(metadata, version, encoding);
            if (tagModel.Frames.Count > 0)
            {
                // Set the padding (default to 2048)
                if (settings.TryGetValue("TagPadding", out int padding))
                    tagModel.Header.PaddingSize = (uint) padding;
                else
                    tagModel.Header.PaddingSize = 2048;

                tagModel.UpdateSize();

                if (!settings.ContainsKey("TagPadding") && existingTagLength >= tagModel.Header.TagSizeWithHeaderFooter)
                {
                    logger.LogDebug("Existing tag will be overwritten in-place.");
                    Overwrite(stream, existingTagLength, tagModel);
                }
                else
                {
                    logger.LogDebug("Entire file will be re-written with the new tag.");
                    FullRewrite(stream, existingTagLength, tagModel);
                }
            }
            else if (existingTagLength > 0)
            {
                // Remove the ID3v2 tag, if present
                using (var tempStream = new TempFileStream())
                {
                    stream.CopyTo(tempStream);
                    stream.Position = 0;
                    stream.SetLength(stream.Length - existingTagLength);
                    tempStream.Position = 0;
                    tempStream.CopyTo(stream);
                }
            }

            // Remove the ID3v1 tag, if present
            if (stream.Length < 128) return;
            stream.Seek(-128, SeekOrigin.End);
            using (var reader = new BinaryReader(stream, Encoding.ASCII, true))
                if (new string(reader.ReadChars(3)).Equals("TAG", StringComparison.Ordinal))
                    stream.SetLength(stream.Length - 128);
            stream.Position = tagModel.Frames.Count == 0
                ? 0
                : tagModel.Header.TagSizeWithHeaderFooter + tagModel.Header.PaddingSize;
        }

        static TagModel? GetExistingTag(Stream stream)
        {
            try
            {
                return TagManager.Deserialize(stream);
            }
            catch (TagNotFoundException)
            {
                return null;
            }
        }

        static void Overwrite(Stream stream, uint existingTagLength, TagModel tagModel)
        {
            // Write the new tag over the old one, leaving unused space as padding
            stream.Position = 0;
            tagModel.Header.PaddingSize = existingTagLength - tagModel.Header.TagSizeWithHeaderFooter;
            TagManager.Serialize(tagModel, stream);
        }

        static void FullRewrite(Stream stream, uint existingTagLength, TagModel tagModel)
        {
            // Copy the audio to memory, then rewrite the whole stream
            stream.Position = existingTagLength;
            using (var tempStream = new TempFileStream())
            {
                // Copy the MP3 portion to memory
                stream.CopyTo(tempStream);

                // Rewrite the stream with the new tag in front
                stream.Position = 0;
                stream.SetLength(tagModel.Header.TagSizeWithHeaderFooter + tagModel.Header.PaddingSize + tempStream.Length);
                TagManager.Serialize(tagModel, stream);
                tempStream.Position = 0;
                tempStream.CopyTo(stream);
            }
        }

        static bool ContainsUnicode(AudioMetadata metadata) => ContainsUnicode(metadata.Title) ||
                                                               ContainsUnicode(metadata.Artist) ||
                                                               ContainsUnicode(metadata.Album) ||
                                                               ContainsUnicode(metadata.AlbumArtist) ||
                                                               ContainsUnicode(metadata.Composer) ||
                                                               ContainsUnicode(metadata.Genre) ||
                                                               ContainsUnicode(metadata.Comment);

        static bool ContainsUnicode(string input) => input.Any(c => c > 255);
    }
}
