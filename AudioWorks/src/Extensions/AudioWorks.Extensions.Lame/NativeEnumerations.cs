using JetBrains.Annotations;

namespace AudioWorks.Extensions.Lame
{
    enum VbrMode
    {
        [UsedImplicitly] Off,
        [UsedImplicitly] Mt,
        [UsedImplicitly] Rh,
        Abr,
        Mtrh
    }
}
