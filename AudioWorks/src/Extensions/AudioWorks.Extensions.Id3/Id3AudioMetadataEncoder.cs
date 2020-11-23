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
using System.IO;
using System.Text;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Id3
{
    [AudioMetadataEncoderExport(".mp3", "ID3", "ID3 version 2.x")]
    sealed class Id3AudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new()
        {
            ["TagVersion"] = new StringSettingInfo("2.3", "2.4"),
            ["TagEncoding"] = new StringSettingInfo("Latin1", "UTF16", "UTF8"),
            ["TagPadding"] = new IntSettingInfo(0, 16_777_216)
        };

        public void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            var existingTagLength = GetExistingTagLength(stream);

            string encoding = "Latin1";
            if (settings.TryGetValue("TagEncoding", out string? encodingValue))
                encoding = encodingValue!;

            var version = 3;
            if (settings.TryGetValue("TagVersion", out string? versionValue))
            {
                if (versionValue!.Equals("2.3", StringComparison.Ordinal) &&
                    encoding.Equals("UTF8", StringComparison.Ordinal))
                {
                    var logger = LoggerManager.LoggerFactory.CreateLogger<Id3AudioMetadataEncoder>();
                    logger.LogWarning("ID3 version 2.3 tags don't support UTF-8. Using version 2.4.");
                }

                if (versionValue!.Equals("2.4", StringComparison.Ordinal))
                    version = 4;
            }

            // Force version 2.4 when UTF-8 is requested
            if (encoding.Equals("UTF8", StringComparison.Ordinal))
                version = 4;

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
                    Overwrite(stream, existingTagLength, tagModel);
                else
                    FullRewrite(stream, existingTagLength, tagModel);
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

        static uint GetExistingTagLength(Stream stream)
        {
            try
            {
                var existingTag = TagManager.Deserialize(stream);
                return existingTag.Header.TagSizeWithHeaderFooter - existingTag.Header.PaddingSize;
            }
            catch (TagNotFoundException)
            {
                return 0;
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
    }
}
