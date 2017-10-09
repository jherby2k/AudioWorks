using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    abstract class WritableAtom
    {
        [Pure, NotNull]
        internal abstract byte[] GetBytes();
    }
}
