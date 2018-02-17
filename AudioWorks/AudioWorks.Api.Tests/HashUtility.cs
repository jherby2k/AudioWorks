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
                    .Replace("-", string.Empty, StringComparison.InvariantCulture);
        }

        [Pure, NotNull]
        [SuppressMessage("Microsoft.Security", "CA5351:Do not use insecure cryptographic algorithm MD5.",
            Justification = "This method is not security critical")]
        public static string CalculateHash([NotNull] byte[] data)
        {
            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(data))
                    .Replace("-", string.Empty, StringComparison.InvariantCulture);
        }
    }
}
