using System;
using System.Management.Automation;
using AudioWorks.Api;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    static class ProgressAdapter
    {
        [NotNull]
        internal static ProgressRecord TokenToRecord([NotNull] ProgressToken progressToken)
        {
            return new ProgressRecord(0, progressToken.Description,
                $"{progressToken.Completed} out of {progressToken.Total} complete")
            {
                PercentComplete = (int) Math.Floor(progressToken.Completed / (double) progressToken.Total * 100)
            };
        }
    }
}