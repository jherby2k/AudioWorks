using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

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
            using (var fileStream = audioFile.FileInfo.OpenRead())
                return BitConverter.ToString(md5.ComputeHash(fileStream))
                    .Replace("-", string.Empty, StringComparison.InvariantCulture);
        }
    }
}
