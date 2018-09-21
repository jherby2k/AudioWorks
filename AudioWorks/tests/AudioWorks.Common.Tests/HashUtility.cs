/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace AudioWorks.Common.Tests
{
    public static class HashUtility
    {
        [Pure, NotNull]
        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "This method is not security critical")]
        public static string CalculateHash([CanBeNull] byte[] data)
        {
            if (data == null) return string.Empty;

            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(data))
#if NETCOREAPP2_1
                    .Replace("-", string.Empty, StringComparison.InvariantCulture);
#else
                    .Replace("-", string.Empty);
#endif
        }
    }
}
