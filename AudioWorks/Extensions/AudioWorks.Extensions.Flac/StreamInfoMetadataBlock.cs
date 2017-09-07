namespace AudioWorks.Extensions.Flac
{
    struct StreamInfoMetadataBlock
    {
        internal MetadataType Type;

        internal bool IsLast;

        internal uint Length;

        internal StreamInfo StreamInfo;
    }
}