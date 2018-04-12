using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests
{
    public static class HashUtility
    {
        [Pure, NotNull]
        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "This method is not security critical")]
        public static string CalculateHash([NotNull] IAudioFile audioFile)
        {
            using (var md5 = MD5.Create())
            using (var fileStream = File.OpenRead(audioFile.Path))
                return BitConverter.ToString(md5.ComputeHash(fileStream))
#if NETCOREAPP2_1
                    .Replace("-", string.Empty, StringComparison.InvariantCulture);
#else
                    .Replace("-", string.Empty);
#endif
        }

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
