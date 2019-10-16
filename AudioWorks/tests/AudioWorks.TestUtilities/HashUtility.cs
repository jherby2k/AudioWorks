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
using System.Security.Cryptography;

namespace AudioWorks.TestUtilities
{
    public static class HashUtility
    {
        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "This method is not security critical")]
        public static string CalculateHash(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var fileStream = File.OpenRead(filePath))
                return BitConverter.ToString(md5.ComputeHash(fileStream))
                    .Replace("-", string.Empty);
        }

        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "This method is not security critical")]
        public static string CalculateHash(byte[]? data)
        {
            if (data == null) return string.Empty;

            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(data))
                    .Replace("-", string.Empty);
        }
    }
}
