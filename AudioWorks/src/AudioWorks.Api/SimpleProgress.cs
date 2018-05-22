using System;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// An <see cref="IProgress{T}"/> implementation that simply invokes the callbacks on the same thread, rather than
    /// attempting to capture the synchronization context like <see cref="Progress{T}"/>.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the progress report value.</typeparam>
    /// <seealso cref="IProgress{T}"/>
    public sealed class SimpleProgress<T> : IProgress<T>
    {
        [NotNull] readonly Action<T> _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleProgress{T}"/> class.
        /// </summary>
        /// <param name="handler">The handler to invoke for each reported progress value.</param>
        public SimpleProgress([NotNull] Action<T> handler)
        {
            _handler = handler;
        }

        /// <inheritdoc/>
        public void Report([NotNull] T value)
        {
            _handler(value);
        }
    }
}
