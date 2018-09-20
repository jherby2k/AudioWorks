using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// An extension that checks for prerequisites required by other extensions within the same assembly.
    /// </summary>
    [PublicAPI]
    public interface IPrerequisiteValidator
    {
        /// <summary>
        /// Determines whether this extension module's prerequisites are met.
        /// </summary>
        /// <returns><c>true</c> if this module has all required prerequisites; otherwise, <c>false</c>.</returns>
        bool HasPrerequisites();
    }
}