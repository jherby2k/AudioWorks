using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TrackNumberAtom
    {
        internal byte TrackNumber { get; }

        internal byte TrackCount { get; }

        internal TrackNumberAtom([NotNull] IReadOnlyList<byte> data)
        {
            TrackNumber = data[27];
            TrackCount = data[29];
        }
    }
}