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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using SixLabors.ImageSharp;

namespace AudioWorks.Common
{
    /// <summary>
    /// Provides static methods for creating <see cref="ICoverArt"/> objects.
    /// </summary>
    public static class CoverArtFactory
    {
        static readonly string[] _acceptedExtensions = [".bmp", ".png", ".jpg", ".jpeg"];
        static readonly Dictionary<string, WeakReference<CoverArt>> _internedCovers = new();

        /// <summary>
        /// Returns an <see cref="ICoverArt"/> object from a byte array.
        /// </summary>
        /// <remarks>
        /// If an identical image has already been loaded into memory, this method will return a copy of the existing
        /// <see cref="ICoverArt"/> instance.
        /// </remarks>
        /// <param name="data">The raw image data.</param>
        /// <exception cref="ImageUnsupportedException">Thrown if <paramref name="data"/> is not in a supported image
        /// format.</exception>
        /// <exception cref="ImageInvalidException">Thrown if <paramref name="data"/> is not a valid image.</exception>
        public static unsafe ICoverArt GetOrCreate(ReadOnlySpan<byte> data)
        {
            fixed (byte* dataAddress = data)
                using (var memoryStream = new UnmanagedMemoryStream(dataAddress, data.Length))
                    return GetOrCreate(memoryStream);
        }

        /// <summary>
        /// Returns an <see cref="ICoverArt"/> object for an image file.
        /// </summary>
        /// <remarks>
        /// If an identical image has already been loaded into memory, this method will return a copy of the existing
        /// <see cref="ICoverArt"/> instance.
        /// </remarks>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        /// <exception cref="ImageUnsupportedException">Thrown if <paramref name="path"/> is not in a supported image
        /// format.</exception>
        /// <exception cref="ImageInvalidException">Thrown if <paramref name="path"/> is not a valid image file.
        /// </exception>
        public static ICoverArt GetOrCreate(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "Value cannot be null or empty.");
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' cannot be found.", path);

            if (!_acceptedExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase))
                throw new ImageUnsupportedException("Not a supported image format.");

            using (var fileStream = File.OpenRead(path))
                return GetOrCreate(fileStream);
        }

        /// <summary>
        /// Creates a lossy <see cref="ICoverArt"/> from a lossless one. If <paramref name="coverArt"/> is already
        /// lossless, this method returns it unmodified.
        /// </summary>
        /// <param name="coverArt">The cover art.</param>
        /// <returns>A lossy copy of an <see cref="ICoverArt"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="coverArt"/> is null.</exception>
        public static ICoverArt ConvertToLossy(ICoverArt coverArt)
        {
            if (coverArt == null) throw new ArgumentNullException(nameof(coverArt));

            if (!coverArt.Lossless) return coverArt;

            using (var tempStream = new MemoryStream())
            using (var image = Image.Load(coverArt.Data))
            {
                image.SaveAsJpeg(tempStream);
                tempStream.Position = 0;
                return new CoverArt(tempStream);
            }
        }

        static ICoverArt GetOrCreate(Stream stream)
        {
            var hash = GetHash(stream);
            lock (_internedCovers)
            {
                // See if the cover is already interned
                if (_internedCovers.TryGetValue(hash, out var coverReference))
                {
                    // If the reference is alive, use it
                    if (coverReference.TryGetTarget(out var coverArt))
                        return coverArt;

                    // If the reference is dead, remove it
                    _internedCovers.Remove(hash);
                }

                // Intern the cover
                var result = new CoverArt(stream);
                _internedCovers.Add(hash, new(result));
                return result;
            }
        }

        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "Usage is not security critical")]
        static string GetHash(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                var result = BitConverter.ToString(md5.ComputeHash(stream));
                stream.Position = 0;
                return result;
            }
        }
    }
}
