namespace AudioWorks.Extensions.Mp3
{
    struct OptionalHeader
    {
        internal uint FrameCount { get; set; }

        internal uint ByteCount { get; set; }

        internal bool Incomplete => FrameCount == 0 || ByteCount == 0;
    }
}
