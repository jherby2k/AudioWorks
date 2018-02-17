using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class CoverArtToPictureBlockAdapter : PictureBlock
    {
        internal CoverArtToPictureBlockAdapter([NotNull] CoverArt coverArt)
        {
            SetData(coverArt.GetData());
            SetType(PictureType.CoverFront);
            SetMimeType(coverArt.MimeType);
            SetWidth(coverArt.Width);
            SetHeight(coverArt.Height);
            SetColorDepth(coverArt.ColorDepth);
        }
    }
}