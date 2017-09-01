using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    class AtomInfo
    {
        internal uint Start { get; }

        internal uint Size { get; }

        internal uint End => Start + Size;

        internal string FourCc { get; }

        internal AtomInfo(uint start, uint size, [NotNull] string fourCc)
        {
            Start = start;
            Size = size;
            FourCc = fourCc;
        }
    }
}