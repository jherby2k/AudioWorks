using System;

#pragma warning disable 169, 649

namespace AudioWorks.Extensions.Vorbis
{
    struct VorbisComment
    {
        internal IntPtr UserComments;

        internal IntPtr CommentLengths;

        internal int Comments;

        internal IntPtr Vendor;
    }
}

#pragma warning restore 169, 649