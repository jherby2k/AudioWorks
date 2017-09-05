using System;

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