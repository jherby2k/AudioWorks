using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    abstract class WritableAtom
    {
        internal abstract void Write([NotNull] Stream output);
    }
}
