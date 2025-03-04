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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace AudioWorks.Common
{
    /// <inheritdoc/>
    [Serializable]
    public sealed class CoverArt : ICoverArt
    {
        /// <inheritdoc/>
        public int Width { get; }

        /// <inheritdoc/>
        public int Height { get; }

        /// <inheritdoc/>
        public int ColorDepth { get; }

        /// <inheritdoc/>
        public bool Lossless { get; }

        /// <inheritdoc/>
        public string MimeType { get; }

        /// <inheritdoc/>
        public string FileExtension { get; }

        /// <inheritdoc/>
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays",
            Justification = "Property is part of a Data Transfer Object")]
        public byte[] Data { get; }

        internal CoverArt(Stream stream)
        {
            try
            {
                var format = Image.DetectFormat(stream);

                var info = Image.Identify(stream);
                Width = info.Width;
                Height = info.Height;
                ColorDepth = info.PixelType.BitsPerPixel;

                stream.Position = 0;

                // Bitmaps should be compressed in PNG format
                if (format.Name.Equals("BMP", StringComparison.OrdinalIgnoreCase))
                    using (var pngStream = new MemoryStream())
                    {
                        // Only create an alpha channel if necessary
                        using (var image = Image.Load(stream))
                            if (ColorDepth <= 24)
                                image.SaveAsPng(pngStream, new() { ColorType = PngColorType.Rgb });
                            else
                                image.SaveAsPng(pngStream);

                        Data = pngStream.ToArray();
                        pngStream.Position = 0;
                        format = Image.DetectFormat(pngStream);
                    }

                // JPEG and PNG images should be stored verbatim
                else
                {
                    if (stream is MemoryStream currentStream)
                        Data = currentStream.ToArray();
                    else
                    {
                        Data = new byte[stream.Length];
                        using (var memoryStream = new MemoryStream(Data))
                            stream.CopyTo(memoryStream);
                    }
                }

                Lossless = format.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase);
                MimeType = format.DefaultMimeType;
                FileExtension = $".{format.FileExtensions.First()}";
            }
            catch (UnknownImageFormatException e)
            {
                throw new ImageInvalidException("Not a valid image.", e);
            }
        }
    }
}
