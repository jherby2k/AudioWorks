using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that checks for prerequisites required by other extensions within the same assembly.
    /// </summary>
    [PublicAPI]
    public interface IPrerequisiteHandler
    {
        /// <summary>
        /// Performs one-time setup actions for this assembly, and indicates whether the prerequisites have been met.
        /// </summary>
        /// <returns><c>true</c> if this assembly's prerequisites have been met; otherwise, <c>false</c>.</returns>
        bool Handle();
    }
}