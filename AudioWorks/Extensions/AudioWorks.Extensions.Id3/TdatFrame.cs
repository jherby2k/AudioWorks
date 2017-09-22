using AudioWorks.Common;
using Id3Lib.Frames;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Id3
{
    sealed class TdatFrame : FrameText
    {
        public TdatFrame([NotNull] AudioMetadata metadata)
            : base("TDAT")
        {
            if (string.IsNullOrEmpty(metadata.Day) || string.IsNullOrEmpty(metadata.Month))
                Text = string.Empty;
            else
                Text = metadata.Day + metadata.Month;
        }
    }
}