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
