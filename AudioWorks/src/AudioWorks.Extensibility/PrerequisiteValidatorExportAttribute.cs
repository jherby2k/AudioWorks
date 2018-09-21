using System;
using System.Composition;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by AudioWorks.
    /// </summary>
    /// <remarks>
    /// Classes marked with this attribute must implement <see cref="IPrerequisiteHandler"/>.
    /// </remarks>
    /// <seealso cref="ExportAttribute"/>
    [PublicAPI, MeansImplicitUse, BaseTypeRequired(typeof(IPrerequisiteHandler))]
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class PrerequisiteHandlerExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrerequisiteHandlerExportAttribute"/> class.
        /// </summary>
        public PrerequisiteHandlerExportAttribute()
            : base(typeof(IPrerequisiteHandler))
        {
        }
    }
}
