using System.Threading.Tasks;
using JetBrains.Annotations;
using NuGet.Common;

namespace AudioWorks.Extensions
{
    sealed class NugetLogger : LoggerBase
    {
        public override void Log([CanBeNull] ILogMessage message)
        {
        }

        [NotNull]
        public override async Task LogAsync([CanBeNull] ILogMessage message)
        {
            await Task.Run(() => null).ConfigureAwait(false);
        }
    }
}