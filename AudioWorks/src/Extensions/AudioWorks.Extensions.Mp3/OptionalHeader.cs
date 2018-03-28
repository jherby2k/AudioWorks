namespace AudioWorks.Extensions.Mp3
{
    struct OptionalHeaderInfo
    {
        internal uint FrameCount { get; set; }

        internal uint ByteCount { get; set; }

        internal bool Incomplete => FrameCount == 0 || ByteCount == 0;
    }
}
