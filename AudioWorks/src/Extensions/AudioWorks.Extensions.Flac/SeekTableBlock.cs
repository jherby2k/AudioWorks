namespace AudioWorks.Extensions.Flac
{
    sealed class SeekTableBlock : MetadataBlock
    {
        internal SeekTableBlock(uint count, ulong sampleCount)
            : base(MetadataType.SeekTable)
        {
            SafeNativeMethods.MetadataObjectSeekTableTemplateAppendSpacedPoints(Handle, count, sampleCount);
        }
    }
}