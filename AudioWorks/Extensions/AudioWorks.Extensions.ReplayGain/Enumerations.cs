using JetBrains.Annotations;
using System;

namespace AudioWorks.Extensions.ReplayGain
{
    enum Ebur128Error
    {
        [UsedImplicitly] Success
    }

    [Flags]
    enum Modes
    {
        Global = 0x5,
        TruePeak = 0x31
    }
}
