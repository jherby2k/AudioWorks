using System;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    sealed class SimpleProgress<T> : IProgress<T>
    {
        [NotNull] readonly Action<T> _handler;

        internal SimpleProgress([NotNull] Action<T> handler)
        {
            _handler = handler;
        }

        public void Report([NotNull] T value)
        {
            _handler(value);
        }
    }
}
