using System;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    class AudioInfoStreamDecoder : StreamDecoder
    {
        [CanBeNull]
        internal AudioInfo AudioInfo { get; private set; }

        internal AudioInfoStreamDecoder([NotNull] Stream stream)
            : base(stream)
        {
        }

        protected override void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
            if ((MetadataType) Marshal.ReadInt32(metadataBlock) != MetadataType.StreamInfo)
                return;

            var streamInfo = Marshal.PtrToStructure<StreamInfoMetadataBlock>(metadataBlock).StreamInfo;
            AudioInfo = AudioInfo.CreateForLossless(
                "FLAC",
                (int) streamInfo.Channels,
                (int) streamInfo.BitsPerSample,
                (int) streamInfo.SampleRate,
                (long) streamInfo.TotalSamples);
        }
    }
}